using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oukanu
{
    [CreateAssetMenu(menuName = "Turns/DrawPhase")]
    public class DrawcardPhase : Phase
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
                Debug.Log(Settings.gameManager.CurrentPlayer.username +"'s " + this.name + " Start");
                Settings.gameManager.SetState(null);
                Settings.gameManager.onPhaseChanged.Raise();
                isInit = true;

                bool drawSuccess = Settings.gameManager.CurrentPlayer.DrawCard();
                if (drawSuccess)
                {
                    forceExit = true;
                }
                else
                {
                    //
                    Debug.Log("No cards in deck,press end phase to continue");
                    Settings.RegisterEvent("No cards in deck,press end phase to continue", Color.red);
                    //some effect

                }
            }
        }

    }

}
