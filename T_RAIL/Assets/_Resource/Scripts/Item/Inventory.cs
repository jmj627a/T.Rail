using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public static Inventory instance;
    public GameObject Item_Inventory;
    public Transform slot;

    public List<Slot> slotScripts = new List<Slot>();

    // Use this for initialization


    private void Start()
    {

        Item_Inventory.SetActive(true); // 꼴보기 싫으니까 꺼놓은 상태에서
        // 스크립트에서 켜주는걸로해ㅎ..; 나중에 켜놓고 이 코드 지워도 되고
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {

                // slot 수정 시 이거 키면 slot 생성.

                //  Transform newSlot = Instantiate(slot);
                //  newSlot.name = "Slot" + (i + 1) + "." + (j + 1);
                // newSlot.parent = Item_Inventory.transform;
                //  RectTransform slotRect = newSlot.GetComponent<RectTransform>();

                //  slotRect.anchorMin = new Vector2(0.2f * j + 0.05f, 1 - (0.2f * (i + 1) - 0.05f));
                //   slotRect.anchorMax = new Vector2(0.2f * (j + 1) - 0.05f, 1 - (0.2f * i + 0.05f));

                //   slotRect.offsetMin = Vector2.zero;
                //   slotRect.offsetMax = Vector2.zero;
                // slotScripts.Add(newSlot.GetComponent<Slot>());
                // newSlot.GetComponent<Slot>().number = i * 5 + j;


                // 제대로 잘 들어감
                Transform newSlot = Item_Inventory.transform.GetChild(i * 5 + j);
                 slotScripts.Add(newSlot.GetComponent<Slot>());
                 newSlot.GetComponent<Slot>().number = i * 5 + j;  
                // 수정사항 추가할 거 -> 애초에 인벤토리 창을 마우스 클릭받은 그 위치로

            }
        }

        // 만약에 이 위에서 오류나면 밑에 additem이 진행됐는데 itemdatabase의 items가 추가되지 않았을 때ㅐ
        // 그니까 itemdatabase의 add가 발동 안됐을 때 items에 접근하면 비어있는 상태라서 오류발생


        Item_Inventory.SetActive(false); // 다 넣고 꺼놓기 클릭할 때마다 true

        // test를 위한 add
        //AddItem(0, 1);
        //AddItem(1, 1);
        //AddItem(1, 99);
        //AddItem(1, 5);
    }

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

    public void Inventory_Off()
    {
        Item_Inventory.SetActive(false);
    }

}
