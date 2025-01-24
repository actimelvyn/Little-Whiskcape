using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class newDoor : MonoBehaviour
{
    public GameObject intText;
    public bool interactable1, toggle;
    public Animator doorAnim;
    public VisualEffect doorVFX;
    //public GameObject ColliderDis;

    // Reference to the Script_PickUp_Light script
    public Script_PickUp_bottle scriptPickUpBottle;

    private void Start()
    {
        intText.SetActive(false);
        interactable1 = false;

        // Optional: Automatically find Script_PickUp_Light if it's on the same GameObject
        if (scriptPickUpBottle == null)
        {
            scriptPickUpBottle = FindObjectOfType<Script_PickUp_bottle>();
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player") && scriptPickUpBottle.drank)
        {
            intText.SetActive(true);
            interactable1 = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            intText.SetActive(false);
            interactable1 = false;
        }
    }

    private void Update()
    {
        // Access the 'drank' bool from Script_PickUp_Light
        if (interactable1 && Input.GetKeyUp(KeyCode.E) && scriptPickUpBottle.drank)
        {
            doorAnim.SetTrigger("open");
            Destroy (intText);
            PlayParticle();

        }
    }
    public void PlayParticle()
     {
         // instantiate
         VisualEffect newBurstEffect = Instantiate(doorVFX, transform.position, transform.rotation);
        //

        // play
        newBurstEffect.Play();

         // destroy
         Destroy(newBurstEffect.gameObject, 5f);
     }
}
