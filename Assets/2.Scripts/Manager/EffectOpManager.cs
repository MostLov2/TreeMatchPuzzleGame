using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectOpManager : MonoBehaviour
{
    //-----------------EffectPrefab--------------
    GameObject      ChestBugDeadP;
    GameObject      ChestnutBlastP;
    GameObject      FertilizertBlastP;
    GameObject      GoldChestOpenP;
    GameObject      ChestnutBugHitP;
    GameObject      ChestnutPopP;
    GameObject      EggDeadP;
    GameObject      MudEffectP;
    GameObject      SquirrelDeadP;
    GameObject      SquirrelHitP;
    GameObject      WormDeadP;
    GameObject      TelpoP;
    GameObject      NinjaRabbitDeadP;
    //-----------------EffectObjectArray--------------
    GameObject[]    ChestBugDead;
    GameObject[]    ChestnutBlast;
    GameObject[]    FertilizertBlast;
    GameObject[]    GoldChestOpen;
    GameObject[]    ChestnutBugHit;
    GameObject[]    ChestnutPop;
    GameObject[]    EggDead;
    GameObject[]    MudEffect;
    GameObject[]    SquirrelDead;
    GameObject[]    SquirrelHit;
    GameObject[]    WormDead;
    GameObject[]    Telpo;
    GameObject[]    NinjaRabbitDead;
    //-----------------targetPool--------------
    GameObject[]    targetPool;
    //-----------------SingleTurn--------------
    public static EffectOpManager instance;
    void Awake()
    {
        instance =          this;
        if (instance == null)
            Destroy(gameObject);

        ChestBugDeadP =     Resources.Load<GameObject>("Effect/ChestBugDead");
        ChestnutBugHitP =   Resources.Load<GameObject>("Effect/ChestnutBugHit");
        ChestnutPopP =      Resources.Load<GameObject>("Effect/ChestnutPop");
        EggDeadP =          Resources.Load<GameObject>("Effect/EggDead");
        MudEffectP =        Resources.Load<GameObject>("Effect/MudEffect");
        SquirrelDeadP =     Resources.Load<GameObject>("Effect/SquirrelDead");
        SquirrelHitP =      Resources.Load<GameObject>("Effect/SquirrelHit");
        WormDeadP =         Resources.Load<GameObject>("Effect/WormDead");
        ChestnutBlastP =    Resources.Load<GameObject>("Effect/ChestnutBlast");
        FertilizertBlastP = Resources.Load<GameObject>("Effect/FertilizertBlast");
        GoldChestOpenP =    Resources.Load<GameObject>("Effect/GoldChestOpen");
        TelpoP =            Resources.Load<GameObject>("Effect/Telpo");
        NinjaRabbitDeadP =  Resources.Load<GameObject>("Effect/NinjaRabbitDead");

        ChestBugDead =      new GameObject[10];
        ChestnutBugHit =    new GameObject[70];
        ChestnutPop =       new GameObject[30];
        EggDead =           new GameObject[10];
        MudEffect =         new GameObject[20];
        SquirrelDead =      new GameObject[10];
        SquirrelHit =       new GameObject[10];
        WormDead =          new GameObject[10];
        ChestnutBlast =     new GameObject[2];
        FertilizertBlast =  new GameObject[2];
        GoldChestOpen =     new GameObject[2];
        Telpo =             new GameObject[30];
        NinjaRabbitDead =   new GameObject[30];
        GetObject();
    }
    /// <summary>
    /// 이팩트 오브잭트를 필요한 만큼 만드는 함수
    /// </summary>
    void GetObject()
    {
        for (int i = 0; i < ChestBugDead.Length; i++)
        {
            ChestBugDead[i] = Instantiate<GameObject>(ChestBugDeadP);
            ChestBugDead[i].transform.SetParent(transform);
            ChestBugDead[i].SetActive(false);
        }
        for (int i = 0; i < ChestnutBugHit.Length; i++)
        {
            ChestnutBugHit[i] = Instantiate<GameObject>(ChestnutBugHitP);
            ChestnutBugHit[i].transform.SetParent(transform);
            ChestnutBugHit[i].SetActive(false);
        }
        for (int i = 0; i < ChestnutPop.Length; i++)
        {
            ChestnutPop[i] = Instantiate<GameObject>(ChestnutPopP);
            ChestnutPop[i].transform.SetParent(transform);
            ChestnutPop[i].SetActive(false);
        }
        for (int i = 0; i < EggDead.Length; i++)
        {
            EggDead[i] = Instantiate<GameObject>(EggDeadP);
            EggDead[i].transform.SetParent(transform);
            EggDead[i].SetActive(false);
        }
        for (int i = 0; i < MudEffect.Length; i++)
        {
            MudEffect[i] = Instantiate<GameObject>(MudEffectP);
            MudEffect[i].transform.SetParent(transform);
            MudEffect[i].SetActive(false);
        }
        for (int i = 0; i < SquirrelDead.Length; i++)
        {
            SquirrelDead[i] = Instantiate<GameObject>(SquirrelDeadP);
            SquirrelDead[i].transform.SetParent(transform);
            SquirrelDead[i].SetActive(false);
        }
        for (int i = 0; i < SquirrelHit.Length; i++)
        {
            SquirrelHit[i] = Instantiate<GameObject>(SquirrelHitP);
            SquirrelHit[i].transform.SetParent(transform);
            SquirrelHit[i].SetActive(false);
        }
        for (int i = 0; i < ChestnutBlast.Length; i++)
        {
            ChestnutBlast[i] = Instantiate<GameObject>(ChestnutBlastP);
            ChestnutBlast[i].transform.SetParent(transform);
            ChestnutBlast[i].SetActive(false);
        }
        for (int i = 0; i < FertilizertBlast.Length; i++)
        {
            FertilizertBlast[i] = Instantiate<GameObject>(FertilizertBlastP);
            FertilizertBlast[i].transform.SetParent(transform);
            FertilizertBlast[i].SetActive(false);
        }
        for (int i = 0; i < GoldChestOpen.Length; i++)
        {
            GoldChestOpen[i] = Instantiate<GameObject>(GoldChestOpenP);
            GoldChestOpen[i].transform.SetParent(transform);
            GoldChestOpen[i].SetActive(false);
        }
        for (int i = 0; i < WormDead.Length; i++)
        {
            WormDead[i] = Instantiate<GameObject>(WormDeadP);
            WormDead[i].transform.SetParent(transform);
            WormDead[i].SetActive(false);
        }
        for (int i = 0; i < Telpo.Length; i++)
        {
            Telpo[i] = Instantiate<GameObject>(TelpoP);
            Telpo[i].transform.SetParent(transform);
            Telpo[i].SetActive(false);
        }
        for (int i = 0; i < NinjaRabbitDead.Length; i++)
        {
            NinjaRabbitDead[i] = Instantiate<GameObject>(NinjaRabbitDeadP);
            NinjaRabbitDead[i].transform.SetParent(transform);
            NinjaRabbitDead[i].SetActive(false);
        }
    }
    /// <summary>
    /// 오브젝트를 필요에 맞게 꺼내는 함수
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public GameObject SetObject(string type)
    {
        switch (type)
        {
            case "ChestBugDead":
                targetPool = ChestBugDead;
                break;
            case "ChestnutBugHit":
                targetPool = ChestnutBugHit;
                break;
            case "ChestnutPop":
                targetPool = ChestnutPop;
                break;
            case "EggDead":
                targetPool = EggDead;
                break;
            case "MudEffect":
                targetPool = MudEffect;
                break;
            case "SquirrelDead":
                targetPool = SquirrelDead;
                break;
            case "SquirrelHit":
                targetPool = SquirrelHit;
                break;
            case "WormDead":
                targetPool = WormDead;
                break;
            case "ChestnutBlast":
                targetPool = ChestnutBlast;
                break;
            case "FertilizertBlast":
                targetPool = FertilizertBlast;
                break;
            case "GoldChestOpen":
                targetPool = GoldChestOpen;
                break;
            case "Telpo":
                targetPool = Telpo;
                break;
            case "NinjaRabbitDead":
                targetPool = NinjaRabbitDead;
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
            if(transform.childCount != 0)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}
