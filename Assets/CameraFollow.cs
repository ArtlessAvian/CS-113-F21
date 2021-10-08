using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Allows things to "steal" the camera.
// Well, not yet. Don't need it yet.
public class CameraFollow : MonoBehaviour
{
    public Transform toFollow;
    
    readonly Vector3 offset = new Vector3(0, 0, -10);

    // Start is called before the first frame update
    void Start()
    {
        transform.position = toFollow.position + offset;
        // toFollowLastPosition = toFollow.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = toFollow.position + offset;
    }
}
