using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 1f;
    public Animator animator;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.GameIsPaused)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                animator.SetBool("IsJumping", true);
            } else if (Input.GetButtonDown("Crouch"))
            {
                crouch = true;
                animator.SetBool("IsCrouching", true);
            } else if (Input.GetButtonUp("Crouch"))
            {
                crouch = false;
                animator.SetBool("IsCrouching", false);
            }
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }


    private void FixedUpdate()
    {
        if (crouch) horizontalMove = 0;
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
