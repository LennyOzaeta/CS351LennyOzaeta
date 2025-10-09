using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTriggerZone : MonoBehaviour
{
    bool active = true; // Keeps track of whether trigger zone active

    public AudioClip scoreSound; // Set this to score sound
    private AudioSource playerAudio; // Set this to player audio

    private void Start()
    {
        playerAudio = GetComponent<AudioSource>(); // Set reference to audio source
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (active && collision.gameObject.tag == "Player") // Only increment score if trigger zone active and player enters trigger zone
        {
            active = false; // Deactivate trigger zone

            ScoreManager.score++; // Add 1 to score when player enters trigger zone

            playerAudio.PlayOneShot(scoreSound, 1.0f); // Play score sound effect

            // Below three lines avoids issue where score sound isn't played (because gameObject destroyed before that can occur)
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;
            Destroy(gameObject, 2.0f); // Destroy this GameObject (but first delaying it so score sound can play)
        }
    }
}
