using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // Import the TextMeshPro namespace

public class EnterWarzoneController : MonoBehaviour
{
    public TextMeshProUGUI warzoneEntryText;
    public GameObject warzoneTriggerGameObject;
    public TurretShoot turret;

    void Start()
    {
        // Try to assign the warzoneEntryText if it's not assigned in the inspector
        if (warzoneEntryText == null)
        {
            GameObject textObject = GameObject.Find("ENTER WARZONE");

            if (textObject != null)
            {
                warzoneEntryText = textObject.GetComponent<TextMeshProUGUI>();

                if (warzoneEntryText == null)
                {
                    Debug.LogError("The TextMeshProUGUI component is missing on the ENTER WARZONE GameObject!");
                }
            }
            else
            {
                Debug.LogError("There is no GameObject named ENTER WARZONE in the scene!");
            }
        }

        // Do the same for the warzoneTriggerGameObject if needed
        if (warzoneTriggerGameObject == null)
        {
            warzoneTriggerGameObject = GameObject.Find("Plane");

            if (warzoneTriggerGameObject == null)
            {
                Debug.LogError("There is no GameObject named Plane in the scene!");
            }
        }

        if (turret == null)
        {
            turret = FindObjectOfType<TurretShoot>();
            if (turret == null)
            {
                Debug.LogError("TurretController is not found in the scene!");
            }
        }

        // Initially set the text to be visible, if it's found and assigned
        if (warzoneEntryText != null)
        {
            warzoneEntryText.enabled = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // If the player has entered the collider of the warzone trigger GameObject
        if (other.gameObject == warzoneTriggerGameObject)
        {
            // Hide the text, if it's found and assigned
            if (warzoneEntryText != null)
            {
                warzoneEntryText.enabled = false;
                if (turret != null)
                {
                    turret.ActivateTurret(); // Activate the turret when entering the warzone
                }
                else
                {
                    Debug.LogError("TurretController is not assigned or found!");
                }
            }
            else
            {
                Debug.LogError("Warzone Entry Text is not assigned!");
            }
        }
    }
}
