using UnityEngine;
using UnityEngine.SceneManagement; // Required for loading scenes

public class Turret : MonoBehaviour
{
    public int health = 100; // Initial health of the turret
    public GameObject playerWinUI;
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is a bullet
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(5); // Assume each bullet does 20 damage
            Destroy(collision.gameObject); // Destroy the bullet on impact
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(health);

        // Check if health is depleted
        if (health <= 0)
        {
            PlayerWins();
        }
    }

    void PlayerWins()
    {
        Time.timeScale = 0;
        playerWinUI.SetActive(true);

    }
}
