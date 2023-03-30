using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 direction;
    public float speed = 8; //public kako bismo mogli mijenjati brzinu
    public float jumpForce = 10;
    public float gravity = -20; //sila gravitacije
    
    public Transform groundCheck;
    public LayerMask groundLayer;

    public bool ableToMakeADoubleJump = true;

    public Animator animator;

    public Transform model;


    void Update()
    {
        if(PlayerManager.gameOver)
        {
            animator.SetTrigger("die");

            this.enabled = false;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        direction.x = horizontalInput * speed; //kretanje po x-osi

        animator.SetFloat("speed", Mathf.Abs(horizontalInput));

        bool isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer); //je li igraè na tlu

        animator.SetBool("isGrounded", isGrounded);

        direction.y += gravity * Time.deltaTime;

        if (isGrounded)
        {
            ableToMakeADoubleJump = true;

            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }

            if(Input.GetKeyDown(KeyCode.F))
            {
                animator.SetTrigger("fireballAttack");
            }
        }

        else
        {
            if (ableToMakeADoubleJump && Input.GetButtonDown("Jump"))
            {
                DoubleJump();
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fireball Attack"))
            return;

        if(horizontalInput != 0) //promjena smjera igraèa
        {
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(horizontalInput, 0, 0)); //negativno znaèi lijevo
            model.rotation = newRotation;
        }

        controller.Move(direction * Time.deltaTime);
    }

    private void DoubleJump()
    {
        animator.SetTrigger("doubleJump");
        direction.y = jumpForce;
        ableToMakeADoubleJump = false;
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }
}
