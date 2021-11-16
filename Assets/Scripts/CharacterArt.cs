using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterArt : MonoBehaviour
{
    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;

    public Rigidbody2D rigidbody;

    public SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rigidbody.velocity.magnitude < 0.1) { return; }

        if (rigidbody.velocity.y > rigidbody.velocity.x)
        {
            if (rigidbody.velocity.y > -rigidbody.velocity.x)
            {
                renderer.sprite = up;
            }
            else
            {
                renderer.sprite = left;
            }
        }
        else
        {
            if (rigidbody.velocity.y > -rigidbody.velocity.x)
            {
                renderer.sprite = right;
            }
            else
            {
                renderer.sprite = down;
            }
        }
    }
}
