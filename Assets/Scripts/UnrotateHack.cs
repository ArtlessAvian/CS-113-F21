using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnrotateHack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 worldPos = transform.position;
        transform.rotation = Quaternion.identity;
        transform.position = worldPos;
    }
}
