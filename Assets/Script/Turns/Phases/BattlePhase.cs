using UnityEngine;
using System.Collections;


namespace Oukanu
{
    [CreateAssetMenu(menuName = "Turns/BattlePhase")]
    public class BattlePhase : Phase
    {

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
                forceExit = !isBattleValid.IsValid();
                Settings.gameManager.SetState((!forceExit) ? BattlePhaseControlState: null);
                Settings.gameManager.onPhaseChanged.Raise();
                isInit = true;
            }
        }
    }
}

