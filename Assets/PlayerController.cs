using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float mouseSensitivity = 2.0f;
    public float elevationSpeed = 2.0f;

    private Rigidbody rb;
    public GameObject gameOverUI;  // Declare gameOverUI here


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        // Ensure the Game Over UI is initially set to inactive
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
        else
        {
            Debug.LogError("Game Over UI not assigned in the Inspector");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        float currentSpeed = speed;
        if (Input.GetKey(KeyCode.Space))
        {
            currentSpeed *= 2.0f; // Boost factor
            Debug.Log("BOOST!");
        }

        // Use Rigidbody for movement
        Vector3 move = transform.TransformDirection(new Vector3(horizontalInput, 0, verticalInput) * currentSpeed);
        rb.velocity = move;

        // Aircraft pitch and yaw based on mouse movement
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Quaternion deltaRotation = Quaternion.Euler(new Vector3(-mouseY, mouseX, 0) * mouseSensitivity);
        rb.MoveRotation(rb.rotation * deltaRotation);

        // Elevation based on mouse scroll
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 elevation = new Vector3(0, scroll * elevationSpeed, 0);
        rb.velocity += elevation;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Detected with " + collision.gameObject.name);  // Log the name of the collided object

        if (collision.gameObject.CompareTag("Obstacles"))
        {
            Debug.Log("Hit an obstacle! Attempting to show Game Over UI.");
            ShowGameOverUI();

            Shooting shootingComponent = GetComponent<Shooting>(); // Get the Shooting component
            if (shootingComponent != null)
            {
                shootingComponent.EndGame(); // Call the EndGame method to disable shooting
            }
            else
            {
                Debug.LogWarning("Shooting component not found on this GameObject.");
            }
        }
        else
        {
            Debug.Log("Collided object is not an obstacle. It's tag is: " + collision.gameObject.tag);
        }
    }


    void ShowGameOverUI()
    {
        if (gameOverUI != null)
        {
            Debug.Log("Game Over UI activated.");
            gameOverUI.SetActive(true);
            Time.timeScale = 0;  // Freeze the game
        }
        else
        {
            Debug.Log("Game Over UI is not assigned in the Inspector.");
        }
    }

}
