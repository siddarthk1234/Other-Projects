﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float range = 3f;
    [SerializeField] private float timeBetweenAttacks = 1f;
    [SerializeField] Transform fireLocation;

    private Animator anim;
    private GameObject player;
    private bool playerInRange;
    
    private EnemyHealth enemyHealth;
    private GameObject arrow;



    void Start()
    {
        arrow = GameManager.instance.Arrow;
        enemyHealth = GetComponent<EnemyHealth>();
       
        player = GameManager.instance.Player;
        anim = GetComponent<Animator>();
        StartCoroutine(attack());
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < range && enemyHealth.IsAlive)
        {
            playerInRange = true;
            anim.SetBool("PlayerInRange", true);
            RotateTowards(player.transform);
        }
        else
        {
            playerInRange = false;
            anim.SetBool("PlayerInRange", false);

        }
    }

    IEnumerator attack()
    {
        if (playerInRange && !GameManager.instance.GameOver)
        {

            anim.Play("Attack");
            yield return new WaitForSeconds(timeBetweenAttacks);

        }

        yield return null;
        StartCoroutine(attack());
    }

   private void RotateTowards(Transform player)
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime);
    }


    public void FireArrow()
    {
        //1) First create an arrow
        //2) Set the position to a specific position
        //3)Set the orientation so that it is shooting towards the hero
        //4) Apply a velocity to the arrow
        GameObject newArrow = Instantiate(arrow) as GameObject;
        newArrow.transform.position = fireLocation.position;
        newArrow.transform.rotation = transform.rotation;
        newArrow.GetComponent<Rigidbody>().velocity = transform.forward * 25;

    }
}
