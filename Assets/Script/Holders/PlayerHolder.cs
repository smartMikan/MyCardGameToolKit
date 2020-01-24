using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Oukanu
{
    [CreateAssetMenu(menuName = "Holders/Player Holder")]
    public class PlayerHolder : ScriptableObject
    {
        public string username;
        public string[] startingCards;

        public SO.TransformVariable handGrid;
        public SO.TransformVariable downGrid;

        public Objects.ObjectsLogic handLogic;
        public Objects.ObjectsLogic downLogic;

        [System.NonSerialized]
        public List<CardInstance> handCards = new List<CardInstance>();
        [System.NonSerialized]
        public List<CardInstance> downCards = new List<CardInstance>();

    }

}