using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class lobbyController : MonoBehaviourPunCallbacks {

    public string gameVersion = "1.0";
    //public string nickName = "player";

    bool isConnecting;

    #region Game UI
    public InputField playerNameInputField;

    
    void setPlayerRandomName()
    {
        //랜덤 닉네임 생성
        playerNameInputField.text = "Player " + Random.Range(1000, 10000);
    }

    #endregion



    #region Unity

    private void Awake()
    {
        setPlayerRandomName();

        //하나의 클라이언트가 룸 내의 모든 클라이언트들에게 로드해야할 레벨을 정의
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
      
    }

    #endregion

    #region public Method
    public void Connect()
    {
        string playerName = playerNameInputField.text;
        isConnecting = true;

        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.LocalPlayer.NickName = playerName;
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    #endregion


    #region Photon Callbacks

    public void OnLoginButtonClicked()
    {
        string playerName = playerNameInputField.text;
        if (!playerName.Equals(""))
        {
            Connect();
        }
        else
        {
            Debug.LogError("Player Name is invalid.");
        }
    }
 

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected!!!!!!!! " + playerNameInputField.text);
        if (isConnecting)
            PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

        this.CreateRoom();
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("join room!!!!!!!! ");
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Debug.Log("We load the 'MainScene' ");

            PhotonNetwork.LoadLevel("MainScene"); //겜 시작하면 게임화면 씬 로드
        }
        Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
    }

    void CreateRoom()
    {
        //CreateRoom(방이름, 방옵션)
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 3 });
    }

    #endregion
}
