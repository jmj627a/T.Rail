﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Train_Ctrl : MonoBehaviour {


    // 여기서 기차의 속성을 총 감독
    // 기차의 Train_Object에는 기차의 '체력' 을 관리.

    public float Durability { get; set; } // 기차의 내구도
    public float speed { get; set; } // 현재 기차가 달리는 스피드 -> 맵에서 사용할거임
    public float noise { get; set; } // 현재 기차가 내는 소음

    public int InTrain_Passenger; // 기차안에 승객이 몇명있는지

    int speed_count=1; // 스피드 몇단계인지 스피드 [1~4]단계

    public List<Train_Object> train = new List<Train_Object>();
    public GameObject Train_Prefab;

    

    
    private void Awake()
    {
        // 기본 초기화
        Durability = GameValue.Durability;
        speed = GameValue.speed;
        noise = GameValue.noise;
    }

    void Start () {
        
	}
	
    // 기차 추가하기
    public void Train_Add()
    {
        // 사실 얘는 이렇게 add하면 안된다..
        // instantiate로 기차 생성하고 그 기차에 달린 trainobject를 넣어야해..
        // 근데 ㅇㅏ직 그거 필요업스니 안한다.
        train.Add(new Train_Object());
    }

    public void SpeedUp()
    {
        if (speed_count < 4)
        {
            speed_count += 1;
        }
        speed = 10.0f*speed_count;
    }
    public void SpeedDown()
    {
        if (speed_count > 1)
        {
            speed_count -= 1;
        }
        speed = 10.0f * speed_count;
    }

    public void Passenger_In()
    {
        // 역에서 승객을 태울 때
        InTrain_Passenger += 1;
    }
    public void Passenger_Out()
    {
        // 역에서 승객을 내리게 할때
        InTrain_Passenger -= 1;
    }

}
