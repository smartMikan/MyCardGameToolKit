using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oukanu
{
    public abstract class CardType : ScriptableObject
    {

        public string typeName;
        public bool canAttack;
        
        //public typeLogic logic;

        public virtual void OnSetType(CardViz viz)
        {
            Element t = Settings.GetResourcesManager().typeElement;
            CardVizProperty type = viz.GetProperty(t);
            type.text.text = typeName;
        }

        public bool TypeAllowsForAttack(CardInstance card)
        {
            //
            //e.g. flying tye can attack even if flatgooted
            ////bool r = logic.execute(Inst)->is(inst.isflatfooted)/inst.isfalatfooted = false return true;

            if (canAttack)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

