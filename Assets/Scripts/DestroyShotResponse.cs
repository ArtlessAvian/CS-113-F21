using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyShotResponse : MonoBehaviour
{
    public GameObject replaceWithPrefab = null; // nullable

    public void OnGetShot(Vector2 point, Vector2 dir)
    {
        if (replaceWithPrefab != null)
        {
            Instantiate(replaceWithPrefab, this.transform.position, this.transform.rotation);
        }
        Destroy(this.gameObject);
    }
}
