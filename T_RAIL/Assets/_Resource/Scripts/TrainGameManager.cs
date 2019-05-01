using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainGameManager : MonoBehaviour {

    public static TrainGameManager instance = null;

    // 여기서 각 플레이어들 체력 관리해야되나?
    // 여기서 뭐해야될까

    // 아 머리깨지겠네 진짜 스트ㅡ레스당


    public float Durability { get; set; } // 기차의 내구도
    public float speed { get; set; } // 현재 기차가 달리는 스피드 -> 맵에서 사용할거임
    public float noise { get; set; } // 현재 기차가 내는 소음

    public int trainindex; // 지금 기차 몇개 붙어있는지
                           // 몇개 붙어있는지 가지고 제일 마지막 위치 -> 기관총
                           // 제일 마지막 위치 -> enemy1 


    // # TrainCtrl
    public Train_Ctrl TrainCtrl;

    public GameObject InGame_Notice; // 게임 내에서의 알림사항 ex) 몬스터 등장
    public Text InGame_Text;
    // # UI
    public GameObject Info_Canvas;

    private void Awake()
    {
       instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Error_print()
    {
        Debug.Log("Error Somewhere");
    }


    public void Notice_EnemyAppear()
    {
        InGame_Notice.SetActive(true);
        InGame_Text.text = "코뿔소 등장 ! ";
    }
}
