using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator animator;
    private CharacterMovement movement;
    public void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<CharacterMovement>();
    }
  public void LateUpdate()
    {
       UpdateAnimator();
    }


    void UpdateAnimator()
    {
        
    }
}
