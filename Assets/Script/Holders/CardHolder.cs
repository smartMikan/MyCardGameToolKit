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


        public void LoadPlayer(PlayerHolder player)
        {
            foreach (CardInstance c in player.downCards)
            {
                Settings.SetParentForCard(c.viz.gameObject.transform, downGrid.value);
                //c.viz.gameObject.transform.SetParent(downGrid.value);

            }
            foreach (CardInstance c in player.handCards)
            {
                Settings.SetParentForCard(c.viz.gameObject.transform, handGrid.value);
                //c.viz.gameObject.transform.SetParent(handGrid.value);

            }
            foreach (ResourcesHolder c in player.resourcesList)
            {
                Settings.SetParentForCard(c.cardObj.transform, resourcesCardGrid.value);
                //c.cardObj.transform.SetParent(resourcesCardGrid.value);
            }
        }

    }
}