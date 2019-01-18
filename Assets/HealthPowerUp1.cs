using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp1 : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject player;
    private PlayerHealth playerHealth;
    void Start()
    {
        player = GameManager.instance.Player;
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            playerHealth.PowerUpHealth();
            Destroy(gameObject);
        }
    }
}
