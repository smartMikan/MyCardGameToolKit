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

        private void Start()
        {
            for (int i = 0; i < downcardareas.Length; i++)
            {
                downcardareas[i].ClearDomain();
            }
        }
        public CardInstance SelectACard(CardInstance card)
        {
            cardVariable.value = card;
            return card;
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
                CardInstance card = SelectACard(aiPlayer.handCards[c]);
                downcardareas[i].Execute();

                if (i == 0)
                {
                    aiPlayer.MoveCard(card, aiPlayer.handCards, aiPlayer.downCards);
                }
                if (i == 1)
                {
                    aiPlayer.MoveCard(card, aiPlayer.handCards, aiPlayer.downCards2);
                }


                card.belongsToArea = downcardareas[i];


                Settings.SetParentForCard(card.viz.gameObject.transform, downcardareas[i].CardGrid.value);
                //aiPlayer.currentHolder.LoadPlayer(aiPlayer);
                
            }
        }
    }
}

