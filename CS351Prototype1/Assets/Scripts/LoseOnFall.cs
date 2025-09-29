/*
    Author: Lenny Ozaeta
    Assignment: Follow Along Lesson 4
    Description: Lose conditions for player
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseOnFall : MonoBehaviour
{
    public float lowestY; // Set in Inspector

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < lowestY)
        {
            ScoreManager.gameOver = true;
        }
    }
}
