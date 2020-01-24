using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Oukanu.GameStates
{
    [CreateAssetMenu(menuName = "Actions/MouseOverDetection")]
    public class MouseOverDetection : Action
    {

        public override void Execute(float deltatime)
        {
            
            List<RaycastResult> results = Settings.GetUIObjs();


            foreach (RaycastResult r in results)
            {
                IClickable c = r.gameObject.GetComponentInParent<IClickable>();

                if (c != null)
                {
                    c.OnHighlight();
                    break;
                }
            }
            
        }
    }
}

