using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public static Inventory instance;

    public Transform slot;

    public List<Slot> slotScripts = new List<Slot>();

    // Use this for initialization


    private void Start()
    {
        // 이거를 instance시키지말고 아예 오브젝트풀로할까 슬롯?

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Transform newSlot = Instantiate(slot);
                newSlot.name = "Slot" + (i + 1) + "." + (j + 1);
                newSlot.parent = transform;
                RectTransform slotRect = newSlot.GetComponent<RectTransform>();


                // 수정사항 -> 이 좌표들을 마우스 클릭받은 그 위치로
                slotRect.anchorMin = new Vector2(0.2f * j + 0.05f, 1 - (0.2f * (i + 1) - 0.05f));
                slotRect.anchorMax = new Vector2(0.2f * (j + 1) - 0.05f, 1 - (0.2f * i + 0.05f));

                slotRect.offsetMin = Vector2.zero;
                slotRect.offsetMax = Vector2.zero;

                slotScripts.Add(newSlot.GetComponent<Slot>());
                newSlot.GetComponent<Slot>().number = i * 5 + j;
            }
        }

        // 만약에 이 위에서 오류나면 밑에 additem이 진행됐는데 itemdatabase의 items가 추가되지 않았을 때ㅐ
        // 그니까 itemdatabase의 add가 발동 안됐을 때 items에 접근하면 비어있는 상태라서 오류발생

        AddItem(0, 1);
        AddItem(1, 1);
        AddItem(1, 99);
        AddItem(1, 5);
    }

    //void AddItem(int number)
    //{


    //    for (int i = 0; i < slotScripts.Count; i++)
    //    {
    //        if (slotScripts[i].item.itemValue == 0)
    //        {

    //            //slotScripts[i], 즉 i 번째 Slot의 itemValue가 0일 때 ItemDatabase의 items에서 number 번째의 아이템을 추가시킴
    //            slotScripts[i].item = ItemDatabase.instance.items[number];
    //            ItemImageChange(slotScripts[i].transform);
    //            // 그니까 만약에 0번 슬롯에 아이템 있으면 건너뛰고 1번 슬롯에 아이템 추가하고 for문 끝냄
    //            break;
    //        }
    //    }
    //}
    void AddItem(int number, int itemCount)
    {

        // 조건에 맞는 슬롯 발견?-> sameItemSlotNumber가 해당 slot이 몇 번째 slot인지 기억.
        // 해당 x - > -1
        // 개수가 999개 미만인 거 찾기


        // 그리고 만약에 999개 넘어간다? 그때는 따로 예외처리 해주기
        // 아직안함 ㅎ
        int sameItemSlotNumber = -1;

        for (int i = 0; i < slotScripts.Count; i++)
        {
            if (slotScripts[i].item.itemName == ItemDatabase.instance.items[number].itemName &&
                slotScripts[i].item.itemCount < 999)
            {

                sameItemSlotNumber = i;
                break;
            }
        }

        if (sameItemSlotNumber == -1)
        {
            for (int i = 0; i < slotScripts.Count; i++)
            {
                // i번째의 slot의 itemvalue가 0일때 itemdatabase의 item에서 number번째의 아이템을 추가시키는 것.

                if (slotScripts[i].item.itemValue == 0)
                {
                    slotScripts[i].item = ItemDatabase.instance.items[number];
                    slotScripts[i].item.itemCount += itemCount;
                    ItemImageChange(slotScripts[i]);
                    break;
                }
            }
        }
        else
        {

            Item item = ItemDatabase.instance.items[number];
            item.itemCount+= itemCount;
            slotScripts[sameItemSlotNumber].item = item;
            ItemImageChange(slotScripts[sameItemSlotNumber]);
        }
    }


    void ItemImageChange(Slot _slot)
    {
        if (_slot.item.itemValue == 0)
        {
            // itemValue==0 -> 아이템이 없을 때.
            // 이미지 게임 오브젝트 비활성화
            _slot.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {

            _slot.transform.GetChild(0).gameObject.SetActive(true);
            _slot.transform.GetChild(0).GetComponent<Image>().sprite = _slot.item.itemImage;

            _slot.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);


            // 조건에 맞는 slot이 없어서 sameItemSlotNumber가 -1일 때.
            // 빈 slot에 아이템을 추가시킨다.
            // else 구문에서는 sameItemSloatNumber 번째의 sloat의 아이템 개수를 증가시킨다.
            // 그리고 이 함수를 사용해서 아이템 개수를 바뀌게 만들어ㅜㅈㅁ

            _slot.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "" + _slot.item.itemCount;
        }
    }

}

// 