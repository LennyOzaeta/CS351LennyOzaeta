using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Follow Along Lesson 7
    Description: Allows player to shoot projectiles (attach to PlatformerPlayer)
*/

public class ShootProjectile : MonoBehaviour
{
    public GameObject projectilePrefab; // Reference to projectile prefab

    // Reference to firepoint transform
    /* This is where projectile will be instantiated:
        Make an empty child object of the player and position it where want projectile to be fired from
        and then assign it to this variable in the Inspector
    */
    public Transform firePoint;

    // Update is called once per frame
    void Update()
    {
        // If player presses fire button, call shoot function
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Instantiate projectile at firepoint position & rotation and store reference to instantiated projectile in variable
        GameObject firedProjectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        Destroy(firedProjectile, 3f); // Destroy projectile after 3 seconds
    }
}
