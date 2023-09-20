
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 20.0f;
    public int numberOfBullets = 3; // Number of bullets to spawn per click
    public float bulletSpacing = 0.2f; // Spacing between bullets

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < numberOfBullets; i++)
            {
                // Calculate a new spawn point based on the original spawn point and bulletSpacing
                Vector3 newSpawnPoint = bulletSpawnPoint.position + bulletSpawnPoint.right * (i * bulletSpacing - (numberOfBullets - 1) * bulletSpacing / 2);

                // Instantiate the bullet at the new spawn point
                GameObject bullet = Instantiate(bulletPrefab, newSpawnPoint, Quaternion.Euler(0, 90, 0));

                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = -bulletSpawnPoint.forward * bulletSpeed; // Negate the forward vector
                }
                else
                {
                    Debug.LogWarning("The bullet prefab does not have a Rigidbody component.");
                }
            }
        }
    }
}
