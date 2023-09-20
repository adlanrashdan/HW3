using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float lifetime = 50f;
    public float destroyRange = 20f;

    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(initialPosition, transform.position) > destroyRange)
        {
            Destroy(gameObject);
        }
    }
}