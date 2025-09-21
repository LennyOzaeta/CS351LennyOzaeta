using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTriggerZone : MonoBehaviour
{
    bool active = true; // Keeps track of whether trigger zone active

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (active && collision.gameObject.tag == "Player") // Only increment score if trigger zone active and player enters trigger zone
        {
            active = false; // Deactivate trigger zone

            ScoreManager.score++; // Add 1 to score when player enters trigger zone

            Destroy(gameObject); // Destroy this GameObject
        }
        

    }
}
