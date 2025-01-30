using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_ObjectGrabbable : MonoBehaviour
{
    private Rigidbody objectRigidbody;
    private Transform objectGrabPointTansform;
    public GameObject textDrop;
    public GameObject textGrab;

    private static bool isTextDisplayed = false; // Shared across all objects

    private void Start()
    {
        objectRigidbody = GetComponent<Rigidbody>();
        textGrab.SetActive(false);
    }

    public void Grab(Transform objectGrabPointTansform)
    {
        this.objectGrabPointTansform = objectGrabPointTansform;
        objectRigidbody.useGravity = false;
        objectRigidbody.isKinematic = false;
    }

    public void Drop()
    {
        this.objectGrabPointTansform = null;
        objectRigidbody.useGravity = true;
        objectRigidbody.isKinematic = false;
    }

    private void FixedUpdate()
    {
        if (objectGrabPointTansform != null)
        {
            objectRigidbody.freezeRotation = true;
            float lerpSpeed = 8f;
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTansform.position, Time.deltaTime * lerpSpeed);
            objectRigidbody.MovePosition(newPosition);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTextDisplayed)
        {
            textGrab.SetActive(true);
            isTextDisplayed = true; // Mark text as active
            Debug.Log("Text displayed for: " + gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textGrab.SetActive(false);
            isTextDisplayed = false; // Allow other objects to display text
            Debug.Log("Text hidden for: " + gameObject.name);
        }
    }
}
