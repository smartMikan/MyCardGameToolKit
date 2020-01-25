using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oukanu.GameStates;


namespace Oukanu
{
    public class GameManager : MonoBehaviour
    {
        public PlayerHolder[] all_Player_Holders;
        public PlayerHolder currentPlayer;
        public CardHolder playerOneHolder;
        public CardHolder otherPlayersHolder;


        public State currentState;
        public GameObject cardPrefab;

        public int turnIndex = 0;
        public Turn[] turns;


        public SO.GameEvent onTurnChanged;
        public SO.GameEvent onPhaseChanged;
        public SO.StringVariable turnText;



        public static GameManager Instance;

        private void Awake()
        {
            Instance = this;
        }
        

        private void Start()
        {
            Settings.gameManager = this;

            //currentPlayer.isHumanPlayer = true;
            //currentPlayer.currentHolder = playerOneHolder;
            SetupPlayers();

            CreateStartingCards();

            turnText.value = turns[turnIndex].player.username;
            onTurnChanged.Raise();
        }



        public bool switchPlayer;

        private void Update()
        {

            if (switchPlayer)
            {
                switchPlayer = false;
                playerOneHolder.LoadPlayer(all_Player_Holders[1]);
                otherPlayersHolder.LoadPlayer(all_Player_Holders[0]);
            }

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

        void SetupPlayers()
        {
            foreach (PlayerHolder player in all_Player_Holders)
            {
                if (player.isHumanPlayer)
                {
                    player.currentHolder = playerOneHolder;
                }
                else
                {
                    player.currentHolder = otherPlayersHolder;
                }
            }
        }



        void CreateStartingCards()
        {
            ResourcesManager rm = Settings.GetResourcesManager();
            for (int p = 0; p < all_Player_Holders.Length; p++)
            {
                for (int i = 0; i < all_Player_Holders[p].startingCards.Length; i++)
                {
                    GameObject go = Instantiate(cardPrefab) as GameObject;
                    CardViz viz = go.GetComponent<CardViz>();
                    viz.LoadCard(rm.GetCardInstance(all_Player_Holders[p].startingCards[i]));

                    CardInstance inst = go.GetComponent<CardInstance>();
                    inst.currentLogic = all_Player_Holders[p].handLogic;
                    Settings.SetParentForCard(go.transform, all_Player_Holders[p].currentHolder.handGrid.value);


                    all_Player_Holders[p].handCards.Add(inst);
                }

                Settings.RegisterEvent("Created cards for player " + all_Player_Holders[p].username, all_Player_Holders[p].playerColor);

            }
            
        }


        public void SetState(State state)
        {
            currentState = state;
        }



        public void EndCurrentPhase()
        {
            turns[turnIndex].EndCurrentPhase();
        }

    }

}
