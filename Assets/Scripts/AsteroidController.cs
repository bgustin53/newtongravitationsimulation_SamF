using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    [SerializeField] private GameObject earth;
    private Rigidbody rb;
    private Vector3 initialForce;
    private const float MASS_OF_DUST = 1;
    private const float SCALE = 1;
    private const float MIN_INITIAL_MASS = 9.9f;
    private const float MAX_INITIAL_MASS = 10.1f;
    private const float MIN_INITIAL_FORCE = 1000.0f;
    private const float MAX_INITIAL_FORCE = 1500.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale *= SCALE;
        rb = GetComponent<Rigidbody>();
        rb.mass = Random.Range(MIN_INITIAL_MASS, MAX_INITIAL_MASS);

        Vector3 radius = (earth.transform.position - transform.position);
        initialForce = Vector3.Cross(radius.normalized, Vector3.up) * Random.Range(MIN_INITIAL_FORCE, MAX_INITIAL_FORCE);
        rb.AddForce(initialForce, ForceMode.Acceleration);
    }

    private void Update()
    {
        if(rb.mass < 72000)
        {
            ChangeInMass(MASS_OF_DUST);  // Adds dust to asteroid
        }
        
        if (rb.mass > 100000)
        {
            GetComponent<TrailRenderer>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Earth"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Asteroid"))
        {
            // Get rb attributes of other object
            Rigidbody rbOther = other.gameObject.GetComponent<Rigidbody>();
            float massOther = rbOther.mass;

            if (rb.mass > massOther)
            {
                ChangeInMass(massOther);  // Combines asteroids
            }
            else
            {
                Destroy(gameObject);
            }
        } 
    }

    private void ChangeInMass(float massOther)
    {
        float proportionalChange = (rb.mass + massOther) / rb.mass;
        if (rb.mass + massOther < 60000)
        {
            rb.mass *= proportionalChange;
            transform.localScale *= Mathf.Pow(proportionalChange, 1 / 3.0f);
        }
    }
}
