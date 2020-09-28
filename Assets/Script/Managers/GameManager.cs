using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oukanu.GameStates;


namespace Oukanu
{
    public class GameManager : MonoBehaviour
    {
        [System.NonSerialized]
        public PlayerHolder[] all_Players;
        public PlayerHolder GetEnemyOf(PlayerHolder player)
        {
            for (int i = 0; i < all_Players.Length; i++)
            {
                if (all_Players[i] != player)
                {
                    return all_Players[i];
                }
            }
            return null;
        }

        public PlayerHolder CurrentPlayer { get; private set; }


        public CardHolder P1CardHolder;
        public CardHolder P2CardHolder;

        public PlayerStatsUI[] statsUIs;


        public State currentState;
        public GameObject cardPrefab;

        public int turnIndex = 0;
        public Turn[] turns;


        public SO.GameEvent onTurnChanged;
        public SO.GameEvent onPhaseChanged;
        public Variable.StringVariable turnText;

        public Variable.CardVariables currentMouseoverCard;


        Dictionary<CardInstance, BlockInstance> blockInstances = new Dictionary<CardInstance, BlockInstance>();

        public Dictionary<CardInstance, BlockInstance> GetBlockInstances()
        {
            return blockInstances;
        }

        public void ClearBlockInstances()
        {
            blockInstances.Clear();
        }

        public void AddBlockInstance(CardInstance attacker, CardInstance blocker)
        {
            BlockInstance b = null;
            b = GetBlockInstanceOfAttacker(attacker);
            if (b == null)
            {
                b = new BlockInstance();
                b.Attacker = attacker;
                blockInstances.Add(attacker, b);
            }
            if (!b.Blockers.Contains(blocker))
            {
                b.Blockers.Add(blocker);
            }
        }

        BlockInstance GetBlockInstanceOfAttacker(CardInstance attacker)
        {
            BlockInstance result = null;
            blockInstances.TryGetValue(attacker, out result);
            return result;
        }


        private void Awake()
        {
            Settings.gameManager = this;


            //create Player by turns' data
            all_Players = new PlayerHolder[turns.Length];

            for (int i = 0; i < turns.Length; i++)
            {
                all_Players[i] = turns[i].player;
            }

            CurrentPlayer = turns[0].player;

        }


        private void Start()
        {
            SetupPlayers();

            all_Players[0].Shuffle();
            all_Players[1].Shuffle();

            all_Players[0].DrawCard(3);
            all_Players[1].DrawCard(3);

            turnText.value = turns[turnIndex].player.username;
            turns[0].OnTurnStart();
            onTurnChanged.Raise();
        }

        private void Update()
        {

            bool isComplete = turns[turnIndex].Execute();

            if (isComplete)
            {
                turnIndex++;
                if (turnIndex > turns.Length - 1)
                {
                    turnIndex = 0;
                }

                //The current player has changed here
                CurrentPlayer = turns[turnIndex].player;

                turns[turnIndex].OnTurnStart();
                turnText.value = turns[turnIndex].player.username;

                onTurnChanged.Raise();

            }
            //update current state
            if (currentState != null)
            {
                currentState.Tick(Time.deltaTime);
            }
        }

        void SetupPlayers()
        {
            for (int i = 0; i < all_Players.Length; i++)
            {

                //if (all_Players[i].isHumanPlayer)
                //{
                //    all_Players[i].currentHolder = P1CardHolder;
                //    if (i < 2)
                //    {
                //        P1CardHolder.LoadPlayer(all_Players[i], statsUIs[i]);
                //    }
                //}
                //else
                //{
                //    all_Players[i].currentHolder = P2CardHolder;
                //      if (i < 2)
                //      {
                //          P2CardHolder.LoadPlayer(all_Players[i], statsUIs[i]);
                //      }
                //}
                //assign holder
                if (i == 0)
                {
                    all_Players[i].currentHolder = P1CardHolder;
                }
                if (i == 1)
                {
                    all_Players[i].currentHolder = P2CardHolder;
                }
                all_Players[i].Init();

                //load deck data
                all_Players[i].deckCards = InstantiateCardsByPlayer(all_Players[i]);

                LoadPlayerOnHolder(all_Players[i], all_Players[i].currentHolder, statsUIs[i]);
            }
        }


        public CardInstance InstantiateCardByName(ResourcesManager rm, string cardName)
        {
            //instantiate a card prefab
            GameObject go = Instantiate(cardPrefab);
            CardViz viz = go.GetComponent<CardViz>();
            //give the instance card value from resource manager by name
            viz.LoadCard(rm.GetCardInstance(cardName));
            //get the intance controller ref
            CardInstance inst = go.GetComponent<CardInstance>();
            return inst;
        }

        public List<CardInstance> InstantiateCardsByNames(List<string> cardsName)
        {
            List<CardInstance> cards = new List<CardInstance>();
            ResourcesManager rm = Settings.GetResourcesManager();
            foreach (var name in cardsName)
            {
                GameObject go = Instantiate(cardPrefab);
                CardViz viz = go.GetComponent<CardViz>();
                //give the instance card value from resource manager by name
                viz.LoadCard(rm.GetCardInstance(name));
                //get the intance controller ref
                CardInstance inst = go.GetComponent<CardInstance>();
                cards.Add(inst);
            }
            return cards;
        }

        public List<CardInstance> InstantiateCardsByPlayer(PlayerHolder player)
        {
            List<CardInstance> cards = new List<CardInstance>();
            ResourcesManager rm = Settings.GetResourcesManager();

            foreach (var name in player.CurrentDeck)
            {
                GameObject go = Instantiate(cardPrefab);
                CardViz viz = go.GetComponent<CardViz>();
                //give the instance card value from resource manager by name
                viz.LoadCard(rm.GetCardInstance(name));
                //get the intance controller ref
                CardInstance inst = go.GetComponent<CardInstance>();

                inst.owner = player;
                cards.Add(inst);
            }
            return cards;
        }

        public void LoadPlayerOnActive(PlayerHolder player)
        {
            if (P1CardHolder.possessedPlayer != player)
            {
                PlayerHolder prevPlayer = P1CardHolder.possessedPlayer;
                if (prevPlayer != null)
                {
                    LoadPlayerOnHolder(prevPlayer, P2CardHolder, statsUIs[1]);
                    LoadPlayerOnHolder(player, P1CardHolder, statsUIs[0]);
                }
                else
                {
                    Debug.LogError("prev player is null");
                }

            }
        }

        internal void LoadPlayerOnHolder(PlayerHolder player, CardHolder holder, PlayerStatsUI statsUI)
        {
            holder.LoadPlayer(player, statsUI);
        }


        public void SetState(State state)
        {
            currentState = state;
        }



        public void EndCurrentPhase()
        {
            turns[turnIndex].EndCurrentPhase();
        }


        public void DeHighlightCurrentCard()
        {
            if (currentMouseoverCard.value != null)
            {
                currentMouseoverCard.value.viz.SetHighight(false);
            }

        }

    }

}
