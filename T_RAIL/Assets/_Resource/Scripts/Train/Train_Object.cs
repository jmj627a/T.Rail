using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Train_Object : MonoBehaviourPunCallbacks
{


    public float HP { get; set; } //  (기차의 체력) 

    [SerializeField]
    int index; // 몇번째 기차인지 

    Transform tr;

    bool Position_Set_Go; // 포지션을 세팅하면서 달려와서 붙는거. slerp 연산 중에는 true, 연산 끝나면 false
    Vector3 Position_Set_Destination;


    int passenger; // 이 기차에 승객이 몇명있는지
    int box;  // 이 기차에 박스 몇개있는지

   // bool on2Floor; // 2층에 누가 있는지없는지

    float Coroutine_calltime; // 코루틴 안끄곸ㅋㅋㅋㅋ 그 안에 호출할 상황이면 0.01
                              // 호출안할 상황이면 0.5

   // 미리가지고 있어야 할 기차 내부의 메타
    public GameObject Machine_gun;
    public GameObject Ladder;
    public GameObject Ceiling; // 천장

    public GameObject[] choiceInTrainObject;
    [SerializeField]
    public Train_Ctrl ctrl;
    PhotonView photonView;

    // public GameObject Ladder_collider;

    private void Awake()
    {
        ctrl = TrainGameManager.instance.TrainCtrl.GetComponent<Train_Ctrl>();
        photonView = GetComponent<PhotonView>();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        tr = gameObject.transform;
        Coroutine_calltime = 0.5f;
        StartCoroutine(Train_Position_Setting_Change());

        Init_AddTrain();
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

    }

    public void Machine_Gun_OnOff(bool onoff)
    {
        // train_ctrl에서 기차가 새로 add되거나 delete되면서 기차의 index가 변하고
        // 제일 마지막에 있는 머신건만 켜져야하니까 마지막이면 true 아니면 false
        if (onoff)
        {
            Machine_gun.SetActive(true);
        }
        else
        {
            Machine_gun.SetActive(false);
        }
    }
    public void Ceiling_OnOff(bool onoff)
    {
        // 천장 onoff함수
        if (onoff)
        {
            Ceiling.SetActive(true);
        }
        else
        {
            Ceiling.SetActive(false);
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
        // 기차가 slerp 연산을 통해 add가 될 때 자연스럽게 달려오면서 붙는것처럼 하기위한 position_set
        // 코루틴에서 호출중 -> 굳이 update에서 호출할 필요없어서
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

   void Init_AddTrain()
    {

        //train Add부분 한번 호출했으니 여기로 옮김
        ctrl.train.Add(this.gameObject);
        ctrl.trainscript.Add(this);
        TrainGameManager.instance.trainindex = ctrl.train.Count;
        ChangeTrainSetting(ctrl.train.Count);

        if (TrainGameManager.instance.trainindex != 1)
        {
            ctrl.train[TrainGameManager.instance.trainindex - 1].transform.position =
                new Vector3(GameValue.Train_distance * (ctrl.train.Count), GameValue.Train_y, GameValue.Train_z);

        }
        else if (TrainGameManager.instance.trainindex == 1)
        {
            ctrl.train[TrainGameManager.instance.trainindex - 1].transform.position =
                new Vector3(GameValue.Train_distance * (ctrl.train.Count - 1), GameValue.Train_y, GameValue.Train_z);
        }

        this.gameObject.SetActive(true);

        for (int i = 0; i < TrainGameManager.instance.trainindex; i++)
        {
            if (i < TrainGameManager.instance.trainindex - 1)
            {
                ctrl.trainscript[i].Machine_Gun_OnOff(false);
            }
            else
            {
                ctrl.trainscript[i].Machine_Gun_OnOff(true);
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(GameValue.enemy_layer))
        {
            // 기차 hp감소 . enemy의 damage에 따라
        }
    }
}
