using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_Ctrl : MonoBehaviour {

    // enemy1 은 ㄷ기차가 달릴 때 뒤에서 따라오는 애들 


    Enemy_Actor enemy;

    Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

        enemy.speed = 10.0f;  // enemy1은 스피드 기본고정
    }



    // Update is called once per frame
    void Update()
    {

    }

    public void Enemy1_Appear_Condition()
    {
        // enemy1이 등장할 조건같은거

        // 1. 기차의 소음 확률

        //if(GameManager.instance.noise)
        {

        }

        

    }



    // 
}
