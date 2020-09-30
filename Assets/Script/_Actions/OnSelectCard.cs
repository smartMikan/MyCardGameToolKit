using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Oukanu.GameStates
{
    [CreateAssetMenu(menuName = "Actions/OnSelectCard")]
    class OnSelectCard : Action
    {
        public SO.GameEvent onCurrentCardSelected;
        public Variable.CardVariables currentCard;
        public State holdingCardState;

        public override void Execute(float deltatime)
        {

            

            if (Input.GetMouseButtonDown(0))
            {
                List<RaycastResult> results = Settings.GetUIObjects();

                foreach (RaycastResult r in results)
                {

                    CardInstance c = r.gameObject.GetComponentInParent<CardInstance>();
                    if (c != null)
                    {
                        if (c.owner.downCards.Contains(c))
                        {
                            GameManager gm = Settings.gameManager;
                            PlayerHolder enemy = gm.GetEnemyOf(gm.CurrentPlayer);

                            if (c.owner == enemy)
                            {

                                Debug.Log("Holding card");
                                currentCard.Set(c);

                                gm.SetState(holdingCardState);
                                onCurrentCardSelected.Raise();
                            }
                        }
                       
                    }

                }
            }
        }
    }
}
