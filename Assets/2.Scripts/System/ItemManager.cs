using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
[System.Serializable]
public class ItemInfo
{
    public int user_num;
    public string meta_id;
    public string game_id;
    public int goldKey;
    public int heartPotion;
    public int statusChangePotion;
}
public class ItemManager : MonoBehaviour
{
    public ItemInfo itemInfo;
    public static int heartPotionCount;
    public static int statusPotionCount;
    public static int goldKeyCount;
    public static List<GameObject> itemList;
    public static ItemManager instance;
    private void Awake()
    {
        instance = this;
        itemList = new List<GameObject>();
        StartCoroutine(GetItem());
    }
    public IEnumerator GetItem()
    {
        WWWForm www = new WWWForm();
        www.AddField("loginUser", MySqlSystem.localID);
        www.AddField("status", "getItem");
        using (UnityWebRequest w = UnityWebRequest.Post("https://conbox.kr/Order", www))
        {
            yield return w.SendWebRequest();
            while (!w.isDone)
            {
                InfiniteLoopDetector.Run();
                Debug.Log("작동안함");
            }
            string str = w.downloadHandler.text;
            string[] charsToRemove = new string[] { "[", "]" };
            foreach (var c in charsToRemove)
            {
                str = str.Replace(c, string.Empty);
            }
            itemInfo = JsonUtility.FromJson<ItemInfo>(str);
            heartPotionCount = itemInfo.heartPotion;
            statusPotionCount = itemInfo.statusChangePotion;
            goldKeyCount = itemInfo.goldKey;
        }
        CreateItem();
    }
    public IEnumerator SetIHeartPotion()
    {
        Debug.Log(MySqlSystem.localID);
        WWWForm www = new WWWForm();
        www.AddField("loginUser", MySqlSystem.localID);
        www.AddField("status", "setItem");
        www.AddField("heartPotion", heartPotionCount);
        using (UnityWebRequest w = UnityWebRequest.Post("https://conbox.kr/Order", www))
        {
            yield return w.SendWebRequest();
            while (!w.isDone)
            {
                InfiniteLoopDetector.Run();
                Debug.Log("작동안함");
            }
        }
        Debug.Log(heartPotionCount +"HeartPotion Save");
    }
    public IEnumerator SetIStatusResetPotion()
    {
        WWWForm www = new WWWForm();
        www.AddField("loginUser", MySqlSystem.localID);
        www.AddField("status", "setItem");
        www.AddField("statusChangePotion", statusPotionCount);
        using (UnityWebRequest w = UnityWebRequest.Post("https://conbox.kr/Order", www))
        {
            yield return w.SendWebRequest();
            while (!w.isDone)
            {
                InfiniteLoopDetector.Run();
                Debug.Log("작동안함");
            }
        }
    }
    public IEnumerator SetIGoldKey()
    {
        Debug.Log(MySqlSystem.localID);
        WWWForm www = new WWWForm();
        www.AddField("loginUser", MySqlSystem.localID);
        www.AddField("status", "setItem");
        www.AddField("goldKey", goldKeyCount);
        using (UnityWebRequest w = UnityWebRequest.Post("https://conbox.kr/Order", www))
        {
            yield return w.SendWebRequest();
            while (!w.isDone)
            {
                InfiniteLoopDetector.Run();
                Debug.Log("작동안함");
            }
        }
    }
    void CreateItem()
    {
        if (heartPotionCount > 0)
        {
            GameObject heartPotion = Resources.Load<GameObject>("item/HeartPotion");
            heartPotion.GetComponent<InItem>().item.ItemQuantity = heartPotionCount;
            Instantiate<GameObject>(heartPotion,GameObject.FindGameObjectWithTag("Main").transform);
            itemList.Add(heartPotion);
        }
        if (statusPotionCount > 0)
        {
            GameObject StatusChangePotion = Resources.Load<GameObject>("item/StatusChangePotion");
            StatusChangePotion.GetComponent<InItem>().item.ItemQuantity = statusPotionCount;
            Instantiate<GameObject>(StatusChangePotion, GameObject.FindGameObjectWithTag("Main").transform);
            itemList.Add(StatusChangePotion);
        }
        if (goldKeyCount > 0)
        {
            GameObject GoldKey = Resources.Load<GameObject>("item/GoldKey");
            GoldKey.GetComponent<InItem>().item.ItemQuantity = goldKeyCount;
            Instantiate<GameObject>(GoldKey, GameObject.FindGameObjectWithTag("Main").transform);
            itemList.Add(GoldKey);
        }
        GetComponent<ItemInventory>().SlotInItem();
        TreeSetManager.instance.UpdateStatePotion();
    }
   

}
