﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oukanu.Objects
{
    [CreateAssetMenu(menuName ="Game Objects/HandCard")]
    public class HandCard : ObjectsLogic
    {

        public SO.GameEvent onCurrentCardSelected;
        public CardVariables currentCard;
        public GameStates.State holdingCard;
        public HandCardAreaLogic handCardArea;


        public override void OnClick(CardInstance c)
        {
            Debug.Log("this card is in hand");
            currentCard.Set(c);
            currentCard.value.viz.SetHighight(false);
            Settings.gameManager.SetState(holdingCard);
            onCurrentCardSelected.Raise();
            c.belongsToArea = handCardArea;
        }

        public override void OnHighlight(CardInstance c)
        {
            //Vector3 o = Vector3.one * 1.5f;

            //this.transform.localScale = o;
            Settings.gameManager.DeHighlightCurrentCard();
            currentCard.Set(c);
            currentCard.value.viz.SetHighight(true);
        }
    }

}
