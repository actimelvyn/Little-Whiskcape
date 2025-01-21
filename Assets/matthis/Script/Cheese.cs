using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheese : MonoBehaviour
{
    public GameObject explosionEffect;
    public float explosionForce = 10f;
    public float radius = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    private void Explode()
    {
        // Find all colliders within the explosion radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider near in colliders)
        {
            // Check if the collider has the tag "Cat"
            if (near.CompareTag("Cat"))
            {
                // Optionally apply explosion force if the object has a Rigidbody
                Rigidbody rig = near.GetComponent<Rigidbody>();
                if (rig != null)
                {
                    rig.AddExplosionForce(explosionForce, transform.position, radius, 1f, ForceMode.Impulse);
                }

                // Destroy the GameObject with the "Cat" tag
                Destroy(near.gameObject);
            }
        }

        // Instantiate explosion effect
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
        }

        // Destroy the object that triggered the explosion
        Destroy(gameObject);
    }
}
