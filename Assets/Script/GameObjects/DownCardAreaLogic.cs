using UnityEngine;
using System.Collections;

namespace Oukanu.Objects
{
    [CreateAssetMenu(menuName = "Areas/DownCardsAreaLogic")]
    public class DownCardAreaLogic : AreaLogic
    {
        public CardVariables card;
        public CardType creatureType;
        public SO.TransformVariable areaGrid;
        public ObjectsLogic cardDownLogic;

        public override void Execute()
        {
            if(card.value == null)
            {
                return;
            }


            if(card.value.viz.card.cardType == creatureType)
            {
                Debug.Log("Place Card Down");

                Settings.SetParentForCard(card.value.transform, areaGrid.value.transform);
                card.value.currentLogic = cardDownLogic;
                //Place Card Down 
            }
        }
    }
}

