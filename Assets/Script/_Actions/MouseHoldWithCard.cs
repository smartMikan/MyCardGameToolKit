using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Oukanu.GameStates
{
    [CreateAssetMenu(menuName = "Actions/MouseHoldWithCard")]
    public class MouseHoldWithCard : Action
    {

        public Variable.CardVariables currentHouldingCard;
        public State playerControlState;
        public State blockState;


        public SO.GameEvent onPlayerControlState;
        public Phase blockPhase;


        public override void Execute(float deltatime)
        {
            bool isMouseDown = Input.GetMouseButton(0);

            //when mouse up
            if (!isMouseDown)
            {
                GameManager gm = Settings.gameManager;

                List<RaycastResult> results = Settings.GetUIObjects();

                //not block phase
                if (gm.turns[gm.turnIndex].currentPhaseVariable.value != blockPhase)
                {
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
                    currentHouldingCard.Value.gameObject.SetActive(true);
                    currentHouldingCard.Clear();


                    gm.SetState(playerControlState);
                    //hide
                    onPlayerControlState.Raise();
                    return;
                }
                //block phase mouse detect
                else
                {
                    foreach (RaycastResult r in results)
                    {
                        CardInstance attacker = r.gameObject.GetComponentInParent<CardInstance>();

                        if (attacker != null)
                        {
                            int count = 0;
                            bool block = attacker.CanBeBlocked(currentHouldingCard.Value, ref count);
                            if (block)
                            {
                                Debug.Log("find card to block");
                                Settings.SetCardToBlockPos(currentHouldingCard.Value.transform, attacker.transform, count);
                                
                            }
                        }


                        gm.SetState(blockState);
                        onPlayerControlState.Raise();
                        //Bring Back Cards
                        currentHouldingCard.Value.gameObject.SetActive(true);
                        currentHouldingCard.Clear();
                        break;
                    }
                }



            }
        }
    }
}

