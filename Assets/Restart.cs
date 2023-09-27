using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Restart : MonoBehaviour
{

    public void RestartGame()
    {
        Debug.Log("Button clicked!");
        Debug.Log("Restarting the game.");
        Time.timeScale = 1;  // Reset the timescale to normal before reloading the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

}
