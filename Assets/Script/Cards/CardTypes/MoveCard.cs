using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oukanu
{
    [CreateAssetMenu(menuName = "CardTypes/Move")]
    public class MoveCard : CardType
    {
        public override void OnSetType(CardViz viz)
        {
            base.OnSetType(viz);
            viz.statsHolder.SetActive(false);
            viz.resourceHolder.SetActive(false);
        }
    }

}
