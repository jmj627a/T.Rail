using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Actor : Move_Actor {


    public Player_Actor()
    {
        base.Actor_Property = (int)Actor.Player; // property에 player 라고 정의
        position = new Pos(-1, 3.3f, -2.5f);
        speed =100.0f; // speed 는 km/h 로 따지나 

       Where_Train = 1;
       Where_Floor = 1; // 처음에는 1층, 1번째칸에 존재하니까 

    }


    // 서버잘한다~~ 화ㅏ이팅 >_<~~

    public override void Move(char key)
    {
       // base.Move(key);

        if (Actor_State == 1)
        {
            //walk일 때만 이동
            switch (key) {
                case 'a':
                    position.x -= 0.01f*speed*Time.deltaTime;
                    rotate.y = -90.0f;
                    Direction = 1;
                    //RPC("down", all, position)
                    break;
                case 's':
                    position.z -= 0.01f * speed * Time.deltaTime;
                    rotate.y = 180;
                    Direction = 2;
                    break;
                case 'd':
                    position.x += 0.01f * speed * Time.deltaTime;
                    rotate.y = 90.0f;
                    Direction = 3;
                    break;
                case 'w':
                    position.z += 0.01f * speed * Time.deltaTime;
                    rotate.y = 0;
                    Direction = 4;
                    break;

            }
        }


        //RPC("move", all, position)
    }
    
    public void To_UpStair(float _x)
    {
        // 윗층으로
        Debug.Log(_x);
        position.x = _x;
        position.y += 0.005f * Time.deltaTime * 100.0f;
        rotate.y = 180;
        Direction = 3; // 근데 이거 direction 어따쓰려고 만들어뒀더라
    }

    public void To_DownStair(float _x)
    {
        // 아랫층으로
        // 이건 floor2의 x 받아와야함
    }

    public void Animate_State(int _key)
    {
        Actor_State = _key;

    }



    public void On_Floor2_yPosition()
    {
        position.y = GameValue.player_2f_position_y;
    }
}
