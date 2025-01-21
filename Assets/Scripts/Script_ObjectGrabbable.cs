using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_ObjectGrabbable : MonoBehaviour
{

    private Rigidbody objectRigidbody;
    private Transform objectGrabPointTansform;

    private void Awake()
    {
        objectRigidbody = GetComponent<Rigidbody>();
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
            //objectRigidbody.freezePosition = false;


            float lerpSpeed = 8f;
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTansform.position, Time.deltaTime * lerpSpeed);
            objectRigidbody.MovePosition(newPosition);
        }
    }
}
