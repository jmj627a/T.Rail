using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightingSystem;
using Photon.Pun;





public class Player_Ctrl : MonoBehaviourPunCallbacks
{
    enum player_space_state
    {

        Ladder = 1,
        Ladder_Down = 2,
        Machine_gun = 3,
        bullet = 4
    }

    // 기본 플레이어에 달린 컴포넌트들
    Player_Actor player;
    Transform tr;
    Animator anim;

    public Color hoverColor = Color.white;
    [SerializeField]
    Highlighter highlighter;
    Transform Near_Object; // 사다리, 머신건 등 space_state로 할 모든 object담기
    Transform gun_child; // 머신건.... 각도 회전하려면 밑에 자식 오브젝트 담아와야 돼서 총전용
    MachineGun_Ctrl gun_ctrl; // 그 머신건에 달린 ctrl 스크립트. 머신건을 받아올 떄 마다 얘도 같이
    bool stair_up; // 사다리 올라가고 있는 중
    bool stair_down; // 사다리 내려가고 있는 중
    bool jump_nextTrain; // 다른칸으로 점프 중

    int space_state = 0; // 기본은 0인데 space가 눌려지는 상황 (highlight되는 모든애들) 에서 state change
    bool near_stair; // 사다리근처
    bool near_gun; // 머신건 근처

    // 사다리
    [SerializeField]
    Transform floor1;
    [SerializeField]
    Transform floor2;


    // render 
    Camera MCam; // maincamera
    CamCtrl MCam_Ctrl; // 카메라에 달린 camctrl

    // ui
    public GameObject Push_Space_UI_pref; // space 누르라고 뜨는 ui. 얘는 프리팹 연결
    GameObject Push_Space_UI; // space 누르라고 뜨는 ui


    // particle

    public GameObject parti_player_move;


    /// ////////////////////////////////////////////////////////////////////////
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        player = new Player_Actor();

