using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Follow Along Lesson 7
    Description: Projectile class that controls movement of projectile (attach this script to projectile prefab) 
*/

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb; // Rigidbody component of projectile
    public float speed = 20f; // Speed of projectile with default value 20
    public int damage = 20; // Damage of projectile with default value 20
    public GameObject impactEffect; // Impact effect of projectile

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get rigidbody component
        rb.velocity = transform.right * speed; // Set velocity of projectile to fire to the right (at the speed)
    }

    // Function called when projectile collides with another object
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>(); // Get Enemy component of object that was hit

        if(enemy != null) // If object that was hit has Enemy component..
        {
            enemy.TakeDamage(damage); //..call TakeDamage function of Enemy component
        }

        if(hitInfo.gameObject.tag != "Player") // If object that was hit isn't the player..
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity); //..instantiate impact effect

            Destroy(gameObject); //..and destroy projectile
        }
    }
}
