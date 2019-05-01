using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player_Actor : Move_Actor {


    public Player_Actor()
    {
        base.Actor_Property = (int)Actor.Player; // property에 player 라고 정의

        //position = new Pos(-1, 3.8f, -2.5f);
        //플레이어 생성 위치 
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; ++i)
        {
            if (PhotonNetwork.PlayerList[i].NickName == PhotonNetwork.LocalPlayer.NickName)
            {
                position = new Pos(-1 * i * 2, 3.8f, -2.5f);
            }
        }

        speed =15.0f; // speed 는 km/h 로 따지나 

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
                    position.x -= 0.1f*speed*Time.deltaTime;
                    rotate.y = -90.0f;
                    Direction = 1;
                    break;
                case 's':
                    position.z -= 0.1f * speed * Time.deltaTime;
                    rotate.y = 180;
                    Direction = 2;
                    break;
                case 'd':
                    position.x += 0.1f * speed * Time.deltaTime;
                    rotate.y = 90.0f;
                    Direction = 3;
                    break;
                case 'w':
                    position.z += 0.1f * speed * Time.deltaTime;
                    rotate.y = 0;
                    Direction = 4;
                    break;

            }
        }


 
    }
    
    public void To_UpStair(float _x)
    {
        // 윗층으로
        position.x = _x;
        position.y += 0.01f * Time.deltaTime * 100.0f;
        rotate.y = 180;
        Direction = 3; // 근데 이거 direction 어따쓰려고 만들어뒀더라
    }

    public void To_DownStair(float _x)
    {
        // 아랫층으로
        // 이건 floor2의 x 받아와야함
        position.x = _x;
        position.y -= 0.01f * Time.deltaTime * 100.0f;
        rotate.y = 180;
        Direction = 3; // 근데 이거 direction 어따쓰려고 만들어뒀더라
    }

    public void Animate_State(int _key)
    {
        Actor_State = _key;

    }



    public void On_Floor2_yPosition()
    {
        position.y = GameValue.player_2f_position_y;
    }
    public void On_Floor1_yPosition()
    {
        position.y = GameValue.player_1f_position_y;
    }


    public void Jump_ToNextTrain()
    {
        // 여기서 해야될거 position 증가 
        position.x -= 0.2f * speed * Time.deltaTime;
        rotate.y = -90.0f;
        Direction = 1;


    }

    public void Jump_ToPrevTrain()
    {
        // 여기서 해야 할것 position - 
        position.x += 0.2f * speed * Time.deltaTime;
        rotate.y = 90.0f;
        Direction = 3;

    }

    public void Jump_NextTrain(bool prev, bool next)
    {
        // prev, next bool 변수를 ctrl에서 받아서 prev가 true이면 jump_toprevtrain 호출하고
        // next가 true이면 jump_tonexttrain 호출

        if (prev)
        {
            Jump_ToPrevTrain();
        }

        else if (next)
        { 
            Jump_ToNextTrain();
        }
    }

}
