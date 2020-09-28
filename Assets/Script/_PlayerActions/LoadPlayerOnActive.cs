using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Oukanu
{
    [CreateAssetMenu(menuName = "PlayerActions/LoadPlayerOnActive")]
    public class LoadPlayerOnActive : GameStates.PlayerActions
    {
        public override void Excute(PlayerHolder player)
        {
            if (player!=null)
            {
                Settings.gameManager.LoadPlayerOnActive(player);
            }
            else
            {
                Debug.LogError("Current Player is Null,Please Check your Turn Object");
            }
        }
    }
}