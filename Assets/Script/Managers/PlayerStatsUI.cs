using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Oukanu
{
    public class PlayerStatsUI : MonoBehaviour
    {
        [HideInInspector]
        public PlayerHolder player;
        public Image playerPotrait;

        public Text health;
        public Text userName;

        

        public void UpdateAll()
        {
            UpdateUsername();
            UpdatePotrait();
            UpdateHealth();
        }

        void UpdateUsername()
        {
            userName.text = player.username;
            
        }
        void UpdatePotrait()
        {
            playerPotrait.sprite = player.potrait;
        }
        public void UpdateHealth()
        {
            health.text = player.health.ToString();
        }
    }

}
