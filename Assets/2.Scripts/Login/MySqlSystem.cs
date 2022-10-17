using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;
using System.Runtime;


[Serializable]
public class DB//유저 데이터 연동 관련 DB값 클래스
{
    public string game_id;
    public int chestnut;
    public int jewelry;
    public int energy;
    public int fertilizer;
    public int weapon1;
    public int weapon2;
    public string start_time;
    public string end_Time;
}

[Serializable]
public class UserObject//나무별 특성 및 레벨 이름 DB값 클래스
{
    public int id ;
    public string nft_id ;
    public string nft_name ;
    public string game_id ;
    public int level ;
    public int experience ;
    public int chestnutHarvest ;
    public int doubleTheChestnutHarvest ;
    public int feverTimeIncrease ;
    public int increasedFeverTimeRewards ;
    public int feverTimeAutomation ;
    public int reductionOfLevelUpFertilizerRequirement ;
    public int increaseGameTime ;
    public int chestnutAppearanceRate ;
    public int monsterRegenerationRate ;
    public int birdMovementSpeed ;
    public int whistleOverallGaugeReduction ;
    public int position ;
}

[Serializable]
public class RootObject//나무 특성 클래스 배열
{
    public UserObject[] users;
}

public class MySqlSystem : MonoBehaviour
{
    //--------------DB 연동 클래스--------------------
    public RootObject           myObject;
    public DB                   db;
    //--------------유저 DB 변수----------------------
    public static int           chestnutPoint;
    public static int           jewelryPoint;
    public static int           energy;
    public static int           fertilizerPoint;
    public static int           sprayLevelPoint;
    public static int           dragonflyStickLevelPoint;
    public string               loginTime;
    public string               energySpendTime;
    //--------------나무 특성 변수배열--------------------
    public static int[]         id;
    public static string[]      game_id;
    public static string[]      nft_id;
    public static string[]      nft_name;
    public static int[]         level;
    public static int[]         experience;
    public static int[]         chestnutHarvest;
    public static int[]         doubleTheChestnutHarvest;
    public static int[]         feverTimeIncrease;
    public static int[]         increasedFeverTimeRewards;
    public static int[]         feverTimeAutomation;
    public static int[]         reductionOfLevelUpFertilizerRequirement;
    public static int[]         increaseGameTime;
    public static int[]         chestnutAppearanceRate;
    public static int[]         monsterRegenerationRate;
    public static int[]         birdMovementSpeed;
    public static int[]         whistleOverallGaugeReduction;
    public static int[]         position;
    //--------------로컬 아이디 저장--------------------
    public static string        localID;//로컬에 아이디 저장
    public static string        localPS;//로컬에 비밀번호 저장
    //--------------Panel--------------------
    Transform                   uncorrectPanel;//비밀번호가 맞지 않을 때 뜨는 판넬
    Text                        uncorrectText;
    //--------------에너지 시간 로컬 저장 값--------------------
    public static int           minute;
    public static float         second;
    //--------------TreeObject Array--------------------
    GameObject                  treeObject;//TreePrefab;
    GameObject[]                treeOb;//TreeArray
    //--------------Single turn--------------------
    public static MySqlSystem   instance;

