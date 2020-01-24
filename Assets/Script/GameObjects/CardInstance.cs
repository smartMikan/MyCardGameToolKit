using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Oukanu
{
    public class CardInstance : MonoBehaviour, IClickable
    {
        public CardViz viz;
        public Objects.ObjectsLogic currentLogic;



        private void Start()
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
    }

}
