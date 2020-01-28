using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oukanu
{
    [CreateAssetMenu(menuName = "Holders/Card Holder")]
    public class CardHolder : ScriptableObject
    {
        public SO.TransformVariable handGrid;
        public SO.TransformVariable downGrid;
        public SO.TransformVariable downGrid2;
        public SO.TransformVariable resourcesCardGrid;
        public SO.TransformVariable deckGrid;
        public SO.TransformVariable graveyardGrid;
        public SO.TransformVariable destroyedGrid;
        
        public bool isPlayerOneHolder;

        [System.NonSerialized]
        public Dictionary<List<CardInstance>, SO.TransformVariable> cardListTransDic = new Dictionary<List<CardInstance>, SO.TransformVariable>();




        public void LoadPlayer(PlayerHolder player)
        {
            player.currentHolder = this;

            cardListTransDic.Clear();
            cardListTransDic.Add(player.handCards, handGrid);
            cardListTransDic.Add(player.deckCards, deckGrid);
            cardListTransDic.Add(player.downCards, downGrid);
            cardListTransDic.Add(player.downCards2, downGrid2);
            cardListTransDic.Add(player.graveyardCards, graveyardGrid);
            cardListTransDic.Add(player.destroyedCards, destroyedGrid);



            foreach (CardInstance c in player.downCards)
            {
                Settings.SetParentForCard(c.viz.gameObject.transform, downGrid.value);
                //c.viz.gameObject.transform.SetParent(downGrid.value);
                c.belongsToPlayer = player;
            }
            foreach (CardInstance c in player.downCards2)
            {
                Settings.SetParentForCard(c.viz.gameObject.transform, downGrid2.value);
                //c.viz.gameObject.transform.SetParent(downGrid.value);
                c.belongsToPlayer = player;
            }
            if (isPlayerOneHolder)
            {
                foreach (CardInstance c in player.handCards)
                {
                    Settings.SetParentForCard(c.viz.gameObject.transform, handGrid.value);
                    //c.viz.gameObject.transform.SetParent(handGrid.value);
                    c.viz.SetBackward(false);
                    c.currentLogic = player.handLogic;
                    c.belongsToPlayer = player;
                }
            }
            else
            {

                foreach (CardInstance c in player.handCards)
                {
                    Settings.SetParentForCard(c.viz.gameObject.transform, handGrid.value);
                    //c.viz.gameObject.transform.SetParent(handGrid.value);
                    c.viz.SetBackward(true);
                    c.currentLogic = null;
                    c.belongsToPlayer = player;
                }

            }
            
            foreach (ResourcesHolder c in player.resourcesList)
            {
                Settings.SetParentForCard(c.cardObj.transform, resourcesCardGrid.value);
                //c.cardObj.transform.SetParent(resourcesCardGrid.value);
            }


            foreach (CardInstance c in player.deckCards)
            {
                Settings.SetParentForCard(c.viz.gameObject.transform, deckGrid.value);
                //c.cardObj.transform.SetParent(resourcesCardGrid.value);

                c.viz.SetBackward(true);
                c.belongsToPlayer = player;
            }

            foreach (CardInstance c in player.graveyardCards)
            {
                Settings.SetParentForCard(c.viz.gameObject.transform, graveyardGrid.value);
                //c.cardObj.transform.SetParent(resourcesCardGrid.value);

                c.viz.SetBackward(false);
                c.belongsToPlayer = player;
            }

            foreach (CardInstance c in player.destroyedCards)
            {
                Settings.SetParentForCard(c.viz.gameObject.transform, destroyedGrid.value);
                //c.cardObj.transform.SetParent(resourcesCardGrid.value);

                c.viz.SetBackward(false);
                c.belongsToPlayer = player;
            }
        }

        internal void DrawCard(CardInstance c)
        {
            Settings.SetParentForCard(c.viz.gameObject.transform, handGrid.value);
            if (isPlayerOneHolder)
            {
                c.viz.SetBackward(false);

            }
            else
            {
                c.viz.SetBackward(true);
                c.currentLogic = null;
            }
        }


        internal void Shuffle(List<CardInstance> cards)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                Settings.SetCardOnbeneth(cards[i].viz.gameObject.transform);
            }
        }

        internal void ReLoadGraveyardToDeck(PlayerHolder player)
        {
            foreach (CardInstance c in player.deckCards)
            {
                Settings.SetParentForCard(c.viz.gameObject.transform, deckGrid.value);
                //c.cardObj.transform.SetParent(resourcesCardGrid.value);

                c.viz.SetBackward(true);
            }

        }

        internal void ReLoadHandcardsToGraveyard(PlayerHolder player)
        {

            foreach (CardInstance c in player.graveyardCards)
            {
                Settings.SetParentForCard(c.viz.gameObject.transform, graveyardGrid.value);
                //c.cardObj.transform.SetParent(resourcesCardGrid.value);

                c.viz.SetBackward(false);
            }
        }

        internal void ReLoadDestroyedCard(PlayerHolder player,CardInstance c)
        {
            Settings.SetParentForCard(c.viz.gameObject.transform, destroyedGrid.value);
            c.viz.SetBackward(false);
        }




        public IEnumerator UpdateCardList(List<CardInstance> cardList, float waitTime, List<CardInstance> targetList)
        {
            for (int i = 0; i < cardList.Count; i++)
            {
                CardInstance c = cardList[i];

                Settings.SetParentForCard(c.viz.gameObject.transform, cardListTransDic[targetList].value);
                c.viz.SetBackward(!isPlayerOneHolder);
                yield return new WaitForSeconds(waitTime);

            }
            cardList.Clear();
        }

    }
}