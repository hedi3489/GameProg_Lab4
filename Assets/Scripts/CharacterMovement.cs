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
    public float gravity = -9.18f;
    
    private CharacterController controller;
    private Animator animator;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        ProcessMovement();
        ProcessGravity();
    }

    public void LateUpdate()
    {
       UpdateAnimator();
    }

    void DisableRootMotion()
    {
        animator.applyRootMotion = false;  
    }

    void UpdateAnimator()
    {
        bool isGrounded = controller.isGrounded; 
        // TODO 
        
    }

    void ProcessMovement()
    { 
        move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
    }

    public void ProcessGravity()
    {
        bool isGrounded = controller.isGrounded;
        // Since there is no physics applied on character controller we have this applies to reapply gravity
        
        if (isGrounded  )
        {
            if (playerVelocity.y < 0.0f) // we want to make sure the players stays grounded when on the ground
            {
                playerVelocity.y = -1.0f;
            }
            
            if (Input.GetButtonDown("Jump")) // Code to jump
            {
                playerVelocity.y +=  Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            }
        }
        else // if not grounded
        {
            playerVelocity.y += gravity * Time.deltaTime;
        }       

        controller.Move(move * Time.deltaTime * GetMovementSpeed() + playerVelocity * Time.deltaTime);
        
    }

    float GetMovementSpeed()
    {
        if (Input.GetButton("Fire3"))// Left shift
        {
            return runSpeed;
        }
        else
        {
            return walkSpeed;
        }
    }
}
