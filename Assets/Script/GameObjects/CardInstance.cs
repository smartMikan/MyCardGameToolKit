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

        public void CardInstanceToGraveyard()
        {
            Debug.Log("card to graveyard");
        }


        public bool CanBeBlocked(CardInstance block)
        {
            bool result = owner.attackingCards.Contains(this);

            if (result && viz.Card.cardType.canAttack)
            {
                result = true;

                //if card has flying that can be block by non-flying,check it here
                //or cases like that should be here


                if (result)
                {
                    Settings.gameManager.AddBlockInstance(this, block);
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


        public void SetFlatfooted(bool isFlat)
        {
            IsFlatfooted = isFlat;
            if (IsFlatfooted)
            {
                viz.SetTargetRotation(new Vector3(0, 0, 90));
            }
            else
            {
                viz.SetTargetRotation(Vector3.zero);
            }
        }

      
    }

}
