using UnityEngine;
using System.Collections;


namespace Oukanu
{
    [CreateAssetMenu(menuName = "Turns/BattlePhase")]
    public class BattlePhase : Phase
    {
        //current actions to be excuted
        public GameStates.State BattlePhaseControlState;
        public GameStates.Condition isBattleValid;

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
                //if have no battle cards then forceExit will be true
                forceExit = !isBattleValid.IsValid();
                //if force exit no state will be set, else set battle phase control state
                Settings.gameManager.SetState((forceExit) ? null : BattlePhaseControlState);
                Settings.gameManager.onPhaseChanged.Raise();
                isInit = true;
            }
        }
    }
}

