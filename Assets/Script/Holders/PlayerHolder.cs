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
        public int drawAmountPerTurn;

        public Objects.ObjectsLogic handLogic;
        public Objects.ObjectsLogic downLogic;


        [System.NonSerialized]
        public List<CardInstance> deckCards = new List<CardInstance>();
        [System.NonSerialized]
        public List<CardInstance> handCards = new List<CardInstance>();
        [System.NonSerialized]
        public List<CardInstance> downCards = new List<CardInstance>();
        [System.NonSerialized]
        public List<CardInstance> attackingCards = new List<CardInstance>();

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


        public void ResetAllFlatFootedCards()
        {
            foreach (CardInstance c in downCards)
            {
                if (c.isFlatfooted)
                {
                    c.isFlatfooted = false;
                    c.transform.localEulerAngles = Vector3.zero;
                }
            }
        }


        public bool DrawCard(int cardAmounts)
        {
            for (int i = 0; i < cardAmounts; i++)
            {
                if (deckCards.Count > 0)
                {
                    CardInstance c = deckCards[deckCards.Count - 1];
                    deckCards.Remove(c);

                    handCards.Add(c);

                    c.currentLogic = handLogic;

                    currentHolder.DrawCard(c);
                }
                else
                {
                    return false;
                }

            }
            return true;
        }

        public bool DrawCard()
        {
            return DrawCard(drawAmountPerTurn);
        }


        public void Shuffle(List<CardInstance> cards)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                int a = Random.Range(0, cards.Count - 1);
                int b = Random.Range(0, cards.Count - 1);
                Swap(cards, a, b);
            }
            currentHolder.Shuffle(cards);
        }

        public void Shuffle()
        {
            Shuffle(deckCards);
        }


        public void Swap(List<CardInstance> cards, int a, int b)
        {
            if(a>cards.Count - 1 || b > cards.Count - 1)
            {
                throw new System.IndexOutOfRangeException();
            }
            else
            {
                CardInstance tempA = cards[a];
                CardInstance tempB = cards[b];
                cards[a] = tempB;
                cards[b] = tempA;
            }
        }

        public void Swap(List<CardInstance> cards, CardInstance a, CardInstance b)
        {
            if (cards.Find(t => t = a) && cards.Find(t => t = b))
            {
                int tempA = cards.IndexOf(a);
                int tempB = cards.IndexOf(b);
                cards[tempA] = b;
                cards[tempB] = a;
            }
            else
            {
                throw new System.MissingMemberException();
            }
        }
    }

}