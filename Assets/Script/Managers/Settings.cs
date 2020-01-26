using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Oukanu
{
    public static class Settings
    {
        public static GameManager gameManager;

        private static ResourcesManager _resourcesManager;

        public static ResourcesManager GetResourcesManager()
        {
            if (_resourcesManager == null)
            {
                _resourcesManager = Resources.Load("ResourcesManager") as ResourcesManager;
                _resourcesManager.Init();
            }

            return _resourcesManager;
        }

        private static ConsoleHook _consoleManager;

        public static void RegisterEvent(string e, Color color)
        {
            if(_consoleManager == null)
            {
                _consoleManager = Resources.Load("ConsoleHook") as ConsoleHook;
            }

            _consoleManager.RegisterEvent(e, color);

        }


        public static List<RaycastResult> GetUIObjs()
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);
            return results;
        }

        public static void DropCreatureCard(Transform c, Transform p, CardInstance card)
        {
			
			SetParentForCard(c, p);
			card.isFlatfooted = true;

			//Execute Any Special ability on drop

			if (card.isFlatfooted == true) 
			{
				c.localEulerAngles = new Vector3 (0, 0, 90);
			}

            
            gameManager.currentPlayer.UseResourceCards(card.viz.card.cost);
            gameManager.currentPlayer.DropCreatureCard(card);
        }
        public static void DropResourceCard(Transform c, Transform p, CardInstance card)
        {
            SetParentForCard(c, p);
            gameManager.currentPlayer.DropResourceCard(card);
            gameManager.currentPlayer.AddResourceCard(card.gameObject);
        }

        public static void SetParentForCard(Transform c,Transform p)
        {
            if (c.parent == p) return;
            c.SetParent(p);
            c.localPosition = Vector3.zero;
            c.localEulerAngles = Vector3.zero;
            c.localScale = Vector3.one;
        }


        public static void SetCardOntop(Transform c)
        {
            c.SetAsLastSibling();
        }
        public static void SetCardOnbeneth(Transform c)
        {
            c.SetAsFirstSibling();
        }
    }

}
