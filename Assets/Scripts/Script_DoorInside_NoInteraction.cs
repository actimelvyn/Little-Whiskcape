using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_DoorInside_NoInteraction : MonoBehaviour
{
    public Animator doorAnim;    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // Check if the Player exits the trigger
        {
            doorAnim.SetTrigger("openDoor"); // Trigger the vase animation
        }
    }
}
