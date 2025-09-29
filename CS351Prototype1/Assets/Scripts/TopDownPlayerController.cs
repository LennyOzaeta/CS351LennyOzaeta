using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this in Inspector to set player's movement speed

    private Rigidbody2D rb;
    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get Rigidbody2D component attached to GameObject
    }

    // Update is called once per frame
    void Update()
    {
        // Get input values for horizontal & vertical movement
        movement.x = Input.GetAxisRaw("Horizontal"); // "GetAxisRaw" retrieves input that's either 1, 0, or -1
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize(); // Normalize movement vector to prevent faster diagonal movement (i.e. set magnitude to 1)
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed); // Move player using Rigidbody2D in FixedUpdate
    }
}
