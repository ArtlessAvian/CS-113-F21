using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui_script : MonoBehaviour
{
    Transform stamina;
    Transform bulletCount;
    PlayerController pc;

    public Sprite[] numbers;

    // Start is called before the first frame update
    void Start()
    {
        stamina = transform.GetChild(0);
        bulletCount = transform.GetChild(1);
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        stamina.GetChild(1).GetComponent<Image>().fillAmount = pc.stamina;

        bulletCount.GetChild(0).GetComponent<Image>().sprite = numbers[pc.ammoCount];
        
    }
}
