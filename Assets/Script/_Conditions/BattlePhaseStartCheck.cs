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
            GameManager gm = Settings.gameManager;
            PlayerHolder p = gm.CurrentPlayer;

            int count = p.downCards.Count;
            for (int i = 0; i < p.downCards.Count; i++)
            {
                if (p.downCards[i].IsFlatfooted)
                {
                    count--;
                }
            }

            if (count>0)
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