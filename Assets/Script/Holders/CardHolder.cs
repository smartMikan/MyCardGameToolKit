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
        public SO.TransformVariable battleLine;

        public bool isP1Holder;

        [System.NonSerialized]
        public PlayerHolder possessedPlayer;

        public void SetCardOnBattleLine(CardInstance card)
        {
            Vector3 position = card.viz.gameObject.transform.position;

            Settings.SetParentForCard(card.viz.gameObject.transform, battleLine.value);
            //not change x value
            position.z = card.viz.gameObject.transform.position.z;
            position.y = card.viz.gameObject.transform.position.y;
            card.viz.gameObject.transform.position = position;

        }
        public void SetCardDown(CardInstance card)
        {
            Settings.SetParentForCard(card.viz.gameObject.transform, downGrid.value);
        }

        public void LoadPlayer(PlayerHolder player,PlayerStatsUI statsUI)
        {
            if (player==null)
            {
                Debug.LogError("try to load a player that is null, please check any ref that call loadplayer");
                return;
            }

            player.currentHolder = this;
            possessedPlayer = player;

            foreach (CardInstance c in player.downCards)
            {
                Settings.SetParentForCard(c.viz.gameObject.transform, downGrid.value);
                if (c.IsFlatfooted)
                {
                    c.SetFlatfooted(true);
                }
            }

            
            foreach (CardInstance c in player.handCards)
            {
                Settings.SetParentForCard(c.viz.gameObject.transform, handGrid.value);
                if (isP1Holder)
                {
                    c.viz.SetBackward(false);
                    c.currentLogic = player.handLogic;
                }
                else
                {
                    c.viz.SetBackward(true);
                    //should be Enemy card hand logic
                    c.currentLogic = null;
                }
            }
            
            foreach (ResourcesHolder c in player.resourcesList)
            {
                Settings.SetParentForCard(c.cardObj.transform, resourcesCardGrid.value);
            }


            foreach (CardInstance c in player.deckCards)
            {
                Settings.SetParentForCard(c.viz.gameObject.transform, deckGrid.value);
                c.viz.SetBackward(true);
            }


            player.statsUI = statsUI;
            player.LoadPlayerOnStatsUI();
        }

        internal void SetCardToHand(CardInstance c)
        {
            Settings.SetParentForCard(c.viz.gameObject.transform, handGrid.value);
            if (isP1Holder)
            {
                c.viz.SetBackward(false);
                c.currentLogic = possessedPlayer.handLogic;
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