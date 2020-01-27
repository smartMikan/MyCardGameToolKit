using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{

    public Oukanu.PlayerHolder player;
    public Text HpUI;
    public GameObject SkyBackground;
    public GameObject SkyStatus;
    int Hp;


    // Start is called before the first frame update
    void Start()
    {
        if (player)
        {
            player.mystatus = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Hp = player.GetHp(); 
        HpUI.text = Hp.ToString();

        SkyBackground.SetActive(player.isFlying);
        SkyStatus.SetActive(player.isFlying);
    }
}
