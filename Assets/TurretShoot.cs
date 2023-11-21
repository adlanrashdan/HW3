using System.Collections;
using UnityEngine;

public class TurretShoot : MonoBehaviour
{
    public Transform player;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float rotationSpeed = 5.0f;
    public float shootingInterval = 2.0f;
    public float bulletSpeed = 20f; // Speed of the bullet for predictive aiming
    public float aggressiveDistance = 10f;
    public float aggressiveShootingInterval = 1.0f;
    public LayerMask obstaclesLayer;
    public enum ShootingPattern { SingleShot, Burst, Random }
    public ShootingPattern shootingPattern;
    public int burstCount = 3;
    private int currentBurstCount = 0;

    private float shootingTimer;
    private bool isTurretActive = false;

    void Update()
    {
        if (player != null)
        {
            Vector3 directionToPlayer = player.position - transform.position;
            if (!Physics.Raycast(transform.position, directionToPlayer, directionToPlayer.magnitude, obstaclesLayer))
            {
                isTurretActive = true;
                PredictiveAiming();
                ShootingMechanism();
            }
            else
            {
                isTurretActive = false;
            }
        }
    }

    void PredictiveAiming()
    {
        Vector3 targetDir = player.position - transform.position;
        float distance = targetDir.magnitude;
        float travelTime = distance / bulletSpeed;
        Vector3 predictedPosition = player.position + player.GetComponent<Rigidbody>().velocity * travelTime;
        Quaternion rotation = Quaternion.LookRotation(predictedPosition - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    void ShootingMechanism()
    {
        // Adjust shooting interval based on player's distance
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < aggressiveDistance)
        {
            shootingInterval = aggressiveShootingInterval;
        }
        else
        {
            shootingInterval = 2.0f; // or original shooting interval
        }

        // Shooting logic
        shootingTimer += Time.deltaTime;
        if (shootingTimer >= shootingInterval)
        {
            Shoot();
            shootingTimer = 0f;
        }
    }

    void Shoot()
    {
        switch (shootingPattern)
        {
            case ShootingPattern.SingleShot:
                FireBullet();
                break;
            case ShootingPattern.Burst:
                if (currentBurstCount < burstCount)
                {
                    FireBullet();
                    currentBurstCount++;
                }
                else
                {
                    currentBurstCount = 0;
                    shootingTimer = -shootingInterval; // Reset timer for a longer pause
                }
                break;
            case ShootingPattern.Random:
                if (Random.Range(0, 2) > 0) // 50% chance to shoot
                    FireBullet();
                break;
        }
    }

    void FireBullet()
    {
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            //Debug.Log("Turret shot a bullet at time: " + Time.time);
        }
        else
        {
            Debug.LogError("Bullet Prefab or Spawn Point is not assigned!");
        }
    }

    public void ActivateTurret()
    {
        isTurretActive = true;
        //Debug.Log("Turret is activated.");
    }
}
