using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarzoneTrigger : MonoBehaviour
{
    public TurretShoot turret; // Reference to the turret script

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assuming the player GameObject has the tag "Player"
        {
            turret.ActivateTurret();
        }
    }
}
