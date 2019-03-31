using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Com.myGame
{
    public class PlayerUI : MonoBehaviour
    {
        #region Private Fields

        [Tooltip("UI Text to display Player's Name")]
        [SerializeField]
        private Text playerNameText;

        [Tooltip("UI Slider to display Player's Health")]
        [SerializeField]
        private Slider playerHealthSlider;

        PlayerManager target;
        float characterControllerHeight = 0f;
        Transform targetTransform;
        Vector3 targetPosition;
        #endregion

        #region Public Fields
        [Tooltip("Pixel offset from the player target")]
        [SerializeField]
        private Vector3 screenOffset = new Vector3(0f, 30f, 0f);
        #endregion

        #region MonoBehaviour CallBacks
        private void Awake()
        {
            this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
        }
        void Update()
        {
            // 체력 반영
            if (playerHealthSlider != null)
            {
                playerHealthSlider.value = target.Health;
            }

            //타겟이 null이라면 삭제. 이렇게 해야 photon이 네트워크를 통해 인스턴스를 제거할때 안전
            if(target == null)
            {
                Destroy(this.gameObject);
                return;
            }
        }
        #endregion


        #region Public Methods
        public void SetTarget(PlayerManager _target)
        {
            if (_target == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
                return;
            }
           
            target = _target;
            CharacterController _characterController = _target.GetComponent<CharacterController>();
            // Get data from the Player that won't change during the lifetime of this Component
            // 플레이어로부터 데이터를 가져오기. 컴포넌트가 살아있는동안 변화가 없었다면.
            if (_characterController != null)
            {
                characterControllerHeight = _characterController.height;
            }

            if (playerNameText != null)
            {
                playerNameText.text = target.photonView.Owner.NickName;
            }
        }

        void LateUpdate()
        {
            // #Critical
            // 씬에서 타겟을 따라가기. 2d와 3d위치를 맞추기 위해 worldToScreenPoint 함수 이용
            if (targetTransform != null)
            {
                targetPosition = targetTransform.position;
                targetPosition.y += characterControllerHeight;
                this.transform.position = Camera.main.WorldToScreenPoint(targetPosition) + screenOffset;
            }
        }
        #endregion


    }
}