using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffectList
{
    public List<CardEffect> cardEffects = new List<CardEffect>();
}



public class CardEffect
{
    public bool IsInstance { get; private set; }
}



public class AttackEffect : CardEffect
{

}

public class DefenceEffect : CardEffect
{

}

public class StatusChangeEffect : CardEffect
{

}





