﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Actor : Move_Actor {


    public Player_Actor()
    {
        base.Actor_Property = (int)Actor.Player; // property에 player 라고 정의

        speed = 3.0f; // speed 는 km/h 로 따지나 
    }

    public override void Move(char key)
    {
       // base.Move(key);

        if (Actor_State == 1)
        {
            //walk일 때만 이동
            switch (key) {
                case 'a':
                    position.x -= 1f * speed * Time.deltaTime;
                    rotate.y = -90.0f;
                    Direction = 1;
                    break;
                case 's':
                    position.z -= 1f * speed * Time.deltaTime;
                    rotate.y = 180;
                    Direction = 2;
                    break;
                case 'd':
                    position.x += 1f * speed * Time.deltaTime;
                    rotate.y = 90.0f;
                    Direction = 3;
                    break;
                case 'w':
                    position.z += 1f * speed * Time.deltaTime;
                    rotate.y = 0;
                    Direction = 4;
                    break;

            }
        }


 
    }
    public void Jump()
    {
        Debug.Log("3");
        if (Actor_State == 2)
        {
            position.y += 0.05f * speed;
            switch (Direction)
            {
                case 1:
                    // a
                    position.x -= 0.03f * speed;
                    break;
                case 2:
                    //s
                    position.z -= 0.03f * speed;
                    break;
                case 3:
                    //d 
                    position.x += 0.03f * speed;
                    break;
                case 4:
                    //w
                    position.z += 0.03f * speed;
                    break;
            }

        }
    }

    public void Animate_State(int _key)
    {
        Actor_State = _key;

    }


}
