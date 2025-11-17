/*
    Author: Lenny Ozaeta
    Assignment: Follow Along Lesson 4, 5, 6, 7, 9
    Description: Controls the main platformer player
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Player movement speed

    public float jumpForce = 10f; // Force applied when jumping
    public LayerMask groundLayer; // Layer mask for detecting ground
    public Transform groundCheck; // Transform representing position to check for ground
    public float groundCheckRadius = 0.2f; // Radius for ground check

    private Rigidbody2D rb; // Reference to Rigidbody2D
    private float horizontalInput; // Horziontal input

    private bool isGrounded; // Boolean to keep track of if we are touching the ground

    // Set these references in Inspector
    public AudioClip jumpSound; // Set this to jump sound
    private AudioSource playerAudio; // Set this to player audio

    private Animator animator; // Reference to animator

    // Start is called before the first frame update
    void Start()
    {
        // Set reference to animator
        animator = GetComponent<Animator>();

        // Set reference to player audio source
        playerAudio = GetComponent<AudioSource>();

        // Get Rigidbody2D component attached to GameObject
        rb = GetComponent<Rigidbody2D>();

        // Ensure groundCheck variable is assigned
        if (groundCheck == null)
        {
            Debug.LogError("groundCheck not assigned to player controller!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get input values for horizontal movement
        horizontalInput = Input.GetAxis("Horizontal");

        // Check for jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Apply upward force for jumping

            playerAudio.PlayOneShot(jumpSound, 1.0f); // Play jump sound effect
        }
    }

    void FixedUpdate()
    {
        if (!PlayerHealth.hitRecently)
        {
            // Move player using RigidBody2 in FixedUpdate
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        }

        // Set animator parameter xVelocityAbs to absolute value of x velocity
        animator.SetFloat("xVelocityAbs", Mathf.Abs(rb.velocity.x));
        // Set animator parameter yVelocity to y velocity
        animator.SetFloat("yVelocity", rb.velocity.y);

        // Check if player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Set animator parameter onGround to isGrounded
        animator.SetBool("onGround", isGrounded);

        // Rotate player sprite with transform.rotate to face right or left based on horizontal input
        if (horizontalInput > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            // transform.localScale = new Vector3(1f, 1f, 1f); // Facing right
        }
        else if (horizontalInput < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            // transform.localScale = new Vector3(-1f, 1f, 1f); // Facing left

        }
    }
}
