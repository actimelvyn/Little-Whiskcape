using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class SC_FPSController : MonoBehaviour
{
    public float walkingSpeed;
    public float defwalkingSpeed = 7.5f;

    public float runningSpeed;
    public float defrunningSpeed = 11.5f;

    public float jumpSpeed;
    public float defjumpSpeed = 8.0f;

    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    private PlayFootsteps playFootsteps;
    private bool isPlayingFootstep = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Reference the PlayFootsteps script
        playFootsteps = GetComponent<PlayFootsteps>();
        if (playFootsteps == null)
        {
            Debug.LogError("PlayFootsteps script not found on this GameObject!");
        }
    }

    void Update()
    {
        // Recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        // Detect if the player is moving
        bool isMoving = curSpeedX != 0 || curSpeedY != 0;

        // Play footsteps sound when moving
        if (isMoving && characterController.isGrounded && playFootsteps != null && !isPlayingFootstep)
        {
            StartCoroutine(PlayFootstepSound());
        }

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    IEnumerator PlayFootstepSound()
    {
        isPlayingFootstep = true;
        playFootsteps.AllFootsteps();
        yield return new WaitForSeconds(0.5f); // Adjust timing based on walking/running speed
        isPlayingFootstep = false;
    }
}
