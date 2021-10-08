using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

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
    }

    void Update()
    {
        // Get cursor's offset from player
        Vector2 cursorRelative = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        // Find the angle. If cursor is below player, negate angle.
        float angle = Vector2.Angle(cursorRelative, Vector2.right) * (cursorRelative.y < 0 ? -1 : 1);
        // Rotate the player's visuals to face the cursor.
        // TODO: Tends to wobble while moving.
        Transform sprite = transform.Find("Sprite");
        sprite.eulerAngles = new Vector3(0, 0, angle);
    }
}
