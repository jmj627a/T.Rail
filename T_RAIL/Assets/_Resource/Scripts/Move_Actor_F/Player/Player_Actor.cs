using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Actor : Move_Actor {


    public Player_Actor()
    {
        base.Actor_Property = (int)Actor.Player; // property에 player 라고 정의
        position = new Pos(-1, 3.3f, -2.5f);
        speed = 3.0f; // speed 는 km/h 로 따지나 

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
                    position.x -= 0.01f*speed;
                    rotate.y = -90.0f;
                    Direction = 1;
                    break;
                case 's':
                    position.z -= 0.01f * speed;
                    rotate.y = 180;
                    Direction = 2;
                    break;
                case 'd':
                    position.x += 0.01f * speed;
                    rotate.y = 90.0f;
                    Direction = 3;
                    break;
                case 'w':
                    position.z += 0.01f * speed;
                    rotate.y = 0;
                    Direction = 4;
                    break;

            }
        }


 
    }
    

    public void Animate_State(int _key)
    {
        Actor_State = _key;

    }

    public void UpToCeiling()
    {
        // playerctrl에서 천장으로 올라갈 때 호출할 함수.
        // 왜 이름 똑같은거 또 실행하냐? -> 여기서만 움직이는 position 바꾸게
    }
}
