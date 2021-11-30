using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // components
    Rigidbody2D rb;

    // logic
    bool queueShoot = false;

    public readonly float shotPeriod = 1f; // seconds
    float shotCooldown = 0;

    public readonly int maxShots = 6; // balance.
    public int ammoCount;

    public float stamina = 1;
    public float walkSpeed = 1; // units per second
    public float runSpeed = 1.2f; // units per second
    
    public float zombieNearbySpeedScale = 0.5f;
    public float zombieRange = 0.3f;

    public float runStaminaDrain = 1f; // per second
    public float runStaminaRecover = 0.05f; // per second

    public int health = 3;
    public float healthTimerMax = 1;
    public float healthTimer = 1; // seconds to take hit;

    // visuals
    public GameObject tracerPrefab;

    public FlashOnHit hitFlash;
    public GameObject gameOverUI;
    public GameObject gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        ammoCount = maxShots;
    }

    void FixedUpdate()
    {
        Vector2 input = new Vector2();
        input += Vector2.right * Input.GetAxisRaw("Horizontal");
        input += Vector2.up * Input.GetAxisRaw("Vertical");

        // Adjust current velocity towards input direction.
        // Setting rb.velocity = targetVel is fine? Bad if something pushes it externally.
        Vector2 targetVel = input;

        if (Input.GetAxisRaw("Run") > 0 && stamina > 0)
        {
            targetVel *= runSpeed;
            stamina = Mathf.Max(0, stamina - runStaminaDrain * Time.fixedDeltaTime);
        }
        else
        {
            targetVel *= walkSpeed;
            if (Input.GetAxisRaw("Run") == 0)
            {
                stamina = Mathf.Min(1, stamina + runStaminaRecover * Time.fixedDeltaTime);
            }
        }

        if (ZombieNearby())
        {
            targetVel *= zombieNearbySpeedScale;
            healthTimer -= Time.fixedDeltaTime;
            if (healthTimer <= 0)
            {
                healthTimer += healthTimerMax;
                health--;
                Debug.Log("OOF");

                hitFlash.Flash();

                if (health == 0)
                {
                    this.enabled = false;
                    gameOverText.SetActive(true);
                }
            }
        }
        else
        {
            healthTimer = healthTimerMax;
        }

        rb.velocity = Vector2.MoveTowards(rb.velocity, targetVel, 0.25f);

        if (queueShoot)
        {
            queueShoot = false;

            if (ammoCount > 0)
            {
                ammoCount--;
                Shoot();
            }
            else
            {
                print("Click!");
            }
        }

        if (shotCooldown > 0)
        {
            shotCooldown -= Time.fixedDeltaTime;
        }

        if (health <= 0)
        {
            Time.timeScale = 0f;
            gameOverUI.SetActive(true);
        }
    }
    private bool ZombieNearby()
    {
        foreach (ZombieController zombie in FindObjectsOfType<ZombieController>(false))
        {
            if ((zombie.transform.position - transform.position).magnitude < zombieRange)
            {
                return true;
            }
        }
        return false;
    }

    private void Shoot()
    {
        shotCooldown = shotPeriod;
        Vector2 origin = (Vector2)transform.position + GetCursorOffset().normalized * 0.21f; // woo magic numbers
        Ray2D ray = new Ray2D(origin, GetCursorOffset());
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        // Notify the thing that it got hit.
        hit.collider?.gameObject.GetComponent<ShotResponse>()?.OnGetShot.Invoke(hit.point, ray.direction.normalized);

        // Create a visual representation.            
        GameObject instance = Instantiate(tracerPrefab, transform.position, transform.rotation);
        TracerVisuals tracer = instance.GetComponent<TracerVisuals>();
        tracer.hit = hit;
        tracer.ray = ray;
    }

    void Update()
    {
        // Controls stuff

        if (Input.GetMouseButtonDown(0))
        {
            if (shotCooldown <= 0)
            {
                queueShoot = true;
            }
        }

        // Visual stuff

        // Get cursor's offset from player
        Vector2 cursorRelative = GetCursorOffset();
        // Find the angle. If cursor is below player, negate angle.
        float angle = Vector2.Angle(cursorRelative, Vector2.right) * (cursorRelative.y < 0 ? -1 : 1);
        // Rotate the player's visuals to face the cursor.
        // TODO: Tends to wobble while moving.
        Transform sprite = transform.Find("GunOffset");
        sprite.eulerAngles = new Vector3(0, 0, angle);
        sprite.localScale = new Vector3(1, cursorRelative.x < 0 ? -1 : 1, 1);

        transform.Find("GunOffset/CooldownBar").gameObject.SetActive(shotCooldown > 0); // Only show if reloading.
        transform.Find("GunOffset/CooldownBar/Progress").localScale = new Vector3(1 - shotCooldown / shotPeriod, 1, 1);
    }

    Vector2 GetCursorOffset()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }
}
