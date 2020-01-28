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
    bool Endgame = false;

    // Start is called before the first frame update
    void Start()
    {
        if (player)
        {
            player.mystatus = this;
        }
        Endgame = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Endgame) return;
        Hp = player.GetHp(); 
        HpUI.text = Hp.ToString();

        SkyBackground.SetActive(player.isFlying);
        SkyStatus.SetActive(player.isFlying);

        if (Hp <= 0)
        {
            Oukanu.Settings.gameManager.EndGame(player,player.enemy);
            Endgame = true;
        }

    }
}
