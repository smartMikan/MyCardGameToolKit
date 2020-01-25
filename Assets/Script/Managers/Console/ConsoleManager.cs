using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Oukanu
{
    
    public class ConsoleManager : MonoBehaviour
    {
        public Transform consoleGrid;
        public GameObject prefab;

        Text[] textobjects;
        int index;

        public ConsoleHook hook;


        private void Awake()
        {
            hook.consoleManager = this;

            textobjects = new Text[5];
            for (int i = 0; i < textobjects.Length; i++)
            {
                GameObject go = Instantiate(prefab) as GameObject;
                textobjects[i] = go.GetComponent<Text>();
                go.transform.SetParent(consoleGrid);
            }
        }

        public void RegisterEvent(string s, Color color)
        {
            index++;
            if (index > textobjects.Length - 1)
            {
                index = 0;
            }

            textobjects[index].color = color;
            textobjects[index].text = s;
            textobjects[index].gameObject.SetActive(true);
            textobjects[index].transform.SetAsLastSibling();
        }
    }
}