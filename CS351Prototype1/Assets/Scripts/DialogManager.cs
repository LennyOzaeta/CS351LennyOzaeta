using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Needed to use TMP_Text

public class DialogManager : MonoBehaviour
{
    public TMP_Text textbox;
    public string[] sentences;
    private int index;
    public float typingSpeed;

    public GameObject continueButton;
    public GameObject dialogPanel;

    private void OnEnable()
    {
        continueButton.SetActive(false); // Disables continue button
        StartCoroutine(Type());
    }

    // Coroutine to type one letter at a time in dialog box
    IEnumerator Type()
    {
        textbox.text = ""; // Start textbox as empty
        foreach(char letter in sentences[index]) // Loop through sentence, adding one letter at a time
        {
            textbox.text += letter;
            yield return new WaitForSeconds(typingSpeed); // Wait for given number of seconds before continue executing coroutine
        }
        continueButton.SetActive(true); // Enables continue button after sentence fully displayed
    }

    public void NextSentence()
    {
        continueButton.SetActive(false); // Disables continue button
        if (index < sentences.Length - 1)
        {
            index++;
            textbox.text = "";
            StartCoroutine(Type());
        } else // Done displaying message
        {
            textbox.text = ""; // Clears textbox
            dialogPanel.SetActive(false); // Closes dialog panel
        }
    }
}
