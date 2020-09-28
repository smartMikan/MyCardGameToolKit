using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Oukanu.GameStates
{
    [CreateAssetMenu(menuName = "Actions/MouseHoldWithCard")]
    public class MouseHoldWithCard : Action
    {

        public Variable.CardVariables currentCard;
        public State playerControlState;
        public SO.GameEvent onPlayerControlState;


        public override void Execute(float deltatime)
        {
            bool isMouseDown = Input.GetMouseButton(0);

           

            if (!isMouseDown)
            {

                List<RaycastResult> results = Settings.GetUIObjs();
                

                foreach (RaycastResult r in results)
                {
                    //Check For Dropable Areas
                    Objects.Area a = r.gameObject.GetComponentInParent<Objects.Area>();
                    if (a)
                    {
                        a.OnDrop();
                        break;
                    }
                }

                
                //Bring Back Cards
                currentCard.value.gameObject.SetActive(true);
                currentCard.value = null;
                

                Settings.gameManager.SetState(playerControlState);
                onPlayerControlState.Raise();
                return;
            }
        }
    }
}

