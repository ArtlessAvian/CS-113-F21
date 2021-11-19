using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ZombieDebug : MonoBehaviour
{
    ZombieController contr;
    Transform home;
    Transform vision;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (contr is null) { contr = GetComponent<ZombieController>(); }
        if (home is null) { home = transform.Find("debug home");  }
        if (vision is null) { vision = transform.Find("debug vision"); }

        home.position = contr.lastSeenAt;
        home.localScale = Vector2.one * contr.wanderRadius * 2;
        vision.localScale = Vector2.one * contr.visionRadius * 2;
    }
}
