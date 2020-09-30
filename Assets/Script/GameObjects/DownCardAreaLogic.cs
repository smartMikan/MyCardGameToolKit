using UnityEngine;
using System.Collections;

namespace Oukanu.Objects
{
    [CreateAssetMenu(menuName = "Areas/DownCardsAreaLogic")]
    public class DownCardAreaLogic : AreaLogic
    {
        public Variable.CardVariables currentSelectedCard;
        public CardType creatureType;
        public CardType resourceType;
        public Variable.TransformVariable creatureAreaGrid;
        public Variable.TransformVariable resourceAreaGrid;

        

        public override void Execute()
        {
            if(currentSelectedCard.Value == null)
            {
                return;
            }

            CardInstance cardInstance = currentSelectedCard.Value;
            bool canUse = Settings.gameManager.CurrentPlayer.CanUseCard(cardInstance.viz.Card);
            if (canUse)
            {
                if (cardInstance.viz.Card.cardType == creatureType)
                {
                    Debug.Log("Place Card Down");

                    cardInstance.owner.UseResourceCards(cardInstance.viz.Card.cost);
                    cardInstance.owner.DropCreatureCard(cardInstance);
                    cardInstance.SetFlatfooted(true, true);
                }
                else if (cardInstance.viz.Card.cardType == resourceType)
                {
                    Debug.Log("Place Resources Card Down");
                    cardInstance.owner.DropResourceCard(cardInstance);
                }

                cardInstance.gameObject.SetActive(true);

            }

           
        }
    }
}

