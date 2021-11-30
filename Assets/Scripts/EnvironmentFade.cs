using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentFade : MonoBehaviour
{
    public GameObject VictoryText;
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Finish")
        {
            VictoryText.SetActive(true);
        }
        col.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
    }
    void OnTriggerExit2D(Collider2D col)
    {
        col.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }
}
