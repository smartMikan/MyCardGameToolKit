using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Oukanu
{
    [CreateAssetMenu(menuName = "Holders/Player Holder")]
    public class PlayerHolder : ScriptableObject
    {
        public string username;
        public Color playerColor;
        public string[] startingCards;

        public bool isHumanPlayer = false;
        [System.NonSerialized]
        public CardHolder currentHolder;


        public Objects.ObjectsLogic handLogic;
        public Objects.ObjectsLogic downLogic;

       
        [System.NonSerialized]
        public List<CardInstance> handCards = new List<CardInstance>();
        [System.NonSerialized]
        public List<CardInstance> downCards = new List<CardInstance>();
         [System.NonSerialized]
        public List<ResourcesHolder> resourcesList = new List<ResourcesHolder>();
        
        public int resourcesDroppablePerTurn = 1;
        [System.NonSerialized]
        public int resourcesDropedThisTurn;



        public int ResourceCount { get { return currentHolder.resourcesCardGrid.value.GetComponentsInChildren<CardViz>().Length; } }

        public void AddResourceCard(GameObject cardObject)
        {
            ResourcesHolder resourcesHolder = new ResourcesHolder
            {
                cardObj = cardObject
            };
            resourcesList.Add(resourcesHolder);
        }


        public int NonUsedCard()
        {
            int result = 0;
            for (int i = 0; i < resourcesList.Count; i++)
            {
                if (!resourcesList[i].isUsed)
                {
                    result++;
                }
            }
            return result;
        }

        public bool CanUseCard(Card c)
        {
            bool result = false;
            if(c.cardType is CreatureCard || c.cardType is SpellCard)
            {
                int currentResources = NonUsedCard();

                if (c.cost <= currentResources)
                {
                    result = true;
                }
                else
                {
                    Settings.RegisterEvent(username + "Dont have enough Resources to Drop " + c.name + " for cost of "+ c.cost.ToString() , Color.red);
                }
            }
            if(c.cardType is ResourceCard)
            {
                if(resourcesDropedThisTurn < resourcesDroppablePerTurn)
                {
                    result = true;
                }
                else
                {
                    Settings.RegisterEvent("Cant Drop More Resource Cards " + resourcesDropedThisTurn.ToString() + "/" + resourcesDroppablePerTurn.ToString(), Color.red);
                }
            }
            return result;
        }

        public List<ResourcesHolder> GetUnusedResources()
        {
            List<ResourcesHolder> resources = new List<ResourcesHolder>();
            for (int i = 0; i < resourcesList.Count; i++)
            {
                if (!resourcesList[i].isUsed)
                {
                    resources.Add(resourcesList[i]);
                }
            }
            return resources;
        }


        public void MakeAllResourcesUsable()
        {
            for (int i = 0; i < resourcesList.Count; i++)
            {
                resourcesList[i].isUsed = false;
                resourcesList[i].cardObj.transform.localEulerAngles = Vector3.zero;
            }
            resourcesDropedThisTurn = 0;
        }

        public void UseResourceCards(int amount)
        {
            Vector3 euler = new Vector3(0, 0, 90f);

            for (int i = 0; i < amount; i++)
            {
                if (i >= resourcesList.Count)
                {
                    //Should Never Get There;
                    Debug.LogError("OverLoad Resource!");
                    break;
                }
                if(resourcesList[i].isUsed)
                {
                    amount++;
                    continue;
                }
                resourcesList[i].isUsed = true;
                resourcesList[i].cardObj.transform.localEulerAngles = euler;
            }
        }
        public void DropResourceCard(CardInstance cardInst)
        {
            if (handCards.Contains(cardInst)) 
            {
                handCards.Remove(cardInst);
            }

            resourcesDropedThisTurn++;
            Settings.RegisterEvent(username + " Dropped " + cardInst.viz.card.name, Color.white);
        }


        public void DropCreatureCard(CardInstance cardInst)
        {
            if (handCards.Contains(cardInst))
            {
                handCards.Remove(cardInst);
            }

            downCards.Add(cardInst);
            Settings.RegisterEvent(username + " Dropped " + cardInst.viz.card.name + " for " + cardInst.viz.card.cost + " resources", Color.white);
        }


        private void DropCard(CardInstance cardInst)
        {
            if (handCards.Contains(cardInst))
            {
                handCards.Remove(cardInst);
            }
        }

    }

}