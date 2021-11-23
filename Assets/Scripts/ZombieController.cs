using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    // components
    Rigidbody2D rb;

    // logic
    public float visionRadius = 1; // unity units
    public float wanderRadius = 0;

    public float moveSpeed = 0.5f; 

    public GameObject chaseAfter;
    public Vector2 lastSeenAt;

    //private static 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (chaseAfter == null)
        {
            chaseAfter = GameObject.FindWithTag("Player");
        }
        lastSeenAt = transform.position;
    }

    private void FixedUpdate()
    {
        Vector2 targetVel = Vector2.zero;

        Vector2 chaseDirection = GetChaseVector();

        if (wanderRadius < 0.1)
        {
            if (chaseDirection.magnitude >= 0.1)
            {
                targetVel = chaseDirection.normalized * moveSpeed;
            }
            else
            {
                targetVel = Vector2.zero;
            }
        }
        else
        {
            if (chaseDirection.magnitude >= wanderRadius)
            {
                targetVel = chaseDirection.normalized * moveSpeed;
            }
            else
            {
                targetVel = (rb.velocity + new Vector2(Random.value - 0.5f, Random.value - 0.5f) * 0.3f).normalized * moveSpeed;
            }
        }

        rb.velocity = Vector2.MoveTowards(rb.velocity, targetVel, 0.25f);
    }

    bool SeesTarget()
    {
        Vector2 direction = chaseAfter.transform.position - transform.position;
        if (direction.magnitude > visionRadius) { return false; }

        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + direction.normalized * 0.16f, direction, visionRadius);

        //RaycastHit2D hit = Physics2D.CircleCast((Vector2)transform.position + direction.normalized * (0.16f + 0.1f), 0.05f, direction, visionRadius);

        return (hit.collider is object) && (hit.collider.gameObject == chaseAfter);
    }

    // Returns the direction of the target if seen, otherwise where it was last.
    Vector2 GetChaseVector()
    {
        // Update the last seen position, if in sight.
        if (SeesTarget())
        {
            // hack to copy the position by value. otherwise, it gets the reference and tracks player through walls.
            lastSeenAt = new Vector2(chaseAfter.transform.position.x, chaseAfter.transform.position.y);
        }

        return lastSeenAt - (Vector2)transform.position;
    }
}
