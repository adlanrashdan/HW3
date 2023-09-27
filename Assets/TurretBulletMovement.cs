using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBulletMovement : MonoBehaviour
{
    public float speed = 100f;
    public float lifetime = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = transform.forward * speed; // Sets the bullet's velocity
        }
        Destroy(gameObject, lifetime); // Destroys the bullet after a specified lifetime to save resources
    }

    void Update()
    {
        // If you want to apply additional movement behavior, add it here
    }
}