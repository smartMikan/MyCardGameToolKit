using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Oukanu
{
    [CreateAssetMenu(menuName = "Holders/Player Holder")]
    public class PlayerHolder : ScriptableObject
    {
        public string username;
        public Sprite potrait;
        public Color playerColor;

        [System.NonSerialized]
        public int health = 20;

        [System.NonSerialized]
        public PlayerStatsUI statsUI;


        public List<string> CurrentDeck = new List<string>();


        public bool isHumanPlayer = false;
        [System.NonSerialized]
        public CardHolder currentHolder;
        public int drawAmountPerTurn;

        public Objects.ObjectsLogic handLogic;

        //TODO: should defined later
        public Objects.ObjectsLogic deckLogic;
        public Objects.ObjectsLogic attackingLogic;
        public Objects.ObjectsLogic downLogic;
        public Objects.ObjectsLogic graveyardLogic;
        public Objects.ObjectsLogic resourcesLogic;
        //

        #region Runtime Card Data Holder

        [System.NonSerialized]
        public List<CardInstance> deckCards = new List<CardInstance>();
        [System.NonSerialized]
        public List<CardInstance> handCards = new List<CardInstance>();
        [System.NonSerialized]
        public List<CardInstance> downCards = new List<CardInstance>();
        [System.NonSerialized]
        public List<CardInstance> attackingCards = new List<CardInstance>();
        [System.NonSerialized]
        public List<CardInstance> graveyardCards = new List<CardInstance>();
        [System.NonSerialized]
        public List<CardInstance> resourcesList = new List<CardInstance>();

        #endregion

        internal void MoveCardFromHolder(CardInstance card, List<CardInstance> from,List<CardInstance> to)
        {
            if (from.Contains(card))
            {
                from.Remove(card);
                to.Add(card);
                card.currentBelongs = to;
            }
            else
            {
                Debug.LogError("from holder doesn't contain this card");
            }
        }

        public void MoveCardToGraveyard(CardInstance card, List<CardInstance> from)
        {
            MoveCardFromHolder(card, from, graveyardCards);
            currentHolder.SetCardToGraveyard(card);
        }


        //can drop how mush resources per turn
        public int resourcesDroppablePerTurn = 1;
        [System.NonSerialized]
        public int resourcesDropedThisTurn;


        public void Init()
        {
            health = 20;
        }

        public int ResourceCount { get { return currentHolder.resourcesCardGrid.value.GetComponentsInChildren<CardViz>().Length; } }



        #region Resources

        public int NonUsedResourceCard()
        {
            int result = 0;
            for (int i = 0; i < resourcesList.Count; i++)
            {
                if (!resourcesList[i].IsFlatfooted)
                {
                    result++;
                }
            }
            return result;
        }

        public bool CanUseCard(Card c)
        {
            bool result = false;
            if (c.cardType is CreatureCard || c.cardType is SpellCard)
            {
                int currentResources = NonUsedResourceCard();

                if (c.cost <= currentResources)
                {
                    result = true;
                }
                else
                {
                    Settings.RegisterEvent(username + "Dont have enough Resources to Drop " + c.name + " for cost of " + c.cost.ToString(), Color.red);
                }
            }
            if (c.cardType is ResourceCard)
            {
                if (resourcesDropedThisTurn < resourcesDroppablePerTurn)
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

        public List<CardInstance> GetUnusedResourcesInst()
        {
            List<CardInstance> resources = new List<CardInstance>();
            for (int i = 0; i < resourcesList.Count; i++)
            {
                if (!resourcesList[i].IsFlatfooted)
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
                resourcesList[i].SetFlatfooted(false);
            }
            resourcesDropedThisTurn = 0;
        }

        public void UseResourceCards(int amount)
        {

            for (int i = 0; i < amount; i++)
            {
                if (i >= resourcesList.Count)
                {
                    //Should Never Get There;
                    Debug.LogError("OverLoad Resource!");
                    break;
                }
                if (resourcesList[i].IsFlatfooted)
                {
                    amount++;
                    continue;
                }
                resourcesList[i].SetFlatfooted(true);
            }
        }

        public void DropResourceCard(CardInstance cardInst, bool registerEvent = true)
        {
            MoveCardFromHolder(cardInst, handCards, resourcesList);
            resourcesDropedThisTurn++;
            currentHolder.SetCardToResourcesHolder(cardInst);
            if (registerEvent)
            {
                Settings.RegisterEvent(username + " Dropped " + cardInst.viz.Card.name, Color.white);
            }
            cardInst.currentLogic = downLogic;
        }

        public void DropCreatureCard(CardInstance cardInst,bool registerEvent = true)
        {
            MoveCardFromHolder(cardInst, handCards, downCards);
            currentHolder.SetCardDown(cardInst);

            if (registerEvent)
            {
                Settings.RegisterEvent(username + " Dropped " + cardInst.viz.Card.name + " for " + cardInst.viz.Card.cost + " resources", Color.white);
            }
            cardInst.currentLogic = downLogic;
        }


        #endregion

        public void ResetAllFlatFootedCards()
        {
            foreach (CardInstance c in downCards)
            {
                if (c.IsFlatfooted)
                {
                    c.SetFlatfooted(false);
                }
            }
        }


        #region DrawCard
        public bool DrawCard(int cardAmounts)
        {
            for (int i = 0; i < cardAmounts; i++)
            {
                if (deckCards.Count > 0)
                {
                    CardInstance c = deckCards[deckCards.Count - 1];
                    MoveCardFromHolder(c, deckCards, handCards);

                    currentHolder.SetCardToHand(c);
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
        #endregion


        #region Shuffle
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
            if (a > cards.Count - 1 || b > cards.Count - 1)
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


        #endregion


        //Should Be called after assign A stats UI
        public void LoadPlayerOnStatsUI()
        {
            if (statsUI!=null)
            {
                statsUI.player = this;
                statsUI.UpdateAll();
            }
        }

        public void DoDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                health = 0;
            }
            if (statsUI != null)
            {
                statsUI.UpdateHealth();
            }
        }

    }

}