using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPickup : MonoBehaviour
{
    public AudioSource reload_sfx;
    public float volume = 1.0f;
    public Renderer bullet_graphic;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (collision.GetComponent<PlayerController>().ammoCount < 6)
            {
                collision.GetComponent<PlayerController>().ammoCount += 1;
                Debug.Log("Playing pickup audio");
                reload_sfx.PlayOneShot(reload_sfx.clip, volume);
                //reload_sfx.PlayDelayed(0f);
                bullet_graphic.enabled = false;
                Destroy(this.gameObject, reload_sfx.clip.length);
            }

            // play reload sound.


        }
    }
}
