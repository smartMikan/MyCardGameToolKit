using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oukanu
{
    public abstract class CardType : ScriptableObject
    {

        public string typeName;
        public virtual void OnSetType(CardViz viz)
        {
            Element t = Settings.GetResourcesManager().typeElement;
            CardVizProperty type = viz.GetProperty(t);
            type.text.text = typeName;
        }
    }
}

