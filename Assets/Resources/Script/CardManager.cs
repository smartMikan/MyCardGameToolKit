﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oukanu;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;




    
    public Card CurrentSelectedCard;
    public Card LastSelectedCard;


    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
