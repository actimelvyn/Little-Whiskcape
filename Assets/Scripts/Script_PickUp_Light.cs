using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_PickUp_Light : MonoBehaviour
{
    public GameObject LightOnPlayer;

    void Start()
    {
        // Ensure the light on the player starts as inactive
        LightOnPlayer.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the trigger and the light is currently inactive
        if (other.gameObject.CompareTag("Player") && !LightOnPlayer.activeSelf)
        {
            // Deactivate the pickup object
            this.gameObject.SetActive(false);

            // Activate the light
            LightOnPlayer.SetActive(true);
        }
    }
}
