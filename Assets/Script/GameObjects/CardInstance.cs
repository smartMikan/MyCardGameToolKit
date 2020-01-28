using System;
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

        [System.NonSerialized]
        public Objects.AreaLogic belongsToArea;
        
        [System.NonSerialized]
        public PlayerHolder belongsToPlayer;

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

        public IEnumerator TickEffect()
        {
            bool result = false;
            if (viz.card.cardEffect.isMoveCard)
            {
                result = Settings.TickMove(this, viz.card.cardEffect.targetPosition);
                yield return new WaitForSeconds(1f);
            }
            if (viz.card.cardEffect.isAttackCard)
            {
                result = Settings.TickAttack(this, viz.card.cardEffect.ATKDamage, viz.card.cardEffect.targetAttackPosition);
                yield return new WaitForSeconds(2f);
            }

            yield return result;
        }

        //internal bool TickEffect()
        //{
        //    bool result = false;
        //    if (viz.card.cardEffect.isMoveCard)
        //    {
        //        result = Settings.TickMove(this,viz.card.cardEffect.targetPosition);
        //    }
        //    if (viz.card.cardEffect.isAttackCard)
        //    {
        //        result = Settings.TickAttack(this,viz.card.cardEffect.ATKDamage);
        //    }

        //    return result;
           
        //}
    }

}
