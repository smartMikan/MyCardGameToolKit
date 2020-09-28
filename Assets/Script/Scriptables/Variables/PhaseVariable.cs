using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Oukanu.Variable
{
    [CreateAssetMenu(menuName = "Variables/Phase Variable")]
    public class PhaseVariable : ScriptableObject
    {
        public Phase value;

        public void Set(Phase v)
        {
            value = v;
        }

        public void Set(PhaseVariable v)
        {
            value = v.value;
        }

        public bool IsEmptyOrNull()
        {
            return value == null;
        }

        public void Clear()
        {
            value = null;
        }
    }

}