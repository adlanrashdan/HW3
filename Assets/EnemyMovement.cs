// EnemyMovement.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 1f;
    public float changeDirectionInterval = 3f;

    private Vector3 targetDirection;
    private float minX, maxX, minZ, maxZ;

    void Start()
    {
        // Adjust these values according to your plane's dimensions and position
        minX = 112.64f - 25f / 2;
        maxX = 112.64f + 25f / 2;
        minZ = 165.024f - 30f / 2;
        maxZ = 165.024f + 30f / 2;

        InvokeRepeating("ChangeDirection", 0, changeDirectionInterval);
    }

    void Update()
    {
        Vector3 newPosition = transform.position + targetDirection * speed * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);
        newPosition.y = 50.34f; // Constrain to the plane at y = 50.34
        transform.position = newPosition;
    }

    void ChangeDirection()
    {
        targetDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }
}
