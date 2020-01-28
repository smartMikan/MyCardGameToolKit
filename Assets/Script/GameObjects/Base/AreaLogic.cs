using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oukanu.Objects
{
    [System.Serializable]
    public abstract class AreaLogic : ScriptableObject
    {
        [System.NonSerialized]
        public CardInstance domainCard = null;

        public SO.TransformVariable CardGrid;

        public abstract void Execute();

        public void ClearDomain()
        {
            if (domainCard)
            {
                domainCard.belongsToArea = null;
            }
            domainCard = null;
        }

    }
}

