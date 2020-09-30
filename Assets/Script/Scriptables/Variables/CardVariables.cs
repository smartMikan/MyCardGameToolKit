using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Oukanu.Variable
{
    [CreateAssetMenu(menuName = "Variables/Card Variable")]
    public class CardVariables : ScriptableObject
    {
        public CardInstance Value { get; private set; }

        public void Set(CardInstance v)
        {
            Value = v;
        }
        public void Set(CardVariables v)
        {
            Value = v.Value;
        }

        public bool IsEmptyOrNull()
        {
            return Value == null;
        }

        public void Clear()
        {
            Value = null;
        }
    }
}

