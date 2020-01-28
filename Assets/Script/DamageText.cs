using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    public Text text;


    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    internal void SetMiss()
    {
        text.text = "Miss!";
        text.color = Color.yellow;

    }

    internal void SetDamage(int atkDamage, Oukanu.PlayerHolder playerHolder, Oukanu.PlayerHolder fromPlayer)
    {
        text.text = atkDamage.ToString();
        text.color = Color.red;

        Oukanu.Settings.RegisterEvent(playerHolder.username + "が" + atkDamage.ToString() + "のダメージを受けた!("+ fromPlayer.username +")", Color.white);


    }
}
