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
        [System.NonSerialized]
        public int instId;
        [System.NonSerialized]
        public CardViz cardViz;


        public CardType cardType;
        public int cost;
        public CardProperty[] properties;


        public CardProperty GetProperty(Element definition)
        {
            for (int i = 0; i < properties.Length; i++)
            {
                if (properties[i].element == definition)
                {
                    return properties[i];
                }
            }

            return null;
        }
    }
}


