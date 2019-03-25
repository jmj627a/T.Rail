using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Com.myGame
{
    public class launcherScript : MonoBehaviourPunCallbacks
    {
        #region private Serializable Fields
        
        //방에 참여할 수 있는 최대 인원 수 
        [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
        [SerializeField]
        private byte maxPlayersPerRoom = 3;

        #endregion



        #region private Fields

        //This client's version number. Users are separated from each other by gameVersion (which allows you to make breaking changes).
        //클라이언트의 버전 숫자. 그냥 게임 버전.  
        string gameVersion = "1";

        #endregion



        #region MonoBehaviour Callbacks
        private void Awake()
        {
            //이렇게하면 마스터 클라이언트에서 PhotonNetwork.LoadLevel()을 사용할 수 있고 같은 방의 모든 클라이언트가 자동으로 레벨을 동기화합니다
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        // Use this for initialization
        //MonoBehaviour 메서드가 초기화 단계에서 Unity에 의해 GameObject를 호출했습니다.
        void Start()
        {
            //Connect();
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
        }
        #endregion


        #region public Methods
        //연결 프로세스를 시작하십시오.
        // - 이미 연결되어 있다면 우리는 임의의 방에 합류하려고 시도합니다.
        // - 아직 연결되지 않은 경우이 애플리케이션 인스턴스를 Photon Cloud Network에 연결합니다.
        public void Connect()
        {
            progressLabel.SetActive(true);
            controlPanel.SetActive(false);

            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.GameVersion = gameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
            
        }

        [Tooltip("The Ui Panel to let the user enter name, connect and play")]
        [SerializeField]
        private GameObject controlPanel;
        [Tooltip("The UI Label to inform the user that the connection is in progress")]
        [SerializeField]
        private GameObject progressLabel;
        #endregion


        #region MonoBehaviourPunCallbacks Callbacks

        public override void OnConnectedToMaster()
        {
            Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
            Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

            // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
            //PhotonNetwork.CreateRoom(null, new RoomOptions());
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
        }
        #endregion
    }
}
