using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VignetteEffect : MonoBehaviour
{
    public Volume volume;  // The Volume component
    private Vignette vignette;

    public float duration = 5f;  // Total duration for the animation (seconds)
    private float timeElapsed = 0f;
    private bool transitioningTo100 = true;

    private void Start()
    {
        // Check if the volume has the Vignette effect
        if (volume.profile.TryGet<Vignette>(out vignette))
        {
            // Set the initial vignette intensity to 0
            vignette.intensity.Override(0f);
        }
        else
        {
            Debug.LogError("Vignette effect not found in the Volume Profile.");
        }
    }

    private void Update()
    {
        if (vignette != null)
        {
            timeElapsed += Time.deltaTime;

            if (transitioningTo100)
            {
                // Transition from 0 to 100
                vignette.intensity.Override(Mathf.Lerp(0f, 100f, timeElapsed / duration));

                // Once we reach 100, start transitioning to 50
                if (timeElapsed >= duration)
                {
                    transitioningTo100 = false;
                    timeElapsed = 0f;  // Reset the time for the next transition
                }
            }
            else
            {
                // Transition from 100 to 50
                vignette.intensity.Override(Mathf.Lerp(100f, 50f, timeElapsed / duration));

                // Once we reach 50, stop updating
                if (timeElapsed >= duration)
                {
                    timeElapsed = duration;  // Make sure the value stays at 50
                }
            }
        }
    }
}