
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Oukanu
{
    public class CurrentSelected : MonoBehaviour
    {
        public Variable.CardVariables currentCard;
        public CardViz cardViz;


        Transform mTrans;

        public void LoadCard()
        {
            if(currentCard == null)
            {
                return;
            }


            currentCard.value.gameObject.SetActive(false);
            cardViz.LoadCard(currentCard.value.viz.Card);
            cardViz.gameObject.SetActive(true);
        }

        public void CloseCard()
        {
            cardViz.gameObject.SetActive(false);
        }

        private void Start()
        {
            mTrans = this.transform;
            CloseCard();
        }

        private void Update()
        {
            mTrans.position = Input.mousePosition;
        }

    }
}

