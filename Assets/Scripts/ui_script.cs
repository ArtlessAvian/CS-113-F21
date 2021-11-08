using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui_script : MonoBehaviour
{
    Transform stamina;
    Transform bulletCount;
    Transform healthCount;
    PlayerController pc;

    public Sprite[] numbers;

    // Start is called before the first frame update
    void Start()
    {
        stamina = transform.GetChild(0);
        bulletCount = transform.GetChild(1);
        healthCount = transform.GetChild(2);
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        stamina.GetChild(1).GetComponent<Image>().fillAmount = pc.stamina;

        bulletCount.GetChild(0).GetComponent<Image>().sprite = numbers[pc.ammoCount];

        healthCount.GetChild(0).gameObject.SetActive(pc.health > 0);
        healthCount.GetChild(1).gameObject.SetActive(pc.health > 1);
        healthCount.GetChild(2).gameObject.SetActive(pc.health > 2);
    }
}
