using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Need to use TextMeshPro (library)
using TMPro;

public class TriggerZone : MonoBehaviour
{
    // Set this reference in inspector
    public TMP_Text output;

    // Enter text to display in inspector
    public string textToDisplay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Print text message to console
        // Debug.Log("Triggered by: " + collision.gameObject.name);

        // Set "Player" tag on player in inspector
        if(collision.gameObject.tag == "Player")
        {
            // Display "You Win!" on screen
            output.text = textToDisplay;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
