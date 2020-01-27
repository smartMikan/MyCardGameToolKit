using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oukanu.Objects
{
    [CreateAssetMenu(menuName = "Game Objects/CardDown")]
    public class CardDown : ObjectsLogic
    {

        public SO.GameEvent onCurrentCardSelected;
        public CardVariables currentCard;
        public GameStates.State holdingCard;


        public override void OnClick(CardInstance c)
        {
            Debug.Log("This Card is on the table");
            currentCard.Set(c);
            currentCard.value.viz.SetHighight(false);
            Settings.gameManager.SetState(holdingCard);
            onCurrentCardSelected.Raise();
            if (c.belongsToArea != null)
            {
                c.belongsToArea.domainCard = null;
            }
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