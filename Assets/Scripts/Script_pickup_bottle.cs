using System.Collections;
using Unity.VisualScripting;
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

    private Volume vignetteVolume;
    private Volume postProcessVolume;
    private Vignette vignette;
    private Vignette postProcessVignette;

    void Start()
    {
        // Initialize components
        PostProcessVolume.SetActive(false);
        _Vignette.SetActive(true);

        PickUpText.SetActive(false);
        drank = false;

        if (screenOut.HasProperty("_Activator_out"))
        {
            screenOut.SetFloat("_Activator_out", 0f); // Set _Activator_out to false
        }

        // Cache Volume and Vignette components
        vignetteVolume = _Vignette.GetComponent<Volume>();
        postProcessVolume = PostProcessVolume.GetComponent<Volume>();

        if (vignetteVolume != null && vignetteVolume.profile.TryGet(out vignette))
        {
            vignette.intensity.overrideState = true;
            vignette.intensity.value = 0f; // Set initial vignette intensity
        }

        // Temporarily disable movement
        if (SC_FPSController != null)
        {
            SC_FPSController.walkingSpeed = 0;
            SC_FPSController.runningSpeed = 0;
            SC_FPSController.jumpSpeed = 0;
        }

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
                    }
                }
            }
        }

        // Find all Script_SquashNStretch instances in the scene
        squashNStretchObjects = FindObjectsOfType<Script_SquashNStretch>();
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
                GetComponent<AudioSource>().Play();

                StartCoroutine(GoToDrunk(0.565f, 1.5f)); // Fade in to 1 over 1.5 seconds
                StartCoroutine(DrinkEffectAfterDelay()); // Start the coroutine

                drank = true;

                // Hide or destroy the pickup text
                if (PickUpText != null) PickUpText.SetActive(false);
                Destroy(PickUpText);
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
        yield return new WaitForSeconds(delayBeforEffect);

        // Apply post-process effect after delay
        foreach (var squashNStretch in squashNStretchObjects)
        {
            squashNStretch.playsEveryTime = true;
            squashNStretch.PlaySquashAndStretch(); // Trigger squash and stretch
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
                    }
                }
            }
        }

        if (screenOut.HasProperty("_Activator_out"))
        {
            screenOut.SetFloat("_Activator_out", 1.0f); // Set _Activator_out to true
        }

        // Add star and play background music
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

        StartCoroutine(GoToTipsy(0.4f, 1.5f));
    }

    private IEnumerator GoToDrunk(float targetIntensity, float duration)
    {
        if (vignette != null)
        {
            float initialIntensity = vignette.intensity.value;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;
                vignette.intensity.value = Mathf.Lerp(initialIntensity, targetIntensity, t);
                yield return null;
            }

            vignette.intensity.value = targetIntensity;
        }
    }

    private IEnumerator GoToTipsy(float targetIntensity, float duration)
    {
        // Deactivate the first vignette volume
        if (postProcessVignette != null)
        {
            _Vignette.SetActive(false);
            Debug.Log("First vignette volume deactivated.");
            if (postProcessVignette != null)
            {
                float initialIntensity = postProcessVignette.intensity.value;
                float elapsedTime = 0f;

                while (elapsedTime < duration)
                {
                    elapsedTime += Time.deltaTime;
                    float t = elapsedTime / duration;
                    postProcessVignette.intensity.value = Mathf.Lerp(initialIntensity, targetIntensity, t);
                    yield return null;
                }

                postProcessVignette.intensity.value = targetIntensity;
            }
        }
    }
}