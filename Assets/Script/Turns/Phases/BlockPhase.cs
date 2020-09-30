using UnityEngine;
using System.Collections;


namespace Oukanu
{
    [CreateAssetMenu(menuName = "Turns/BlockPhase")]
    public class BlockPhase : Phase
    {

        public GameStates.PlayerActions ChangeActivePlayer;
        public GameStates.State playerControlState;

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
                GameManager gm = Settings.gameManager;

                Debug.Log(this.name + " Start");
                gm.SetState(playerControlState);
                gm.onPhaseChanged.Raise();
                isInit = true;

                if (gm.CurrentPlayer.attackingCards.Count == 0)
                {
                    forceExit = true;
                    return;
                }



                if (gm.P2CardHolder.possessedPlayer.isHumanPlayer)
                {
                    //current player not changed, active player changed
                    gm.LoadPlayerOnActive(gm.P2CardHolder.possessedPlayer);
                }
                else
                {
                    //ai block phase
                }
                //if (ChangeActivePlayer != null)
                //{
                //    ChangeActivePlayer.Excute(gm.CurrentPlayer);
                //}

                
            }

           
        }


       

    }
}

