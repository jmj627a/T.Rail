using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public static Inventory instance;

    public Transform slot;

    public List<Slot> slotScripts = new List<Slot>();

    // Use this for initialization


    private void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                Transform newSlot = Instantiate(slot);
                newSlot.name = "Slot" + (i + 1) + "." + (j + 1);
                newSlot.parent = transform;
                RectTransform slotRect = newSlot.GetComponent<RectTransform>();
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

        AddItem(0);
        AddItem(1);
    }

    void AddItem(int number)
    {


        for(int i = 0; i < slotScripts.Count; i++)
        {
            if(slotScripts[i].item.itemValue == 0)
            {

                //slotScripts[i], 즉 i 번째 Slot의 itemValue가 0일 때 ItemDatabase의 items에서 number 번째의 아이템을 추가시킴
                slotScripts[i].item = ItemDatabase.instance.items[number];
                ItemImageChange(slotScripts[i].transform);
                // 그니까 만약에 0번 슬롯에 아이템 있으면 건너뛰고 1번 슬롯에 아이템 추가하고 for문 끝냄
                break;
            }
        }
    }

    void ItemImageChange(Transform _slot)
    {
        if (_slot.GetComponent<Slot>().item.itemValue == 0)
        {
            // itemValue==0 -> 아이템이 없을 때.
            // 이미지 게임 오브젝트 비활성화
            _slot.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            _slot.GetChild(0).gameObject.SetActive(true);
            _slot.GetChild(0).GetComponent<Image>().sprite = _slot.GetComponent<Slot>().item.itemImage;
        }
    }
}
