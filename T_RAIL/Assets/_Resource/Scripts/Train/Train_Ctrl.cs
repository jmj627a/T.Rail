using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Train_Ctrl : MonoBehaviour
{


    // 여기서 기차의 속성을 총 감독
    // 기차의 Train_Object에는 기차의 '체력' 을 관리.

    //public float Durability { get; set; } // 기차의 내구도
    //public float speed { get; set; } // 현재 기차가 달리는 스피드 -> 맵에서 사용할거임
    //public float noise { get; set; } // 현재 기차가 내는 소음

    public int InTrain_Passenger; // 기차안에 승객이 몇명있는지

    int speed_count = 1; // 스피드 몇단계인지 스피드 [1~4]단계
    public float Run_Meter { get; set; } // 달린미터


    public GameObject Train_Prefab;
    public List<GameObject> train = new List<GameObject>();
    // 얘를 이렇게 하나 더 한 이유 -> 쓸 때 마다 호출하면 낭비
    public List<Train_Object> trainscript = new List<Train_Object>();
    public Animator[] Wheel_Anim;



    // 기차 처음 시작할 때 슬슬 빨라지는 애니메이션 추가하자
    // Mathf 로 계산해서


    private void Awake()
    {
        // 기본 초기화
        TrainGameManager.instance.Durability = GameValue.Durability;
        // speed = GameValue.speed;
        TrainGameManager.instance.speed = 0;
        TrainGameManager.instance.noise = GameValue.noise;
    }

    void Start()
    {
        Train_Add();
    }

    private void Update()
    {
        Run_Meter += (TrainGameManager.instance.speed * 0.2f) * Time.deltaTime;

    }

    // 기차 추가하기
    public void Train_Add()
    {
        if (TrainGameManager.instance.trainindex < GameValue.MaxTrainNumber)
        {
            // 나중에 과부하 너무크면 
            GameObject newTrain = Instantiate(Train_Prefab);
            train.Add(newTrain);
            TrainGameManager.instance.trainindex = train.Count;
            trainscript.Add(train[TrainGameManager.instance.trainindex - 1].GetComponent<Train_Object>());
            trainscript[TrainGameManager.instance.trainindex - 1].ChangeTrainSetting(train.Count);
            // -14만큼 더 멀리 생성됨

            if (TrainGameManager.instance.trainindex != 1)
            {
                train[TrainGameManager.instance.trainindex - 1].transform.position = new Vector3(GameValue.Train_distance * (train.Count), GameValue.Train_y, GameValue.Train_z);
            }
            else if (TrainGameManager.instance.trainindex == 1)
            {
                train[TrainGameManager.instance.trainindex - 1].transform.position = new Vector3(GameValue.Train_distance * (train.Count - 1), GameValue.Train_y, GameValue.Train_z);
            }

            // 그리고 여기서 add 되는 조건 설정
            // -> 몬스터가 나와있는 상태에서는 add되면 안됨.
            // -> 기차 추가 조건은 따로 함수 만들기
            // -> 기관총에 붙어있는데 add 되면 바꿔야되니까 add되면 기관총설정ㅇ  reset

            // 제일마지막 칸 ㅔ외하고 나머지는 기관총끄기
            for (int i = 0; i < TrainGameManager.instance.trainindex; i++)
            {
                if (i < TrainGameManager.instance.trainindex-1)
                {
                    trainscript[i].Machine_Gun_OnOff(false);
                }
                else
                {
                    trainscript[i].Machine_Gun_OnOff(true);
                }
            }

        }
    }

    public void Train_Delete(int _removeindex)
    {
        // 세상에나..! 기차의 hp가 다 떨어져서 끝났어

        for (int i = TrainGameManager.instance.trainindex - 1; i >= _removeindex; i--)
        {

            // 만약에 removeindex면 뭔가 더 추가적으로 뭘 해야될거같음
            if (i.Equals(_removeindex))
            {
                // 추가적으로 실행할 파티클같은거
                // 얘가 일단 펑 터지고 그 다음에 기차에 불 한다음에 끝에서부터 떨어ㅣ는걸로
            }
            trainscript[i].Machine_Gun_OnOff(false);
            Destroy(train[i]);
            train.RemoveAt(i);
            trainscript.RemoveAt(i);
        }

        trainscript[_removeindex - 1].Machine_Gun_OnOff(true);
    }


    public void SpeedUp()
    {
        if (speed_count < 4)
        {
            speed_count += 1;
        }
        TrainGameManager.instance.speed = 10.0f * speed_count;
    }
    public void SpeedDown()
    {
        if (speed_count > 1)
        {
            speed_count -= 1;
        }
        TrainGameManager.instance.speed = 10.0f * speed_count;
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
        TrainGameManager.instance.speed = GameValue.speed;
        TrainGameManager.instance.speed = 10.0f * speed_count;
    }
    public void StopTrain()
    {
        TrainGameManager.instance.speed = 0.0f;
    }
    public void Wheel_Animation_Speed()
    {
        for (int i = 0; i < Wheel_Anim.Length; i++)
        {
            Wheel_Anim[i].speed = TrainGameManager.instance.speed / 20;
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


    // hp 체크 코루틴
    // 얘를 gamemanger로 해야되나?
}
