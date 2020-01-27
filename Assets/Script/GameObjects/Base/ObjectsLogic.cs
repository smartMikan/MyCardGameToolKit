using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Oukanu.Objects
{
    public abstract class ObjectsLogic : ScriptableObject
    {
        public abstract void OnClick(CardInstance c);
        public abstract void OnHighlight(CardInstance c);
    }
}

