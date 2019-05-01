using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_Ctrl : MonoBehaviour {

    // 각종조건을 관리하는 스크립트
    // 이걸로 train add, monster 등장 조건 관리


    public GameObject rhino;

    GameObject enemy1;
    private void Awake()
    {
        Init_Make();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void Init_Make()
    {
        // 처음에 만들어놓을 몬스터들이나 기관총 ㅇ총알. 총알 방식은 좀 바꿔야될걳같음 기관총이 생기는거를
        // 기차도 

        enemy1 =Instantiate(rhino);
        enemy1.SetActive(false);
    }



    public void Rhino_Add()
    {
        // 
        enemy1.SetActive(true);
    }

}
