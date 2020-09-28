using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Oukanu.Variable
{
    [CreateAssetMenu(menuName = "Variables/Card Variable")]
    public class CardVariables : ScriptableObject
    {
        public CardInstance value;

        public void Set(CardInstance v)
        {
            value = v;
        }
        public void Set(CardVariables v)
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

