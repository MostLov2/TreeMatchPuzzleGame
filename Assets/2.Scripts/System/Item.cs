using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Item",menuName ="Scriptable Object/Item",order = int.MaxValue)]

public class Item : ScriptableObject
{
    public string itemName;
    public string itemExplain;
    public enum ItemType
    {
        SPENDITEM,
        SPENDINIVENTORYITEM
    }
    public ItemType itemType;
    public int ItemQuantity;
    public Sprite ItemSprite;
}
