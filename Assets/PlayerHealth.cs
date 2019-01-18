﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] int startingHealth = 100;
    [SerializeField] float timeSinceLastHit = 2f;

    private float timer = 0f;
    private CharacterController characterController;
    private Animator anim;
    [SerializeField]private int currentHealth;
    private AudioSource audio;
    [SerializeField] Slider healthSlider;
    private ParticleSystem blood;

    public int CurrentHealth
    {
        get { return currentHealth;
        }
        set
        {
            if(value < 0)
            {
                currentHealth = 0;
            }

            else
            {
                currentHealth = value;
            }
        }
    }


    void Awake()
    {
        //Assert.IsNotNull(healthSlider);
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        currentHealth = startingHealth;
        audio = GetComponent<AudioSource>();
        blood = GetComponentInChildren<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    void OnTriggerEnter (Collider other)
    {
        if(timer >=  timeSinceLastHit && !GameManager.instance.GameOver)
        {
            if(other.tag == "Weapon")
            {
                takeHit();
                timer = 0;
            }
        }
    }

    void takeHit()
    {
        if(currentHealth > 0)
        {
            GameManager.instance.PlayerHit(currentHealth);
            anim.Play("Hurt");
            currentHealth -= 10;
            healthSlider.value = currentHealth;
            audio.PlayOneShot(audio.clip);
            blood.Play();
        }

        if(currentHealth <= 0)
        {
            killPlayer();
        }
    }


    void killPlayer()
    {
        GameManager.instance.PlayerHit(currentHealth);
        anim.SetTrigger("HeroDie");
        characterController.enabled = false;
        audio.PlayOneShot(audio.clip);
        blood.Play();
    }


    public void PowerUpHealth()
    {
        if(currentHealth <= 70)
        {
            CurrentHealth += 30;
        }

        else if(currentHealth < startingHealth)
        {
            currentHealth = startingHealth;
        }

        healthSlider.value = currentHealth;
    }
}
