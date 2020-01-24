using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oukanu.GameStates;


namespace Oukanu
{
    public class GameManager : MonoBehaviour
    {

        public PlayerHolder currentPlayer;


        public State currentState;
        public GameObject cardPrefab;

        public int turnIndex = 0;
        public Turn[] turns;


        public SO.GameEvent onTurnChanged;
        public SO.GameEvent onPhaseChanged;
        public SO.StringVariable turnText;
        private void Start()
        {
            Settings.gameManager = this;
            CreateStartingCards();

            turnText.value = turns[turnIndex].player.username;
            onTurnChanged.Raise();
        }

        private void FixedUpdate()
        {
            bool isComplete = turns[turnIndex].Execute();

            if (isComplete)
            {
                
                turnIndex++;
                if (turnIndex > turns.Length-1)
                {
                    turnIndex = 0;
                }

                turnText.value = turns[turnIndex].player.username;

                onTurnChanged.Raise();

            }

            if (currentState != null)
            {
                currentState.Tick(Time.deltaTime);
            }
        }


        void CreateStartingCards()
        {
            ResourcesManager rm = Settings.GetResourcesManager();
            
            for (int i = 0; i < currentPlayer.startingCards.Length; i++)
            {
                GameObject go = Instantiate(cardPrefab) as GameObject;
                CardViz viz = go.GetComponent<CardViz>();
                viz.LoadCard(rm.GetCardInstance(currentPlayer.startingCards[i]));

                CardInstance inst = go.GetComponent<CardInstance>();
                inst.currentLogic = currentPlayer.handLogic;
                Settings.SetParentForCard(go.transform, currentPlayer.handGrid.value);
                
            }
        }


        public void SetState(State state)
        {
            currentState = state;
        }
    }

}
