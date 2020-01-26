using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Oukanu
{
    public class CardInstance : MonoBehaviour, IClickable
    {
        public CardViz viz;
        public Objects.ObjectsLogic currentLogic;

		public bool isFlatfooted;

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


        public bool CanAttack()
        {
            bool result = false;
            
            if (viz.card.cardType.TypeAllowsForAttack(this))
            {
                result = true;
                if (isFlatfooted)
                {
                    result = false;
                }
            }

            return result;
        }
    }

}
