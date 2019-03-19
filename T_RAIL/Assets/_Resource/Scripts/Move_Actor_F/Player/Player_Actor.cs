using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Actor : Move_Actor {


    public Player_Actor()
    {
        base.Actor_Property = (int)Actor.Player; // property에 player 라고 정의

        speed = 10.0f; // speed 는 km/h 로 따지나 
    }

    public override void Move(char key)
    {
        base.Move(key);

        if (Actor_State == 1)
        {
            //walk일 때만 이동
            switch (key) {
                case 'a':
                    position.x -= 0.01f;
                    
                    break;
                case 's':
                    position.z -= 0.01f;
                    break;
                case 'd':
                    position.x += 0.01f;
                    break;
                case 'w':
                    position.z += 0.01f;
                    break;

            }
        }
    }

    public void Animate_State(int _key)
    {
        Actor_State = _key;

    }


}
