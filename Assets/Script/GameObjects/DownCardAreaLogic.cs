﻿using UnityEngine;
using System.Collections;

namespace Oukanu.Objects
{
    [CreateAssetMenu(menuName = "Areas/DownCardsAreaLogic")]
    public class DownCardAreaLogic : AreaLogic
    {
        public Variable.CardVariables card;
        public CardType creatureType;
        public CardType resourceType;
        public SO.TransformVariable creatureAreaGrid;
        public SO.TransformVariable resourceAreaGrid;
        public ObjectsLogic cardDownLogic;

        public override void Execute()
        {
            if(card.value == null)
            {
                return;
            }

            CardInstance c = card.value;
            bool canUse = Settings.gameManager.CurrentPlayer.CanUseCard(c.viz.Card);
            if (canUse)
            {
                if (c.viz.Card.cardType == creatureType)
                {
                    Debug.Log("Place Card Down");
                    Settings.DropCreatureCard(c.transform, creatureAreaGrid.value.transform, c);
                }
                else if (c.viz.Card.cardType == resourceType)
                {
                    Debug.Log("Place Resources Card Down");
                    Settings.DropResourceCard(c.transform, resourceAreaGrid.value.transform, c);
                }


                
                c.gameObject.SetActive(true);
                c.currentLogic = cardDownLogic;
            }

           
        }
    }
}

