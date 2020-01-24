using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Oukanu.GameStates
{
    [CreateAssetMenu(menuName = "Actions/OnMouseClick")]
    public class OnMouseClick : Action
    {
        public override void Execute(float deltatime)
        {

            if (Input.GetMouseButtonDown(0))
            {
                List<RaycastResult> results = Settings.GetUIObjs();

                foreach (RaycastResult r in results)
                {
                    IClickable c = r.gameObject.GetComponentInParent<IClickable>();

                    if (c != null)
                    {
                        c.OnClick();
                        break;
                    }
                }
            }
            
        }
    }
}

