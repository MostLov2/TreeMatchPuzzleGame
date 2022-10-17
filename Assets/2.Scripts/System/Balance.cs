using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
[System.Serializable]
public class BalanceNum
{
    public int balance_id;
    public int energy_charging;
}
[System.Serializable]
public class GoldBoxPercentage
{
    public int box_num;
    public int chestnut_1;
    public int fertilizer_1;
    public int chestnut_2;
    public int fertilizer_2;
    public int chestnut_3;
    public int fertilizer_3;
    public int chestnut_4;
    public int fertilizer_4;
    public int chestnut_5 ;
    public int fertilizer_5;
}
[System.Serializable]
public class SilverBoxPercentage
{
    public int box_num;
    public int chestnut_1;
    public int chestnut_2;
    public int chestnut_3;
    public int chestnut_4;
    public int chestnut_5;
    public int chestnut_6;
    public int chestnut_7;
    public int chestnut_8;
    public int chestnut_9;
    public int fertilizer_1;
    public int fertilizer_2;
    public int fertilizer_3;
}
public class Balance : MonoBehaviour
{
    public BalanceNum balanceNum;
    public int balance_id;
    public int energy_charging;

    [Header("GoldBox")]
    public GoldBoxPercentage goldBoxPercentage;
    public int chestnut_1;
    public int fertilizer_1;
    public int chestnut_2;
    public int fertilizer_2;
    public int chestnut_3;
    public int fertilizer_3;
    public int chestnut_4;
    public int fertilizer_4;
    public int chestnut_5;
    public int fertilizer_5;

    [Header("SilverBox")]
    public SilverBoxPercentage silverBoxPercentage;
    public int chestnut_1S;
    public int chestnut_2S;
    public int fertilizer_1S;
    public int chestnut_3S;
    public int chestnut_4S;
    public int chestnut_5S;
    public int chestnut_6S;
    public int fertilizer_2S;
    public int chestnut_7S;
    public int chestnut_8S;
    public int fertilizer_3S;
    public int chestnut_9S;

    public static Balance instance;
    void Awake()
    {
        instance = this;
        StartCoroutine(EnergyTimeSet());
        StartCoroutine(GoldBoxPercentage());
        StartCoroutine(SilverBoxPercentage());
    }
    /// <summary>
    /// 하트 충전수 조절
    /// </summary>
    /// <returns></returns>
    public IEnumerator EnergyTimeSet()
    {
        WWWForm www = new WWWForm();
        www.AddField("status", "balanceCheck");
        using (UnityWebRequest w = UnityWebRequest.Post("https://conbox.kr/Order", www))
        {
            yield return w.SendWebRequest();
            while (!w.isDone)
            {
                InfiniteLoopDetector.Run();
                Debug.Log("작동안함");
            }
            Debug.Log(w.downloadHandler.text);
            string str = w.downloadHandler.text;
            string[] charsToRemove = new string[] { "[", "]" };
            foreach (var c in charsToRemove)
            {
                str = str.Replace(c, string.Empty);
            }
            balanceNum = JsonUtility.FromJson<BalanceNum>(str);


            balance_id = balanceNum.balance_id;
            energy_charging = balanceNum.energy_charging;
        }
    }
    public IEnumerator SilverBoxPercentage()
    {
        WWWForm www = new WWWForm();
        www.AddField("status", "silverCheck");
        www.AddField("haheu", 1);
        using (UnityWebRequest w = UnityWebRequest.Post("https://conbox.kr/Order", www))
        {
            yield return w.SendWebRequest();
            while (!w.isDone)
            {
                InfiniteLoopDetector.Run();
                Debug.Log("작동안함");
            }
            Debug.Log(w.downloadHandler.text);
            string str = w.downloadHandler.text;
            string[] charsToRemove = new string[] { "[", "]" };
            foreach (var c in charsToRemove)
            {
                str = str.Replace(c, string.Empty);
            }
            silverBoxPercentage = JsonUtility.FromJson<SilverBoxPercentage>(str);
            chestnut_1S=  silverBoxPercentage.chestnut_1; 
            chestnut_2S=  silverBoxPercentage.chestnut_2; 
            chestnut_3S=  silverBoxPercentage.chestnut_3; 
            chestnut_4S=  silverBoxPercentage.chestnut_4; 
            chestnut_5S=  silverBoxPercentage.chestnut_5; 
            chestnut_6S=  silverBoxPercentage.chestnut_6; 
            chestnut_7S=  silverBoxPercentage.chestnut_7; 
            chestnut_8S=  silverBoxPercentage.chestnut_8;
            chestnut_9S = silverBoxPercentage.chestnut_9;
            fertilizer_1S = silverBoxPercentage.fertilizer_1;
            fertilizer_2S = silverBoxPercentage.fertilizer_2;
            fertilizer_3S = silverBoxPercentage.fertilizer_3;
        }
    }
    public IEnumerator GoldBoxPercentage()
    {
        WWWForm www = new WWWForm();
        www.AddField("status", "goldCheck");
        www.AddField("haheu", 1);
        using (UnityWebRequest w = UnityWebRequest.Post("https://conbox.kr/Order", www))
        {
            yield return w.SendWebRequest();
            while (!w.isDone)
            {
                InfiniteLoopDetector.Run();
                Debug.Log("작동안함");
            }
            Debug.Log(w.downloadHandler.text);
            string str = w.downloadHandler.text;
            string[] charsToRemove = new string[] { "[", "]" };
            foreach (var c in charsToRemove)
            {
                str = str.Replace(c, string.Empty);
            }
            goldBoxPercentage = JsonUtility.FromJson<GoldBoxPercentage>(str);
            chestnut_1 =    goldBoxPercentage.chestnut_1;
            fertilizer_1 =  goldBoxPercentage.fertilizer_1;
            chestnut_2 =    goldBoxPercentage.chestnut_2;
            fertilizer_2 =  goldBoxPercentage.fertilizer_2;
            chestnut_3 =    goldBoxPercentage.chestnut_3;
            fertilizer_3 =  goldBoxPercentage.fertilizer_3;
            chestnut_4 =    goldBoxPercentage.chestnut_4;
            fertilizer_4 =  goldBoxPercentage.fertilizer_4;
            chestnut_5 =    goldBoxPercentage.chestnut_5;
            fertilizer_5 =  goldBoxPercentage.fertilizer_5;  


        }
    }
}
