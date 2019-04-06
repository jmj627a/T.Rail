using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class photonInit : MonoBehaviourPunCallbacks {

    public string gameVersion = "1.0";
    public string nickName = "player";

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected!!!!!!!! " + nickName);
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log("Fail join room!!!!!!!! ");
        this.CreateRoom();
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("join room!!!!!!!! ");
    }

    void CreateRoom()
    {
        //CreateRoom(방이름, 방옵션)
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 3 });
    }

}
