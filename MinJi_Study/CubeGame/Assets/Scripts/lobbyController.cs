using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class lobbyController : MonoBehaviourPunCallbacks {


    #region Game UI
    public InputField playerNameInputField;

    
    void setPlayerRandomName()
    {
        playerNameInputField.text = "Player " + Random.Range(1000, 10000);
    }

    #endregion



    #region Unity

    private void Awake()
    {
        setPlayerRandomName();
    }


    #endregion


    #region Photon Callbacks

    public void OnLoginButtonClicked()
    {
        string playerName = playerNameInputField.text;

        if (!playerName.Equals(""))
        {
            PhotonNetwork.LocalPlayer.NickName = playerName;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.LogError("Player Name is invalid.");
        }


        //플레이어가 ready!를 누르면 이 버튼이 활성화 되고, 이걸 누르면 게임시작
        //public void OnStartGameButtonClicked()
        {
            //PhotonNetwork.CurrentRoom.IsOpen = false;           //더이상 플레이어들이 참여 못하게
            //PhotonNetwork.CurrentRoom.IsVisible = false;        //더이상 다른 사람에게 로비가 보이지도 않게

            PhotonNetwork.LoadLevel("MainScene"); //겜 시작하면 게임화면 씬 로드
        }
    }

    #endregion
}
