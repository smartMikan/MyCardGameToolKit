using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffectList
{
    public List<CardEffect> cardEffects = new List<CardEffect>();
}

public enum PlayerPosition
{
    ground,sky
}

[System.Serializable]
public class CardEffect
{
    public bool isMoveCard;
    public PlayerPosition targetPosition;

    public bool isAttackCard;
    public PlayerPosition targetAttackPosition;
    public int ATKDamage;

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





