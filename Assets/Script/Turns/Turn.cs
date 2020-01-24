using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Oukanu
{
    [CreateAssetMenu(menuName = "Turns/Turn")]
    public class Turn : ScriptableObject
    {
        public PlayerHolder player;
        [System.NonSerialized]
        public int index = 0;
        public PhaseVariable currentPhase;
        public Phase[] phases;
        //public SO.GameEvent onPhaseComplete;


        public bool Execute()
        {
            bool result = false;

            bool phaseIsComplete = phases[index].IsComplete();

            currentPhase.value = phases[index];
            phases[index].OnStartPhase();

           
            if (phaseIsComplete)
            {
                phases[index].OnEndPhase();
                Debug.Log("End Phase");
                index++;
                if(index > phases.Length - 1)
                {
                    index = 0;
                    result = true;
                }
                //onPhaseComplete.Raise();
            }


            return result;
        }


        public void EndCurrentPhase()
        {
            phases[index].forceExit = true;
        }


    }

}