using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.GetComponent<PlayerController>().ammoCount += 1;
            this.enabled = false;
            Destroy(this.gameObject, 0.1f);

            // play reload sound.
            

        }
    }
}