    private void Awake()
    {
        treeObject =            Resources.Load<GameObject>("Tree");
        treeOb =                GameObject.FindGameObjectsWithTag("Tree");
        if(GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(2).transform.GetChild(0).GetComponent<Transform>() != null)
        {
            uncorrectPanel = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(2).transform.GetChild(0).GetComponent<Transform>();
            uncorrectText = uncorrectPanel.transform.GetChild(0).GetComponent<Text>();
        }
        //-------------Single Turn-----------------------
        if (null == instance)
        {
            instance =          this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void SetLength()
    {
        id =                                        new int[myObject.users.Length];
        nft_name =                                  new string[myObject.users.Length];
        nft_id =                                    new string[myObject.users.Length];
        game_id =                                   new string[myObject.users.Length];
        level =                                     new int[myObject.users.Length];
        experience =                                new int[myObject.users.Length];
        chestnutHarvest =                           new int[myObject.users.Length];
        doubleTheChestnutHarvest =                  new int[myObject.users.Length];
        feverTimeIncrease =                         new int[myObject.users.Length];
        increasedFeverTimeRewards =                 new int[myObject.users.Length];
        feverTimeAutomation =                       new int[myObject.users.Length];
        reductionOfLevelUpFertilizerRequirement =   new int[myObject.users.Length];
        increaseGameTime =                          new int[myObject.users.Length];
        chestnutAppearanceRate =                    new int[myObject.users.Length];
        monsterRegenerationRate =                   new int[myObject.users.Length];
        birdMovementSpeed =                         new int[myObject.users.Length];
        whistleOverallGaugeReduction =              new int[myObject.users.Length];
        position =                                  new int[myObject.users.Length];
       
    }
    
    private void Update()
    {
        TimeUpdate();
    }
    /// <summary>
    /// 실시간 시간 업데이트 함수
    /// </summary>
    void TimeUpdate()
    {
        if (energy >= 10)
        {
            minute = (Balance.instance.energy_charging - 1) - (int)(myObject.users.Length / 10);
            second = 60;
        }
        if (second <= 0)
        {
            second = 60;
            minute--;
        }
        if (minute >= 0)
        {
            if (energy <= 9)
            {
                second -= Time.deltaTime;
                if (minute <= 0&&second<=0)
                {
                    energy++;
                    Debug.Log(energy);
                    minute = (Balance.instance.energy_charging - 1) - (int)(myObject.users.Length / 10);
                    StartCoroutine(SetEnergy(0));
                    StartCoroutine(EnergyTimeS());
                }
            }
        }
    }
    #region SetTree
    /// <summary>
    /// DB에서 받아온 특성들을 특성 배열에 넣는 함수
    /// </summary>
    void SetTreeStatus()
    {
        SetLength();
        for (int i = 0; i < myObject.users.Length; i++)
        {
            id[i] = myObject.users[i].id;
            nft_id[i] = myObject.users[i].nft_id;
            nft_name[i] = myObject.users[i].nft_name;
            game_id[i] = myObject.users[i].game_id;
            level[i] = myObject.users[i].level;
            experience[i] = myObject.users[i].experience;
            chestnutHarvest[i] = myObject.users[i].chestnutHarvest;
            doubleTheChestnutHarvest[i] = myObject.users[i].doubleTheChestnutHarvest;
            feverTimeIncrease[i] = myObject.users[i].feverTimeIncrease;
            increasedFeverTimeRewards[i] = myObject.users[i].increasedFeverTimeRewards;
            feverTimeAutomation[i] = myObject.users[i].feverTimeAutomation;
            reductionOfLevelUpFertilizerRequirement[i] = myObject.users[i].reductionOfLevelUpFertilizerRequirement;
            increaseGameTime[i] = myObject.users[i].increaseGameTime;
            chestnutAppearanceRate[i] = myObject.users[i].chestnutAppearanceRate;
            monsterRegenerationRate[i] = myObject.users[i].monsterRegenerationRate;
            birdMovementSpeed[i] = myObject.users[i].birdMovementSpeed;
            whistleOverallGaugeReduction[i] = myObject.users[i].whistleOverallGaugeReduction;
            position[i] = myObject.users[i].position;
        }
        SetTreeObjectStatus();
    }
    /// <summary>
    /// 특성 배열을 나무에 있는 특성에 넣는 함수
    /// </summary>
    void SetTreeStatus1()
    {
        SetLength();
        for (int i = 0; i < myObject.users.Length; i++)
        {
            id[i] = myObject.users[i].id;
            nft_id[i] = myObject.users[i].nft_id;
            nft_name[i] = myObject.users[i].nft_name ;
            level[i] = myObject.users[i].level;
            experience[i] = myObject.users[i].experience;
            chestnutHarvest[i] = myObject.users[i].chestnutHarvest;
            doubleTheChestnutHarvest[i] = myObject.users[i].doubleTheChestnutHarvest;
            feverTimeIncrease[i] = myObject.users[i].feverTimeIncrease;
            increasedFeverTimeRewards[i] = myObject.users[i].increasedFeverTimeRewards;
            feverTimeAutomation[i] = myObject.users[i].feverTimeAutomation;
            reductionOfLevelUpFertilizerRequirement[i] = myObject.users[i].reductionOfLevelUpFertilizerRequirement;
            increaseGameTime[i] = myObject.users[i].increaseGameTime;
            chestnutAppearanceRate[i] = myObject.users[i].chestnutAppearanceRate;
            monsterRegenerationRate[i] = myObject.users[i].monsterRegenerationRate;
            birdMovementSpeed[i] = myObject.users[i].birdMovementSpeed;
            whistleOverallGaugeReduction[i] = myObject.users[i].whistleOverallGaugeReduction;
            position[i] = myObject.users[i].position;
        }
        SetTreeObjectStatus1();
    }    
    /// <summary>
    /// 나무는 생성하지 않고 특성만 넣는 함수
    /// </summary>
    void SetTreeObjectStatus1()
    {
        for (int i = 0; i < treeOb.Length; i++)
        {
            treeOb[i].GetComponent<TreeStatus>().TreeLevel = level[i];
            treeOb[i].GetComponent<TreeStatus>().TreeName = id[i];
            treeOb[i].GetComponent<TreeStatus>().nft_id = nft_id[i];
            treeOb[i].GetComponent<TreeStatus>().nft_name = nft_name[i];
            treeOb[i].GetComponent<TreeStatus>().chestnutHarvest = chestnutHarvest[i];
            treeOb[i].GetComponent<TreeStatus>().doubleTheChestnutHarvest = doubleTheChestnutHarvest[i];
            treeOb[i].GetComponent<TreeStatus>().feverTimeIncrease = feverTimeIncrease[i];
            treeOb[i].GetComponent<TreeStatus>().increasedFeverTimeRewards = increasedFeverTimeRewards[i];
            treeOb[i].GetComponent<TreeStatus>().fevertimeAutomation = feverTimeAutomation[i];
            treeOb[i].GetComponent<TreeStatus>().reductionOfLevelUpFertilizerRequirement = reductionOfLevelUpFertilizerRequirement[i];
            treeOb[i].GetComponent<TreeStatus>().increaseGameTime = increaseGameTime[i];
            treeOb[i].GetComponent<TreeStatus>().chestnutAppearanceRate = chestnutAppearanceRate[i];
            treeOb[i].GetComponent<TreeStatus>().monsterRegenerationRate = monsterRegenerationRate[i];
            treeOb[i].GetComponent<TreeStatus>().birdMovementSpeed = birdMovementSpeed[i];
            treeOb[i].GetComponent<TreeStatus>().whistleOverallGaugeReduction = whistleOverallGaugeReduction[i];
            treeOb[i].GetComponent<TreeStatus>().TreePosition = position[i];
        }
    }
    /// <summary>
    /// 나무를 생성 하고 그나무에 특성을 넣는 함수
    /// </summary>
    void SetTreeObjectStatus()
    {
        for (int i = 0; i < myObject.users.Length; i++)
        {
            GameObject treeOb = Instantiate<GameObject>(treeObject,transform.position,transform.rotation);
            treeOb.transform.SetParent(transform);
            treeOb.GetComponent<TreeStatus>().TreeLevel = level[i];
            treeOb.GetComponent<TreeStatus>().TreeName = id[i];
            treeOb.GetComponent<TreeStatus>().nft_id = nft_id[i];
            treeOb.GetComponent<TreeStatus>().nft_name = nft_name[i];
            treeOb.GetComponent<TreeStatus>().chestnutHarvest = chestnutHarvest[i];
            treeOb.GetComponent<TreeStatus>().doubleTheChestnutHarvest = doubleTheChestnutHarvest[i];
            treeOb.GetComponent<TreeStatus>().feverTimeIncrease = feverTimeIncrease[i];
            treeOb.GetComponent<TreeStatus>().increasedFeverTimeRewards = increasedFeverTimeRewards[i];
            treeOb.GetComponent<TreeStatus>().fevertimeAutomation = feverTimeAutomation[i];
            treeOb.GetComponent<TreeStatus>().reductionOfLevelUpFertilizerRequirement = reductionOfLevelUpFertilizerRequirement[i];
            treeOb.GetComponent<TreeStatus>().increaseGameTime = increaseGameTime[i];
            treeOb.GetComponent<TreeStatus>().chestnutAppearanceRate = chestnutAppearanceRate[i];
            treeOb.GetComponent<TreeStatus>().monsterRegenerationRate = monsterRegenerationRate[i];
            treeOb.GetComponent<TreeStatus>().birdMovementSpeed = birdMovementSpeed[i];
            treeOb.GetComponent<TreeStatus>().whistleOverallGaugeReduction = whistleOverallGaugeReduction[i];
            treeOb.GetComponent<TreeStatus>().TreePosition = position[i];
            treeOb.GetComponent<TreeStatus>().sizeNum = ((int)(level[i] / 10)) + 1;
            treeOb.GetComponent<SpriteRenderer>().sortingOrder = position[i];
            treeOb.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = position[i]+1;
            treeOb.transform.GetChild(0).transform.GetChild(0).GetComponent<Canvas>().sortingOrder = position[i]+2;
            if (treeOb.GetComponent<TreeStatus>().TreeLevel == 1)
            {
                treeOb.GetComponent<TreeStatus>().needFertilizerNum = 10;
            }
            else if (treeOb.GetComponent<TreeStatus>().TreeLevel == 2)
            {
                treeOb.GetComponent<TreeStatus>().needFertilizerNum = 20;
            }
            else if (treeOb.GetComponent<TreeStatus>().TreeLevel >= 3)
            {
                treeOb.GetComponent<TreeStatus>().needFertilizerNum = 0;
                treeOb.GetComponent<TreeStatus>().needFertilizerNum = 10 * (treeOb.GetComponent<TreeStatus>().TreeLevel) * treeOb.GetComponent<TreeStatus>().sizeNum;
            }
            treeOb.GetComponent<Tree>().UpdateData();
            //StartCoroutine(treeOb.GetComponent<StatusLooklike>().LevelCStheck()); 
        }
    }
    /// <summary>
    /// 나무 특성들을 DB에서 받아오는 나무 생성 함수 
    /// </summary>
    /// <returns></returns>
    public IEnumerator GetTree()
    {
        WWWForm w = new WWWForm();
        w.AddField("loginUser", localID);
        w.AddField("status", "getTree");
        using (UnityWebRequest www = UnityWebRequest.Post("https://conbox.kr/Order", w))
        {
            yield return www.SendWebRequest();
            while (!www.isDone)
            {
                InfiniteLoopDetector.Run();
                Debug.Log("작동안함");
            }
            if (string.IsNullOrEmpty(www.downloadHandler.text))
            {
                yield break;
            }
            //Debug.Log(www.downloadHandler.text);
            myObject = JsonUtility.FromJson<RootObject>("{\"users\":" + www.downloadHandler.text + "}");
            SetTreeStatus();
            
        }
    }
    /// <summary>
    /// 나무 특성만 받아오고 나무객체는 생성하지않는 함수 
    /// </summary>
    /// <returns></returns>
    public IEnumerator GetTree1()
    {
        WWWForm w = new WWWForm();
        w.AddField("loginUser", localID);//�޾� �;��� ���� ���̵�
        w.AddField("status", "getTree");//Ű�� status �� getValue
        using (UnityWebRequest www = UnityWebRequest.Post("https://conbox.kr/Order", w))
        {
            yield return www.SendWebRequest();
            while (!www.isDone)
            {
                InfiniteLoopDetector.Run();
                Debug.Log("작동안함");
            }
            if (string.IsNullOrEmpty(www.downloadHandler.text))
            {
                yield break;
            }
            Debug.Log(www.downloadHandler.text);
            string str = www.downloadHandler.text;
            string[] charsToRemove = new string[] { "[", "]" };
            foreach (var c in charsToRemove)
            {
                str = str.Replace(c, string.Empty);
            }
            Debug.Log(www.downloadHandler.text);
            myObject = JsonUtility.FromJson<RootObject>("{\"users\":" + www.downloadHandler.text + "}");
            SetTreeStatus1();
        }
    }
    /// <summary>
    /// 나무 레벨 업시 DB에 있는 나무 레벨을 업데이트 하는 함수
    /// </summary>
    /// <param name="treeNum"></param>
    /// <param name="level"></param>
    /// <returns></returns>
    public IEnumerator SetTreeLevel(int treeNum,int level)
    {
        WWWForm www = new WWWForm();
        www.AddField("loginUser", localID);
        www.AddField("status", "levelValue");
        www.AddField("treeNum", treeNum);
        www.AddField("level", level);
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
    /// <summary>
    /// 게임 마지막에 추가로 밤을 얻는 특성 업데이트 함수
    /// </summary>
    /// <param name="value"></param>
    /// <param name="treeNum"></param>
    /// <returns></returns>
    public IEnumerator SetChestnutHarvest(int value, int treeNum)
    {
        WWWForm www = new WWWForm();
        www.AddField("loginUser", localID);
        www.AddField("status", "setTree");
        www.AddField("treeNum", treeNum);
        www.AddField("property", "chestnutHarvest");
        www.AddField("propertyValue", value);
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
    /// <summary>
    /// 밤 수확률을 두배 증가 시키는 특성을 나무특성에 추가하는 함수
    /// </summary>
    /// <param name="value"></param>
    /// <param name="treeNum"></param>
    /// <returns></returns>
    public IEnumerator SetDoubleTheChestnutHarvest(int value, int treeNum)
    {
        WWWForm www = new WWWForm();
        www.AddField("loginUser", localID);
        www.AddField("status", "setTree");
        www.AddField("treeNum", treeNum);
        www.AddField("property", "doubleTheChestnutHarvest");
        www.AddField("propertyValue", value);
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
    /// <summary>
    /// FeverTime 시간을 증가시켜주는 특성 업데이트 함수
    /// </summary>
    /// <param name="value"></param>
    /// <param name="treeNum"></param>
    /// <returns></returns>
    public IEnumerator SetFeverTimeIncrease(int value, int treeNum)
    {
        WWWForm www = new WWWForm();
        www.AddField("loginUser", localID);
        www.AddField("status", "setTree");
        www.AddField("treeNum", treeNum);
        www.AddField("property", "feverTimeIncrease");
        www.AddField("propertyValue", value);
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
    /// <summary>
    /// 피버타임 보상 증가 특성 추가 함수
    /// </summary>
    /// <param name="value"></param>
    /// <param name="treeNum"></param>
    /// <returns></returns>
    public IEnumerator SetIncreasedFeverTimeRewards(int value, int treeNum)
    {
        WWWForm www = new WWWForm();
        www.AddField("loginUser", localID);
        www.AddField("status", "setTree");
        www.AddField("treeNum", treeNum);
        www.AddField("property", "increasedFeverTimeRewards");
        www.AddField("propertyValue", value);
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
    /// <summary>
    /// 피버타임 자동수확 함수 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="treeNum"></param>
    /// <returns></returns>
    public IEnumerator SetFeverTimeAutomation(int value, int treeNum)
    {
        WWWForm www = new WWWForm();
        www.AddField("loginUser", localID);
        www.AddField("status", "setTree");
        www.AddField("treeNum", treeNum);
        www.AddField("property", "feverTimeAutomation");
        www.AddField("propertyValue", value);
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
    /// <summary>
    /// 비료 필요량 감소 특성
    /// </summary>
    /// <param name="value"></param>
    /// <param name="treeNum"></param>
    /// <returns></returns>
    public IEnumerator SetReductionOfLevelUpFertilizerRequirement(int value, int treeNum)
    {
        WWWForm www = new WWWForm();
        www.AddField("loginUser", localID);
        www.AddField("status", "setTree");
        www.AddField("treeNum", treeNum);
        www.AddField("property", "reductionOfLevelUpFertilizerRequirement");
        www.AddField("propertyValue", value);
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
    /// <summary>
    /// 게임 시간 증가 특성 추가 함수
    /// </summary>
    /// <param name="value"></param>
    /// <param name="treeNum"></param>
    /// <returns></returns>
    public IEnumerator SetIncreaseGameTime(int value, int treeNum)
    {
        WWWForm www = new WWWForm();
        www.AddField("loginUser", localID);
        www.AddField("status", "setTree");
        www.AddField("treeNum", treeNum);
        www.AddField("property", "increaseGameTime");
        www.AddField("propertyValue", value);
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
    /// <summary>
    /// 밤 등장 시간 감소 특성 추가 함수
    /// </summary>
    /// <param name="value"></param>
    /// <param name="treeNum"></param>
    /// <returns></returns>
    public IEnumerator SetChestnutAppearanceRate(int value, int treeNum)
    {
        WWWForm www = new WWWForm();
        www.AddField("loginUser", localID);
        www.AddField("status", "setTree");
        www.AddField("treeNum", treeNum);
        www.AddField("property", "chestnutAppearanceRate");
        www.AddField("propertyValue", value);
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
    /// <summary>
    /// 몬스터 리젠률 감소 특성 추가 함수
    /// </summary>
    /// <param name="value"></param>
    /// <param name="treeNum"></param>
    /// <returns></returns>
    public IEnumerator SetMonsterRegenerationRate(int value, int treeNum)
    {
        WWWForm www = new WWWForm();
        www.AddField("loginUser", localID);
        www.AddField("status", "setTree");
        www.AddField("treeNum", treeNum);
        www.AddField("property", "monsterRegenerationRate");
        www.AddField("propertyValue", value);
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
    /// <summary>
    /// 새 이동속도 감소 특성 추가 함수
    /// </summary>
    /// <param name="value"></param>
    /// <param name="treeNum"></param>
    /// <returns></returns>
    public IEnumerator SetBirdMovementSpeed(int value, int treeNum)
    {
        WWWForm www = new WWWForm();
        www.AddField("loginUser", localID);
        www.AddField("status", "setTree");
        www.AddField("treeNum", treeNum);
        www.AddField("property", "birdMovementSpeed");
        www.AddField("propertyValue", value);
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
    /// <summary>
    /// 휘슬 게이지 감소 특성 추가 함수
    /// </summary>
    /// <param name="value"></param>
    /// <param name="treeNum"></param>
    /// <returns></returns>
    public IEnumerator SetWhistleOverallGaugeReduction(int value, int treeNum)
    {
        WWWForm www = new WWWForm();
        www.AddField("loginUser", localID);
        www.AddField("status", "setTree");
        www.AddField("treeNum", treeNum);
        www.AddField("property", "whistleOverallGaugeReduction");
        www.AddField("propertyValue", value);
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
    /// <summary>
    /// 나무 위치값 DB 변경 특성 함수
    /// </summary>
    /// <param name="treePosition"></param>
    /// <param name="treeNum"></param>
    /// <returns></returns>
    public IEnumerator SetTreePosition(int treePosition, int treeNum)
    {
        WWWForm www = new WWWForm();
        www.AddField("loginUser", localID);
        www.AddField("status", "setTree");
        www.AddField("position", treePosition);
        www.AddField("treeNum", treeNum);
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
    #endregion
    /// <summary>
    /// 유저 DB 데이터 받아오고 변수 값 초기화 함수
    /// </summary>
    /// <returns></returns>
    public IEnumerator GetUser()
    {
        WWWForm w = new WWWForm();
        w.AddField("loginUser", localID);
        w.AddField("status", "getValue");
        using (UnityWebRequest www = UnityWebRequest.Post("https://conbox.kr/Order", w))
        {
            yield return www.SendWebRequest();
            while (!www.isDone)
            {
                InfiniteLoopDetector.Run();
                Debug.Log("작동안함");
            }
            if (string.IsNullOrEmpty(www.downloadHandler.text))
            {
                yield break;
            }
            Debug.Log(www.downloadHandler.text);
            string str = www.downloadHandler.text;
            string[] charsToRemove = new string[] { "[", "]" };
            foreach (var c in charsToRemove)
            {
                str = str.Replace(c, string.Empty);
            }
            db = JsonUtility.FromJson<DB>(str);
            if (db.chestnut == null)
            {
                chestnutPoint = 0;
            }
            else
            {
                chestnutPoint = db.chestnut;
            }
            if (db.jewelry == null)
            {
                jewelryPoint = 0;
            }
            else
            {
                jewelryPoint = db.jewelry;
            }
            if (db.energy == null)
            {
                energy = 0;
            }
            else
            {
                energy = db.energy;
            }
            if (db.fertilizer == null)
            {
                fertilizerPoint = 0;
            }
            else
            {
                fertilizerPoint = db.fertilizer;
            }
            if (db.weapon1 == null)
            {
                sprayLevelPoint = 0;
            }
            else
            {
                sprayLevelPoint = db.weapon1;
            }
            if (db.weapon2 == null)
            {
                dragonflyStickLevelPoint = 0;
            }
            else
            {
                dragonflyStickLevelPoint = db.weapon2;
            }
            if (db.start_time == null)
            {
                loginTime = null;
            }
            else
            {
                loginTime = db.start_time;
            }
            if (db.end_Time == null)
            {
                energySpendTime = null;
            }
            else
            {
                energySpendTime = db.end_Time;
            }
            EnergyCalculator();
        }
    }
    /// <summary>
    /// 에너지가 올라가면 남은 분 초 DB에 저장하는 함수
    /// </summary>
    /// <param name="minsec"></param>
    /// <returns></returns>
    public IEnumerator SetMinSec(string minsec)
    {
        WWWForm www = new WWWForm();
        www.AddField("loginUser", localID);
        www.AddField("loginPass", localPS);
        www.AddField("date_time", minsec);
        using (UnityWebRequest w = UnityWebRequest.Post("https://conbox.kr/Login", www))
        {
            yield return w.SendWebRequest();
            while (!w.isDone)
            {
                InfiniteLoopDetector.Run();
                Debug.Log("작동안함");
            }
            Debug.Log(minsec);
        }
    }

    /// <summary>
    /// 저장 시간과 현재 DB시간을 가져와 차를 구하여 에너지 생성 및 분 초를 계산 하는 함수
    /// </summary>
    public void EnergyCalculator()
    {
        DateTime NowTime =          Convert.ToDateTime(loginTime);                      //현 DB시간 
        DateTime SaveTime =         Convert.ToDateTime(energySpendTime);                //저장된 시간
        TimeSpan HeartGaugeTime =   NowTime - SaveTime;                                 //현DB시간 - 저장된 시간의 차 값
        int diffTime =              HeartGaugeTime.Hours;                               //차 시간 값
        int diffMinutes =           HeartGaugeTime.Minutes;                             //차 분 값
        int diffSeconds =           HeartGaugeTime.Seconds;                             //차 초 값
        int EnergyTime =            diffTime * 3600 + diffMinutes * 60 + diffSeconds;   //시간 분 초를 초로 바꿔 더한 값
        if (EnergyTime > 0)
        {
            if (energy < 10 && EnergyTime / ((60 * Balance.instance.energy_charging) - (int)(myObject.users.Length/10)) > 0)                              //에너지 값이 10 미만이고 에너지 생성이 되었을때
            {
                if ((energy + EnergyTime / (60 * Balance.instance.energy_charging) - (int)(myObject.users.Length / 10)) > 10)                             //DB에너지 값 + 생성된 에너지 갯수가 10 초과이면
                {
                    energy =       10;                                                  //10으로 초기화
                    StartCoroutine(SetEnergy(0));                                       //초기화 이후 저장
                }
                else if ((energy + EnergyTime / (60 * Balance.instance.energy_charging) - (int)(myObject.users.Length / 10)) <= 10)                       //DB에너지 값 + 생성된 에너지 갯수가 10 이하이면
                {
                    energy +=      EnergyTime / (60 * Balance.instance.energy_charging) - (int)(myObject.users.Length / 10);                              //에너지에 추가할 에너지 더해줌
                    StartCoroutine(SetEnergy(0));                                       //에너지 DB에도 저장
                    StartCoroutine(EnergyTimeS());                                      //남은 분초 저장
                    StartCoroutine(SetMinSec(minute.ToString() + "minutes" + second.ToString() + "seconds"));//남은 분초 저장
                }
            }
            if (EnergyTime > 60 * 30)                                                   //생성할 에너지가 있으면 
            {
                minute =            (Balance.instance.energy_charging - 1) - ((EnergyTime % ((60 * Balance.instance.energy_charging) - (int)(myObject.users.Length / 10)))/60);                     //생성할 에너지 제외 분 저장
                second =            60-((EnergyTime % ((60 * Balance.instance.energy_charging) - (int)(myObject.users.Length / 10)))%60);                    //생성할 에너지 제외 초 저장
            }
            else if(EnergyTime <= (60 * (Balance.instance.energy_charging - 1)) - (int)(myObject.users.Length / 10))                                              //생성할 에너지가 없으면
            {
                minute =            (Balance.instance.energy_charging - 1) - (int)(myObject.users.Length / 10) - diffMinutes;                                    //분 저장
                second =            60 - diffSeconds;                                    //초 저장
            }
        }
        //값 잘 저장 되는지 확인 하는 디버그
        //Debug.Log(energy);
        //Debug.Log(NowTime);
        //Debug.Log(SaveTime);
        //Debug.Log(diffTime + "H");
        //Debug.Log(diffMinutes + "M");
        //Debug.Log(diffSeconds + "S");
        //Debug.Log(EnergyTime + "Sum");
        //Debug.Log(EnergyTime / (60 * 30) + "Heart");
        //Debug.Log(minute + "m");
        //Debug.Log(second + "s");
    }     
    /// <summary>
    /// 밤갯수를 DB에 저장하는 함수 
    /// </summary>
    /// <param name="chestValue"></param>
    /// <returns></returns>
    public IEnumerator Setchestnut(int chestValue)
    {
        WWWForm www = new WWWForm();
        www.AddField("loginUser", localID);
        www.AddField("status", "setValue");
        www.AddField("chestnut", chestValue);
        using (UnityWebRequest w = UnityWebRequest.Post("https://conbox.kr/Order", www))
        {
            yield return w.SendWebRequest();
            while (!w.isDone)
            {
                InfiniteLoopDetector.Run();
                Debug.Log("작동안함");
            }
            Debug.Log(w.downloadHandler.text);
        }
    }
    /// <summary>
    /// 보석 DB에 값을 저장 하는 함수 
    /// </summary>
    /// <param name="jewelryValue"></param>
    /// <returns></returns>
    public IEnumerator SetJewelry(int jewelryValue)//������ ���� spl�� ����� ����ϴ� �Լ� ����� StartCoroutine(MySqlSystem.instance.SetJewelry(�־���� ��));
    {
        WWWForm www = new WWWForm();
        www.AddField("loginUser", localID);
        www.AddField("status", "setValue");
        www.AddField("jewelry", jewelryValue);
        using (UnityWebRequest w = UnityWebRequest.Post("https://conbox.kr/Order", www))
        {
            yield return w.SendWebRequest();
            while (!w.isDone)
            {
                InfiniteLoopDetector.Run();
                Debug.Log("작동안함");
            }
            Debug.Log("����" + jewelryValue + "�� ���� �Ǿ����ϴ�");

        }
    }
    /// <summary>
    /// 에너지를 DB에 저장하는 함수 
    /// </summary>
    /// <param name="energyValue"></param>
    /// <returns></returns>
    public IEnumerator SetEnergy(int energyValue)
    {
        WWWForm www = new WWWForm();
        www.AddField("loginUser", localID);
        www.AddField("status", "setValue");
        www.AddField("energy",  energy + energyValue);
        using (UnityWebRequest w = UnityWebRequest.Post("https://conbox.kr/Order", www))
        {
            yield return w.SendWebRequest();
            while (!w.isDone)
            {
                InfiniteLoopDetector.Run();
                Debug.Log("작동안함");
            }
            Debug.Log("에너지저장" + energy + energyValue);
        }
    }
    /// <summary>
    /// 비료 값을 DB에 저장 하는 함수
    /// </summary>
    /// <param name="fertilizerValue"></param>
    /// <returns></returns>
    public IEnumerator SetFertilizer(int fertilizerValue)
    {
        WWWForm www = new WWWForm();
        www.AddField("loginUser", localID);
        www.AddField("status", "setValue");
        www.AddField("fertilizer",fertilizerValue);
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
    /// <summary>
    /// 스프레이 레벨을 DB에 저장하는 함수
    /// </summary>
    /// <param name="sprayLevel"></param>
    /// <returns></returns>
    public IEnumerator SetSprayLevel(int sprayLevel)
    {
        WWWForm www = new WWWForm();
        www.AddField("loginUser", localID);
        www.AddField("status", "weaponValue");
        www.AddField("weapon1", sprayLevel);
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
    /// <summary>
    /// 잠자리채 무기 레벨을 DB에 저장하는 함수
    /// </summary>
    /// <param name="dragonflyStickLevel"></param>
    /// <returns></returns>
    public IEnumerator SetDragonflyStickLevel(int dragonflyStickLevel)
    {
        WWWForm www = new WWWForm();
        www.AddField("loginUser", localID);
        www.AddField("status", "weaponValue");
        www.AddField("weapon2", dragonflyStickLevel);
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
    /// <summary>
    /// 로그인 시 아이디 비밀번호를 쏴주는 함수
    /// </summary>
    /// <param name="id"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public IEnumerator LoginOnOff(string id,string password)
    {
        WWWForm www = new WWWForm();
        www.AddField("loginUser", id);
        www.AddField("loginPass", password);
        using (UnityWebRequest w = UnityWebRequest.Post("https://conbox.kr/Login", www))
        {
            yield return w.SendWebRequest();
            while (!w.isDone)
            {
                InfiniteLoopDetector.Run();
                Debug.Log("작동안함");
            }
            //Response(w.downloadHandler.text, "luiesong", "mellonvus09@");
            Response(w.downloadHandler.text, id, password);
        }
    }
    /// <summary>
    /// 에너지가 채워질떄 DB에 시간을 저장하는 함수
    /// </summary>
    /// <returns></returns>
    public IEnumerator EnergyTimeS()
    {
        WWWForm www = new WWWForm();
        www.AddField("loginUser", localID);
        www.AddField("status", "setTime");

        using (UnityWebRequest w = UnityWebRequest.Post("https://conbox.kr/Order", www))
        {
            yield return w.SendWebRequest();
            while (!w.isDone)
            {
                InfiniteLoopDetector.Run();
                Debug.Log("작동안함");
            }
            Debug.Log(w.downloadHandler.text);
        }
    }
    /// <summary>
    /// 아이디 비밀번호를 걸러 로그인 할지 오류 요청을 할지 구분하는 함수
    /// </summary>
    /// <param name="json"></param>
    /// <param name="id"></param>
    /// <param name="password"></param>
    public void Response(string json,string id, string password)
    {
        string str = json;
        string[] charsToRemove = new string[] { "[", "]","\"" };
        foreach (var c in charsToRemove)
        {
            str = str.Replace(c, string.Empty);
        }
        if (str == "로그인 성공")
        {
            localID = id;
            localPS = password;
            //ButtonManager.instance.Save(id, password);
            StartCoroutine(GetUser());
            StartCoroutine(GetTree());
            StartCoroutine(UpdateData.instance.UpdatePanelAndVersion());
            LoadingSceneController.LoadingScene("LobbySceneRestart");
        }
        if (str == "아이디 또는 비밀번호가 맞지 않습니다.")
        {
            uncorrectPanel.gameObject.SetActive(true);
            uncorrectText.text = "ID or password does not match.";
        }

    }
    /// <summary>
    /// 잘못된 아이디 혹은 비밀번호 입력시 나오는 판업창 나가기 함수
    /// </summary>
    public void UncorrectPanelExitButton()
    {
        uncorrectPanel.gameObject.SetActive(false);
    }
}
