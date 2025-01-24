using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_UI : MonoBehaviour
{
    [Header("Star System")]
    public Image[] starSlots; // Array to hold the star slots
    public Sprite emptyStar;  // Empty star sprite
    public Sprite filledStar; // Filled star sprite

    [Header("Timer System")]
    public GameObject threeMinImage; // GameObject for 3 minutes left
    public GameObject twoMinImage;  // GameObject for 2 minutes left
    public GameObject oneMinImage;  // GameObject for 1 minute left
    public GameObject fifteenSecImage; // GameObject for 15 seconds left

    private int filledStars = 0;
    private float timeLeft = 180f; // 3 minutes in seconds
    private bool isTimerRunning = false;

    void Start()
    {
        ResetStars(); // Ensure stars start as empty
        UpdateTimer(timeLeft); // Start the timer at 180 seconds (3 minutes)
        StartTimer(); // Start the timer countdown
    }

    void Update()
    {
        if (isTimerRunning)
        {
            // Decrement the time and update the timer
            timeLeft -= Time.deltaTime;

            // Update the timer sprite based on remaining time
            if (timeLeft <= 0)
            {
                timeLeft = 0;
                isTimerRunning = false; // Stop the timer once it reaches 0
            }

            UpdateTimer(timeLeft); // Update the timer image based on remaining seconds
        }
    }

    // Method to add a filled star
    public void AddStar()
    {
        if (filledStars < starSlots.Length)
        {
            starSlots[filledStars].sprite = filledStar; // Fill the next star
            filledStars++;
        }
    }

    // Method to reset all stars to empty
    public void ResetStars()
    {
        filledStars = 0;
        foreach (Image star in starSlots)
        {
            star.sprite = emptyStar; // Set all stars to empty
        }
    }

    // Method to start the timer
    private void StartTimer()
    {
        isTimerRunning = true;
    }

    // Method to update the timer image based on remaining time (in seconds)
    public void UpdateTimer(float secondsLeft)
    {
        // Hide all timer images first
        threeMinImage.SetActive(false);
        twoMinImage.SetActive(false);
        oneMinImage.SetActive(false);
        fifteenSecImage.SetActive(false);

        // Determine the current time range and show the appropriate image
        if (secondsLeft > 121) // More than 2 minutes left (i.e., 3 minutes)
        {
            threeMinImage.SetActive(true); // Display "3 minutes left" sprite
        }
        else if (secondsLeft <= 120 && secondsLeft > 60) // Between 2 minutes and 1 minute
        {
            twoMinImage.SetActive(true); // Display "2 minutes left" sprite
        }
        else if (secondsLeft <= 60 && secondsLeft > 15) // Between 1 minute and 15 seconds
        {
            oneMinImage.SetActive(true); // Display "1 minute left" sprite
        }
        else if (secondsLeft <= 15) // Less than or equal to 15 seconds
        {
            fifteenSecImage.SetActive(true); // Display "15 seconds left" sprite
        }
    }
}
