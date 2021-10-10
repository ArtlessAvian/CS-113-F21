using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieShotResponse : MonoBehaviour
{
    Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnGetShot(Vector2 point, Vector2 dir)
    {
        this.DisableLogic();

        // do like a fancy animation or something.
        Debug.Log("Ouchy");
        rb.velocity = Vector2.zero;
        rb.AddForceAtPosition(dir, point, ForceMode2D.Impulse);

        Destroy(this.gameObject, 0.5f);
    }

    private void DisableLogic()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<ZombieController>().enabled = false;
    }
}
