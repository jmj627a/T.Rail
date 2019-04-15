using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightingSystem;



enum player_space_state {

    Ladder = 1,
    Machine_gun =2

}


public class Player_Ctrl : MonoBehaviour
{
    public Camera camera;
    Player_Actor player;

    Animator anim;
    public Color hoverColor = Color.white;
    Highlighter highlighter;
    Transform Ladder; // 사다리데스
    bool stair_up; // 사다리 올라가고 있는 중


    int space_state = 0; // 기본은 0인데 space가 눌려지는 상황 (highlight되는 모든애들) 에서 state change
    bool near_stair; // 사다리근처

    public GameObject Push_Space_UI; // space 누르라고 뜨는 ui

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Use this for initialization
    void Start()
    {
        player = new Player_Actor();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!stair_up)
        {
            if (other.gameObject.layer.Equals(GameValue.ladder_layer))
            {

                if (!near_stair)
                {
                    space_state = (int)player_space_state.Ladder;
                    Ladder = other.transform;
                    highlighter = Ladder.GetComponent<Highlighter>();
                    near_stair = true;
                    Push_Space_UI.SetActive(true);

                    Push_Space_UI.transform.position = camera.WorldToScreenPoint(Ladder.position) + new Vector3(10, 150, 0);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!stair_up)
        {
            if (other.gameObject.layer.Equals(GameValue.ladder_layer))
            {
                // 사다리!
                if (near_stair)
                {
                    space_state = 0;
                    near_stair = false;
                    Push_Space_UI.SetActive(false);
                }
            }
        }
    }



    // Update is called once per frame
    void Update()
    {



        // 키보드 입력
        Player_key();

        Quaternion rot = Quaternion.identity;
        rot.eulerAngles = new Vector3(player.rotate.x, player.rotate.y, player.rotate.z);
        this.gameObject.transform.position = new Vector3(player.position.x, player.position.y, player.position.z);
        this.gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 5.0f);

        if (near_stair)
        {
            // 근데 이것도 사다리 올라가는 중에는 X 
            highlighter.Hover(hoverColor);
        }



    }

    void Player_key()
    {

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
        if (Input.GetKey(KeyCode.S))
        {
            // y = -1
            Move('s');
            anim.SetBool("IsWalk", true);
        }
        if (Input.GetKey(KeyCode.W))
        {
            // y = +1
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

            switch (space_state)
            {
                case 0:
                    stair_up = false; // 혹시몰라서 초기화해놓는거 -> 무의미함
                    break;
                case 1:
                    // 사다리
                    stair_up = true;
                    anim.SetBool("UpToLadder", true);
                    near_stair = false;
                    Push_Space_UI.SetActive(false);

                    // 그러고 나서사다리 끝나면
                    // 올라가고 stair_up = false; 하고
                    // 천장에 올라가면 뚜껑도 setactive.true해줘야되네
                    break;
                case 2:
                    // 기관총
                    break;
            }

        }

        if (Input.GetKey(KeyCode.C))
        {
            // 카메라 전환 
        }

    }

    void Player_Animate(int pnum)
    {

    }

    public void Move(char key)
    {

        player.Move(key);
        player.Animate_State(1); // walk로 state바꾸기
        // 아 c#에서는 enum을 어떻게 써야되는지 모르겠네
        // 좀 다른듯. 그래서 지금 있는 순서 idle,walk,jump,run,attack 유지하면서
        // 더 필요한 state들은 뒤로 추가하는걸로
    }

    public void UpToCeiling()
    {
        // 천장으로 올라갈 때 호출되는 함수
        player.UpToCeiling();
    }
}
