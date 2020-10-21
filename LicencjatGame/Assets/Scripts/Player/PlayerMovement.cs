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
    public float stamina = 100f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public StaminaBar staminaBar;


    public Vector3 velocity;
    public bool isGrounded;
    bool isRunning;
    Animator animator;
    [SerializeField]
    float currentStamina;
    float x;
    float z;

    private void Start()
    {
        staminaBar.SetMaxStamina(stamina);
        currentStamina = stamina;
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if(isGrounded)
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
        }

        Vector3 move = transform.right * x + transform.forward*z;
        //Debug.Log(z);
        controller.Move(move * speed * Time.deltaTime);
        if (move == Vector3.zero)
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isIdle", false);        
        }

        if (Input.GetButtonDown("Jump")&& isGrounded)
        {
            animator.Play("BasicMotions@Jump01");
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        if (Input.GetButton("Sprint"))
        {
            SetRunning(true);   
        }
        else if(!Input.GetButton("Sprint") && move != Vector3.zero)
        {
            SetRunning(false);
        }
       

        if (isRunning)
        {
            SubstractStamina();
            if(currentStamina < 0)
            {
                currentStamina = 0;
                SetRunning(false);
            }
        }
        else if(currentStamina < stamina)
        {
            currentStamina += Time.deltaTime * 4;
        }

        

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        animator.SetBool("isJumping", false);
        staminaBar.SetStamina(currentStamina);
    }
    void SubstractStamina()
    {
        currentStamina -= Time.deltaTime * 10;
        
    }
    void SetRunning(bool running)
    {
        isRunning = running;
        if (isRunning)
        {
            speed = 6f;
            animator.SetBool("isWalking", false);
            animator.SetBool("isIdle", false);
            animator.SetBool("isRunning", true);
        }
        else
        {
            speed = 4f;
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);
        }
    }

   
}
