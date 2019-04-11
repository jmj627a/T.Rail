using System.Collections;
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
    public float Run_Meter { get; set; } // 달린미터

   
    public GameObject Train_Prefab;
    public List<GameObject> train = new List<GameObject>();
    // 얘를 이렇게 하나 더 한 이유 -> 쓸 때 마다 호출하면 낭비
    public List<Train_Object> trainscript = new List<Train_Object>();
    public Animator[] Wheel_Anim;

    int train_index; // 기차 지금 몇개 붙어있는지
    
    // 기차 처음 시작할 때 슬슬 빨라지는 애니메이션 추가하자
    // Mathf 로 계산해서
    
    private void Awake()
    {
        // 기본 초기화
        Durability = GameValue.Durability;
        // speed = GameValue.speed;
        speed = 0;
        noise = GameValue.noise;
    }

    void Start () {
        Train_Add();

    }

    private void Update()
    {
        Run_Meter += (speed * 0.2f) * Time.deltaTime;
    }

    // 기차 추가하기
    public void Train_Add()
    {
        if  (train_index < GameValue.MaxTrainNumber)
        {
            // 나중에 과부하 너무크면 
            GameObject newTrain = Instantiate(Train_Prefab);
            train.Add(newTrain);
            train_index = train.Count;
            trainscript.Add(train[train_index-1].GetComponent<Train_Object>());
            trainscript[train_index- 1].InitSetting(train.Count);
            // -14만큼 더 멀리 생성됨

            if (train_index != 1)
            {
                train[train_index - 1].transform.position = new Vector3(GameValue.Train_distance * (train.Count),
    GameValue.Train_y, GameValue.Train_z);
            }
            else if(train_index == 1)
            {
                train[train_index - 1].transform.position = new Vector3(GameValue.Train_distance * (train.Count - 1),
    GameValue.Train_y, GameValue.Train_z);
            }


        }
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

    public void RunStartTrain()
    {
        speed = GameValue.speed;
        speed = 10.0f * speed_count;
    }
    public void StopTrain()
    {
        speed = 0.0f;
    }
    public void Wheel_Animation_Speed()
    {
        for (int i = 0; i < Wheel_Anim.Length; i++)
        {
            Wheel_Anim[i].speed = speed / 20;
        }
    }

    // TrainAnimation_all
    void TrainAnimation()
    {
        // no contain Wheel
    }

    public void Train_HPMinus()
    {
        for (int i = 0; i < train.Count; i++)
        {
            //train[i].Run_TrainHPMinus(Run_Meter);
        }
    }

}
