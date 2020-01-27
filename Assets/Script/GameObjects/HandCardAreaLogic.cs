using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oukanu.Objects
{
    [CreateAssetMenu(menuName = "Areas/HandCardAreaLogic")]
    public class HandCardAreaLogic : AreaLogic
    {
        public CardVariables card;
        //public SO.TransformVariable CardGrid;
        public ObjectsLogic handCardLogic;

        public override void Execute()
        {
            if (card.value == null)
            {
                return;
            }

            CardInstance c = card.value;

            Settings.MoveCard(c.transform, CardGrid.value, c, Settings.gameManager.currentPlayer.cardBelongsToDic[c], Settings.gameManager.currentPlayer.handCards);

            c.belongsToArea = this;
            c.gameObject.SetActive(true);

            c.currentLogic = handCardLogic;

            domainCard = c;
            c.viz.SetBackward(false);


        }
    }
}

