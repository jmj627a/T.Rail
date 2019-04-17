﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Train_Object : MonoBehaviour
{


    public float HP { get; set; } //  (기차의 체력) 

    [SerializeField]
    int index; // 몇번째 기차인지 

    Transform tr;

    bool Position_Set_Go; // 포지션을 세팅하면서 달려와서 붙는거
    Vector3 Position_Set_Destination;


    int passenger; // 이 기차에 승객이 몇명있는지
    int box;  // 이 기차에 박스 몇개있는지


    float Coroutine_calltime; // 코루틴 안끄곸ㅋㅋㅋㅋ 그 안에 호출할 상황이면 0.01
                              // 호출안할 상황이면 0.5

    // 그러니까 position_set이 trrue가 되고 dest를 확인해서 거기까지 lerp

    public GameObject Machine_gun;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        tr = gameObject.transform;
        Coroutine_calltime = 0.5f;
        StartCoroutine(Train_Position_Setting_Change());
    }

    public Train_Object()
    {
        HP = GameValue.Train_Standard_HP; // 기차의 기본 체력
        Position_Set_Go = true;
        CoroutineCallTimeSet(Position_Set_Go);
        // 처음 기차의 초깃값 설정 
        // 여기로 처음에는 가야돼
    }

    public void Run_TrainHPMinus(float meter)
    {
        // 기차가 달릴수록 체력이 감소
        HP -= meter;
    }

    public void ChangeTrainSetting(int _index)
    {

        SetIndex(_index);

        // 근데 만약에 1번 기차가 떨어지면??


        if (index.Equals(1))
        {
            Position_Set_Go = false;
            // 1번 기차는 그럴 필요가 없어서
            // 일단 꺼놨음

            StopCoroutine(Train_Position_Setting_Change());
            // 코루틴도 껐음 1번기차는
        }
        else
        {
            Position_Set_Go = true;
            Position_Set_Destination = new Vector3(GameValue.Train_distance * (_index - 1),
            GameValue.Train_y, GameValue.Train_z);

        }

        Debug.Log(GameManager.instance.trainindex);


       

    }

    public void Machine_Gun_OnOff(bool onoff)
    {

        if (onoff)
        {
            Machine_gun.SetActive(true);
        }
        else
        {
            Machine_gun.SetActive(false);
        }
    }
    public void SetIndex(int _index)
    {
        index = _index;
    }
    void CoroutineCallTimeSet(bool _Position_Set_Go)
    {
        // position_set_go를 넣을거임
        if (_Position_Set_Go)
        {
            Coroutine_calltime = 0.001f;
        }
        else
        {
            Coroutine_calltime = 0.5f;
        }
    }
    void Position_Set()
    {
        if (Position_Set_Go)
        {

            tr.position = Vector3.Slerp(tr.position, Position_Set_Destination, Time.deltaTime * 30.0f);

            if (tr.position.x == Position_Set_Destination.x)
            {
                Position_Set_Go = false;
                CoroutineCallTimeSet(Position_Set_Go);
            }
        }
    }


    // startcoroutine쓰면서 stopcoroutine쓸거면
    // iEnumerator 변수명
    // 이렇게해서 startcoroutine(변수명)
    // 이렇게 지정해줘야 한다는데 애초에 nullrefer가 뜨는데
    // 그럴거면 그냥 코루틴 게속 켜놓는 게 나을거같음
    IEnumerator Train_Position_Setting_Change()
    {
        while (true)
        {
            Position_Set();

            yield return new WaitForSeconds(0.1f);
        }
    }


}
