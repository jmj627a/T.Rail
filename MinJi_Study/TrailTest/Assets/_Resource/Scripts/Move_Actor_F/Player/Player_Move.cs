using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Player_Move : MonoBehaviourPunCallbacks
{

// 플레이어 움직일라고 키 입력받는 스크립트입니다요



    private PhotonView photonView;
    Animator anim; // player animator component
    Player_Ctrl pc;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }
    private void Start()
    {
        pc = this.GetComponent<Player_Ctrl>();

        anim = this.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update () {

        if (!photonView.IsMine)// || !controllable)
        {
            return;
        }

        if (Input.GetKey(KeyCode.A))
        {
            // x = -1
            // 임시 이동
            pc.Move('a');
            anim.SetBool("IsWalk", true);
 
        }
        if (Input.GetKey(KeyCode.D))
        {
            // x = +1  
            pc.Move('d');
            anim.SetBool("IsWalk", true);
        }
        if (Input.GetKey(KeyCode.S))
        {
            // y = -1
            pc.Move('s');
            anim.SetBool("IsWalk", true);
        }
        if (Input.GetKey(KeyCode.W))
        {
            // y = +1
            pc.Move('w');
            anim.SetBool("IsWalk", true);
        }

        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) ||
            Input.GetKeyUp(KeyCode.D)|| Input.GetKeyUp(KeyCode.W))
        {
            anim.SetBool("IsWalk", false);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            // 공격

            
        }

            ///////// 일단 점프 보류!
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    // Jump
        //    pc.Jump();
        //    anim.SetBool("IsJump", true);
        //    Debug.Log("jump");

        //}

        if (Input.GetKey(KeyCode.C))
        {
            // 카메라 전환 
        }


      
    }
}
