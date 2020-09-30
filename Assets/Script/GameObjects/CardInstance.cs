using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Oukanu
{
    public class CardInstance : MonoBehaviour, IClickable
    {
        public PlayerHolder owner;

        public CardViz viz;
        public Objects.ObjectsLogic currentLogic;

        public bool IsFlatfooted { get; private set; }
        
        [System.NonSerialized]
        public List<CardInstance> currentBelongs;

        private void Awake()
        {
            viz = GetComponent<CardViz>();
        }

        public void OnClick()
        {
            if (currentLogic == null)
            {
                return;
            }
            currentLogic.OnClick(this);
        }

        public void OnHighlight()
        {
            if (currentLogic == null)
            {
                return;
            }

            //Debug.Log("Highlight");
            currentLogic.OnHighlight(this);
        }

        public void SetOnBattleLine()
        {
            owner.MoveCardFromHolder(this, currentBelongs, owner.attackingCards);
            owner.currentHolder.SetCardOnBattleLine(this);
        }

        public void CardInstanceToGraveyard()
        {
            owner.MoveCardToGraveyard(this, currentBelongs);
            //some special effect when goes to graveyard 
        }

        public void BackToDownGrid()
        {
            owner.MoveCardFromHolder(this, currentBelongs, owner.downCards);
            //
            owner.currentHolder.SetCardDown(this);
        }

        //check if this card is in an attack state and can be block by cardinstance"block"
        public bool CanBeBlocked(CardInstance block,ref int count)
        {
            bool result = owner.attackingCards.Contains(this);

            if (result && viz.Card.cardType.canAttack)
            {
                result = true;

                //if card has flying that can be block by non-flying,check it here
                //or cases like that should be here


                if (result)
                {
                    //can block, create a block instance to handle in resolve phase
                    Settings.gameManager.AddBlockInstance(this, block,ref count);
                }
                return result;
            }
            else
            {
                return false;
            }
        }


        public bool CanAttack()
        {
            bool result = false;
            
            if (viz.Card.cardType.TypeAllowsForAttack(this))
            {
                result = true;
                if (IsFlatfooted)
                {
                    result = false;
                }
            }

            return result;
        }


        public void SetFlatfooted(bool isFlat,bool withanim = false)
        {
            IsFlatfooted = isFlat;
            if (IsFlatfooted)
            {
                if (!withanim)
                {

                    viz.transform.localEulerAngles = new Vector3(0, 0, 90);
                }
                viz.SetTargetRotation(new Vector3(0, 0, 90));
            }
            else
            {
                if (!withanim)
                {
                    viz.transform.localEulerAngles = Vector3.zero;
                }
                viz.SetTargetRotation(Vector3.zero);
            }
        }

      
    }

}
