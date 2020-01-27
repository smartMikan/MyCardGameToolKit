using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Oukanu
{
    [CreateAssetMenu(menuName = "Variables/CardList Variable")]
    public class CardListVariables : ScriptableObject
    {
        public List<CardInstance> value;

        public void Set(List<CardInstance> v)
        {
            value = v;
        }
    }
}

