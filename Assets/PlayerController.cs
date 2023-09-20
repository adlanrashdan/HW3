using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float mouseSensitivity = 2.0f;
    public float elevationSpeed = 2.0f;

    // Update is called once per frame
    void Update()
    {
        // Basic movement with keyboard
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Check if SPACEBAR is held down for boost
        float currentSpeed = speed;
        if (Input.GetKey(KeyCode.Space))
        {
            currentSpeed *= 2.0f; // Boost factor
        }

        // Note: using Space.Self instead of Space.World
        Vector3 move = new Vector3(horizontalInput, 0, verticalInput);
        transform.Translate(move * currentSpeed * Time.deltaTime, Space.Self);

        // Aircraft pitch and yaw based on mouse movement
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up, mouseX * mouseSensitivity, Space.World);
        transform.Rotate(Vector3.left, mouseY * mouseSensitivity);

        // Elevation based on mouse scroll
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(Vector3.up * scroll * elevationSpeed, Space.World);
    }


}
