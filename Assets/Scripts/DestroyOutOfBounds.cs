using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{

    // Update is called once per frame
    private void Update()
    {
        if (transform.position.x > 2000 || transform.position.x < 1000 ||
           transform.position.z > 400 || transform.position.z < -1200)
        {
            Destroy(gameObject);
        }
    }
}
