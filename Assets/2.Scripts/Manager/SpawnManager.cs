using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //-----------------------Bool-------------------
    public static bool      isGameover;
    public static bool      isGameStart;
    bool                    isRestart;
    //----------------------Point-------------------
    GameObject[]            randomNutPoint;
    GameObject[]            randomMonsterPoint;
    GameObject[]            randomMiniGamePoint;
    GameObject[]            BeeHivePoint;
    //----------------------Time-------------------
    float                   chestnutBugSpawnTime = 0;
    float                   chestnutTime = 0;
    float                   miniChestnutSpawnTime = 0;
    float                   squrrielSpawnTime = 0;
    public static float     chestnutBugSpawnWaitTime;
    public static float     squrrielSpawnWaitTime;
    public static float     firstChestnutSpawnTime = 0;
    //----------------------ObjectCount-------------------
    public static int       chestnutbugSpawnCount;
    public static int       goldenChestbugSpawnCount = 2;
    public static int       squrrielSpawnCount;
    public static int       chestnutSpawnCount;
    public static int       goldenChestBugGauge = 0;
    //----------------------ObjectCount-------------------
    [SerializeField]GameObject              chestnutbur;
    [SerializeField]GameObject              chestnutBug;
    [SerializeField]GameObject              goldenBug;
    [SerializeField]GameObject              squrriel;
    [SerializeField]GameObject              beeHive;
    [SerializeField]GameObject              beeHiveLeaves;
    //----------------------AudioClip-------------------
    AudioClip[]             clip;
    private void Awake()
    {
        clip =                  new AudioClip[1];
        clip[0] =               Resources.Load<AudioClip>("Sound/#409 Country Girls - Sirenphonics");
        randomNutPoint =        GameObject.FindGameObjectsWithTag("ChestNutSpawnPoint");
        randomMonsterPoint =    GameObject.FindGameObjectsWithTag("EnemyMovePoint");
        randomMiniGamePoint =   GameObject.FindGameObjectsWithTag("MiniAconSpawnPoint");
        BeeHivePoint =   GameObject.FindGameObjectsWithTag("BeeHivePoint");
        GoldenBug.goldenChestBugCount = 0;
        isGameover =            false;//?????? ???????? ????
        isRestart =             true;
        isGameStart =           true;
        if(GameLogicManager.treeLevel >= 20)
        {
            SpawnBeeHive();
        }
        StartCoroutine(Upcol());
        if(GameLogicManager.treeLevel >= 30)
        {
            StartCoroutine(SpawnRabbit());
        }
    }
    IEnumerator Upcol()
    {
        while (true)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            SpawnSystem();
        }
    }
    /// <summary>
    /// ?????? ?? ?? ???????? ???? ????
    /// </summary>
    void SpawnSystem()
    {
        if (UIManager.gameTime >= 0)//?????????? ?????? ???????? ?????? 
        {
            chestnutBugSpawnTime += Time.deltaTime;
            firstChestnutSpawnTime += Time.deltaTime;
            miniChestnutSpawnTime += Time.deltaTime;
            chestnutTime += Time.deltaTime;
            squrrielSpawnTime += Time.deltaTime;
            if (chestnutBugSpawnTime > 1)//???? ???? ?????? ?????? 1?????? 2?????? ?? ????
            {
                isRestart = false;
                firstChestnutSpawnTime = 2;
            }
            if(goldenChestBugGauge >= 10)
            {
                SpawnGoldenChestBug();
                goldenChestBugGauge = 0;
            }
            if (isRestart)
            {
                SpawnChestnut();
            }
            {
                if (!MiniGameManager.isMiniGameStart)
                {
                    if (chestnutBugSpawnTime >= chestnutBugSpawnWaitTime + GameLogicManager.MonsterRegenerationRate)//?????? ???? ??????
                    {
                        SpawnChestnutBug();
                        chestnutBugSpawnTime = 0;
                    }
                    if (squrrielSpawnTime >= squrrielSpawnWaitTime + GameLogicManager.MonsterRegenerationRate&&GameLogicManager.treeLevel<30)//?????? ???? ??????
                    {
                        SpawnSqurriel();
                        squrrielSpawnTime = 0;
                    }
                    if (chestnutTime >= 2 - GameLogicManager.chestnutAppearanceRate)//?????? ??????
                    {
                        if (!isRestart)
                        {
                            SpawnChestnut();
                            chestnutTime = 0;
                        }
                    }
                }
                if (MiniGameManager.isMiniGameStart)
                {
                    GameObject[] Squrriel = GameObject.FindGameObjectsWithTag("Squirrel");
                    for (int i = 0; i < Squrriel.Length; i++)
                    {
                        Squrriel[i].SetActive(false);
                    }
                    GameObject[] chestnutBug = GameObject.FindGameObjectsWithTag("ChestBug");
                    for (int i = 0; i < chestnutBug.Length; i++)
                    {
                        chestnutBug[i].SetActive(false);
                    }
                    if (miniChestnutSpawnTime >= 0.5)
                    {
                        SpawnChestnutMini();
                        miniChestnutSpawnTime = 0;
                    }
                }
            }
        }
    }
    void SpawnChestnut()//?? ????
    {
        int randomChestnut = Random.Range(0, randomNutPoint.Length);

        if (ChestnutBur.chestnutburCount < chestnutSpawnCount)//????????
        {
            if (randomNutPoint[randomChestnut].transform.childCount == 0)//???????????? 0???? ????
            {
                chestnutbur = OPManager.instance.SetObject("ChestnutBur");
                chestnutbur.transform.position = randomNutPoint[randomChestnut].transform.position;
                chestnutbur.transform.SetParent(randomNutPoint[randomChestnut].transform);
            }
            else if ((randomNutPoint[randomChestnut].transform.childCount != 0))
            {
                SpawnChestnut();//???? ???? ???? ????????
            }
        }
    }

    void SpawnChestnutMini()//?? ????
    {
        int randomChestnut = Random.Range(0, randomMiniGamePoint.Length);
        if (ChestnutBur.chestnutburCount < 15)//?????????? ????????
        {
            if (randomMiniGamePoint[randomChestnut].transform.childCount == 0)//???????????? 0???? ????
            {
                chestnutbur = OPManager.instance.SetObject("ChestnutBur");
                chestnutbur.transform.position = randomMiniGamePoint[randomChestnut].transform.position;
                chestnutbur.transform.SetParent(randomMiniGamePoint[randomChestnut].transform);
            }
            else if ((randomMiniGamePoint[randomChestnut].transform.childCount != 0))
            {
                SpawnChestnutMini();//???? ???? ???? ????????
            }
        }
    }
    void SpawnChestnutBug()//?????? ????
    {
        int randomChestnut = Random.Range(0, randomNutPoint.Length);
        if (ChestBugs.chestnutBugCount <  chestnutbugSpawnCount)//?????? ??????
        {
            chestnutBug = OPManager.instance.SetObject("ChestBug");
            chestnutBug.transform.position = randomNutPoint[randomChestnut].transform.position;
        }
    }
    void SpawnGoldenChestBug()//?????? ????
    {
        int randomChestnut = Random.Range(0, randomNutPoint.Length);
        if (GoldenBug.goldenChestBugCount <  goldenChestbugSpawnCount)//?????? ??????
        {
            GoldenBug.goldenChestBugCount++;
            goldenBug = OPManager.instance.SetObject("GoldenChestBug");
            goldenBug.transform.position = randomNutPoint[randomChestnut].transform.position;
        }
    }
    void SpawnNinjaRabbit()//?????? ????
    {
        if (NinjaRabbit.NInjaRabbit < 5)
        {
            int randomChestnut = Random.Range(0, randomNutPoint.Length);
            GameObject ninjaRabbit = OPManager.instance.SetObject("NinjaRabbit");
            ninjaRabbit.transform.position = randomNutPoint[randomChestnut].transform.position;
        }
    }
    void SpawnBeeHive()//???? ????
    {
        int randomChestnut = Random.Range(0, BeeHivePoint.Length);
        beeHive = OPManager.instance.SetObject("BeeHive");
        beeHive.transform.position = BeeHivePoint[randomChestnut].transform.position;
        beeHiveLeaves = OPManager.instance.SetObject("BeeHiveLeaves");
        beeHiveLeaves.transform.position = beeHive.transform.GetChild(1).transform.position;
        beeHive.GetComponent<BeeHive>().hiveLeaves = beeHiveLeaves;
    }
    void SpawnSqurriel()//?????? ????
    {
        int randomMonster = Random.Range(0, randomMonsterPoint.Length);
        if (Squirrel.squirrelCount < squrrielSpawnCount)//?????? ??????
        {
            squrriel = OPManager.instance.SetObject("Squirrel");
            squrriel.transform.position = randomMonsterPoint[randomMonster].transform.position;
        }
    }
    IEnumerator SpawnRabbit()
    {
        yield return new WaitForSeconds(3);
        SpawnNinjaRabbit();
        yield return new WaitForSeconds(18);
        SpawnNinjaRabbit();
    }

}
