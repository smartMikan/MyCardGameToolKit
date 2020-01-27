using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oukanu
{
    [CreateAssetMenu(menuName = "Turns/EndPhase")]
    public class EndPhase : Phase
    {
        public override bool IsComplete()
        {
            foreach (PlayerHolder player in Settings.gameManager.all_Player_Holders)
            {
                player.AbandonHandCard();
            }
            return true;
        }

        public override void OnEndPhase()
        {
            if (isInit)
            {
                Debug.Log(this.name + " End");
                Settings.gameManager.SetState(null);
                isInit = false;
            }
        }

        public override void OnStartPhase()
        {
            if (!isInit)
            {
                Debug.Log(Settings.gameManager.currentPlayer.username + "'s " + this.name + " Start");
                Settings.gameManager.SetState(null);
                Settings.gameManager.onPhaseChanged.Raise();
                isInit = true;
            }
        }
    }

}
