using UnityEngine;

namespace BigRookGames.Weapons
{
    public class ProjectileController : MonoBehaviour
    {
        public float speed = 100;
        public LayerMask collisionLayerMask;
        public GameObject rocketExplosion;
        public MeshRenderer projectileMesh;
        private bool targetHit;
        public AudioSource inFlightAudioSource;
        public ParticleSystem disableOnHit;

        private void Update()
        {
            if (targetHit) return;
            transform.position += transform.forward * (speed * Time.deltaTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!enabled) return;
            Explode();
            projectileMesh.enabled = false;
            targetHit = true;
            inFlightAudioSource.Stop();
            foreach (Collider col in GetComponents<Collider>())
            {
                col.enabled = false;
            }
            disableOnHit.Stop();
            Destroy(gameObject, 5f); // Adjust time as necessary
        }

        private void Explode()
        {
            Instantiate(rocketExplosion, transform.position, rocketExplosion.transform.rotation);
        }
    }
}
