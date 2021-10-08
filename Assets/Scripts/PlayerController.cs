using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // components
    Rigidbody2D rb;

    // logic
    bool queueShove = false;
    bool queueShoot = false;

    // visuals
    public GameObject tracerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 input = new Vector2();
        input += Vector2.right * Input.GetAxisRaw("Horizontal");
        input += Vector2.up * Input.GetAxisRaw("Vertical");

        // Adjust current velocity towards input direction.
        // Setting rb.velocity = targetVel is fine? Bad if something pushes it externally.
        Vector2 targetVel = input;
        rb.velocity = Vector2.MoveTowards(rb.velocity, targetVel, 0.25f);

        if (queueShoot)
        {
            queueShoot = false;
            Vector2 origin = (Vector2)transform.position + GetCursorOffset().normalized * 0.1f; // woo magic numbers
            Ray2D ray = new Ray2D(origin, GetCursorOffset());
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            
            GameObject instance = Instantiate(tracerPrefab, transform.position, transform.rotation);
            TracerVisuals tracer = instance.GetComponent<TracerVisuals>();
            tracer.hit = hit;
            tracer.ray = ray;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.GetMouseButton(1))
            {
                Debug.Log("BLAM");
                queueShoot = true;
                queueShove = false;
            }
            else
            {
                Debug.Log("Shove");
                queueShove = true;
                queueShoot = false;
            }
        }

        // Get cursor's offset from player
        Vector2 cursorRelative = GetCursorOffset();
        // Find the angle. If cursor is below player, negate angle.
        float angle = Vector2.Angle(cursorRelative, Vector2.right) * (cursorRelative.y < 0 ? -1 : 1);
        // Rotate the player's visuals to face the cursor.
        // TODO: Tends to wobble while moving.
        Transform sprite = transform.Find("Sprite");
        sprite.eulerAngles = new Vector3(0, 0, angle);
    }

    Vector2 GetCursorOffset()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }
}
