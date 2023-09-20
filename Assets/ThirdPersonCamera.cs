using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;  // Target to follow (your player)
    public float distance = 5.0f;  // Distance from the target
    public float height = 2.0f;  // Height above the target
    public float damping = 5.0f;  // Damping effect

    void LateUpdate()
    {
        // Calculate the desired position
        Vector3 wantedPosition = target.TransformPoint(0, height, -distance);

        // Smoothly transition to that position
        transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);

        // Make the camera look at the target
        transform.LookAt(target);
    }
}
