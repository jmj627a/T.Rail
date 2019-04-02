using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map_Ctrl : MonoBehaviour
{

    //public Transform Mountain1;
    //public Transform Cloud;
    //public Transform Mountain2;
    //public Transform plane;
    //public Transform RailRoad;


    //Transform Mountain1_1;
    //Transform Mountain1_2;
    //Transform Mountain2_1;
    //Transform Mountain2_2;
    //Transform Cloud_1;
    //Transform Cloud_2;
    //Transform plane_1;
    //Transform plane_2;
    //Transform RailRoad_1;
    //Transform RailRoad_2;


    //// 초기 포지션
    //Vector3[] init_mountain1;
    //Vector3[] init_mountain2;
    //Vector3[] init_cloud;
    //Vector3[] init_plane;
    //Vector3[] init_railroad;

    //List<Transform> map_object = new List<Transform>();

    //public Train_Ctrl TrainCtrl;// 기차에서 속도 받아와야 하므로.

    //public GameObject RunMeterText; // 예시임. 여기말고 나중에 UI용 스크립트 만들어서 그리로 옮길 예정

    //float map_speed; // 맵이 움직이는 스피드

    //float Run_Meter; // 달린 미터. 여기서 계산하는 이유는 여기서 speed해서 맵을 움직이기 때문에

    //void Start()
    //{
    //    // 빈 오브젝트 아래에 있는 움직이는 오브젝트들을 연결
    //    MapObject_GetChild();
    //    // 찾은 오브젝트들을 리스트에 넣기
    //    MapObject_AddList();
    //    // 찾은 오브젝트들 중 1번째 (기차가 달려가는 방향쪽에 있는 게 1번) 오브젝트들의 초기 위치값 찾기
    //    MapObject_InitPosition();

    //}
    //void Find_MapObject()
    //{
    //    // 캐싱안하고 Find하는 건 성능에 매우 좋지않지만.. 
    //    // 혹시나 해서 만든 함수.
    //    // 만약에 얘네도 프리팹으로 만들어야 되면...
    //    // 맵안의 Mountain1, cloud 등등을 프리팹화해서
    //    // instantiate 할 때 
    //    Mountain1 = GameObject.Find("Mountain1").transform;
    //    Mountain2 = GameObject.Find("Mountain2").transform;
    //    Cloud = GameObject.Find("Cloud").transform;
    //    plane = GameObject.Find("Plane").transform;
    //    RailRoad = GameObject.Find("RailRoad").transform;
    //}
    //void MapObject_GetChild()
    //{

    //    Mountain1_1 = Mountain1.GetChild(0);
    //    Mountain1_2 = Mountain1.GetChild(1);

    //    Mountain2_1 = Mountain2.GetChild(0);
    //    Mountain2_2 = Mountain2.GetChild(1);

    //    Cloud_1 = Cloud.GetChild(0);
    //    Cloud_2 = Cloud.GetChild(1);

    //    plane_1 = plane.GetChild(0);
    //    plane_2 = plane.GetChild(1);

    //    RailRoad_1 = RailRoad.GetChild(0);
    //    RailRoad_2 = RailRoad.GetChild(1);
    //}
    //void MapObject_InitPosition()
    //{
    //    init_mountain1[0] = Mountain1_1.position;
    //    init_mountain1[1] = Mountain1_2.position;
    //    init_mountain2[0] = Mountain2_1.position;
    //    init_mountain2[1] = Mountain2_2.position;
    //    init_cloud[0] = Cloud_1.position;
    //    init_cloud[1] = Cloud_2.position;
    //    init_plane[0] = plane_1.position;
    //    init_plane[1] = plane_2.position;
    //    init_railroad[0] = RailRoad_1.position;
    //    init_railroad[1] = RailRoad_2.position;
    //}
    //void MapObject_AddList()
    //{
    //    // 저장된 순서 중요.
    //    // 현재 카메라로부터 먼거리 순서대로 저장해놨음s
    //    map_object.Add(Mountain1_1);
    //    map_object.Add(Mountain1_2);
    //    map_object.Add(Cloud_1);
    //    map_object.Add(Cloud_2);
    //    map_object.Add(Mountain2_1);
    //    map_object.Add(Mountain2_2);
    //    map_object.Add(plane_1);
    //    map_object.Add(plane_2);
    //    map_object.Add(RailRoad_1);
    //    map_object.Add(RailRoad_2);
    //}
    //void MapObject_PositionChange()
    //{
    //    for (int i = 0; i < map_object.Count; i ++)
    //    {
    //        switch (i) {
    //            case 0:
    //                // mountain1_1
    //                if (Mountain1_1.position.Equals(init_mountain1[1]))
    //                {
    //                    Mountain1_1.position = init_mountain1[0];
    //                }

    //                break;
    //            case 1:
    //                // mountain1_2

    //                break;

    //            case 2:
    //                // mountain2_1

    //                break;
    //            case 3:
    //                // mountain2_2

    //                break;
    //            case 4:
    //                // cloud_1

    //                break;
    //            case 5:
    //                //cloud_2

    //                break;

    //            case 6:
    //                // plane_1

    //                break;
    //            case 7:
    //                //plane_2

    //                break;
    //            case 8:
    //                // railroad_1

    //                break;
    //            case 9:
    //                //railroad_2

    //                break;

    //        }

    //    }
    //}

    //void Update()
    //{

    //    // 맵의 스피드는 항상 기차의 스피드를 받아오고 있다
    //    map_speed = TrainCtrl.speed;

    //    for (int i = 0; i < map_object.Count; i += 2)
    //    {
    //        // Mountain1이 제일 멀리 있으니까 제일 느리게 움직여야 해.

    //        // 그리고 기차 멈출 때 속도 자연스럽게 감소하는것도 하고싶은데
    //        // 그럴거면 mathf 써야 되는데
    //        // 이거 local안하면 x,y,z축 맘대로 움직임. 그래서 z축아님
    //        map_object[i].localPosition += new Vector3(0, 0, (map_speed / 20.0f) * ((i + 1) * Time.deltaTime * 2.0f));
    //        map_object[i + 1].localPosition += new Vector3(0, 0, (map_speed / 20.0f) * ((i + 1) * Time.deltaTime * 2.0f));
    //    }

    //    // 나중에 옮길 부분 ㄱ
    //    Run_Meter += map_speed * Time.deltaTime;
    //    RunMeterText.GetComponent<Text>().text = "달린 거리 : " + Run_Meter.ToString("N1") + "M";
    //    // ㄱ

    //}
    public Transform[] Mountain1;
    public Transform[] Cloud;
    public Transform[] Mountain2;
    public Transform[] plane;
    public Transform[] RailRoad;

    // 초기 포지션
    Vector3 init_mountain1;
    Vector3 init_mountain2;
    Vector3 init_cloud;
    Vector3 init_plane;
    Vector3 init_railroad;

    List<Transform> map_object = new List<Transform>();

    public Train_Ctrl TrainCtrl;// 기차에서 속도 받아와야 하므로.

    public GameObject RunMeterText; // 예시임. 여기말고 나중에 UI용 스크립트 만들어서 그리로 옮길 예정

    float map_speed; // 맵이 움직이는 스피드

    float Run_Meter; // 달린 미터. 여기서 계산하는 이유는 여기서 speed해서 맵을 움직이기 때문에

    int[] on_objectindex;
    Vector3[] Object_InitPosition; // 중요! 순서는 저장된 순서대로임!
    void Start()
    {
        // 빈 오브젝트 아래에 있는 움직이는 오브젝트들을 연결
        // 찾은 오브젝트들을 리스트에 넣기
        MapObject_AddList();
        // 찾은 오브젝트들 중 1번째 (기차가 달려가는 방향쪽에 있는 게 1번) 오브젝트들의 초기 위치값 찾기
        //MapObject_InitPosition();

        on_objectindex = new int[map_object.Count / 2]; // 둘중에 어떤게 true 상태인지
    }
  
 
    void MapObject_InitPosition()
    {
        Object_InitPosition = new Vector3[map_object.Count];

        Object_InitPosition[0] = Mountain1[0].position;
        Object_InitPosition[1] = Mountain2[0].position;
        Object_InitPosition[2] =  Cloud[0].position;
        Object_InitPosition[3] =  plane[0].position;
        Object_InitPosition[4] =  RailRoad[0].position;
 
    }
    void MapObject_AddList()
    {
        // 저장된 순서 중요.
        // 현재 카메라로부터 먼거리 순서대로 저장해놨음s
        map_object.Add(Mountain1[0]);
        map_object.Add(Mountain1[1]);
        map_object.Add(Cloud[0]);
        map_object.Add(Cloud[1]);
        map_object.Add(Mountain2[0]);
        map_object.Add(Mountain2[1]);
        map_object.Add(plane[0]);
        map_object.Add(plane[1]);
        map_object.Add(RailRoad[0]);
        map_object.Add(RailRoad[1]);
    }
    void MapObject_PositionChange()
    {
      for(int i = 0; i < map_object.Count; i += 2)
        {
            if (on_objectindex[i / 2].Equals(0))
            {
                // 첫번째꺼가 true
                if(map_object[i].localPosition.z >= 800.0f)
                {
                    map_object[i + 1].localPosition = new Vector3(map_object[i + 1].localPosition.x,
                        map_object[i + 1].localPosition.y, 0);
                    map_object[i + 1].gameObject.SetActive(true);
                    map_object[i].gameObject.SetActive(false);
                    on_objectindex[i / 2] = 1;
                }
               
              
            }
            else if(on_objectindex[i / 2].Equals(1))
            {
                // 두번째꺼가 true인 상태에서
                if (map_object[i + 1].localPosition.z >= 800.0f)
                {
                    map_object[i].localPosition = new Vector3(map_object[i].localPosition.x,
                        map_object[i].localPosition.y, 0);
                    map_object[i].gameObject.SetActive(true);
                    map_object[i + 1].gameObject.SetActive(false);
                    on_objectindex[i / 2] = 0;
                }
                
            }
        }

    }

    void Update()
    {

        // 맵의 스피드는 항상 기차의 스피드를 받아오고 있다
        map_speed = TrainCtrl.speed;

        for (int i = 0; i < map_object.Count; i += 2)
        {
            // Mountain1이 제일 멀리 있으니까 제일 느리게 움직여야 해.

            // 그리고 기차 멈출 때 속도 자연스럽게 감소하는것도 하고싶은데
            // 그럴거면 mathf 써야 되는데
            // 이거 local안하면 x,y,z축 맘대로 움직임. 그래서 z축아님
            if (on_objectindex[i / 2].Equals(0))
            {
                // 첫번째꺼가 true
                map_object[i].localPosition += new Vector3(0, 0, (map_speed / 20.0f) * ((i + 1) * Time.deltaTime * 2.0f));
            }
            else if (on_objectindex[i / 2].Equals(1))
            {
                // 두번째꺼가 true
            map_object[i + 1].localPosition += new Vector3(0, 0, (map_speed / 20.0f) * ((i + 1) * Time.deltaTime * 2.0f));
            }
        }

        MapObject_PositionChange();

        // 나중에 옮길 부분 ㄱ
        Run_Meter += map_speed * Time.deltaTime;
        RunMeterText.GetComponent<Text>().text = "달린 거리 : " + Run_Meter.ToString("N1") + "M";
        // ㄱ

    }
}

// iTween이 과부하가 심하대서 cloud에 iTWeen 뺐음 4/1