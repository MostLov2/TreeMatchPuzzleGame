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
        isGameover =            false;//게임이 끝났는지 확인
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
    /// 몬스터 및 밤 스폰시간 조절 함수
    /// </summary>
    void SpawnSystem()
    {
        if (UIManager.gameTime >= 0)//제한시간이 다되면 자동으로 끝나게 
        {
            chestnutBugSpawnTime += Time.deltaTime;
            firstChestnutSpawnTime += Time.deltaTime;
            miniChestnutSpawnTime += Time.deltaTime;
            chestnutTime += Time.deltaTime;
            squrrielSpawnTime += Time.deltaTime;
            if (chestnutBugSpawnTime > 1)//밤을 전부 소환시 딜레이 1초후에 2초마다 밤 소환
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
                    if (chestnutBugSpawnTime >= chestnutBugSpawnWaitTime + GameLogicManager.MonsterRegenerationRate)//밤벌레 소환 딜레이
                    {
                        SpawnChestnutBug();
                        chestnutBugSpawnTime = 0;
                    }
                    if (squrrielSpawnTime >= squrrielSpawnWaitTime + GameLogicManager.MonsterRegenerationRate&&GameLogicManager.treeLevel<30)//다람쥐 소환 딜레이
                    {
                        SpawnSqurriel();
                        squrrielSpawnTime = 0;
                    }
                    if (chestnutTime >= 2 - GameLogicManager.chestnutAppearanceRate)//밤소환 딜레이
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
    void SpawnChestnut()//밤 소환
    {
        int randomChestnut = Random.Range(0, randomNutPoint.Length);

        if (ChestnutBur.chestnutburCount < chestnutSpawnCount)//밤소환량
        {
            if (randomNutPoint[randomChestnut].transform.childCount == 0)//자식객체수가 0이면 소환
            {
                chestnutbur = OPManager.instance.SetObject("ChestnutBur");
                chestnutbur.transform.position = randomNutPoint[randomChestnut].transform.position;
                chestnutbur.transform.SetParent(randomNutPoint[randomChestnut].transform);
            }
            else if ((randomNutPoint[randomChestnut].transform.childCount != 0))
            {
                SpawnChestnut();//값이 같을 경우 제귀함수
            }
        }
    }

    void SpawnChestnutMini()//밤 소환
    {
        int randomChestnut = Random.Range(0, randomMiniGamePoint.Length);
        if (ChestnutBur.chestnutburCount < 15)//미니게임시 밤소환량
        {
            if (randomMiniGamePoint[randomChestnut].transform.childCount == 0)//자식객체수가 0이면 소환
            {
                chestnutbur = OPManager.instance.SetObject("ChestnutBur");
                chestnutbur.transform.position = randomMiniGamePoint[randomChestnut].transform.position;
                chestnutbur.transform.SetParent(randomMiniGamePoint[randomChestnut].transform);
            }
            else if ((randomMiniGamePoint[randomChestnut].transform.childCount != 0))
            {
                SpawnChestnutMini();//값이 같을 경우 제귀함수
            }
        }
    }
    void SpawnChestnutBug()//밤벌레 소환
    {
        int randomChestnut = Random.Range(0, randomNutPoint.Length);
        if (ChestBugs.chestnutBugCount <  chestnutbugSpawnCount)//밤벌레 소환량
        {
            chestnutBug = OPManager.instance.SetObject("ChestBug");
            chestnutBug.transform.position = randomNutPoint[randomChestnut].transform.position;
        }
    }
    void SpawnGoldenChestBug()//밤벌레 소환
    {
        int randomChestnut = Random.Range(0, randomNutPoint.Length);
        if (GoldenBug.goldenChestBugCount <  goldenChestbugSpawnCount)//밤벌레 소환량
        {
            GoldenBug.goldenChestBugCount++;
            goldenBug = OPManager.instance.SetObject("GoldenChestBug");
            goldenBug.transform.position = randomNutPoint[randomChestnut].transform.position;
        }
    }
    void SpawnNinjaRabbit()//밤벌레 소환
    {
        if (NinjaRabbit.NInjaRabbit < 5)
        {
            int randomChestnut = Random.Range(0, randomNutPoint.Length);
            GameObject ninjaRabbit = OPManager.instance.SetObject("NinjaRabbit");
            ninjaRabbit.transform.position = randomNutPoint[randomChestnut].transform.position;
        }
    }
    void SpawnBeeHive()//벌집 소환
    {
        int randomChestnut = Random.Range(0, BeeHivePoint.Length);
        beeHive = OPManager.instance.SetObject("BeeHive");
        beeHive.transform.position = BeeHivePoint[randomChestnut].transform.position;
        beeHiveLeaves = OPManager.instance.SetObject("BeeHiveLeaves");
        beeHiveLeaves.transform.position = beeHive.transform.GetChild(1).transform.position;
        beeHive.GetComponent<BeeHive>().hiveLeaves = beeHiveLeaves;
    }
    void SpawnSqurriel()//다람쥐 소환
    {
        int randomMonster = Random.Range(0, randomMonsterPoint.Length);
        if (Squirrel.squirrelCount < squrrielSpawnCount)//다람쥐 소환량
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
