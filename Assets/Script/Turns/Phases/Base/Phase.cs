using UnityEngine;
using System.Collections;

namespace Oukanu
{
    /// <summary>
    /// TBS system base
    /// </summary>
    public abstract class Phase : ScriptableObject
    {
        public string phaseName;
        //if true end phase immediate
        public bool forceExit;

        /// <summary>
        /// check if operation havs complete or force exit is true
        /// </summary>
        /// <returns></returns>
        public abstract bool IsComplete();

        [System.NonSerialized]
        protected bool isInit;

        /// <summary>
        /// on start a phase, pass all operation at this time, and init
        /// </summary>
        public abstract void OnStartPhase();

        /// <summary>
        /// reset phase stats(end unprocessed opreration or reset them to init state,etc.)
        /// </summary>
        public abstract void OnEndPhase();
    }

}