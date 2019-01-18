using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class enemyFollow : MonoBehaviour
{
    // Start is called before the first frame update
     Transform player;
    private NavMeshAgent nav;
    private Animator anim;
    private EnemyHealth enemyHealth;
    
    

   
     
    
    void Start() {


        player = GameManager.instance.Player.transform;
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();

        enemyHealth = GetComponent<EnemyHealth>();
    
        
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(enemyHealth);

        if (!GameManager.instance.GameOver && enemyHealth.IsAlive)
        {
            nav.SetDestination(player.position);
        }

        else if((!GameManager.instance.GameOver || GameManager.instance.GameOver) && !enemyHealth.IsAlive)
        {
            nav.enabled = false;
             
        }

        else
        {
            nav.enabled = false;
            anim.Play("Idle");
        }
    }
}
