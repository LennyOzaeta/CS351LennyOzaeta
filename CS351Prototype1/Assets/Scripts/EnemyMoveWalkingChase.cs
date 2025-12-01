using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Author: Lenny Ozaeta
    Follow Along Lesson 10 & 11
*/

// Require Rigidbody2D and Animator on enemy
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class EnemyMoveWalkingChase : MonoBehaviour
{
    // Range at which enemy will chase player
    public float chaseRange = 4f;

    // Speed of enemy movement
    public float enemyMovementSpeed = 1.5f;

    // Transform of player object
    private Transform playerTransform;

    // Rigidbody component of enemy
    private Rigidbody2D rb;

    // Animator component of enemy
    private Animator anim;

    // SpriteRenderer of enemy
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get Rigidbody2D component of enemy
        anim = GetComponent<Animator>(); // Get Animator component of enemy
        playerTransform = GameObject.FindWithTag("Player").transform; // Get player transform using "Player" tag
        sr = GetComponent<SpriteRenderer>(); // Get SpriteRenderer component of enemy
    }

    // Update is called once per frame
    void Update()
    {
        // This Vector2 is a 2D arrow from enemy to player
        Vector2 playerDirection = playerTransform.position - transform.position;

        // Distance between enemy & player (magnitude of vector is the distance without direction)
        float distanceToPlayer = playerDirection.magnitude;

        // Check if player is within chase range..
        if (distanceToPlayer <= chaseRange)
        {
            // Need direction to player on only x-axis

            // Normalize provides direction to player without distance
            playerDirection.Normalize();

            // Setting y-axis to 0 (because only want to move on x-axis)
            playerDirection.y = 0f;

            // Rotate enemy to face player
            FacePlayer(playerDirection);

            // If there is ground ahead of enemy..
            if (IsGroundAhead())
            {
                MoveTowardsPlayer(playerDirection); //..enemy changes direction to continue chasing player
            }
            else //..otherwise, there's no ground ahead (so enemy stops moving)
            {
                StopMoving();
                // Debug.Log("No ground ahead");
            }
        }
        else
        {
            //..otherwise, stop moving if player not within chase range
            StopMoving();
        }
    }

    // Bool function to check if there is ground infront of enemy to walk on
    bool IsGroundAhead()
    {
        // Ground check variables
        float groundCheckDistance = 2.0f; // Adjust this distance as needed
        LayerMask groundLayer = LayerMask.GetMask("Ground");

        // Determine which direction enemy facing
        Vector2 enemyFacingDirection = transform.rotation.y == 0 ? Vector2.left : Vector2.right;

        // Raycast to check for ground ahead of enemy
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down + enemyFacingDirection, groundCheckDistance, groundLayer);

        // Return true if ground detected
        return hit.collider != null;
    }

    private void FacePlayer(Vector2 playerDirection)
    {
        if (playerDirection.x < 0) // If player is to the right of enemy...
        {
            // transform.rotation = Quaternion.Euler(0, 0, 0); [REPLACED BY BELOW LINE]
            sr.flipX = false;
        }
        else //..else if player is to left of enemy
        {
            // transform.rotation = Quaternion.Euler(0, 180, 0); [REPLACED BY BELOW LINE]
            sr.flipX = true;
        }
    }

    private void MoveTowardsPlayer(Vector2 playerDirection)
    {
        // Move enemy towards player by setting velocity to a new Vector2 without changing the y-axis of velocity
        rb.velocity = new Vector2(playerDirection.x * enemyMovementSpeed, rb.velocity.y);

        // Set animator parameter to move
        anim.SetBool("isMoving", true);
    }

    private void StopMoving()
    {
        // Stop moving if player out of range
        rb.velocity = new Vector2(0, rb.velocity.y);

        // Set animator parameter to stop moving
        anim.SetBool("isMoving", false);
    }
}
