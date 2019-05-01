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
    bool Position_Set_Go = false;
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
    }
    public void Enemy1_On()
    {
        // 미리 만들어놔서 on될 때 position 셋팅이 필요하고 달려오는 느낌 
        Debug.Log(TrainGameManager.instance.trainindex - 1);
        Position_Set_Destination = new Vector3((GameValue.Train_distance * (TrainGameManager.instance.trainindex) -12), tr.position.y, tr.position.z);
        Position_Set_Go = true;
    }

    void Position_Set()
    {
        tr.position = Vector3.Slerp(tr.position, Position_Set_Destination, Time.deltaTime * 5.0f);

        if (tr.position.x == Position_Set_Destination.x)
        {
            Position_Set_Go = false;
        }

    }


    IEnumerator Enemy_ActRoutine()
    {
        while (true)
        {

            if (!Position_Set_Go)
            {

            }

            yield return new WaitForSeconds(5.0f);
        }
    }





    // 
}
