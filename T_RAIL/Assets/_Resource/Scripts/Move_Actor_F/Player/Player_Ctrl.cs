using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ctrl : MonoBehaviour {


   
    Player_Actor player;

    Animator anim;

    // Use this for initialization
    void Start()
    {

        player = new Player_Actor();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {

        // 키보드 입력
        Player_key();

        Quaternion rot = Quaternion.identity;
        rot.eulerAngles = new Vector3(player.rotate.x, player.rotate.y, player.rotate.z);
        this.gameObject.transform.position = new Vector3(player.position.x, player.position.y, player.position.z);
        this.gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 5.0f);



     
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
            // 공격 

        }

        if (Input.GetKey(KeyCode.C))
        {
            // 카메라 전환 
        }

    }

    void Player_Animate(int pnum)
    {

    }

    public void Move(char key) {

        player.Move(key);
        player.Animate_State(1); // walk로 state바꾸기
        // 아 c#에서는 enum을 어떻게 써야되는지 모르겠네
        // 좀 다른듯. 그래서 지금 있는 순서 idle,walk,jump,run,attack 유지하면서
        // 더 필요한 state들은 뒤로 추가하는걸로
    }

}
