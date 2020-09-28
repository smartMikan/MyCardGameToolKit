using UnityEngine;
using System.Collections;


namespace Oukanu
{
    //can be a player action
    [CreateAssetMenu(menuName = "Turns/ResetResourcesCardPhase")]
    public class ResetResourcesCardPhase : Phase
    {
        public override bool IsComplete()
        {


            if (forceExit)
            {
                forceExit = false;
                return true;
            }
            return false;
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

                Settings.gameManager.CurrentPlayer.MakeAllResourcesUsable();

                forceExit = true;
            }
        }
    }
}

