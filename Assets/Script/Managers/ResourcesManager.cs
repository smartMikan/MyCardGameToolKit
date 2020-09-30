using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oukanu
{
    [CreateAssetMenu(menuName = "Managers/Resource Manager")]
    public class ResourcesManager : ScriptableObject
    {
        public Element typeElement;
        public Card[] allCards;
        Dictionary<string, Card> cardsDict = new Dictionary<string, Card>();

        public void Init()
        {
            cardsDict.Clear();
            for (int i = 0; i < allCards.Length; i++)
            {
                cardsDict.Add(allCards[i].name, allCards[i]);
            }

        }

        public Card GetCardInstance(string cardName)
        {
            Card originalCard = GetCard(cardName);
            if(originalCard == null)
            {
                Debug.LogError("this card name doesn't exist in database!");
                return null;
            }

            Card newInst = Instantiate(originalCard);
            newInst.name = originalCard.name;
            return newInst;
        }


        Card GetCard(string name)
        {
            Card result = null;
            cardsDict.TryGetValue(name, out result);
            return result;
        }
    }

}
