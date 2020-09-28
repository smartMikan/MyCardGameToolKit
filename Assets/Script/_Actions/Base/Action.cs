using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oukanu.GameStates
{
    /// <summary>
    /// action scriptal object can store commands with scriptable so the command itself can be a component without gameobject ref
    /// </summary>
    public abstract class Action : ScriptableObject
    {
        public abstract void Execute(float deltatime);
    }

}
