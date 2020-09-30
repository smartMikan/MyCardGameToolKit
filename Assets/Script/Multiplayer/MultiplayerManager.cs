using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oukanu
{
    public class MultiplayerManager : Photon.MonoBehaviour
    {
        public static MultiplayerManager singleton;

        void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            singleton = this;
            DontDestroyOnLoad(this.gameObject);

            InstantiateNetworkPrint();
        }

        void InstantiateNetworkPrint()
        {
            PlayerProfile profile = Resources.Load<PlayerProfile>("PlayerProfile");
            object[] data = new object[1];
            data[0] = profile.cardIds;

            PhotonNetwork.Instantiate("NetworkPrint", Vector3.zero, Quaternion.identity, 0, data);
        }

    }
}


