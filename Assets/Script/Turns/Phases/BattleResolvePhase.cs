using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oukanu
{

    [CreateAssetMenu(menuName = "Turns/BattleResolvePhase")]
    public class BattleResolvePhase : Phase
    {
        public override bool IsComplete()
        {
            throw new System.NotImplementedException();
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
