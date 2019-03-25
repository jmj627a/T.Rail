using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {

    public static ItemDatabase instance;

    public List<Item> items = new List<Item>();

    private void Awake()
    {
        instance = this;
      
    }

    private void Start()
    {
        Add("axe", 1, 500, "GoodAxe");
        Add("food", 1, 50, "food");
        Debug.Log("켜지는데");
    }

    void Add(string itemName, int itemValue, int itemPrice, string itemDesc)
    {
        items.Add(new Item(itemName, itemValue, itemPrice, itemDesc,1, Resources.Load<Sprite>("ItemImages/" + itemName)));
    }
}
