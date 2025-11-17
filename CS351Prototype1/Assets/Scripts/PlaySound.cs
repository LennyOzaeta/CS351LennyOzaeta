using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Follow Along Lesson 9
    Description: Controls when the DeathEffect sound plays when PlatformerPlayer dies
*/
// Requre AudioSource component to be attached to GameObject this script attached to
[RequireComponent(typeof(AudioSource))]

public class PlaySound : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip soundToPlay;
    public float volume = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // Set reference for AudioSource
        audioSource = GetComponent<AudioSource>();

        // Play sound on start (reusable)
        audioSource.PlayOneShot(soundToPlay, volume);
    }
}
