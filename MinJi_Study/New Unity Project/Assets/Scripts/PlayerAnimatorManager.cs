using UnityEngine;
using System.Collections;

using Photon.Pun;

namespace Com.myGame
{
    public class PlayerAnimatorManager : MonoBehaviourPun
    {
        #region MonoBehaviour Callbacks

        private Animator animator;
        // Use this for initialization
        void Start()
        {
            animator = GetComponent<Animator>();
            if(!animator) //애니메이터를 얻어오지 못하면 바로 에러 로그
            {
                Debug.LogError("PlayerAnimatorManager is Missing Animator Component", this);
            }
        }

        // Update is called once per frame
        void Update()
        {
            //client 어플리케이션에서 제어되고 있따면 isMine은 true. (=이 인스턴스가 어플리케이션 내에서 물리적으로 플레이하고있는 사람)
            //IsMine == false (=아무것도 하지 않고 PhotonView 컴포넌트에 의해서만 transform과 전에 설정한 애니메이터 컴포넌트 동기화)
            if(photonView.IsMine == false && PhotonNetwork.IsConnected == true) //IsConnected == true 인건 개발할때 연결없이 테스트하고싶어서
            {
                return;
            }
            //점프
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            //달리는 중이라면
            if(stateInfo.IsName("Base Layer.Run"))
            {
                if(Input.GetButtonDown("Fire2"))
                {
                    animator.SetTrigger("Jump");
                }
            }

            if (!animator)
            {
                return;
            }
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            if(v<0)
            {
                v = 0;
            }
            animator.SetFloat("Speed", h * h + v * v);
            animator.SetFloat("Direction", h, directionDamTime, Time.deltaTime);
        }
        #endregion

        #region Private Fields
        [SerializeField]
        private float directionDamTime = 0.25f;
        #endregion
    }
}