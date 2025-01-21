using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Player_PickUpNDrop : MonoBehaviour
{
    [SerializeField] private Transform PlayerCameraTransform;

    [SerializeField] private Transform objectGrabPointTansform;

    [SerializeField] private LayerMask PickUpLayerMask;

    private Script_ObjectGrabbable objectGrabbable;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (objectGrabbable == null)
            {

                float pickUpDistance = 2f;

                if (Physics.Raycast(PlayerCameraTransform.position, PlayerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, PickUpLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                    {
                        objectGrabbable.Grab(objectGrabPointTansform);
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