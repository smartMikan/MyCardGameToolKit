using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oukanu.GameStates
{
    /// <summary>
    /// A state is basically a stuck of actions that to be excuted in one update
    /// </summary>
    [CreateAssetMenu(menuName = "State")]
    public class State : ScriptableObject
    {
        public Action[] actions;

        /// <summary>
        /// It is A Update()
        /// </summary>
        /// <param name="deltatime">time.deltatime</param>
        public void Tick(float deltatime)
        {
            for (int i = 0; i < actions.Length; i++)
            {
                actions[i].Execute(deltatime);
            }
        }

    }

}
