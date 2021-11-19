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

    public const float moveSpeed = 0.5f; 

    public GameObject chaseAfter;
    private Vector2 lastSeenAt;

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
        Vector2 targetVel;

        Vector2 chaseDirection = GetChaseVector();
        targetVel = chaseDirection.normalized * moveSpeed;

        // if close enough, stop. (otherwise it oscillates over the point.)
        if (chaseDirection.sqrMagnitude <= wanderRadius * wanderRadius)
        {
            if (wanderRadius < 0.1)
            {
                targetVel = Vector2.zero;
            }
            else // wander radius is large.
            {
                if (!SeesTarget())
                {
                    // sample disk
                    

                    targetVel = (rb.velocity + new Vector2(Random.value - 0.5f, Random.value - 0.5f) * 0.3f).normalized * moveSpeed;
                }
            }
        }

        rb.velocity = Vector2.MoveTowards(rb.velocity, targetVel, 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Find("debug home").position = lastSeenAt;
        transform.Find("debug home").localScale = Vector2.one * wanderRadius * 2;
        transform.Find("debug vision").localScale = Vector2.one * visionRadius * 2;
    }

    bool SeesTarget()
    {
        Vector2 direction = chaseAfter.transform.position - transform.position;
        if (direction.magnitude > visionRadius) { return false; }
        
        RaycastHit2D hit = Physics2D.CircleCast((Vector2)transform.position + direction.normalized * 0.15f, 0.05f, direction, visionRadius);

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
