using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    public static List<Body> Bodies { get; private set; }

    private const float GRAVITY = 0.0667f;


    // Update is called once per frame
    private void FixedUpdate()
    {
        foreach (Body body in Bodies)
        {
            if (body != this)
            {
                GravitationalForce(body);
            }
        }
    }

    private void OnEnable()
    {
        if (Bodies == null)
        {
            Bodies = new List<Body>();
        }
        Bodies.Add(this);
    }

    private void OnDisable()
    {
        Bodies.Remove(this);
    }

    private void GravitationalForce(Body other)
    {
        Rigidbody rbOfOther = other.rb;

        Vector3 direction = rb.position - rbOfOther.position;
        float radius = direction.magnitude;

        if (radius > 0)
        {
            float gravity = GRAVITY * rb.mass * rbOfOther.mass / (radius * radius);
            Vector3 gravityVector = direction.normalized * gravity;

            rbOfOther.AddForce(gravityVector);

        }
    }
}
