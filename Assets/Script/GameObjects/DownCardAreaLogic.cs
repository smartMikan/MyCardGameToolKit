using UnityEngine;
using System.Collections;

namespace Oukanu.Objects
{
    [CreateAssetMenu(menuName = "Areas/DownCardsAreaLogic")]
    public class DownCardAreaLogic : AreaLogic
    {
        public CardVariables card;
        public CardType[] typeArray;
        public ObjectsLogic cardDownLogic;
        public bool dominated;

        public override void Execute()
        {
            if(card.value == null)
            {
                return;
            }

            dominated = domainCard == null ? false : true;
            //dominated = CardGrid.value.childCount > 0 ? true : false;


            if (dominated)
            {
                Debug.Log("this area already has a card" + domainCard.viz.properties[0].text.text);
                return;
            }
            CardInstance c = card.value;

            Debug.Log("Place Card Down");

            
            Settings.MoveCard(c.transform, CardGrid.value,c,Settings.gameManager.currentPlayer.cardBelongsToDic[c], Settings.gameManager.currentPlayer.downCards);

            c.belongsToArea = this;
            c.gameObject.SetActive(true);
            c.currentLogic = cardDownLogic;
            dominated = true;
            domainCard = c;
            c.viz.SetBackward(true);
            //bool canUse = Settings.gameManager.currentPlayer.CanUseCard(c.viz.card);
            //if (canUse)
            //{
            //    if (c.viz.card.cardType == creatureType)
            //    {
            //        Debug.Log("Place Card Down");
            //        Settings.DropCreatureCard(c.transform, Card1Grid.value.transform, c);
            //    }
            //    else if (c.viz.card.cardType == resourceType)
            //    {
            //        Debug.Log("Place Resources Card Down");
            //        Settings.DropResourceCard(c.transform, Card2Grid.value.transform, c);
            //    }



            //    c.gameObject.SetActive(true);
            //    c.currentLogic = cardDownLogic;
            //}


        }
    }
}

