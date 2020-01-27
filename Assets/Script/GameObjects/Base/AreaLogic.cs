using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oukanu.Objects
{
    [System.Serializable]
    public abstract class AreaLogic : ScriptableObject
    {
        [System.NonSerialized]
        public CardInstance domainCard;

        public SO.TransformVariable CardGrid;

        public abstract void Execute();
        
    }
}

