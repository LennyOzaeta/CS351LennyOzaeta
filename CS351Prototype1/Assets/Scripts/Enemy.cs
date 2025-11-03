using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Follow Along Lesson 7
    Description: Enemy class that controls enemy's behavior/animation (attach this script to enemy GameObject)
*/

public class Enemy : MonoBehaviour
{
    public int health = 100; // Enemy's health

    public GameObject deathEffect; // Prefab to spawn when enemy dies

    public void TakeDamage(int damage)
    {
        health -= damage; // Subtract damage dealt from health

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
}
