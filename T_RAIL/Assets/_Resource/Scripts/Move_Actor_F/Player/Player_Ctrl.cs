using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ctrl : MonoBehaviour {


   
    Player_Actor player;

    // Use this for initialization
    void Start()
    {
        player = new Player_Actor();
    }

    // Update is called once per frame
    void Update () {
        Quaternion rot = Quaternion.identity;
        rot.eulerAngles = new Vector3(player.rotate.x, player.rotate.y, player.rotate.z);
        this.gameObject.transform.position = new Vector3(player.position.x, player.position.y, player.position.z);
        this.gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 5.0f);
        if(player.Actor_State== 2)
        {
            //2는 jump상태
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
    public void Jump()
    {
        player.Animate_State(2);
    }

}
