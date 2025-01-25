using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timeLimit = 1180f; // Set time limit in seconds
    private float timeRemaining;

    public sceneManager sceneController; // Reference to the SceneController

    void Start()
    {
        timeRemaining = timeLimit;
    }

    void Update()
    {
        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            sceneController.GameOver();
        }
    }

    public void EscapeSuccess()
    {
        sceneController.WinCondition();
    }
}
