using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Script_Player_PickUpNDrop : MonoBehaviour
{
    [SerializeField] private Transform PlayerCameraTransform;

    [SerializeField] private Transform objectGrabPointTansform;

    [SerializeField] private LayerMask PickUpLayerMask;

    private Script_ObjectGrabbable objectGrabbable;
    public GameObject textGrabbed;

    float pickUpDistance = 3f;

    private void Update()
    {
       

        if (Input.GetKeyUp(KeyCode.E))
        {
            textGrabbed.SetActive(false);
            if (objectGrabbable == null)
            {
                print("pressed e");

                if (Physics.Raycast(PlayerCameraTransform.position, PlayerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, PickUpLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                    {
                        objectGrabbable.Grab(objectGrabPointTansform);
                        print("object grabbed e");

                    }
                }
            }
            else
            {
                objectGrabbable.Drop();
                objectGrabbable = null;
            }
        }
    }
    
}