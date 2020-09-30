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
                List<RaycastResult> results = Settings.GetUIObjects();

                foreach (RaycastResult r in results)
                {
                    CardInstance inst = r.gameObject.GetComponentInParent<CardInstance>();

                    PlayerHolder player = Settings.gameManager.CurrentPlayer;

                    if (!player.downCards.Contains(inst))
                    {
                        return;
                    }

                    if (inst.CanAttack())
                    {
                        //you can attack
                        inst.SetOnBattleLine();
                    }
                }
            }
        }
    }

}
