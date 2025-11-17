using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Follow Along Lesson 7, 9
    Description: Class that controls destruction of objects (attach this to EnemyDeathEffect prefab)
*/

public class DestroySelfAfterDelay : MonoBehaviour
{
    public float delay = 2f; // Delay before game object destroyed

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, delay); // Destroy game object after 2 seconds
    }
}
