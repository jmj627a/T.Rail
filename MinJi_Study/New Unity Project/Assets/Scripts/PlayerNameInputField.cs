using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

namespace Com.myGame
{
    //플레이어 이름 적는 공간
    [RequireComponent(typeof(InputField))]
    public class PlayerNameInputField : MonoBehaviour
    {
        #region private constants
        const string playerNamePrefKey = "PlayerName";
        #endregion


        #region MonoBehaviour callbacks
        private void Start()
        {
            string defaultName = string.Empty;
            InputField _inputField = this.GetComponent<InputField>();
            if(_inputField != null)
            {
                if(PlayerPrefs.HasKey(playerNamePrefKey))
                {
                    defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                    _inputField.text = defaultName;
                }
            }

            PhotonNetwork.NickName = defaultName;
        }
        #endregion

        #region public methods
        public void SetPlayerName(string value)
        {
            if(string.IsNullOrEmpty(value))
            {
                Debug.LogError("player name is null or empty");
                //value = playerNamePrefKey;
                return;
            }

            PlayerPrefs.SetString(playerNamePrefKey, value); //문자열 값과 함께 저장
        }
        #endregion

    }
}