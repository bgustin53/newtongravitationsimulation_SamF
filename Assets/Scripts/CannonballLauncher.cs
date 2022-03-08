using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballLauncher : MonoBehaviour
{
    [SerializeField] private GameObject Cannonball;
    public float launchForce { get; private set; }
    private bool readyToLaunch;

    // Start is called before the first frame update
    void Start()
    {
        readyToLaunch = false;
    }

    // Update is called once per frame
    void Update()
    {
        SetLaunchForce();
        SpawnCannonball();
    }

    private void SetLaunchForce()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            launchForce = 60;
            readyToLaunch = true;
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            launchForce = 120;
            readyToLaunch = true;
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            launchForce = 180;
            readyToLaunch = true;
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            launchForce = 240;
            readyToLaunch = true;
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            launchForce = 300;
            readyToLaunch = true;
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            launchForce = 320;
            readyToLaunch = true;
        }
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            launchForce = 340;
            readyToLaunch = true;
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            launchForce = 360;
            readyToLaunch = true;
        }
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            launchForce = 600;
            readyToLaunch = true;
        }
    }

    private void SpawnCannonball()
    {
        if (Cannonball != null && readyToLaunch)
        {
            Instantiate(Cannonball, Cannonball.transform.position, Cannonball.transform.rotation);
            readyToLaunch = false;
        }
    }
}
