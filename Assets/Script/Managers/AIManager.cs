using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oukanu
{
    public class AIManager : MonoBehaviour
    {

        public PlayerHolder aiPlayer;
        public CardVariables cardVariable;
        public Objects.EnemyDownCardAreaLogic[] downcardareas;



        private void Awake()
        {
            Settings.aiManager = this;
        }
        

        public void SelectACard(CardInstance card)
        {
            cardVariable.value = card;
        }

        public void DropCard()
        {
            for (int i = 0; i < downcardareas.Length; i++)
            {
                if (aiPlayer.handCards.Count <= 0)
                {
                    break;
                }
                int c = UnityEngine.Random.Range(0, aiPlayer.handCards.Count - 1);
                SelectACard(aiPlayer.handCards[c]);
                downcardareas[i].Execute();
                aiPlayer.MoveCard(aiPlayer.handCards[c], aiPlayer.handCards, aiPlayer.downCards);
                aiPlayer.handCards[c].belongsToArea = downcardareas[i];


                Settings.SetParentForCard(aiPlayer.handCards[c].viz.gameObject.transform, downcardareas[i].CardGrid.value);
                //aiPlayer.currentHolder.LoadPlayer(aiPlayer);
                
            }
        }
    }
}

