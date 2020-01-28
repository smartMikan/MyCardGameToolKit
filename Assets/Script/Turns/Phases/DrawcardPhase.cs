using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oukanu
{
    [CreateAssetMenu(menuName = "Turns/DrawPhase")]
    public class DrawcardPhase : Phase
    {
        [System.NonSerialized]
        bool isDrawComplete;

        public override bool IsComplete()
        {
            return isDrawComplete;
        }

        public override void OnEndPhase()
        {
            if (isInit)
            {
                Debug.Log(this.name + " End");
                Settings.gameManager.SetState(null);
                isInit = false;
                isDrawComplete = false;
            }
        }

        public override void OnStartPhase()
        {
            if (!isInit)
            {
                Debug.Log(Settings.gameManager.currentPlayer.username +"'s " + this.name + " Start");
                Settings.gameManager.SetState(null);
                Settings.gameManager.onPhaseChanged.Raise();
                isInit = true;
                isDrawComplete = false;
                Settings.gameManager.StartCoroutine(Draw());
            }
        }



        public IEnumerator Draw()
        {
            foreach (PlayerHolder player in Settings.gameManager.all_Player_Holders)
            {
                player.DrawCard();
            }
            yield return new WaitForSeconds(0.2f);
            foreach (PlayerHolder player in Settings.gameManager.all_Player_Holders)
            {
                yield return player.currentHolder.UpdateCardList(player.drawingCards,0.1f,player.handCards);
            }

            yield return new WaitForSeconds(0.2f);
            Settings.RegisterEvent("DrawCard Complete", Color.green);
            isDrawComplete = true;
        }

    }

}
