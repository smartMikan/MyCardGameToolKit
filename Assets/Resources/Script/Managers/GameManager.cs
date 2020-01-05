using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oukanu.GameStates;


namespace Oukanu
{
    public class GameManager : MonoBehaviour
    {
        public State currentState;

        private void FixedUpdate()
        {
            currentState.Tick(Time.deltaTime);
        }
    }

}
