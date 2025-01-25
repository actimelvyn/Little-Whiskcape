using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_PickUP_Key : MonoBehaviour
{
    public GameObject keyOnPlayer;
    public bool interactable, toggle;
    public GameObject PickUpText;
    public Animator vaseAnim;
    public Animator vaseAnim2;
    public GameObject fracturedVase;
    public GameObject intactVase;
    public bool canOpenDoor;

    public float delayBeforeBreaking = 5f; // Delay in seconds before breaking
    private bool isBreaking = false;

    void Start()
    {
        keyOnPlayer.SetActive(false); // Initially disable the key on the player
        PickUpText.SetActive(false); // Initially disable the pickup text
        interactable = false;        // Interaction flag set to false
        canOpenDoor = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // Check if the Player enters the trigger
        {
            PickUpText.SetActive(true); // Show the pickup text
            interactable = true;

            if (Input.GetKey(KeyCode.E) && !isBreaking) // If "E" is pressed and not already breaking
            {
                StartCoroutine(BreakVaseAfterDelay()); // Start the breaking vase coroutine

                // Disable the MeshRenderer of this object
                MeshRenderer meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
                if (meshRenderer != null)
                {
                    meshRenderer.enabled = false; // Disables rendering without deactivating the GameObject
                }

                keyOnPlayer.SetActive(true); // Activate the key on the player
                Script_UI Script_UI = FindObjectOfType<Script_UI>();
                Script_UI.AddStar();
                PickUpText.SetActive(false); // Hide the pickup text
                vaseAnim.SetTrigger("pick_up"); // Trigger the vase animation
                vaseAnim2.SetTrigger("pick_up"); // Trigger the vase animation
                canOpenDoor = true;
                if (canOpenDoor == true)
                {
                    print("key!");
                }
                isBreaking = true; // Mark as breaking to prevent duplicate triggers
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // Check if the Player exits the trigger
        {
            PickUpText.SetActive(false); // Hide the pickup text
            interactable = false;        // Reset interaction flag
        }
    }

    private IEnumerator BreakVaseAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayBeforeBreaking);
        Debug.Log("Breaking vase after delay");

        // Replace the intact vase with the fractured one
        if (intactVase != null) intactVase.SetActive(false);
        if (fracturedVase != null) fracturedVase.SetActive(true);

        // Optionally destroy this script
        //Destroy(this); // Removes the script from the GameObject
        //this.gameObject.SetActive(false);
        this.GetComponent<MeshRenderer>().enabled = false;
        Destroy(PickUpText);
    
    }
}
