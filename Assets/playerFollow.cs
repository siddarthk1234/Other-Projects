using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class playerFollow : MonoBehaviour





{
    [SerializeField] GameObject player;
    Vector3 cameraPosition;
    Vector3 playerPosition;
    float cameraSmoothness = 5f;
    Vector3 offset;

   
    // Start is called before the first frame update
    void Start()
    {

        Assert.IsNotNull(player);
        offset = transform.position - player.transform.position;

        
        



        
    }

    // Update is called once per frame
    void Update()
    {


        Vector3 cameraTargetPosition = player.transform.position + offset;

        playerPosition = player.transform.position;
        cameraPosition = this.transform.position;
        transform.position = Vector3.Lerp(cameraPosition,cameraTargetPosition, cameraSmoothness);
        
    }
}
