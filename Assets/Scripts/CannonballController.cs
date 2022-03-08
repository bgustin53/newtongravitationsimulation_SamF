using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballController : MonoBehaviour
{
    private GameObject InputManager;
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        InputManager = GameObject.Find("InputManager");
        rb = GetComponent<Rigidbody>();
        LaunchCannonball();
    }

    private void LaunchCannonball()
    {
        float forceMagnitude = InputManager.GetComponent<CannonballLauncher>().launchForce;
        rb.AddForce(new Vector3(-1, 0, 1) * forceMagnitude, ForceMode.Acceleration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Earth"))
        {
            Destroy(gameObject);
        }
    }
}
