using UnityEngine;
using System.Collections;


namespace Oukanu
{
    [CreateAssetMenu(menuName = "Turns/ResetResourcesCardPhase")]
    public class ResetResourcesCardPhase : Phase
    {
        public override bool IsComplete()
        {
            Settings.gameManager.currentPlayer.MakeAllResourcesUsable();
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
                Debug.Log(this.name + " Start");
                Settings.gameManager.SetState(null);
                Settings.gameManager.onPhaseChanged.Raise();
                isInit = true;
            }
        }
    }
}

