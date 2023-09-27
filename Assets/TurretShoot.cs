using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour
{
    public Transform player;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float rotationSpeed = 5.0f;
    public float shootingInterval = 2.0f; // The turret will shoot every 2 seconds

    private float shootingTimer;
    private bool isTurretActive = false;

    void Update()
    {
        if (isTurretActive && player != null)
        {
            // Turret aiming at player
            Vector3 direction = player.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

            // Turret shooting at intervals
            shootingTimer += Time.deltaTime;
            if (shootingTimer >= shootingInterval)
            {
                Shoot();
                shootingTimer = 0f;
            }
        }
    }
    void Shoot()
    {
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            Debug.Log("Turret shot a bullet at time: " + Time.time);
        }
        else
        {
            Debug.LogError("Bullet Prefab or Spawn Point is not assigned!");
        }
    }
    public void ActivateTurret()
    {
        isTurretActive = true;
        Debug.Log("Turret is activated.");
    }

}
