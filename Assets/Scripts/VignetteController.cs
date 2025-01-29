/*using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal; // For URP

public class VignetteController : MonoBehaviour
{
    private Vignette vignette; // Reference to the Vignette override
    public float transitionDuration = 1.0f; // Time for the transition

    private void Start()
    {

        // Find the Volume component on this GameObject
        Volume volume = GetComponent<Volume>();

        if (volume != null && volume.profile != null)
        {
            // Try to get the Vignette override from the Volume's profile
            if (volume.profile.TryGet<Vignette>(out vignette))
            {
                Debug.Log("Vignette component found!");
            }
            else
            {
                Debug.LogError("Vignette override is not set in the Volume profile!");
            }
        }
        else
        {
            Debug.LogError("No Volume component or profile found on the GameObject!");
        }
    }

    public void ChangeVignette(float targetIntensity)
    {
        if (vignette != null)
        {
            StopAllCoroutines(); // Stop any ongoing vignette transitions
            StartCoroutine(TransitionVignetteIntensity(targetIntensity));
        }
    }

    private System.Collections.IEnumerator TransitionVignetteIntensity(float targetIntensity)
    {
        float startIntensity = vignette.intensity.value; // Get the current intensity
        float elapsedTime = 0.0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            // Gradually change the intensity using Lerp
            vignette.intensity.value = Mathf.Lerp(startIntensity, targetIntensity, elapsedTime / transitionDuration);
            yield return null; // Wait for the next frame
        }

        // Ensure the target intensity is set at the end
        vignette.intensity.value = targetIntensity;
    }
}
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class Script_PickUp_bottle : MonoBehaviour
{
    // Public and serialized fields
    public GameObject PostProcessVolume;
    public GameObject _Vignette;

    public GameObject PickUpText;
    public Material screenOut;

    public bool drank;
    public Animator bottleAnim;
    public GameObject bottle;
    public SC_FPSController SC_FPSController;
    public float delayBeforEffect = 10f; // Delay in seconds before the effect

    // Class-level variables
    private GameObject[] bwObjects;
    public Script_SquashNStretch[] squashNStretchObjects; // Array to store all SquashNStretch instances

    void Start()
    {
        // Initialize components
        PostProcessVolume.SetActive(false);
        _Vignette.SetActive(true);

        PickUpText.SetActive(false);
        drank = false;
        if (screenOut.HasProperty("_Activator_out"))
        {
            screenOut.SetFloat("_Activator_out", 0f); // Set _Activator to false
            Debug.Log("out end");
        }
        // Find and cache SC_FPSController
        if (SC_FPSController == null)
        {
            SC_FPSController = FindObjectOfType<SC_FPSController>();
            if (SC_FPSController == null)
            {
                Debug.LogError("SC_FPSController not found in the scene!");
            }
        }

        // Temporarily disable movement
        SC_FPSController.walkingSpeed = 0;
        SC_FPSController.runningSpeed = 0;
        SC_FPSController.jumpSpeed = 0;

        // Cache all objects with the "BW" tag
        bwObjects = GameObject.FindGameObjectsWithTag("BW");
        foreach (GameObject obj in bwObjects)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                foreach (Material material in renderer.materials)
                {
                    if (material.HasProperty("_Activator"))
                    {
                        material.SetFloat("_Activator", 0f); // Set _Activator to false
                        Debug.Log("Material _Activator set to false.");
                    }
                }
            }
        }

        // Find all Script_SquashNStretch instances in the scene
        squashNStretchObjects = FindObjectsOfType<Script_SquashNStretch>();
        foreach (var squashNStretch in squashNStretchObjects)
        {
            Debug.Log($"Found SquashNStretch on {squashNStretch.gameObject.name}");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PickUpText.SetActive(true);

            if (Input.GetKey(KeyCode.E) && !drank)
            {
                // Trigger drinking animation
                if (bottleAnim != null) bottleAnim.SetTrigger("drink");
                //here!
                StartCoroutine(GoToDrunk(1.0f, 1.5f)); // Fade in to 1 over 1.5 seconds

                StartCoroutine(DrinkEffectAfterDelay()); // Start the coroutine
                drank = true;

                // Hide or destroy the pickup text
                if (PickUpText != null) PickUpText.SetActive(false);
                Destroy(PickUpText);

                Debug.Log("Drinking started.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PickUpText.SetActive(false);
        }
    }

    private IEnumerator DrinkEffectAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayBeforEffect);
        Debug.Log("Apply post-process effect after delay.");

        foreach (var squashNStretch in squashNStretchObjects)
        {
            squashNStretch.playsEveryTime = true;
            Debug.Log($"Enabled canStretch on {squashNStretch.gameObject.name}");
            squashNStretch.PlaySquashAndStretch(); // Trigger squash and stretch
            print("script bottle fini");
        }
        // Activate _Activator property for cached BW objects
        foreach (GameObject obj in bwObjects)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                foreach (Material material in renderer.materials)
                {
                    if (material.HasProperty("_Activator"))
                    {
                        material.SetFloat("_Activator", 1.0f); // Set _Activator to true
                        Debug.Log("Material _Activator set to true.");
                    }
                }
            }
        }
        if (screenOut.HasProperty("_Activator_out"))
        {
            screenOut.SetFloat("_Activator_out", 1.0f); // Set _Activator to true
            Debug.Log("out black");
        }



        Script_UI Script_UI = FindObjectOfType<Script_UI>();
        Script_UI.AddStar();

        SoundManager soundManager = FindObjectOfType<SoundManager>();
        soundManager.PlayBackgroundMusic();
        // Hide the bottle
        if (bottle != null) bottle.SetActive(false);

        // Activate post-process effect
        if (PostProcessVolume != null) PostProcessVolume.SetActive(true);

        // Restore movement settings
        if (SC_FPSController != null)
        {
            SC_FPSController.walkingSpeed = SC_FPSController.defwalkingSpeed;
            SC_FPSController.runningSpeed = SC_FPSController.defrunningSpeed;
            SC_FPSController.jumpSpeed = SC_FPSController.defjumpSpeed;
        }

        StartCoroutine(GoToTipsy(0.5765f, 1.5f)); // Fade out to 0.5765 over 1.5 seconds


        // Destroy this script if no longer needed
        Destroy(this);
    }
    private IEnumerator GoToDrunk(float targetIntensity, float duration)
    {
        Volume volumeComponent = _Vignette.GetComponent<Volume>();
        if (volumeComponent != null && volumeComponent.profile.TryGet<Vignette>(out Vignette vignette))
        {
            vignette.intensity.overrideState = true; // Ensure override is enabled
            float initialIntensity = vignette.intensity.value; // Get current intensity
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration; // Normalize to a 0-1 range
                vignette.intensity.value = Mathf.Lerp(initialIntensity, targetIntensity, t);
                Debug.Log($"Vignette Intensity: {vignette.intensity.value}");
                yield return null; // Wait for the next frame
            }

            // Ensure it reaches the exact target value at the end
            vignette.intensity.value = targetIntensity;
        }
    }

    private IEnumerator GoToTipsy(float targetIntensity, float duration)
    {
        _Vignette.SetActive(false);
        Volume volumeComponent = PostProcessVolume.GetComponent<Volume>();
        if (volumeComponent != null && volumeComponent.profile.TryGet<Vignette>(out Vignette vignette))
        {
            vignette.intensity.overrideState = true; // Ensure override is enabled //2h pour cette putain de ligne de code hasgdlasgfliagidfk,
            float initialIntensity = vignette.intensity.value; // Get current intensity
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration; // Normalize to a 0-1 range
                vignette.intensity.value = Mathf.Lerp(initialIntensity, targetIntensity, t);
                Debug.Log($"Vignette Intensity: {vignette.intensity.value}");
                yield return null; // Wait for the next frame
            }

            // Ensure it reaches the exact target value at the end
            vignette.intensity.value = targetIntensity;
        }
    }

}*/
