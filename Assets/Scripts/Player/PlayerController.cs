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

    public static bool gameOver;
    public GameObject gameOverPanel;
    public bool isCrouching = false;
    public float CCStandHeight = 2.0f;
    public float CCCrouchHeight = 1.9f;
    public AudioClip jumpSound;
    public AudioClip attackSound;

    public Transform groundCheck;
    public LayerMask groundLayer;

    public bool ableToMakeADoubleJump = true;

    public Animator animator;

    public Transform model;


    void Update()
    {
        if (PlayerManager.gameOver)
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


        CapsuleCollider cc = GetComponent<CapsuleCollider>();

        if (isGrounded)
        {
            ableToMakeADoubleJump = true;

            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }

            if(Input.GetKeyDown(KeyCode.F))
            {
                AudioSource.PlayClipAtPoint(attackSound, transform.position);
                animator.SetTrigger("fireballAttack");
            }

            if(Input.GetKeyDown(KeyCode.C))
            {
                speed = 5;
                controller.height = CCCrouchHeight;
                cc.direction = 0;
                cc.center = new Vector3(0, -0.5f, 0);
                animator.SetTrigger("crouch");
                isCrouching = true;
            }

            if (Input.GetKeyUp(KeyCode.C) && isCrouching)
            {
                speed = 8;
                controller.height = CCStandHeight;
                cc.direction = 1;
                cc.center = new Vector3(0, 0, 0);
                animator.SetTrigger("stand");
                isCrouching = false;
            }

        }

        else
        {
            if (ableToMakeADoubleJump && Input.GetButtonDown("Jump"))
            {
                DoubleJump();
            }

            else
            {
                
                if (model.position.y <= -20 && !isGrounded)
                {
                    animator.SetTrigger("die");
                    this.enabled = false;
                    gameOver = true;
                    gameOverPanel.SetActive(true);
                    PlayerManager.currentHP = 100;
                }
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

        if(transform.position.z != 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }

    private void DoubleJump()
    {
        animator.SetTrigger("doubleJump");
        AudioSource.PlayClipAtPoint(jumpSound, transform.position);
        direction.y = jumpForce;
        ableToMakeADoubleJump = false;
    }

    private void Jump()
    {
        AudioSource.PlayClipAtPoint(jumpSound, transform.position);
        direction.y = jumpForce;
    }
}

