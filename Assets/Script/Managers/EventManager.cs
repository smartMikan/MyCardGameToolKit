using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oukanu
{
    public class EventManager : MonoBehaviour
    {
        #region My Calls
        public void CardIsDropped(int instId,int ownerId)
        {
            Card c = NetworkManager.singleton.GetCard(instId, ownerId);
        }

        public void CardIsPickedUpFromDeck(int instId, int ownerId)
        {

            Card c = NetworkManager.singleton.GetCard(instId, ownerId);
        }


        #endregion
    }

}

