using UnityEngine;
using System.Collections;  // Needed for Coroutines

public class Script_Final_Door : MonoBehaviour
{
    public Script_PickUP_Key keyPickup; 
    private Animator animator;          
    private Collider doorCollider;
    public GameObject intText;
    public float waitTime;


    private void Start()
    {
        animator = GetComponent<Animator>();
        doorCollider = GetComponent<Collider>();
        intText.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        print("enter");
        if (other.CompareTag("Player") && keyPickup.canOpenDoor)
        {
            Debug.Log("Player triggered the door. Opening door...");

            animator.SetTrigger("openDoor");
            // Temporarily disable movement //icit
            SC_FPSController fpscController= FindObjectOfType<SC_FPSController>();

            fpscController.walkingSpeed = 0;
            fpscController.runningSpeed = 0;
            fpscController.jumpSpeed = 0;
            // Start the Coroutine to delay the door action
            Timer timer = FindObjectOfType<Timer>();
            timer.timeRemaining = 15f;
            StartCoroutine(DelayDoorAction());  // seconds delay
           // sceneManager SceneManager = FindObjectOfType<sceneManager>();
          //  SceneManager.WinCondition();

        }
        else if (other.CompareTag("Player") && keyPickup.canOpenDoor == false)
        {
            Debug.Log("Player triggered the door, but canOpenDoor is false.");
            intText.SetActive(true);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        intText.SetActive(false);

    }

    // Coroutine to delay the door action
    IEnumerator DelayDoorAction()
    {
        yield return new WaitForSeconds(waitTime); // Wait for 'delay' seconds

        // Now perform the door action after the delay
        animator.SetTrigger("openDoor");
        sceneManager SceneManager = FindObjectOfType<sceneManager>();
        SceneManager.WinCondition();
    }
}
