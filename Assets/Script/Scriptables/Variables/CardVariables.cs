using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Oukanu
{
    [CreateAssetMenu(menuName = "Variables/Card Variable")]
    public class CardVariables : ScriptableObject
    {
        public CardInstance value;

        public void Set(CardInstance v)
        {
            value = v;
        }
    }
}

