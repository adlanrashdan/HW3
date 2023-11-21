using UnityEngine;

namespace BigRookGames.Weapons
{
    public class GunfireController : MonoBehaviour
    {
        // --- Audio ---
        public AudioClip GunShotClip;
        public AudioSource source;

        // --- Muzzle ---
        public GameObject muzzlePrefab;
        public GameObject muzzlePosition;

        // --- Projectile ---
        public GameObject projectilePrefab;

        // --- AI Target ---
        public Transform playerTarget; // The target for the AI
        public LayerMask obstaclesLayer; // Layer to check for line of sight

        // --- AI Shooting ---
        public float rotationSpeed = 5.0f;
        public float shootingInterval = 2.0f;
        public float bulletSpeed = 20f; // Speed of the bullet for predictive aiming
        public float aggressiveDistance = 10f;
        public float aggressiveShootingInterval = 1.0f;

        public enum ShootingPattern { SingleShot, Burst, Random }
        public ShootingPattern shootingPattern;
        public int burstCount = 3;
        private int currentBurstCount = 0;

        private float shootingTimer;
        private bool isGunActive = false;

        private void Update()
        {
            if (playerTarget != null)
            {
                Vector3 directionToPlayer = playerTarget.position - transform.position;
                if (!Physics.Raycast(transform.position, directionToPlayer, directionToPlayer.magnitude, obstaclesLayer))
                {
                    isGunActive = true;
                    PredictiveAiming();
                    ShootingMechanism();
                }
                else
                {
                    isGunActive = false;
                }
            }
        }

        void PredictiveAiming()
        {
            Vector3 targetDir = playerTarget.position - transform.position;
            float distance = targetDir.magnitude;
            float travelTime = distance / bulletSpeed;
            Vector3 predictedPosition = playerTarget.position + playerTarget.GetComponent<Rigidbody>().velocity * travelTime;
            Quaternion rotation = Quaternion.LookRotation(predictedPosition - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }

        void ShootingMechanism()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTarget.position);
            shootingInterval = distanceToPlayer < aggressiveDistance ? aggressiveShootingInterval : 2.0f;

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
                    if (Random.Range(0, 2) > 0)
                        FireBullet();
                    break;
            }
        }

        void FireBullet()
        {
            if (projectilePrefab != null && muzzlePosition != null)
            {
                Instantiate(projectilePrefab, muzzlePosition.transform.position, muzzlePosition.transform.rotation, transform);
                Instantiate(muzzlePrefab, muzzlePosition.transform); // Muzzle flash

            }
            else
            {
                Debug.LogError("Projectile Prefab or Muzzle Position is not assigned!");
            }
        }
    }
}
