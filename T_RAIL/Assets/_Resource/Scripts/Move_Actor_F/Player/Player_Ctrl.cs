using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightingSystem;



enum player_space_state
{

    Ladder = 1,
    Machine_gun = 2,
    bullet = 3
}


public class Player_Ctrl : MonoBehaviour
{

    // 기본 플레이어에 달린 컴포넌트들
    Player_Actor player;
    Transform tr;
    Animator anim;

    public Color hoverColor = Color.white;
    Highlighter highlighter;
    Transform Near_Object; // 사다리, 머신건 등 space_state로 할 모든 object담기
    Transform gun_child; // 머신건.... 각도 회전하려면 밑에 자식 오브젝트 담아와야 돼서 총전용
    bool stair_up; // 사다리 올라가고 있는 중
 

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

    // ui
    public GameObject Push_Space_UI_pref; // space 누르라고 뜨는 ui. 얘는 프리팹 연결
    GameObject Push_Space_UI; // space 누르라고 뜨는 ui

    /// ////////////////////////////////////////////////////////////////////////
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Use this for initialization
    void Start()
    {
        MCam = Camera.main; // 메인카메라 찾기
        player = new Player_Actor();
        anim = GetComponent<Animator>();
        tr = GetComponent<Transform>();
        Make_PushSpaceUI();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (!stair_up && other.gameObject.layer.Equals(GameValue.ladder_layer))
        {

            if (!near_stair)
            {
                space_state = (int)player_space_state.Ladder;
                Near_Object = other.transform;
                highlighter = Near_Object.GetComponent<Highlighter>();
                near_stair = true;
                Push_Space_UI.SetActive(true);

                Push_Space_UI.transform.position = MCam.WorldToScreenPoint(Near_Object.position) + new Vector3(10, 150, 0);
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
                highlighter = Near_Object.GetComponent<Highlighter>();
                near_gun = true;
                Push_Space_UI.SetActive(true);
                Push_Space_UI.transform.position = MCam.WorldToScreenPoint(Near_Object.position) + new Vector3(10, 100, 0);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
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



    // Update is called once per frame
    void Update()
    {

        // 이 highlight는 나중에 따로 함수로 뺄고야 일단 정리ㅣ되면 빼겟음
        if (near_stair)
        {
            // 근데 이것도 사다리 올라가는 중에는 X 
            highlighter.Hover(hoverColor);
        }
        if (near_gun)
        {
            highlighter.Hover(hoverColor);
        }

        if (!stair_up)
        {
            GetKeyInput();

            Quaternion rot = Quaternion.identity;
            rot.eulerAngles = new Vector3(player.rotate.x, player.rotate.y, player.rotate.z);
            // tr.position = new Vector3(player.position.x, player.position.y, player.position.z);
            tr.position = Vector3.Lerp(tr.position, new Vector3(player.position.x, player.position.y, player.position.z), Time.deltaTime * 10.0f);
            tr.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 5.0f);

        }
        else if (stair_up)
        {
            player.To_UpStair(floor1.position.x);

            tr.position = new Vector3(player.position.x, player.position.y, player.position.z);

            Quaternion rot = Quaternion.identity;
            rot.eulerAngles = new Vector3(player.rotate.x, player.rotate.y, player.rotate.z);
            tr.rotation = rot;


            switch (player.Where_Floor)
            {
                case 1:
                    if (tr.position.y >= floor2.position.y)
                    {
                        stair_up = false;
                        // 파티클도 추가하고 2층으로 올라간 위치에 생기게 해야함
                        player.Where_Floor = 2;
                        anim.SetBool("UpToLadder", false);

                        player.On_Floor2_yPosition();
                        Ceiling_CamSetting();
                    }
                    break;
                case 2:
                    if (tr.position.y <= floor1.position.y)
                    {
                        stair_up = false;
                        // 1층으로 내려와
                        player.Where_Floor = 1;
                        anim.SetBool("UpToLadder", false);

                        player.On_Floor1_yPosition();
                        InTrain_CamSetting();
                    }
                    break;

            }
          
          
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



    }

    // 1층에 올라갔을 때의 키입력 함수
    void Player_key_floor1()
    {
        // 사다리 올라가는 중 아닐때만 가능
        if (!stair_up)
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

                if (space_state.Equals(1))
                {

                    stair_up = true;
                    anim.SetBool("UpToLadder", true);
                    near_stair = false;
                    Push_Space_UI.SetActive(false);

                    floor1 = Near_Object.transform.GetChild(0);
                    floor2 = Near_Object.transform.GetChild(1);

                    
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
        // 이거 space_state로하면 안돼 바꿔
        // 어차피 이 함수느 ㄴ2층에서만 호출됨

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
            if (space_state.Equals(2))
            {
                Debug.Log("앉아");
                // 주변에 머신건이 있으면?
                player.Where_Floor = 3;
                space_state = 0;
                near_gun = false; 
                Push_Space_UI.SetActive(false);
            }
            // 그러고나서  머신건에 고정시켜야됨
        }
    }

    void Player_key_MachinGun()
    {
        // player.where_floor = 3일 때 호출되는 함수.
        //머신건에 앉아있음

        // 카메라 위치 조정
        if (Input.GetKey(KeyCode.A))
        {
            //MCam
        }
        if (Input.GetKey(KeyCode.D))
        {

        }

        // 머신건의 각도 조절 
        if (Input.GetKey(KeyCode.S))
        {
            gun_child.transform.Rotate(0,0,-10.0f*Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            gun_child.transform.Rotate(0, 0, 10.0f * Time.deltaTime);
        }

        // 총알 발사
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }

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


    // 카메라 세팅 변화
    void Ceiling_CamSetting()
    {
        //천장에 올라갔을 때
        MCam.GetComponent<Camera>().fieldOfView = GameValue.Mcam_changeFOV;
        Quaternion rot = Quaternion.identity;
        rot.eulerAngles = new Vector3(GameValue.Mcam_changerot_x, 0, 0);
        MCam.GetComponent<Transform>().rotation = rot;
    }
    void InTrain_CamSetting()
    {
        // 기차 안에서 -> 1층
        MCam.GetComponent<Camera>().fieldOfView = GameValue.Mcam_initFOV;
        Quaternion rot = Quaternion.identity;
        rot.eulerAngles = new Vector3(GameValue.Mcam_initrot_x, 0, 0);
        MCam.GetComponent<Transform>().rotation = rot;
    }
    /// ////////////////////////////////////////////////////////////////////////
    ///// UI

    void Make_PushSpaceUI()
    {
        Push_Space_UI = Instantiate(Push_Space_UI_pref);
        // Push_Space_UI.name = "player1_PushSpace_UI";
        Push_Space_UI.transform.parent = TrainGameManager.instance.Info_Canvas.transform;
    }
}
