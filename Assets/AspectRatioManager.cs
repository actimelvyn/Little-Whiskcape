using UnityEngine;

public class AspectRatioManager : MonoBehaviour
{
    private float targetAspect = 4.0f / 3.0f; // Target 4:3 aspect ratio

    void Start()
    {
        SetAspectRatio();
    }

    void SetAspectRatio()
    {
        // Calculate the current screen aspect ratio
        float windowAspect = (float)Screen.width / (float)Screen.height;

        // Calculate the scale based on the target aspect ratio
        float scaleHeight = windowAspect / targetAspect;

        // Adjust the viewport if the aspect ratio is not 4:3
        Camera camera = Camera.main;
        if (scaleHeight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            camera.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = camera.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }
}