        Make_PushSpaceUI();
        Init_Set_Value();
    }


    void Init_Set_Value()
    {
        // 파티클찾기
        //  parti_player_move = this.transform.GetChild(0).gameObject;
        MCam = Camera.main; // 메인카메라 찾기
        MCam_Ctrl = MCam.GetComponent<CamCtrl>();
        anim = GetComponent<Animator>();
        tr = GetComponent<Transform>();


    }

    private void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine) return;

        if (!stair_up && !stair_down)
        {

            if (!near_stair && other.gameObject.layer.Equals(GameValue.ladder_layer))
            {
                space_state = (int)player_space_state.Ladder;
                Near_Object = other.transform;
                highlighter = Near_Object.GetComponent<Highlighter>();
                near_stair = true;
                Push_Space_UI.SetActive(true);

                Push_Space_UI.transform.position = MCam.WorldToScreenPoint(Near_Object.position) + new Vector3(10, 150, 0);
            }
            if (other.gameObject.layer.Equals(GameValue.floor2_layer))
            {
                // 내려갈때 쓸거얌
                space_state = (int)player_space_state.Ladder_Down;

            }
        }

        if (other.gameObject.layer.Equals(GameValue.machinegun_layer))
        {
            // 머신건 근처
            if (!near_gun)
            {
                space_state = (int)player_space_state.Machine_gun;
                Near_Object = other.transform;
                gun_child = other.transform.GetChild(0);
                gun_ctrl = gun_child.GetComponent<MachineGun_Ctrl>();
                highlighter = Near_Object.GetComponent<Highlighter>();
                near_gun = true;
                Push_Space_UI.SetActive(true);
                Push_Space_UI.transform.position = MCam.WorldToScreenPoint(Near_Object.position) + new Vector3(-20, 130, 0);
            }
        }
        // 자꾸 사다리가 더 먼저 걸린다 


        // 다음칸 trigger
        if (other.gameObject.layer.Equals(GameValue.NextTrain_layer)) {

            if(player.Where_Train < GameValue.machinegun_layer)
            {
                Debug.Log("?");
                anim.SetBool("IsJump", true);
                jump_nextTrain = true;
            }

        }

        // 이전칸 trigger
        if (other.gameObject.layer.Equals(GameValue.PrevTrain_layer)) {
            
            if(player.Where_Train != 0)
            {
                anim.SetBool("IsJump", true);
                jump_nextTrain = true;
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!photonView.IsMine) return;

        // 사다리! 올라가는 중이 아니며 
        if (!stair_up && other.gameObject.layer.Equals(GameValue.ladder_layer))
        {
            if (near_stair)
            {
                space_state = 0;
                near_stair = false;
                Push_Space_UI.SetActive(false);
            }
        }

        // 머신건 근처
        if (other.gameObject.layer.Equals(GameValue.machinegun_layer))
        {
            if (near_gun)
            {
                space_state = 0;
                near_gun = false;
                Push_Space_UI.SetActive(false);
            }
        }
    }


    private Vector3 CurPos = new Vector3(-1, 3.3f, -2.5f);
    private Quaternion CurRot = Quaternion.identity;//네트워크에서는 선언과 동시에 초기화해야한다 

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return;

        // 이 highlight는 나중에 따로 함수로 뺄고야 일단 정리ㅣ되면 빼겟음
        if (near_stair)
        {
            // 근데 이것도 사다리 올라가는 중에는 X 
            highlighter.Hover(hoverColor);
        }

         // 키입력
        GetKeyInput();

        if (stair_up)
        {
            // 사다리 오르고 있을 때
            player.To_UpStair(floor1.position.x);

            tr.position = new Vector3(player.position.x, player.position.y, player.position.z);

            Quaternion rot = Quaternion.identity;
            rot.eulerAngles = new Vector3(player.rotate.x, player.rotate.y, player.rotate.z);
            tr.rotation = rot;

            if (tr.position.y >= floor2.position.y)
            {
                stair_up = false;
                // 파티클도 추가하고 2층으로 올라간 위치에 생기게 해야함
                player.Where_Floor = 2;


                anim.SetBool("UpToLadder", false);

                player.On_Floor2_yPosition();
                MCam_Ctrl.uptoCeiling();

                // trainmanager의 trainctrl에 연결해서 그 컨트롤의 list 중 trainscript에서 천장 onoff 변경
                TrainGameManager.instance.TrainCtrl.trainscript[player.Where_Train - 1].Ceiling_OnOff(true);

                //false는 여기가 아니고 space눌렀을 때ㄹ.
            }

        }
        if (stair_down)
        {
            // 분명 up과 다른건 +,- 뿐인데
            // 얘는 제대로 안됨 왜일까?
            player.To_DownStair(floor2.position.x);
            tr.position = new Vector3(player.position.x, player.position.y, player.position.z);

            Quaternion rot = Quaternion.identity;
            rot.eulerAngles = new Vector3(player.rotate.x, player.rotate.y, player.rotate.z);
            tr.rotation = rot;
            // anim.speed = -1;


            if (tr.position.y <= floor1.position.y)
            {
                stair_down = false;
                // 1층으로 내려와
                player.Where_Floor = 1;

                anim.speed = 1;
                anim.SetBool("UpToLadder", false);
                player.On_Floor1_yPosition();
                MCam_Ctrl.inTrain();
            }
        }

        // 점프하고 있을 떄 
        if (jump_nextTrain)
        {
            // 일단 가는방향 받아와야 하고

            // 여기서 계속 증가하고 

            if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
            {
                // 다시 
                // 여기 들어오면 뭐지? 그 걸어다니던것? 그런것도 다 멈ㅇ춰야되네
                anim.SetBool("IsJump",false);

                jump_nextTrain = false;


                // 여기서 이제 player가 존재하는 기차의 인덱스가 몇번인지도 넘겨주기
            }

            
        }

            

        
        // 카메라에 플레이어가 몇층에 있는지 전달 
        MCam_Ctrl.Change_floor(player.Where_Floor);


        switch (player.Where_Floor)
        {

            case 1:
                // 1층에서 칸 이동이나 그런거할 떄
                
                // 플레이어의 x좌표를 전달해줌(카메라 이동관련)
                MCam_Ctrl.GetPlayerX(player.position.x);
                
                break;
            case 2:
            case 3:
                // 마우스 이동에 따른 카메라 변환
                // 뚜껑에서
                MCam_Ctrl.GetPlayerX(player.position.x);
                break;

        }


    }


    /// ////////////////////////////////////////////////////////////////////////
    // key 관련

    void GetKeyInput()
    {
        switch (player.Where_Floor)
        {
            // 플레이어가 몇 층에 있는지에 따라서 입력을 다르게 받을 거라서.
            // where_floor -> player가 이게 3이면 기관총에 앉아잇는거
            case 1:
                // 1층
                Player_key_floor1();
                break;
            case 2:
                // 2층
                Player_key_floor2();
                break;
            case 3:

                // player가 머신건 근처에 있으면 space_state가 2가 되고
                // 2층에 있을 때 머신건 근처에서 스페이스를 누르면 where_floor가 3됨
                Player_key_MachinGun();
                break;
            default:
                break;
        }

        Quaternion rot = Quaternion.identity;
        rot.eulerAngles = new Vector3(player.rotate.x, player.rotate.y, player.rotate.z);
        // tr.position = new Vector3(player.position.x, player.position.y, player.position.z);
        tr.position = Vector3.Lerp(tr.position, new Vector3(player.position.x, player.position.y, player.position.z), Time.deltaTime * 10.0f);
        tr.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 5.0f);

    }

    // 1층에 올라갔을 때의 키입력 함수
    void Player_key_floor1()
    {
        // 사다리 올라가는 중 아닐때만 가능
        if (!stair_up && !jump_nextTrain)
        {
            if (Input.GetKey(KeyCode.A))
            {
                Move('a');
                anim.SetBool("IsWalk", true);
            }
            if (Input.GetKey(KeyCode.D))
            {
                Move('d');
                anim.SetBool("IsWalk", true);
            }
            if (Input.GetKey(KeyCode.S))
            {
                Move('s');
                anim.SetBool("IsWalk", true);
            }
            if (Input.GetKey(KeyCode.W))
            {
                Move('w');
                anim.SetBool("IsWalk", true);
            }

            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) ||
                Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W))
            {
                anim.SetBool("IsWalk", false);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                // 사다리 가까이서 space누르면 올라가기 == 1
                // 기관총 앞에서 space누르면 기관총에 장착되기 == 2

                if (space_state.Equals((int)player_space_state.Ladder))
                {
                    stair_up = true;
                    anim.SetBool("UpToLadder", true);
                    near_stair = false;
                    Push_Space_UI.SetActive(false);

                    floor1 = Near_Object.transform.GetChild(0);
                    floor2 = Near_Object.transform.GetChild(1);

                    //parti_player_move.SetActive(true);
                    // Instantiate(parti_player_move, tr.position+ Vector3.up*2.0f, Quaternion.identity);
                    // 그러고 나서사다리 끝나면
                    // 올라가고 stair_up = false; 하고
                    // 천장에 올라가면 뚜껑도 setactive.true해줘야되네
                }


            }
        }

    }

    // 2층에 올라갔을 때의 키입력 함수
    void Player_key_floor2()
    {
        if (!stair_up && !stair_down)
        {
            // 그냥 2층으로 올라온 상태
            if (Input.GetKey(KeyCode.A))
            {
                // x = -1
                // 임시 이동
                Move('a');
                anim.SetBool("IsWalk", true);

            }
            if (Input.GetKey(KeyCode.D))
            {
                // x = +1  
                Move('d');
                anim.SetBool("IsWalk", true);
            }

            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) ||
                Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W))
            {
                anim.SetBool("IsWalk", false);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                // 머신건에 앉기
                if (space_state.Equals((int)player_space_state.Machine_gun))
                {
                    // 주변에 머신건이 있으면?
                    player.Where_Floor = 3;
                    space_state = 0;
                    near_gun = false;
                    Push_Space_UI.SetActive(false);
                }

                // 밑층으로 내려가기
                if (space_state.Equals((int)player_space_state.Ladder_Down))
                {
                    TrainGameManager.instance.TrainCtrl.trainscript[player.Where_Train - 1].Ceiling_OnOff(false);
                    anim.SetBool("UpToLadder", true);
                    stair_down = true;
                }
            }
        }
    }

    void Player_key_MachinGun()
    {
        // player.where_floor = 3일 때 호출되는 함수.
        //머신건에 앉아있음

        // 카메라 위치 조정

        if (Input.GetKey(KeyCode.D))
        {
            // 기관총에서 벗어나자!
            player.Where_Floor = 2;
        }
        // 

        // 머신건의 각도 조절 
        if (Input.GetKey(KeyCode.S))
        {
            gun_ctrl.gun_down();
        }
        if (Input.GetKey(KeyCode.W))
        {
            gun_ctrl.gun_up();
        }

        // 총알 발사
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gun_ctrl.gun_fire(); // 아직x 
        }

        // 카메라 조절은 마우스로

    }

    public void Move(char key)
    {
        player.Move(key);
        player.Animate_State(1); // walk로 state바꾸기
        // 아 c#에서는 enum을 어떻게 써야되는지 모르겠네
        // 좀 다른듯. 그래서 지금 있는 순서 idle,walk,jump,run,attack 유지하면서
        // 더 필요한 state들은 뒤로 추가하는걸로
    }


    /// ////////////////////////////////////////////////////////////////////////
    ///// UI

    void Make_PushSpaceUI()
    {
        Push_Space_UI = Instantiate(Push_Space_UI_pref);
        // Push_Space_UI.name = "player1_PushSpace_UI";
        Push_Space_UI.transform.parent = TrainGameManager.instance.Info_Canvas.transform;
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {//원격 네트워크 플레이어가 만든 탱크의 위치와 회전 정보를 실시간으로 동기화하도록 하는 콜백 함수
     //(쉽게 말해서 다른 유저의 실시간 위치와 회전 값이 보이게 하는것)
        if (stream.IsWriting)
        {//신호를 보낸다. 송신 로컬플레이의 위치 정보 송신(패킷을 날린다고 표현)
            stream.SendNext(tr.position);
            stream.SendNext(tr.rotation);
        }
        else // 원격 플레이어의 위치 정보 수신
        {
            CurPos = (Vector3)stream.ReceiveNext();
            CurRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
