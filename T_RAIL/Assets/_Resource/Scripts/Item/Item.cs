using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class Item
{

    public string itemName;
    public int itemValue;
    public int itemPrice;
    public string itemDesc;
    public Sprite itemImage;

    public int itemCount;


    public Item(string _itemName, int _itemValue, int _itemPrice, string _itemDesc, int _itemCount,
        Sprite _itemImage)
    {
        itemName = _itemName;
        itemValue = _itemValue;
        itemPrice = _itemPrice;
        itemDesc = _itemDesc; // 아이템 설명
        itemCount = _itemCount;
        itemImage = _itemImage;
    }

    public Item()
    {

    }

}
