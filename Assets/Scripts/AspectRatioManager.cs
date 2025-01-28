using UnityEngine;
using UnityEngine.UI;

public class AspectRatioManager : MonoBehaviour
{
    private float targetAspect = 4.0f / 3.0f; // Target 4:3 aspect ratio
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        SetAspectRatio();
    }

    void SetAspectRatio()
    {
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            Rect rect = mainCamera.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            mainCamera.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = mainCamera.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            mainCamera.rect = rect;
        }

        // Adjust the Canvas Scaler dynamically
        CanvasScaler canvasScaler = FindObjectOfType<CanvasScaler>();
        if (canvasScaler != null)
        {
            canvasScaler.referenceResolution = new Vector2(1024, 768); // Reference 4:3 resolution
        }
    }
}
