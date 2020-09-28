using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oukanu.Variable;

namespace Oukanu
{
    /// <summary>
    /// Turn is basically a holder of phases, which is specific for each player, so player actions can be execute at the beginning of turn
    /// </summary>
    [CreateAssetMenu(menuName = "Turns/Turn")]
    public class Turn : ScriptableObject
    {
        //should be assigned in editor
        public PlayerHolder player;

        //current phase index
        [System.NonSerialized]
        public int index = 0;

        //an static object to cache current phase reference runtime, should be assigned in editor
        public PhaseVariable currentPhaseVariable;
        
        //should be assigned in editor
        public Phase[] phases;

        //actions to be excute at start of this turn
        public GameStates.PlayerActions[] turnStartActions;

        /// <summary>
        /// execute when turn start
        /// </summary>
        public void OnTurnStart()
        {
            if (turnStartActions == null) return;

            for (int i = 0; i < turnStartActions.Length; i++)
            {
                turnStartActions[i].Excute(player);
            }
        }

        /// <summary>
        /// Turn Update
        /// </summary>
        /// <returns>all phases in this turn have finished</returns>
        public bool Execute()
        {
            bool result = false;
            //expose current phase variable
            currentPhaseVariable.value = phases[index];

            //run OnStartPhase(phase will check if is a init run itself,only initial run will be execute) 
            phases[index].OnStartPhase();
            //check
            bool phaseIsComplete = phases[index].IsComplete();

            if (phaseIsComplete)
            {
                //execute OnEndPhase
                phases[index].OnEndPhase();
                //debug
                Debug.Log(phases[index].phaseName + "Phase End");
                //move to next phase
                index++;
                //check if all phases have done
                if (index > phases.Length - 1)
                {
                    index = 0;
                    //all phases done;
                    result = true;
                }
            }
            return result;
        }


        /// <summary>
        /// Used by endPhase button, force end current phase
        /// </summary>
        public void EndCurrentPhase()
        {
            phases[index].forceExit = true;
            Settings.RegisterEvent(player.username + "'s " + phases[index].name + " finished", player.playerColor);

        }


    }

}