using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Oukanu
{
    public class NetworkManager : Photon.PunBehaviour
    {
        public bool isMaster;
        public static NetworkManager singleton;


        List<MultiPlayerHolder> multiPlayerHolders = new List<MultiPlayerHolder>();
        public MultiPlayerHolder GetHolder(int photonId)
        {
            return multiPlayerHolders.Find(m => m.ownerId == photonId);
        }

        public Card GetCard(int instId, int ownerId)
        {
            MultiPlayerHolder h = GetHolder(ownerId);
            return h.GetCard(instId);
        }


        ResourcesManager rm;

        int cardInstIds;

        private void Awake()
        {
            if (singleton == null)
            {
                rm = Resources.Load<ResourcesManager>("ResourcesManager");
                singleton = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Debug.LogError("Multiple NetworkManager Instance!");
                DestroyImmediate(this.gameObject);
            }

        }
        #region MyCalls



        //Master(Host) Only
        public void PlayerJoined(int ownerId, string[] cards)
        {
            MultiPlayerHolder clientHolder = new MultiPlayerHolder();

            clientHolder.ownerId = ownerId;

            for (int i = 0; i < cards.Length; i++)
            {
                Card c = CreateCardMaster(cards[i]);
                if (c == null)
                {
                    continue;
                }
                clientHolder.RegisterCard(c);

                //Rpc
            }
        }


        public void Broadcast_CreatecardsMaster(string[] cards)
        {

            //todo: create all cards on master

            //TOdo: rpc for clients to create cards

        }




        public Card CreateCardMaster(string cardId)
        {
            Card card = rm.GetCardInstance(cardId);
            card.instId = cardInstIds;
            cardInstIds++;
            return card;
        }


        void CreateCardClient_Call(string cardId, int instId, int photonId)
        {
            Card c = CreateCardClient(cardId, instId);
            if (c != null)
            {
                MultiPlayerHolder h = GetHolder(photonId);
                h.RegisterCard(c);
            }

        }

        Card CreateCardClient(string cardId, int instId)
        {
            Card card = rm.GetCardInstance(cardId);
            card.instId = instId;
            return card;
        }

        #endregion

        #region Photon Callback
        public Variable.StringVariable logger;
        public SO.GameEvent loggerUpdated;

        public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
        {
            if (isMaster)
            {
                if (PhotonNetwork.playerList.Length >1)
                {
                    logger.Set("Ready for match");
                    loggerUpdated.Raise();


                    PhotonNetwork.room.IsOpen = false;
                    //SessionManager.singleton.LoadGameLevel(OnGameSceneLoaded);
                }
            }
        }

        void OnGameSceneLoaded()
        {
            
        }

        public override void OnDisconnectedFromPhoton()
        {
            
        }

        public override void OnLeftRoom()
        {
            
        }

        #endregion


        #region RPCs



        #endregion

    }



    public class MultiPlayerHolder
    {
        public int ownerId;

        Dictionary<int, Card> cards = new Dictionary<int, Card>();

        public void RegisterCard(Card c)
        {
            cards.Add(c.instId, c);
        }

        public Card GetCard(int instId)
        {
            Card c = null;

            cards.TryGetValue(instId, out c);

            return c;
        }
    }
}

