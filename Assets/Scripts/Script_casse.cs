using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_casse : MonoBehaviour
{
    public GameObject fracturedVasePrefab; // The fractured vase prefab
    public float breakForceThreshold = 5f; // Force threshold to break the vase

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision force exceeds the break threshold
        if (collision.relativeVelocity.magnitude > breakForceThreshold)
        {
            // Replace the vase with the fractured version
            BreakVase();
        }
    }

    private void BreakVase()
    {
        // Instantiate the fractured vase at the same position and rotation
        Instantiate(fracturedVasePrefab, transform.position, transform.rotation);

        // Destroy the original vase
        Destroy(gameObject);
    }
}
