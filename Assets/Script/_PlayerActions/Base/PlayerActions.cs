using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Oukanu.GameStates
{
    public abstract class PlayerActions : ScriptableObject
    {
        public abstract void Excute(PlayerHolder player);
    }
}

