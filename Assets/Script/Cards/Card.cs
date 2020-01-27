using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

enum CardPhase
{
    check, apply
}

enum CardState
{
    deck,hand,destroy,armor
}

namespace Oukanu
{
    [CreateAssetMenu(menuName = "Card")]
    public class Card : ScriptableObject
    {
        public CardType cardType;
        public int cost;
        public CardProperty[] properties;
        public CardEffect cardEffect;
        public int cardSpeed;

    }
}


