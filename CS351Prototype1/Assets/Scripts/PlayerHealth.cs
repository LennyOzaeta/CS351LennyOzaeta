using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Follow Along Lesson 9
    Description: Manages the player's health
*/

public class PlayerHealth : MonoBehaviour
{
    public int health = 100; // Stores player's health
    public DisplayBar healthBar; // Reference to health bar (set in Inspector)
    public Rigidbody2D rb; // Reference to player's Rigidbody2D (to implement player knockback force to player)
    public float knockbackForce = 5f; // Keeps track of knockback force when player collides with enemy
    public GameObject playerDeathEffect; // Prefab to spawn when player dies (set in Inspector)
    public static bool hitRecently = false; // Keeps track of if player has been hit recently
    public float hitRecoveryTime = 0.2f; // Time (in seconds) to recover from hit

    // References to play sound effects
    private AudioSource playerAudio;
    public AudioClip playerHitSound;
    // public AudioClip playerDeathSound;[REMOVED]

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); // Set animator reference

        playerAudio = GetComponent<AudioSource>(); // Set AudioSource reference

        rb = GetComponent<Rigidbody2D>(); // Set Rigidbody2D reference

        // Check if Rigidbody2D reference is null
        if(rb == null)
        {
            Debug.LogError("Rigidbody2D not found on player"); // Log error message if rb is null
        }

        healthBar.SetMaxValue(health); // Set healthBar's max value to player's health
        hitRecently = false; // Initialize hitRecently to false
    }
    
    // This function knocks player back when they collide with enemy
    public void Knockback(Vector3 enemyPosition)
    {
        if(hitRecently) // If player was hit recently...
        {
            return; // ...return (exit) out of this function
        }

        hitRecently = true; // Set hitRecently to true

        if (gameObject.activeSelf) // If this gameObject is active, then start coroutine
        {
            StartCoroutine(RecoverFromHit()); // Start coroutine to reset hitRecently
        }

        Vector2 direction = transform.position - enemyPosition; // Calculate direction of knockback

        // Normalize direction vector; this gives consistent knockback force regardless of distance between player & enemy
        direction.Normalize();

        direction.y = direction.y * 0.5f + 0.5f; // Add upward direction to knockback (to make knockback more "reliastic")
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse); // Add force to player in direction of knockback
    }

    // Coroutine to reset hitRecently after hitRecoveryTime seconds
    IEnumerator RecoverFromHit()
    {
        yield return new WaitForSeconds(hitRecoveryTime); // Wait for hitRecoveryTime (0.2) seconds
        hitRecently = false; // Set hitRecently to false

        animator.SetBool("hit", false); // Set hit animation to false
    }

    // This function applies damage to player
    public void TakeDamage(int damage)
    {
        health -= damage; // Subtract damage from health
        healthBar.SetValue(health); // Update health bar

        // TODO: Play animation when player takes damage

        if (health <= 0) // If health <= 0...
        {
            Die(); //...call Die()
        }
        else
        {
            playerAudio.PlayOneShot(playerHitSound); //...or else play PlayerHitSound
            animator.SetBool("hit", true); //...and play player hit animation
        }

    }

    // This function called when player dies
    public void Die()
    {
        ScoreManager.gameOver = true; // Set gameover to true

        // Play sound effect when player dies
        // playerAudio.PlayOneShot(playerDeathSound); [REMOVED]

        // Instantiate death effect at player's position
        GameObject deathEffect = Instantiate(playerDeathEffect, transform.position, Quaternion.identity);

        // Destroy(deathEffect, 2f); // Destroy death effect after 2 seconds [REMOVED]

        gameObject.SetActive(false); // Disable player object
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
