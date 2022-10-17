using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInventory : MonoBehaviour
{
    public GameObject[] items;
    public GameObject[] itemSlot;

    public void SlotInItem()
    {
        items = GameObject.FindGameObjectsWithTag("Item");
        for (int i = 0; i < ItemManager.itemList.Count; i++)
        {
            itemSlot[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
            itemSlot[i].transform.GetChild(0).GetComponent<Image>().sprite = ItemManager.itemList[i].GetComponent<InItem>().item.ItemSprite;
            itemSlot[i].GetComponent<ItemSlot>().item = ItemManager.itemList[i].GetComponent<InItem>().item;
            if (ItemManager.itemList[i].GetComponent<InItem>().item.ItemQuantity > 1)
            {
                itemSlot[i].transform.GetChild(1).GetComponent<Image>().enabled = true;
                itemSlot[i].transform.GetChild(1).GetChild(0).GetComponent<Text>().enabled = true;
                itemSlot[i].transform.GetChild(1).GetChild(0).GetComponent<Text>().text = ItemManager.itemList[i].GetComponent<InItem>().item.ItemQuantity.ToString();
            }
        }
    }
}
