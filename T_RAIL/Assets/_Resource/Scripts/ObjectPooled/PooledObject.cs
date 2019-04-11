using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject  {

    // 필요한 객체를 요청하고, 사용한 객체를 반환할 때 객체의 이름으로검색
    // 여러 객체들을 저장할 때 리스트 사용 


    public string poolItemName = string.Empty; // 객체 검색시 사용할 이름
    public GameObject prefab = null; // 오브젝트풀에 저장할 프리팹
    public int poolCount = 0; // 초기화할 때 생성할 객체의 수

    [SerializeField]
    List<GameObject> poolList = new List<GameObject>(); // 생성한 객체들을 저장할 리스트

    public void Initialize(Transform parent)
    {
        // pooledobject 객체를 초기화할 때 처음 한번만 호출하고 poolCount에 저장한 수 만큼 객체를 생성해서
        // poolList 리스트에 추가하는 역할. 이렇게 처음에 필요한 객체를 미리 생성해서 리스트에 저장해둔다.
        // parent는 너무 중구난방으로 만들어놓으면 지저분하니까 parent로 들어가라고
        for(int i= 0; i < poolCount; ++i)
        {
            poolList.Add(CreateItem(parent));
        }
    }

    public void PushToPool(GameObject item, Transform parent)
    {
        // 사용한 객체를 다시 오브젝트풀에 반환할 때 사용할 함수
        // 반환할 오브젝트를 item으로 전달하고 부모 정보 필요하면 같이ㅣ 전달

        item.transform.SetParent(parent);
        item.SetActive(false);
        poolList.Add(item);
    }

    public GameObject PopFromPool(Transform parent)
    {
        // 객체가 필요할 때 오브젝트 풀에 요청하는 함수
        // 먼저 저장해 둔 오브젝트가 남아있는지 확인하고 없으면 새로 생성해서 추가
        // 그리고 미리 저장해둔 리스트에서 하나를 꺼내고 이 객체를 반환

        if(poolList.Count == 0)
        {
            poolList.Add(CreateItem(parent));
        }
        GameObject item = poolList[0];
        poolList.RemoveAt(0);
        return item;
    }

    GameObject CreateItem(Transform parent)
    {
        // 프리팹으로 저장된 게임오브젝트 생성
        // 프리팹으로 지정한 정보를 바탕으로 게임오브젝트를 새로 생성하고
        // poolItemName 으로 저장해둔 이름을 새로 만들어준 애 이름으로
        // 만든거 비활성화 시킨다음에 나중에 쓸수있도록함

        GameObject item = Object.Instantiate(prefab);
        item.name = poolItemName;
        item.transform.SetParent(parent);
        item.SetActive(false);

        return item;
    }



}
