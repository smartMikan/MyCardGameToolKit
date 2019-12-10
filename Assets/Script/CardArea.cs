using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class CardArea
{
    List<Card> cards = new List<Card>();
    protected List<Card> Cards { get => cards; set => cards = value; }
    public Card LastCard()
    {
        if (Cards.Count == 0)
        {
            return null;
        }
        return Cards[Cards.Count - 1];
    }

    public void AddCard(Card card)
    {
        Cards.Add(card);
    }

    public bool RemoveLast()
    {
        if (Cards.Count == 0)
        {
            return false;
        }
        Cards.Remove(LastCard());
        return true;
    }
}


