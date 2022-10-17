using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyEffectManager : MonoBehaviour
{
    //-----------------EffectPrefab--------------
    GameObject HeartEffectP;
    GameObject HeartExplosionEffectP;

    //-----------------EffectObjectArray--------------
    GameObject[] HeartEffect;
    GameObject[] HeartExplosionEffect;

    //-----------------targetPool--------------
    GameObject[] targetPool;
    //-----------------SingleTurn--------------
    public static LobbyEffectManager instance;
    void Awake()
    {
        instance = this;
        if (instance == null)
            Destroy(gameObject);

        HeartEffectP = Resources.Load<GameObject>("LobbyEffect/HeartEffect");
        HeartExplosionEffectP = Resources.Load<GameObject>("LobbyEffect/HeartPotionEffect");

        HeartEffect = new GameObject[30];
        HeartExplosionEffect = new GameObject[30];
        GetObject();
    }
    /// <summary>
    /// ����Ʈ ������Ʈ�� �ʿ��� ��ŭ ����� �Լ�
    /// </summary>
    void GetObject()
    {
        for (int i = 0; i < HeartEffect.Length; i++)
        {
            HeartEffect[i] = Instantiate<GameObject>(HeartEffectP);
            HeartEffect[i].transform.SetParent(transform);
            HeartEffect[i].SetActive(false);
        }
        for (int i = 0; i < HeartExplosionEffect.Length; i++)
        {
            HeartExplosionEffect[i] = Instantiate<GameObject>(HeartExplosionEffectP);
            HeartExplosionEffect[i].transform.SetParent(transform);
            HeartExplosionEffect[i].SetActive(false);
        }
    }
    /// <summary>
    /// ������Ʈ�� �ʿ信 �°� ������ �Լ�
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public GameObject SetObject(string type)
    {
        switch (type)
        {
            case "HeartEffect":
                targetPool = HeartEffect;
                break;
            case "HeartExplosionEffect":
                targetPool = HeartExplosionEffect;
                break;
        }
        for (int i = 0; i < targetPool.Length; i++)
        {
            if (!targetPool[i].activeSelf)
            {
                targetPool[i].SetActive(true);
                return targetPool[i];
            }
        }
        return null;
    }

    public void DestroyEffect()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.childCount != 0)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}
