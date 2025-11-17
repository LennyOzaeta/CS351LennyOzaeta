using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Follow Along Lesson 7, 8, 9
    Description: Enemy class that controls enemy's behavior/animation (attach this script to enemy GameObject)
*/

public class Enemy : MonoBehaviour
{
    public int health = 100; // Enemy's health
    public GameObject deathEffect; // Prefab to spawn when enemy dies
    private DisplayBar healthBar; // Reference to health bar
    public int damage = 10; // Damage enemy deals to player

    private void Start()
    {
        healthBar = GetComponentInChildren<DisplayBar>(); // Find health bar in Children of Enemy

        if(healthBar == null)
        {
            Debug.LogError("HealthBar (DisplayBar script) not found"); // If health bar not found, log an error
            return; // Important to include so code doesn't crash
        }

        healthBar.SetMaxValue(health); // Set max value of health bar to enemy's health
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // Subtract damage dealt from health

        healthBar.SetValue(health); // Update health bar

        if(health <= 0) // If health less than or equal to 0, call Die function
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity); // Spawn death effect

        Destroy(gameObject); // Destroy enemy
    }

    // This function damages player when enemy collides with them
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            // Get player health script from player object
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            // Check if PlayerHealth script is null
            if(playerHealth == null)
            {
                // Log an error if PlayerHealth script is null
                Debug.LogError("PlayerHealth script not found on player");
                return;
            }

            playerHealth.TakeDamage(damage); // Damage player
            playerHealth.Knockback(transform.position); // Knockback player
        }
    }
}
