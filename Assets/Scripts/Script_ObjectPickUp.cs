using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_ObjectPickUp : MonoBehaviour
{
    public GameObject crosshair1, crosshair2;
    public Transform objectTransform, cameraTransform;
    public bool interactable = false, pickedup = false;
    public Rigidbody objectRigidbody;
    public float throwForce = 10f;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            crosshair1.SetActive(false);
            crosshair2.SetActive(true);
            interactable = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !pickedup)
        {
            crosshair1.SetActive(true);
            crosshair2.SetActive(false);
            interactable = false;
        }
    }

    void Update()
    {
        if (interactable)
        {
            if (Input.GetMouseButtonDown(0) && !pickedup)
            {
                PickUpObject();
            }
            else if (Input.GetMouseButtonUp(0) && pickedup)
            {
                DropObject();
            }
        }
    }

    private void PickUpObject()
    {
        objectTransform.SetParent(cameraTransform);
        objectRigidbody.isKinematic = true;
        pickedup = true;
        crosshair1.SetActive(false);
        crosshair2.SetActive(true);  // Keeps interactable crosshair active
    }

    private void DropObject()
    {
        objectTransform.SetParent(null);
        objectRigidbody.isKinematic = false;
        objectRigidbody.velocity = cameraTransform.forward * throwForce;
        pickedup = false;
        crosshair1.SetActive(true);
        crosshair2.SetActive(false);
        interactable = false;
    }
}
