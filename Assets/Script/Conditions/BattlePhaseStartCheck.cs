using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Oukanu.GameStates
{

    [CreateAssetMenu(menuName = "Conditions/BattlePhaseStartCheck")]
    public class BattlePhaseStartCheck : Condition
    {
        
        public override bool IsValid()
        {
            GameManager gm = GameManager.Instance;
            if (gm.currentPlayer.downCards.Count > 0)
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