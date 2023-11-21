// EnemyBehavior.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float lifetime = 50f;
    public float destroyRange = 20f;

    private Vector3 initialPosition;
    private float destroyRangeSqr;

    void Start()
    {
        initialPosition = transform.position;
        destroyRangeSqr = destroyRange * destroyRange;
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if ((initialPosition - transform.position).sqrMagnitude > destroyRangeSqr)
        {
           // Debug.Log("Enemy destroyed due to exceeding range limit.");
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        //Debug.Log("Enemy destroyed.");

        EnemySpawn enemySpawn = FindObjectOfType<EnemySpawn>();
        if (enemySpawn != null)
        {
            enemySpawn.EnemyDestroyed();
        }
    }
}
