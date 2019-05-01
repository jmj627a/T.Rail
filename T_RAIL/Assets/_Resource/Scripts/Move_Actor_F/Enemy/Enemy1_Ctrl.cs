using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_Ctrl : MonoBehaviour {

    // enemy1 은 ㄷ기차가 달릴 때 뒤에서 따라오는 애들 


    Enemy_Actor enemy;

    Animator anim;

    public int E_damage;


    private void Awake()
    {
        StartCoroutine(Enemy_ActRoutine());
    }
    // Use this for initialization
    void Start()
    { 
        anim = GetComponent<Animator>();

        enemy.speed = 10.0f;  // enemy1은 스피드 기본고정

        enemy.Damage = E_damage;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(GameValue.bullet_layer))
        {
            // 총알맞으면

        }
    }


    // Update is called once per frame
    void Update()
    {

    }


    void OnEnemy1_On()
    {
        // 미리 만들어놔서 on될 때 position 셋팅이 필요하고 달려오는 느낌 



    }


    IEnumerator Enemy_ActRoutine()
    {
        while (true)
        {



            yield return new WaitForSeconds(3.0f);
        }
    }


   


    // 
}
