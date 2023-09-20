using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 1f;
    public float changeDirectionInterval = 3f;

    private Vector3 targetDirection;

    // Boundary for the enemy movement
    private float minX, maxX, minZ, maxZ;

    // Start is called before the first frame update
    void Start()
    {
        // Define the movement boundaries based on the plane's position and size
        // Assuming the plane is centered at (112.64, 50.34, 165.0237) and has dimensions 26.13825 x 2.294735 x 26.34827
        minX = 112.64f - 26.13825f / 2;
        maxX = 112.64f + 26.13825f / 2;
        minZ = 165.0237f - 26.34827f / 2;
        maxZ = 165.0237f + 26.34827f / 2;

        InvokeRepeating("ChangeDirection", 0, changeDirectionInterval);
    }

    // Update is called once per frame
    void Update()
    {
        // Move the enemy
        transform.Translate(targetDirection * speed * Time.deltaTime);

        // Constrain to the plane at y = 50.34
        Vector3 position = transform.position;
        position.y = 50.34f;

        // Constrain to the plane's X and Z boundaries
        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.z = Mathf.Clamp(position.z, minZ, maxZ);

        transform.position = position;
    }

    void ChangeDirection()
    {
        // Randomize x and z components for movement direction
        targetDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }
}
