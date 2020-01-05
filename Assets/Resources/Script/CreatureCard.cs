using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oukanu
{
    [CreateAssetMenu(menuName = "Cards/Creature")]
    public class CreatureCard : CardType
    {
        public override void OnSetType(CardViz viz)
        {
            base.OnSetType(viz);
            viz.statsHouder.SetActive(true);
        }
    }

}
