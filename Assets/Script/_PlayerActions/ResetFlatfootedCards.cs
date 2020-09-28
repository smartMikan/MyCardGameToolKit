using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Oukanu
{
    [CreateAssetMenu(menuName = "PlayerActions/ResetFlatfootedCards")]
    public class ResetFlatfootedCards : GameStates.PlayerActions
    {
        public override void Excute(PlayerHolder player)
        {
            if (player!=null)
            {
                player.ResetAllFlatFootedCards();
            }
            else
            {
                Debug.LogError("Current Player is Null,Please Check your Turn Object");
            }
        }
    }
}