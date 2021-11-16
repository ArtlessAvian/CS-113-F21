using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracerVisuals : MonoBehaviour
{
    public Ray2D ray;
    public RaycastHit2D hit;

    public float gunOffset = 0.25f;
    public float localTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = ray.origin;

        float angle = Vector2.Angle(ray.direction, Vector2.right) * (ray.direction.y < 0 ? -1 : 1);

        Transform tracer = transform.Find("Tracer");
        tracer.eulerAngles = new Vector3(0, 0, angle);

        if (hit.collider is object)
        {
            // draw a distance unit long rectangle, centered at the midpoint.
            tracer.localScale = new Vector3(hit.distance - gunOffset, 0.02f, 1);

            tracer.position = (ray.origin + hit.point) / 2f;
            Vector3 offset = ray.direction.normalized * gunOffset / 2f;
            tracer.position += offset;
        }
        else
        {
            // draw a 100 unit long rectangle, centered 50 units away.
            tracer.localScale = new Vector3(100, 0.02f, 1);
            tracer.position = (ray.origin + ray.origin + ray.direction.normalized * (100 + gunOffset)) / 2f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        localTime += Time.deltaTime;

        // Fade Tracer quickly
        SpriteRenderer tracer = transform.Find("Tracer").GetComponent<SpriteRenderer>();
        if (localTime < 0.2f)
        {
            tracer.color = new Color(tracer.color.r, tracer.color.g, tracer.color.b, Mathf.Cos(localTime * 5 * Mathf.PI / 2f));
        }
        else
        {
            tracer.color = new Color(tracer.color.r, tracer.color.g, tracer.color.b, 0);
        }

        // Fade flash quickly. Hopefully no photosensitivity issues.
        //SpriteRenderer flash = transform.Find("Flash").GetComponent<SpriteRenderer>();
        //flash.color = new Color(flash.color.r, flash.color.g, flash.color.b, 0.25f - 5 * localTime * localTime);

        // Darken everything else to sell the flash. Helps with photosensitivity issues.
        SpriteRenderer darken = transform.Find("Darken").GetComponent<SpriteRenderer>();
        darken.color = new Color(0, 0, 0, Mathf.Cos(localTime * Mathf.PI / 2f) / 2f);

        if (localTime > 1)
        {
            Destroy(gameObject, 0.1f);
        }
    }
}
