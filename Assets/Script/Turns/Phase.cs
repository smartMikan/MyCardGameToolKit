﻿using UnityEngine;
using System.Collections;

namespace Oukanu
{
    
    public abstract class Phase : ScriptableObject
    {
        public string phaseName;
        public bool forceExit;
        public abstract bool IsComplete();



        [System.NonSerialized]
        protected bool isInit;

        public abstract void OnStartPhase();

        public abstract void OnEndPhase();
    }

}