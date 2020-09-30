using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oukanu
{
    [CreateAssetMenu(menuName = "Holders/Card Holder")]
    public class CardHolder : ScriptableObject
    {
        public Variable.TransformVariable handGrid;
        public Variable.TransformVariable downGrid;
        public Variable.TransformVariable resourcesCardGrid;
        public Variable.TransformVariable deckGrid;
        public Variable.TransformVariable battleLine;
        public Variable.TransformVariable graveyard;

        public bool isP1Holder;

        [System.NonSerialized]
        public PlayerHolder possessedPlayer;

        public void SetCardOnBattleLine(CardInstance card)
        {
            //Vector3 position = card.viz.gameObject.transform.position;

            Settings.SetParentForCard(card.viz.gameObject.transform, battleLine.value);
            //not change x value
            //position.z = card.viz.gameObject.transform.position.z;
            //position.y = card.viz.gameObject.transform.position.y;
            //card.viz.gameObject.transform.position = position;

        }
        public void SetCardDown(CardInstance card)
        {
            Settings.SetParentForCard(card.viz.gameObject.transform, downGrid.value);
            if (card.IsFlatfooted)
            {
                card.SetFlatfooted(true, false);
            }
        }

        public void SetCardToResourcesHolder(CardInstance card)
        {
            Settings.SetParentForCard(card.viz.gameObject.transform, resourcesCardGrid.value);
            if (card.IsFlatfooted)
            {
                card.SetFlatfooted(true, false);
            }
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
                c.viz.SetBackward(true);
                //should be Enemy card hand logic
                c.currentLogic = null;
            }
        }

        public void SetCardToDeck(CardInstance card)
        {
            Settings.SetParentForCard(card.viz.gameObject.transform, deckGrid.value);
            card.viz.SetBackward(true);
        }

        public void SetCardToGraveyard(CardInstance card)
        {
            Settings.SetParentForCard(card.viz.gameObject.transform, graveyard.value);
            card.viz.SetBackward(false);
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
                SetCardDown(c);
            }
            foreach (CardInstance c in player.handCards)
            {
                SetCardToHand(c);
            }
            foreach (CardInstance c in player.resourcesList)
            {
                Settings.SetParentForCard(c.viz.gameObject.transform, resourcesCardGrid.value);
            }
            foreach (CardInstance c in player.deckCards)
            {
                SetCardToDeck(c);
            }
            foreach (CardInstance c in player.attackingCards)
            {
                SetCardOnBattleLine(c);
            }
            foreach (CardInstance c in player.graveyardCards)
            {
                SetCardToGraveyard(c);
            }


            player.statsUI = statsUI;
            player.LoadPlayerOnStatsUI();
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