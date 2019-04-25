using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Train_Ctrl : MonoBehaviourPunCallbacks
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
        Debug.Log("생성됨 ");
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("Train_Add", RpcTarget.All);
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
            Run_Meter += (TrainGameManager.instance.speed * 0.2f) * Time.deltaTime;
    }

    public void TrainSort()
    {

    }

    // 기차 추가하기
    public void onTrainAddButtonClick()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("Train_Add", RpcTarget.All);
            Debug.Log("마스터 클라가 눌럿음 ");
        }
        else
        {
            photonView.RPC("Train_Add", RpcTarget.All);
            Debug.Log("다른 클라가 눌렀음 ");
        }
    }
   


    [PunRPC]
    public void Train_Add()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        if (TrainGameManager.instance.trainindex < GameValue.MaxTrainNumber)
        {
            GameObject newTrain = PhotonNetwork.InstantiateSceneObject(Train_Prefab.name, new Vector3(0, 2.5f, -2), Quaternion.Euler(0, -90, 0),0,null) as GameObject;

            //train.Add(newTrain);
            //TrainGameManager.instance.trainindex = train.Count;
            //train[TrainGameManager.instance.trainindex-1].GetComponent<Train_Object>().ChangeTrainSetting(train.Count);

            //if (TrainGameManager.instance.trainindex != 1)
            //{
            //    train[TrainGameManager.instance.trainindex - 1].transform.position = 
            //        new Vector3(GameValue.Train_distance * (train.Count), GameValue.Train_y, GameValue.Train_z);
            //}
            //else if (TrainGameManager.instance.trainindex == 1)
            //{
            //    train[TrainGameManager.instance.trainindex - 1].transform.position = 
            //        new Vector3(GameValue.Train_distance * (train.Count - 1), GameValue.Train_y, GameValue.Train_z);
            //}

            //newTrain.SetActive(true);

            //// 제일마지막 칸 ㅔ외하고 나머지는 기관총끄기
            ////rpc호출
            //photonView.RPC("Machine_Gun_OnOff_RPC", RpcTarget.All);
        }
    }

    [PunRPC]
    public void Machine_Gun_OnOff_RPC()
    {
        Debug.Log("train index 개수 : " + TrainGameManager.instance.trainindex);
        for (int i = 0; i < TrainGameManager.instance.trainindex; i++)
        {
            Debug.Log("train index 개수 : " + TrainGameManager.instance.trainindex + "현재 개수 : " + i);
            if (i < TrainGameManager.instance.trainindex - 1)
            {
                //photonView.RPC("Machine_Gun_OnOff_RPC", RpcTarget.All, i, false, trainscript);
                Debug.Log("false    train[i] 이름 : " + train[i].name);
                train[i].GetComponent<Train_Object>().Machine_Gun_OnOff(false);
            }
            else
            {
                //photonView.RPC("Machine_Gun_OnOff_RPC", RpcTarget.All, i, true, trainscript);
                //train[i].Machine_Gun_OnOff(true);
                Debug.Log("true    train[i] 이름 : " + train[i].name);
                train[i].GetComponent<Train_Object>().Machine_Gun_OnOff(true);
            }
        }

        //switch (flag)
        //{
        //    case false:
        //        Debug.Log("RPC 함수는 불림 꺼라 @@@@@@@@@@@@@@@" + photonView.ViewID);
        //        _trainscript[i].Machine_Gun_OnOff(false);
        //        break;
        //
        //    case true:
        //        Debug.Log("RPC 함수는 불림 켜라 @@@@@@@@@@@@@@@" + photonView.ViewID);
        //        _trainscript[i].Machine_Gun_OnOff(true);
        //        break;
        //}

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

    /*public void OnSpeedUpButtonClick()
    {
        RPC("SpeedUp", all);
    }*/

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
