using System;
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
        //public int drawAmountPerTurn;

        public Objects.ObjectsLogic handLogic;
        public Objects.ObjectsLogic downLogic;

        [System.NonSerialized]
        public bool isFlying;

        public int Dexterity;


        public PlayerHolder enemy;
        //public GameStates.State DamagedState;

        public GameObject damageText;

        [System.NonSerialized]
        public bool died = false;

        //public int damageleft;
        [System.NonSerialized]
        public PlayerStatus mystatus;

        [System.NonSerialized]
        public Dictionary<CardInstance, List<CardInstance>> cardBelongsToDic = new Dictionary<CardInstance, List<CardInstance>>();


        [System.NonSerialized]
        public List<CardInstance> deckCards = new List<CardInstance>();
        [System.NonSerialized]
        public List<CardInstance> handCards = new List<CardInstance>();
        [System.NonSerialized]
        public List<CardInstance> downCards = new List<CardInstance>();
        [System.NonSerialized]
        public List<CardInstance> downCards2 = new List<CardInstance>();
        [System.NonSerialized]
        public List<CardInstance> graveyardCards = new List<CardInstance>();
        [System.NonSerialized]
        public List<CardInstance> destroyedCards = new List<CardInstance>();

        [System.NonSerialized]
        public List<CardInstance> attackingCards = new List<CardInstance>();

        [System.NonSerialized]
        public List<CardInstance> drawingCards = new List<CardInstance>();

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

        

        internal void MoveCard(CardInstance card,List<CardInstance> from,List<CardInstance> to)
        {
            to.Add(card);
            from.Remove(card);
            if (cardBelongsToDic.ContainsKey(card))
            {
                cardBelongsToDic[card] = to;
            }
            else
            {
                cardBelongsToDic.Add(card, to);
            }
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
            MoveCard(cardInst, handCards, downCards);
            Settings.RegisterEvent(username + " Dropped " + cardInst.viz.card.name + " for " + cardInst.viz.card.cost + " resources", Color.white);
        }


        private void DropCard(CardInstance cardInst)
        {
            MoveCard(cardInst, handCards, downCards);
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
                    MoveCard(c, deckCards, handCards);

                    c.currentLogic = handLogic;
                    drawingCards.Add(c);
                    //currentHolder.DrawCard(c);
                }
                else
                {
                    if (graveyardCards.Count > 0)
                    {
                        ResetGraveyardToDeck();
                        DrawCard(cardAmounts - i);
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            return true;
        }

        private void ResetGraveyardToDeck()
        {
            while (graveyardCards.Count>0)
            {
                int i = graveyardCards.Count - 1;
                MoveCard(graveyardCards[i], graveyardCards, deckCards);
            }

            currentHolder.ReLoadGraveyardToDeck(this);

            Shuffle(deckCards);
            currentHolder.Shuffle(deckCards);
        }

        public bool DrawCard()
        {
            return DrawCard(Dexterity);
        }

        public void AbandonHandCard()
        {
            while(handCards.Count>0)
            {
                int i = handCards.Count - 1;
                handCards[i].currentLogic = null;
                MoveCard(handCards[i], handCards, graveyardCards);
            }
            currentHolder.ReLoadHandcardsToGraveyard(this);
        }
        public void Shuffle(List<CardInstance> cards)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                int a = UnityEngine.Random.Range(0, cards.Count - 1);
                int b = UnityEngine.Random.Range(0, cards.Count - 1);
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



        public void LoadAllCardToDic()
        {

            cardBelongsToDic.Clear();

            foreach (CardInstance c in handCards)
            {
                cardBelongsToDic.Add(c, handCards);
            }

            foreach (CardInstance c in downCards)
            {
                cardBelongsToDic.Add(c, downCards);
            }
            foreach (CardInstance c in downCards2)
            {
                cardBelongsToDic.Add(c, downCards);
            }
            foreach (CardInstance c in graveyardCards)
            {
                cardBelongsToDic.Add(c, graveyardCards);
            }

            foreach (CardInstance c in destroyedCards)
            {
                cardBelongsToDic.Add(c, destroyedCards);
            }

            
        }

        public int GetHp()
        {
            return handCards.Count + deckCards.Count + downCards.Count + downCards2.Count + graveyardCards.Count;
        }

        internal bool ChangeState(PlayerPosition targetPosition)
        {
            switch (targetPosition)
            {
                case PlayerPosition.ground:
                    isFlying = false;
                    break;
                case PlayerPosition.sky:
                    isFlying = true;
                    break;
                default:
                    break;
            }
            return true;
        }


        internal bool Attack(int atkDamage, PlayerPosition targetatkPosition)
        {
            switch (targetatkPosition)
            {
                case PlayerPosition.ground:
                    if (enemy.isFlying)
                    {
                        enemy.Missed();
                    }
                    else
                    {
                        enemy.Damaged(atkDamage,this);
                    }
                    break;
                case PlayerPosition.sky:
                    if (enemy.isFlying)
                    {
                        enemy.Damaged(atkDamage,this);
                    }
                    else
                    {
                        enemy.Missed();
                        Oukanu.Settings.RegisterEvent(username + "の攻撃が外れた!" , Color.white);

                    }
                    break;
                default:
                    break;
            }

            return true;

        }

        private void Missed()
        {
            GameObject go = Instantiate(damageText,mystatus.transform);
            go.GetComponent<DamageText>().SetMiss();
            Destroy(go, 3f);


        }

        public void Damaged(int atkDamage,PlayerHolder fromPlayer)
        {
            
            GameObject go = Instantiate(damageText, mystatus.transform);
            go.GetComponent<DamageText>().SetDamage(atkDamage,this,fromPlayer);

            Destroy(go, 3f);
            //Settings.gameManager.SetState(DamagedState);
            //damageleft = atkDamage;

            for (int i = 0; i < atkDamage; i++)
            {
                if (died)
                {
                    break;
                }

                if (deckCards.Count>0)
                {
                    CardInstance c = deckCards[deckCards.Count - 1];
                    MoveCard(c, deckCards, destroyedCards);
                    currentHolder.ReLoadDestroyedCard(this, c);
                }
                else if (graveyardCards.Count > 0)
                {
                    CardInstance c = graveyardCards[graveyardCards.Count - 1];
                    MoveCard(c, graveyardCards, destroyedCards);
                    currentHolder.ReLoadDestroyedCard(this, c);
                }
                else if (handCards.Count > 0)
                {
                    CardInstance c = handCards[handCards.Count - 1];
                    MoveCard(c, handCards, destroyedCards);
                    currentHolder.ReLoadDestroyedCard(this, c);
                }
                else if (downCards2.Count > 0)
                {
                    CardInstance c = downCards2[downCards2.Count - 1];
                    MoveCard(c, downCards2, destroyedCards);
                    currentHolder.ReLoadDestroyedCard(this, c);
                }
                else if (downCards.Count > 0)
                {
                    CardInstance c = downCards[downCards.Count - 1];
                    MoveCard(c, downCards, destroyedCards);
                    currentHolder.ReLoadDestroyedCard(this, c);
                }
                else
                {
                    died = true;
                    break;
                }
            }


        }

    }

}