using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField] private int startingHealth = 20;
    [SerializeField] private float timeSinceLastHit = .5f;
    [SerializeField] private float disappearSpeed = 2f;
      

    private AudioSource audio;
    private float timer = 0f;
    private Animator anim;
    private NavMeshAgent nav;
    private bool isAlive;
    private Rigidbody rigidbody;
    private CapsuleCollider capsuleCollider;
    private bool dissapearEnemy = false;
    [SerializeField]private int currentHealth;
    private ParticleSystem blood;

    public bool IsAlive
    {
        get
        {
            return isAlive;
        }
    }
    void Start()
    {
        GameManager.instance.RegisterEnemy(this);
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        isAlive = true;
        currentHealth = startingHealth;
        blood = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (dissapearEnemy)
        {
            transform.Translate(-Vector3.up * disappearSpeed * Time.deltaTime);
        }
        
    }

    void OnTriggerEnter(Collider other)
    {

        
        if(timer >= timeSinceLastHit && !GameManager.instance.GameOver)
        {
            if(other.tag == "PlayerWeapon")
            {

                Debug.Log("hitz the boss");
                takeHit();
                blood.Play();
                timer = 0f;
            }
        }
    }


    void takeHit()
    {
        if(currentHealth > 0)
        {
            audio.PlayOneShot(audio.clip);
            anim.Play("Hurt");
            currentHealth -= 10;
            
        }

        if (currentHealth <= 0)
        {
            isAlive = false;
            KillEnemy();
        }
    }

    void KillEnemy()
    {
        GameManager.instance.KilledEnemy(this);
        capsuleCollider.enabled = false;
        nav.enabled = false;
        anim.SetTrigger("EnemyDie");
        rigidbody.isKinematic = true;
        
        StartCoroutine(removeEnemy());
    }

    IEnumerator removeEnemy()
    {
        yield return new WaitForSeconds(4f);
        dissapearEnemy = true;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
