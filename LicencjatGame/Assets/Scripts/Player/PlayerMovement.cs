using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 4f;
    public float gravity = -20f;
    public float jumpHeight = 2.5f;
    public float WaterHeight = 15.5f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;


    public Vector3 velocity;
    public bool isGrounded;
    Animator animator;
    float x;
    float z;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if(isGrounded){
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        }

        if (x == 0 && z == 0)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isIdle", true);
        }

        Vector3 move = transform.right * x + transform.forward*z;
        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump")&& isGrounded)
        {
            animator.Play("BasicMotions@Jump01");
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        if (Input.GetKey("left shift"))
        {
            speed = 8f;
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);
            speed = 4f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        animator.SetBool("isIdle", false);
        animator.SetBool("isJumping", false);
        
    }

   
}
