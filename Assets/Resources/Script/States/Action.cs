using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oukanu.GameStates
{
    public abstract class Action : ScriptableObject
    {
        public abstract void Excute(float deltatime);
    }

}
