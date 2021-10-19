using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    // components
    Rigidbody2D rb;

    // logic
    public float visionRadius = 1; // unity units

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
        Vector2 chaseDirection = GetChaseVector();

        Vector2 targetVel = chaseDirection.normalized * 0.5f;

        // if close enough, stop. (otherwise it oscillates over the point.)
        if (chaseDirection.sqrMagnitude <= 0.01)
        {
            targetVel = Vector2.zero;
        }

        rb.velocity = Vector2.MoveTowards(rb.velocity, targetVel, 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Returns the direction of the target if seen, otherwise where it was last.
    Vector2 GetChaseVector()
    {
        // Update the last seen position, if in sight.
        Vector2 direction = chaseAfter.transform.position - transform.position;
        RaycastHit2D hit = Physics2D.CircleCast((Vector2)transform.position + direction.normalized * 0.15f, 0.05f, direction, visionRadius);
        
        if ((hit.collider is object) && hit.collider.gameObject == chaseAfter)
        {
            // hack to copy the position by value. otherwise, it gets the reference and tracks player through walls.
            lastSeenAt = new Vector2(chaseAfter.transform.position.x, chaseAfter.transform.position.y);
        }

        return lastSeenAt - (Vector2)transform.position;
    }
}
