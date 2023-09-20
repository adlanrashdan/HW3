// EnemySpawn.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 5f;
    public int maxEnemies = 10;

    public Vector3 planePosition = new Vector3(112.64f, 50.34f, 165.024f);
    public float halfPlaneWidth = 25f / 2;
    public float halfPlaneLength = 30f / 2;

    private int currentEnemies = 0;
    private float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            if (currentEnemies < maxEnemies)
            {
                Vector3 spawnPosition = new Vector3(
                    Random.Range(planePosition.x - halfPlaneWidth, planePosition.x + halfPlaneWidth),
                    planePosition.y,
                    Random.Range(planePosition.z - halfPlaneLength, planePosition.z + halfPlaneLength)
                );
                Debug.Log("Enemy spawned at: " + spawnPosition.ToString());
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                currentEnemies++;
            }
            timer = 0;
        }
    }

    public void EnemyDestroyed()
    {
        currentEnemies--;
    }
}

