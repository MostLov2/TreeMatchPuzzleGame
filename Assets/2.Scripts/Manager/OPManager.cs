using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OPManager : MonoBehaviour
{
    //----------------------GameObjectPrefabs-------------------
    GameObject      chestBugP;
    GameObject      wormP;
    GameObject      eggP;
    GameObject      sparrowP;
    GameObject      squirrelP;
    GameObject      chestnutP;
    GameObject      chestnutburP;
    GameObject      goldenChestBugP;
    GameObject      BeeP;
    GameObject      BeeHiveP;
    GameObject      BeeHiveLeavesP;
    GameObject      NinjaRabbitP;
    GameObject      TelpoWoodP;
    GameObject      DeadLeafP;
    //----------------------GameObjectArray-------------------
    GameObject[]    chestBug;
    GameObject[]    worm;
    GameObject[]    egg;
    GameObject[]    sparrow;
    GameObject[]    squirrel;
    GameObject[]    chestnut;
    GameObject[]    chestnutbur;
    GameObject[]    Bee;
    GameObject[]    BeeHive;
    GameObject[]    BeeHiveLeaves;
    GameObject[]    NinjaRabbit;
    GameObject[]    goldenChestBug;
    GameObject[]    TelpoWood;
    GameObject[]    DeadLeaf;
    //----------------------targetPool------------------------
    GameObject[]    targetPool;
    //----------------------SingleTurn------------------------
    public static OPManager instance;
    private void Awake()
    {
        instance =      this;
        if(instance == null)
            Destroy(gameObject);

        chestBugP =         Resources.Load<GameObject>("ChestBug");
        eggP =              Resources.Load<GameObject>("Egg");
        sparrowP =          Resources.Load<GameObject>("SparrowFly");
        wormP =             Resources.Load<GameObject>("Worm");
        squirrelP =         Resources.Load<GameObject>("Squirrel");
        chestnutP =         Resources.Load<GameObject>("Chestnut");
        chestnutburP =      Resources.Load<GameObject>("ChestnutBur");
        goldenChestBugP =   Resources.Load<GameObject>("GoldenChestBug");
        BeeP =              Resources.Load<GameObject>("Bee");
        BeeHiveP =          Resources.Load<GameObject>("Hive");
        BeeHiveLeavesP =    Resources.Load<GameObject>("HiveLeaf");
        NinjaRabbitP =      Resources.Load<GameObject>("NinjaRabbit");
        TelpoWoodP =        Resources.Load<GameObject>("TelpoWood");
        DeadLeafP =         Resources.Load<GameObject>("DeadLeaf");

        sparrow =           new GameObject[10];
        squirrel =          new GameObject[15];
        chestnut =          new GameObject[100];
        chestnutbur =       new GameObject[50];
        chestBug =          new GameObject[15];
        egg =               new GameObject[20];
        worm =              new GameObject[20];
        goldenChestBug =    new GameObject[2];
        Bee =               new GameObject[30];
        BeeHive =           new GameObject[1];
        BeeHiveLeaves =     new GameObject[1];
        NinjaRabbit =       new GameObject[6];
        TelpoWood =         new GameObject[20];
        DeadLeaf =         new GameObject[20];

        GetObject();
    }
    /// <summary>
    /// 오브젝트 소환
    /// </summary>
    void GetObject()
    {
        for (int i = 0; i < sparrow.Length; i++)
        {
            sparrow[i] = Instantiate<GameObject>(sparrowP);
            sparrow[i].transform.SetParent(transform);
            sparrow[i].SetActive(false);
        }
        for (int i = 0; i < chestnut.Length; i++)
        {
            chestnut[i] = Instantiate<GameObject>(chestnutP);
            chestnut[i].transform.SetParent(transform);
            chestnut[i].SetActive(false);
        }
        for (int i = 0; i < chestnutbur.Length; i++)
        {
            chestnutbur[i] = Instantiate<GameObject>(chestnutburP);
            chestnutbur[i].transform.SetParent(transform);
            chestnutbur[i].SetActive(false);
        }
        for (int i = 0; i < squirrel.Length; i++)
        {
            squirrel[i] = Instantiate<GameObject>(squirrelP);
            squirrel[i].transform.SetParent(transform);
            squirrel[i].SetActive(false);
        }
        for (int i = 0; i < chestBug.Length; i++)
        {
            chestBug[i] = Instantiate<GameObject>(chestBugP);
            chestBug[i].transform.SetParent(transform);
            chestBug[i].SetActive(false);
        }
        for (int i = 0; i < goldenChestBug.Length; i++)
        {
            goldenChestBug[i] = Instantiate<GameObject>(goldenChestBugP);
            goldenChestBug[i].transform.SetParent(transform);
            goldenChestBug[i].SetActive(false);
        }
        for (int i = 0; i < worm.Length; i++)
        {
            worm[i] = Instantiate<GameObject>(wormP);
            worm[i].transform.SetParent(transform);
            worm[i].SetActive(false);
        }
        for (int i = 0; i < egg.Length; i++)
        {
            egg[i] = Instantiate<GameObject>(eggP);
            egg[i].transform.SetParent(transform);
            egg[i].SetActive(false);
        }
        for (int i = 0; i < Bee.Length; i++)
        {
            Bee[i] = Instantiate<GameObject>(BeeP);
            Bee[i].transform.SetParent(transform);
            Bee[i].SetActive(false);
        }
        for (int i = 0; i < BeeHive.Length; i++)
        {
            BeeHive[i] = Instantiate<GameObject>(BeeHiveP);
            BeeHive[i].transform.SetParent(transform);
            BeeHive[i].SetActive(false);
        }
        for (int i = 0; i < BeeHiveLeaves.Length; i++)
        {
            BeeHiveLeaves[i] = Instantiate<GameObject>(BeeHiveLeavesP);
            BeeHiveLeaves[i].transform.SetParent(transform);
            BeeHiveLeaves[i].SetActive(false);
        }
        for (int i = 0; i < NinjaRabbit.Length; i++)
        {
            NinjaRabbit[i] = Instantiate<GameObject>(NinjaRabbitP);
            NinjaRabbit[i].transform.SetParent(transform);
            NinjaRabbit[i].SetActive(false);
        }
        for (int i = 0; i < TelpoWood.Length; i++)
        {
            TelpoWood[i] = Instantiate<GameObject>(TelpoWoodP);
            TelpoWood[i].transform.SetParent(transform);
            TelpoWood[i].SetActive(false);
        }
        for (int i = 0; i < DeadLeaf.Length; i++)
        {
            DeadLeaf[i] = Instantiate<GameObject>(DeadLeafP);
            DeadLeaf[i].transform.SetParent(transform);
            DeadLeaf[i].SetActive(false);
        }
    }
    /// <summary>
    /// 오브젝트 사용
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public GameObject SetObject(string type)
    {
        switch (type)
        {
            case "ChestBug":
                targetPool = chestBug;
                break;
            case "GoldenChestBug":
                targetPool = goldenChestBug;
                break;
            case "Worm":
                targetPool = worm;
                break;
            case "Egg":
                targetPool = egg;
                break;
            case "Sparrow":
                targetPool = sparrow;
                break;
            case "Squirrel":
                targetPool = squirrel;
                break;
            case "Chestnut":
                targetPool = chestnut;
                break;
            case "ChestnutBur":
                targetPool = chestnutbur;
                break;
            case "Bee":
                targetPool = Bee;
                break;
            case "BeeHive":
                targetPool = BeeHive;
                break;
            case "BeeHiveLeaves":
                targetPool = BeeHiveLeaves;
                break;
            case "NinjaRabbit":
                targetPool = NinjaRabbit;
                break;
            case "TelpoWood":
                targetPool = TelpoWood;
                break;
            case "DeadLeaf":
                targetPool = DeadLeaf;
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
    public void DestroyObject()
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

