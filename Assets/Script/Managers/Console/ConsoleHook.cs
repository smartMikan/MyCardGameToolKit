using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Oukanu
{
    [CreateAssetMenu(menuName = "Console/Hook")]
    public class ConsoleHook : ScriptableObject
    {
        [System.NonSerialized]
        public ConsoleManager consoleManager;

        public void RegisterEvent(string s, Color color)
        {
            consoleManager.RegisterEvent(s, color);
        }
    }
}