/*
    Author: Lenny Ozaeta
    Assignment: Follow Along Lesson 4
    Description: Controls platformer player
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

    // Start is called before the first frame update
    void Start()
    {
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
        }
    }

    void FixedUpdate()
    {
        // Move player using RigidBody2 in FixedUpdate
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        // Check if player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // TODO: Optionally, add animations or other behavior (based on player state) here

        // Ensure player facing direction of movement
        if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // Facing right
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); // Facing left

        }
    }
}
