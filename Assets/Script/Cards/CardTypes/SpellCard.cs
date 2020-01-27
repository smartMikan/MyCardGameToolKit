using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oukanu
{
    [CreateAssetMenu(menuName = "CardTypes/Spell")]
    public class SpellCard : CardType
    {
        public override void OnSetType(CardViz viz)
        {
            base.OnSetType(viz);
            viz.statsHolder.SetActive(false);
            viz.resourceHolder.SetActive(true);
        }
    }

}
