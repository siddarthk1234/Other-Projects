using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject);
    }
}
