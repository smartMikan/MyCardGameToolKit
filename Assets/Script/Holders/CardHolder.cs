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
        public SO.TransformVariable resourcesCardGrid;
        public SO.TransformVariable deckGrid;


        public bool isPlayerOneHolder;

        public void LoadPlayer(PlayerHolder player)
        {
            player.currentHolder = this;

            foreach (CardInstance c in player.downCards)
            {
                Settings.SetParentForCard(c.viz.gameObject.transform, downGrid.value);
                //c.viz.gameObject.transform.SetParent(downGrid.value);

            }

            if (isPlayerOneHolder)
            {
                foreach (CardInstance c in player.handCards)
                {
                    Settings.SetParentForCard(c.viz.gameObject.transform, handGrid.value);
                    //c.viz.gameObject.transform.SetParent(handGrid.value);
                    c.viz.SetBackward(false);
                    c.currentLogic = player.handLogic;
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
    }
}