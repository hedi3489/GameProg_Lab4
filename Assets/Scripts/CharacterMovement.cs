using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    Vector3 playerVelocity;
    Vector3 move;

    public float walkSpeed = 5;
    public float runSpeed = 8; 
    public float jumpHeight = 2;
    public int maxJumpCount = 1;
    public float gravity = -9.18f;
    public bool isGrounded;
    public bool isRunning;
    private CharacterController controller;
    private Animator animator;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        if (animator.applyRootMotion == false)
        {
            ProcessMovement();
        }
        ProcessGravity();

    }
  

    void ProcessMovement()
    { 
        move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        // Turns the player toward where wants to go
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        // Check input to know if running is getting pressed
        isRunning = Input.GetButton("Run");
        if (Input.GetButtonDown("Jump") && isGrounded) // Code to jump
        {
                playerVelocity.y +=  Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
        controller.Move(move * Time.deltaTime *((isRunning)?runSpeed:walkSpeed));
    }

    /// Dont Modify -----------------------------------------------
    public void ProcessGravity()
    {
        // Since there is no physics applied on character controller we have this applies to reapply gravity
        
        if (isGrounded  )
        {
            if (playerVelocity.y < 0.0f) // we want to make sure the players stays grounded when on the ground
            {
                playerVelocity.y = -1.0f;
            }
        }
        else // if not grounded
        {
            playerVelocity.y += gravity * Time.deltaTime;
        }

        controller.Move(playerVelocity * Time.deltaTime);
        isGrounded = controller.isGrounded;
        
    }

    private void OnAnimatorMove()
    {
        Vector3 velocity = animator.deltaPosition;
        velocity.y = playerVelocity.y * Time.deltaTime;

        controller.Move(velocity);
    }

    public float GetMoveSpeed()
    {
        if (isRunning && (move != Vector3.zero))// Left shift
        {
            return runSpeed;
        }
        else if (move != Vector3.zero)
        {
            return walkSpeed;
        }
        else 
        {
            return 0f;
        }
    }
    /// Dont Modify -----------------------------------------------

}
