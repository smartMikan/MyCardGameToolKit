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
        Destroy(this, 10f);
    }

    internal void SetMiss()
    {
        text.text = "Miss!";
    }

    internal void SetDamage(int atkDamage)
    {
        text.text = atkDamage.ToString();
    }
}
