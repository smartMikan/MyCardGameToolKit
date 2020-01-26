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
            PlayerHolder p = gm.currentPlayer;


            for (int i = 0; i < p.downCards.Count; i++)
            {
                if (p.downCards[i].isFlatfooted == false)
                {
                    return true;
                }
                    
            }
            return false;
        }
    }
}