using UnityEngine;
using System.Collections;


namespace Oukanu
{
    [CreateAssetMenu(menuName = "Turns/PlayerControlPhase")]
    public class PlayerControlPhase : Phase
    {

        public GameStates.State playerControlState;

        public override bool IsComplete()
        {
            if(forceExit)
            {
                forceExit = false;
                return true;
            }
            return false;
        }


        public override void OnStartPhase()
        {
            if (!isInit)
            {
                Debug.Log(this.name + " Start");
                Settings.gameManager.SetState(playerControlState);
                Settings.gameManager.onPhaseChanged.Raise();
                isInit = true;
            }
        }


        public override void OnEndPhase()
        {
            if (isInit)
            {
                Settings.gameManager.SetState(null);
                isInit = false;
            }
        }

       
    }

}