﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oukanu.GameStates
{
    [CreateAssetMenu(menuName = "State")]
    public class State : ScriptableObject
    {
        public Action[] actions;

        public void Tick(float deltatime)
        {
            for (int i = 0; i < actions.Length; i++)
            {
                actions[i].Execute(deltatime);
            }
        }

    }

}
