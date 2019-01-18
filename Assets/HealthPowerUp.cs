using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;

    void Start()
    {
        GameManager.instance.RegisterPowerUp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
