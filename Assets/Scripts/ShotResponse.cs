using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShotResponse : MonoBehaviour
{    
    [SerializeField]
    public UnityEvent<Vector2, Vector2> OnGetShot;
}
