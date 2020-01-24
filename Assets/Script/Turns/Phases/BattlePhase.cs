using UnityEngine;
using System.Collections;


namespace Oukanu
{
    [CreateAssetMenu(menuName = "Turns/BattlePhase")]
    public class BattlePhase : Phase
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

