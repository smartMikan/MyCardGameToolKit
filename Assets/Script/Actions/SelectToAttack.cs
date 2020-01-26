using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Oukanu.GameStates
{
    [CreateAssetMenu(menuName = "Actions/SelectToAttack")]
    public class SelectToAttack : Action
    {
        public override void Execute(float deltatime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                List<RaycastResult> results = Settings.GetUIObjs();

                foreach (RaycastResult r in results)
                {
                    CardInstance inst = r.gameObject.GetComponentInParent<CardInstance>();

                    PlayerHolder p = Settings.gameManager.currentPlayer;

                    if (!p.downCards.Contains(inst))
                    {
                        return;
                    }

                    if (inst.CanAttack())
                    {
                        p.attackingCards.Add(inst);
                        //you can attack
                    }
                }
            }
        }
    }

}
