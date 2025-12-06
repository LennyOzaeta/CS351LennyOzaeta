using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Follow Along Lesson 12

public class EnemyMoveFlyingPatrolChase : MonoBehaviour
{
    // Public references (array of game objects) for patrol points
    public GameObject[] patrolPoints;

    // Current patrol point index
    private int currentPatrolPointIndex = 0;

    // Public variables for movement
    public float speed = 2f;
    public float chaseRange = 3f;

    // Enemy state enum (enumerative list of possible enemy states)
    public enum EnemyState { PATROLLING, CHASING };

    // Current enemy state
    public EnemyState currentState = EnemyState.PATROLLING;
    public GameObject target;
    private GameObject player; // Reference to player
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player"); // Find player
        rb = GetComponent<Rigidbody2D>(); // Get rb component of enemy
        sr = GetComponent<SpriteRenderer>(); // Get sr component of enemy

        // Check if patrol points are assigned
        if (patrolPoints == null || patrolPoints.Length == 0)
        {
            Debug.LogError("No patrol points assigned!");
        }

        // Set initial target to first patrol point
        target = patrolPoints[currentPatrolPointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState(); // Update state based on player & target distance

        // Move & face based on current state
        switch (currentState)
        {
            case EnemyState.PATROLLING:
                Patrol();
                break;
            case EnemyState.CHASING:
                ChasePlayer();
                break;
            default:
                Debug.LogError("Invalid currentState on OctopusEnemy");
                break;
        }

        // Use Debug.DrawLine to draw a line between two points in Scene view
        Debug.DrawLine(transform.position, target.transform.position, Color.red);
    }

    // Update enemy state based on player proximity
    void UpdateState()
    {
        if (IsPlayerInChaseRange() && currentState == EnemyState.PATROLLING)
        {
            currentState = EnemyState.CHASING;
        }
        else if (!IsPlayerInChaseRange() && currentState == EnemyState.CHASING)
        {
            currentState = EnemyState.PATROLLING;
        }
    }

    bool IsPlayerInChaseRange()
    {
        if (player == null)
        {
            Debug.LogError("Player not found");
            return false;
        }

        float distance = Vector2.Distance(transform.position, player.transform.position);
        return distance <= chaseRange;
    }

    void Patrol()
    {
        // Check if reached current target (i.e. reached "threshold", NOT exact target value)
        if (Vector2.Distance(transform.position, target.transform.position) <= 0.5f)
        {
            // Update target to next patrol point (wrap around to beginning)
            currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Length;
        }

        // Set target to current patrol point
        target = patrolPoints[currentPatrolPointIndex];

        MoveTowardsTarget();
    }

    void ChasePlayer()
    {
        target = player;
        MoveTowardsTarget();
    }

    void MoveTowardsTarget()
    {
        // Calculate direction towards target
        Vector2 direction = target.transform.position - transform.position;

        direction.Normalize(); // Normalize direction

        // Move towards target with rb
        rb.velocity = direction * speed;

        FaceForward(direction); // Face forward
    }

    private void FaceForward(Vector2 direction)
    {
        if (direction.x < 0)
        {
            sr.flipX = false;
        }
        else if (direction.x > 0)
        {
            sr.flipX = true;
        }
    }

    // Draw circles for patrol points in Scene view
    private void OnDrawGizmos()
    {
        if (patrolPoints != null)
        {
            Gizmos.color = Color.green;
            foreach (GameObject point in patrolPoints)
            {
                Gizmos.DrawWireSphere(point.transform.position, 0.5f);
            }
        }
    }
}
