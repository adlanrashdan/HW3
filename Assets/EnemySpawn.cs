using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 5f;
    public int maxEnemies = 10; // maximum number of enemies in the scene at any time

    // Position and size of the plane
    public Vector3 planePosition = new Vector3(112.64f, 50.34f, 165.0237f);
    public float halfPlaneWidth = 26.13825f / 2;
    public float halfPlaneLength = 26.34827f / 2;

    private int currentEnemies = 0; // current number of enemies
    private float timer = 0; // timer to keep track of spawn interval

    void Update()
    {
        timer += Time.deltaTime; // increment timer by the time passed since the last frame

        if (timer >= spawnInterval) // check if it's time to spawn a new enemy
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
            timer = 0; // reset the timer
        }
    }

    // Call this from your enemy script when an enemy is destroyed
    public void EnemyDestroyed()
    {
        currentEnemies--;
    }
}
