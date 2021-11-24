using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashOnHit : MonoBehaviour
{
    UnityEngine.UI.Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<UnityEngine.UI.Image>();
    }

    // Update is called once per frame
    void Update()
    {
        float a = image.color.a * 49/50f;
        image.color = new Color(1, 0, 0, a);
    }

    public void Flash()
    {
        image.color = new Color(1, 0, 0, 0.5f);
    }
}
