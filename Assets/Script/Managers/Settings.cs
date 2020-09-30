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
            if (_consoleManager == null)
            {
                _consoleManager = Resources.Load("ConsoleHook") as ConsoleHook;
            }

            _consoleManager.RegisterEvent(e, color);

        }


        public static List<RaycastResult> GetUIObjects()
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);
            return results;
        }

        
        public static void SetParentForCard(Transform card, Transform parent, bool withAnim = false)
        {
            if (card.parent == parent) return;
            card.SetParent(parent);
            card.localPosition = Vector3.zero;
            if (withAnim)
            {
                card.GetComponent<CardViz>().SetTargetRotation(Vector3.zero);
            }
            else
            {
                card.GetComponent<CardViz>().SetTargetRotation(Vector3.zero);
                card.localEulerAngles = Vector3.zero;
            }
            card.localScale = Vector3.one;
        }

        public static void SetParentForCard(Transform card, Transform parent, Vector3 localPosition, Vector3 eularAngle, bool withAnim = false)
        {
            if (card.parent == parent) return;
            card.SetParent(parent);
            card.localPosition = localPosition;
            if (withAnim)
            {
                card.GetComponent<CardViz>().SetTargetRotation(eularAngle);
            }
            else
            {
                card.GetComponent<CardViz>().SetTargetRotation(eularAngle);
                card.localEulerAngles = eularAngle;
            }
            card.localScale = Vector3.one;
        }


        public static void SetCardToBlockPos(Transform card, Transform parent, int count)
        {
            Vector3 blockPosition = Vector3.zero;

            blockPosition.x -= 150 * count;
            blockPosition.y += 150 * count;

            SetParentForCard(card, parent, blockPosition, new Vector3(0, 0, 180));
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
