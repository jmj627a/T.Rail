using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_Ctrl : MonoBehaviour
{

    // enemy1 은 ㄷ기차가 달릴 때 뒤에서 따라오는 애들 


    Enemy_Actor enemy;
    [SerializeField]
    Transform tr;
    Animator anim;

    public int E_damage;

    Vector3 Position_Set_Destination;
    Vector3 Position_Set_Move;
    bool Position_Set_Go = false;

    public Transform Rhino_child; // 아니 이거 fbx가 이렇게 안잡으면 제대로 안움직임


    Vector3 Init_Rhino_child;
    Vector3 Init_Rhino;

    private void Awake()
    {
        tr = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        StartCoroutine(Enemy_ActRoutine());
        enemy = new Enemy_Actor();
    }
    // Use this for initialization
    void Start()
    {


        Init_Rhino = tr.position;
        Init_Rhino_child = Rhino_child.position;

        enemy.speed = 10.0f;  // enemy1은 스피드 기본고정
        enemy.Damage = E_damage;
        anim.SetBool("IsRun", true);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(GameValue.bullet_layer))
        {
            // 총알맞으면
            enemy.HP -= 5;
        }

        if (other.gameObject.layer.Equals(GameValue.train_layer))
        {
            // 잠깐 뒤로 물러나기

            Debug.Log("기차랑 충돌");
        }
    }

    void Update()
    {
        if (Position_Set_Go)
        {
            Position_Set();
        }
        else
        {
            tr.position = Vector3.Slerp(tr.position, Position_Set_Move, Time.deltaTime);
        }


    }
    public void Enemy1_On()
    {
        Position_Set_Destination = new Vector3((GameValue.Train_distance * (TrainGameManager.instance.trainindex) -12), tr.position.y, tr.position.z);
        Position_Set_Go = true;

        StartCoroutine(Enemy_ActRoutine());
    }

    void Position_Set()
    {
        tr.position = Vector3.Slerp(tr.position, Position_Set_Destination, Time.deltaTime * 5.0f);

        if (tr.position.x == Position_Set_Destination.x)
        {
            Debug.Log("false됐다");
            Position_Set_Go = false;
        }

    }


    IEnumerator Enemy_ActRoutine()
    {
        while (true)
        {

            if (!Position_Set_Go)
            {
                Debug.Log("??");
                // Rhino_child.position -= new Vector3(0, 0, 0.3f);
                Position_Set_Move = new Vector3(tr.position.x + 20 * Time.deltaTime, tr.position.y, tr.position.z);
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    private void OnDisable()
    {
        tr.position = Init_Rhino;
        Rhino_child.position = Init_Rhino_child;
    }

}
