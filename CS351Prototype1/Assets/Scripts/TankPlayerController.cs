using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPlayerController : MonoBehaviour
{
    // Float that holds speed of player (set to 8 in inspector)
    public float speed;

    // Float that holds turn speed of player (set to 100 in inspector)
    public float turnSpeed;

    // Variables for input
    public float horizontalInput;
    public float verticalInput;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move forward (player flies off screen to the right)
        // transform.Translate(1,0,0);
        // transform.Translate(Vector3.right);
        // transform.Translate(Vector3.right * Time.deltaTime * speed); // Move smoothly forward at 8 m/s

        // Get input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Move player side-to-side with horizontal input
        // transform.Translate(Vector3.right * turnSpeed * Time.deltaTime * horizontalInput);

        // Move player forward with vertical input (up/down arrows or w/s keys)
        transform.Translate(Vector3.right * Time.deltaTime * speed * verticalInput);

        // Rotate player with horizontal input
        // transform.Rotate(Vector3.back, turnSpeed * Time.deltaTime * horizontalInput);

        // Rotate player with horizontal input but reverse rotation direction when moving backwards
        if(verticalInput < 0)
        {
            transform.Rotate(Vector3.back, -turnSpeed * Time.deltaTime * horizontalInput);
        }
        else
        {
            transform.Rotate(Vector3.back, turnSpeed * Time.deltaTime * horizontalInput);
        }
    }
}
