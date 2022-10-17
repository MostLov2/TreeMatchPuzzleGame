using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RandomTreeStatus : MonoBehaviour
{
    //----------------Tree status----------------------------
    public int      TreeLevel;
    public int      TreeName;
    public string   nft_name;
    public string   nft_id;
    public int      sizeNum = 1;
    public int      needFertilizerNum;
    public int      chestnutHarvest = 0;
    public int      doubleTheChestnutHarvest = 0;
    public int      FeverTimeIncrease = 0;
    public int      IncreasedFeverTimeRewards = 0;
    public int      FevertimeAutomation = 0;
    public int      ReductionOfLevelUpFertilizerRequirement = 0;
    public int      increaseGameTime = 0;
    public int      chestnutAppearanceRate = 0;
    public int      MonsterRegenerationRate = 0;
    public int      birdMovementSpeed = 0;
    public int      WhistleOverallGaugeReduction = 0;
    public int      TreePosition = 0;
    public int      useStatusPoint = 0;
    public string   status;
    public string   status1;
    //----------------Tree Point----------------------------
    public int          chestnutHarvestPoint = 0;
    public int          doubleTheChestnutHarvestPoint = 0;
    public int          FeverTimeIncreasePoint = 0;
    public int          IncreasedFeverTimeRewardsPoint = 0;
    public int          FevertimeAutomationPoint = 0;
    public int          ReductionOfLevelUpFertilizerRequirementPoint = 0;
    public int          increaseGameTimePoint = 0;
    public int          chestnutAppearanceRatePoint = 0;
    public int          MonsterRegenerationRatePoint = 0;
    public int          birdMovementSpeedPoint = 0;
    public int          WhistleOverallGaugeReductionPoint = 0;
    public List<int>    haveStatusPoint = new List<int>();

    //----------------Tree Sprite----------------------------
    SpriteRenderer  spriteRenderer;
    //----------------Panel----------------------------
    Transform       GameStartPanel;
    [SerializeField]
    Transform       NotEnoughFertilizer;
    [SerializeField]
    Transform       SetInventoryPanel;
    //----------------Tree Image----------------------------
    Sprite          tree1;
    Sprite          tree2;
    Sprite          tree3;
    Sprite          tree4;
    Sprite          tree5;
    Sprite          tree6;
    //----------------Status Image----------------------------
    [SerializeField]Sprite          statusImage1;
    [SerializeField]Sprite          statusImage2;
    [SerializeField]Sprite          statusImage3;
    [SerializeField]Sprite          statusImage4;
    [SerializeField]Sprite          statusImage5;
    [SerializeField]Sprite          statusImage6;
    [SerializeField]Sprite          statusImage7;
    [SerializeField]Sprite          statusImage8;
    [SerializeField]Sprite          statusImage9;
    [SerializeField]Sprite          statusImage10;
    [SerializeField]Sprite          statusImage11;

    private float   clickTime;
    public float    minClickTime = 1;
    private bool    isClick;
    int statusCount = 0;
    private void Awake()
    {
        tree1 = Resources.Load<Sprite>("TreeImage/tree1");
        tree2 = Resources.Load<Sprite>("TreeImage/tree2");
        tree3 = Resources.Load<Sprite>("TreeImage/tree3");
        tree4 = Resources.Load<Sprite>("TreeImage/tree4");
        tree5 = Resources.Load<Sprite>("TreeImage/tree5");
        tree6 = Resources.Load<Sprite>("TreeImage/tree6");
        statusImage1 = Resources.Load<Sprite>("StatusColor/birdMovementSpeed");
        statusImage2 = Resources.Load<Sprite>("StatusColor/chestnutAppearanceRate");
        statusImage3 = Resources.Load<Sprite>("StatusColor/chestnutHarvest");
        statusImage4 = Resources.Load<Sprite>("StatusColor/doubleTheChestnutHarvest");
        statusImage5 = Resources.Load<Sprite>("StatusColor/FevertimeAutomation");
        statusImage6 = Resources.Load<Sprite>("StatusColor/FeverTimeIncrease");
        statusImage7 = Resources.Load<Sprite>("StatusColor/IncreasedFeverTimeRewards");
        statusImage8 = Resources.Load<Sprite>("StatusColor/increaseGameTime");
        statusImage9 = Resources.Load<Sprite>("StatusColor/MonsterRegenerationRate");
        statusImage10 = Resources.Load<Sprite>("StatusColor/ReductionOfLevelUpFertilizerRequirement");
        statusImage11 = Resources.Load<Sprite>("StatusColor/WhistleOverallGaugeReduction");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        ChangeImage();
        CheckStatusPoint();
        StartCoroutine(GetComponent<StatusLooklike>().StatusPointCheck());
    }
    void Update()
    {
        IsClick();
    }
    /// <summary>
    /// 실시간 나무 특성별 밤송이 특성 포인트 변경
    /// </summary>
    public void CheckStatusPoint()
    {
        haveStatusPoint.Clear();
        chestnutHarvestPoint = chestnutHarvest/10;
        haveStatusPoint.Add(chestnutHarvestPoint);
        doubleTheChestnutHarvestPoint = doubleTheChestnutHarvest/1;
        haveStatusPoint.Add(doubleTheChestnutHarvestPoint);
        FeverTimeIncreasePoint = FeverTimeIncrease/1;
        haveStatusPoint.Add(FeverTimeIncreasePoint);
        IncreasedFeverTimeRewardsPoint = IncreasedFeverTimeRewards/10;
        haveStatusPoint.Add(IncreasedFeverTimeRewardsPoint);
        FevertimeAutomationPoint = FevertimeAutomation/1;
        haveStatusPoint.Add(FevertimeAutomationPoint);
        ReductionOfLevelUpFertilizerRequirementPoint = ReductionOfLevelUpFertilizerRequirement/5;
        haveStatusPoint.Add(ReductionOfLevelUpFertilizerRequirementPoint);
        increaseGameTimePoint = increaseGameTime/1;
        haveStatusPoint.Add(increaseGameTimePoint);
        chestnutAppearanceRatePoint = (int)(chestnutAppearanceRate);
        haveStatusPoint.Add(chestnutAppearanceRatePoint);
        MonsterRegenerationRatePoint = (int)(MonsterRegenerationRate);
        haveStatusPoint.Add(MonsterRegenerationRatePoint);
        birdMovementSpeedPoint = birdMovementSpeed;
        haveStatusPoint.Add(birdMovementSpeedPoint);
        WhistleOverallGaugeReductionPoint = WhistleOverallGaugeReduction/10;
        haveStatusPoint.Add(WhistleOverallGaugeReductionPoint);
        StartCoroutine(GetComponent<StatusLooklike>().StatusPointCheck());
    }
    /// <summary>
    /// 클릭시 확인하여 시간을 측정하는 함수
    /// </summary>
    void IsClick()
    {
        if (isClick)
        {
            clickTime += Time.deltaTime;
            if(clickTime < 1)
            {
                spriteRenderer.color = new Color((255 - clickTime * 100) / 255f, (255 - clickTime * 100) / 255f, (255-clickTime * 100) / 255f);
            }
            else
            {
                spriteRenderer.color = new Color((255 - 1 * 100) / 255f, (255 - 1 * 100) / 255f, (255 - 1 * 100) / 255f, 200/255f);

            }
        }
        else
        {
            spriteRenderer.color =  Color.white;
            clickTime = 0;
        }
    }
    /// <summary>
    /// 실시간 이미지 변경 
    /// </summary>
    void ChangeImage()
    {
        if(TreeLevel <= 8)
        {
            spriteRenderer.sprite = tree1;
        }
        else if (TreeLevel <= 18)
        {
            spriteRenderer.sprite = tree2;
        }
        else if (TreeLevel <= 28)
        {
            spriteRenderer.sprite = tree3;
        }
        else if (TreeLevel <= 38)
        {
            spriteRenderer.sprite = tree4;
        }
        else if (TreeLevel <= 48)
        {
            spriteRenderer.sprite = tree5;
        }
        else
        {
            spriteRenderer.sprite = tree6;
        }
    }
    /// <summary>
    /// 레벨 계산하는 함수
    /// </summary>
    void LevelCalculator()
    {
        if(TreeLevel%10 == 8)
        {
            sizeNum++;
        }
        else if (TreeLevel % 10 == 9)
        {
            sizeNum = sizeNum;
        }
        else
        {
            sizeNum = (int)(TreeLevel / 10) + 1;
        }
        
        if (MySqlSystem.fertilizerPoint >= needFertilizerNum- ReductionOfLevelUpFertilizerRequirement && MySqlSystem.fertilizerPoint - (needFertilizerNum- ReductionOfLevelUpFertilizerRequirement) >=0&&TreeLevel<=49)
        {
            MySqlSystem.fertilizerPoint -= needFertilizerNum- ReductionOfLevelUpFertilizerRequirement;
            StartCoroutine(MySqlSystem.instance.SetFertilizer(MySqlSystem.fertilizerPoint));
            TreeLevel++;
            if (TreeLevel == 1)
            {
                needFertilizerNum = 10;
            }
            else if (TreeLevel == 2)
            {
                needFertilizerNum = 20;
            }
            else if (TreeLevel >= 3)
            {
                needFertilizerNum = 0;
                needFertilizerNum = 10 * (TreeLevel) * sizeNum;
            }
            StartCoroutine(MySqlSystem.instance.SetTreeLevel(TreeName, TreeLevel));

            if(TreeLevel % 10 == 0)
            {
                if(TreeLevel != 0)
                {
                    useStatusPoint +=1;
                    ChangeImage();
                    GetStatus();
                    CheckStatusPoint();
                    StartCoroutine(GetComponent<StatusLooklike>().LevelCStheck());
                    StartCoroutine(LobbySceneUIManager.treeImage.GetComponent<InfoTreeStatusChestnut>().LobbyLevelCStheck());
                    StartCoroutine(LobbySceneUIManager.treeImage.GetComponent<InfoTreeStatusChestnut>().LobbyStatusPointCheck());
                }
            }
        }
        else if(MySqlSystem.fertilizerPoint < needFertilizerNum)
        {
            NotEnoughFertilizer = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(16).transform.GetChild(0).GetComponent<Transform>();
            NotEnoughFertilizer.gameObject.SetActive(true);
            Transform treeInfoPanel = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(5).transform.GetChild(0).GetComponent<Transform>();
            treeInfoPanel.gameObject.SetActive(false);
        }

    }
    /// <summary>
    /// 나무 레벨업 함수 
    /// </summary>
    public void TreeLevelUp()
    {
        if (TreeLevel <= 49)
        {
            LevelCalculator();
        }
       
    }
    /// <summary>
    /// 레벨 10당 특성을 하나 추가해주는 함수
    /// </summary>
    public void GetStatus()
    {

        if (useStatusPoint >= 1)
        {
            useStatusPoint--;
            int RandomNum = Random.Range(1,101);
            if(doubleTheChestnutHarvest ==0&&FevertimeAutomation == 0)
            {
                if (RandomNum < 10)//���Ȯ��
                {
                    chestnutHarvest += 10;
                    StartCoroutine(MySqlSystem.instance.SetChestnutHarvest(chestnutHarvest, TreeName));
                }
                else if (RandomNum < 11)//���Ȯ �ι�
                {
                    doubleTheChestnutHarvest = 1;
                    StartCoroutine(MySqlSystem.instance.SetDoubleTheChestnutHarvest(1, TreeName));
                }
                else if (RandomNum < 21)//�ǹ�Ÿ�� �ð� ����
                {
                    FeverTimeIncrease += 1;
                    StartCoroutine(MySqlSystem.instance.SetFeverTimeIncrease(FeverTimeIncrease, TreeName));
                }
                else if (RandomNum < 31)//�ǹ�Ÿ�� ��������
                {
                    IncreasedFeverTimeRewards += 10;
                    StartCoroutine(MySqlSystem.instance.SetIncreasedFeverTimeRewards(IncreasedFeverTimeRewards, TreeName));
                }
                else if (RandomNum < 41)//�ǹ�Ÿ�� �ڵ�ȭ
                {
                    FevertimeAutomation = 1;
                    StartCoroutine(MySqlSystem.instance.SetFeverTimeAutomation(1, TreeName));
                }
                else if (RandomNum < 42)//�������� �ʿ��� ��ᷮ ����
                {
                    ReductionOfLevelUpFertilizerRequirement += 5;
                    StartCoroutine(MySqlSystem.instance.SetReductionOfLevelUpFertilizerRequirement(ReductionOfLevelUpFertilizerRequirement, TreeName));
                }
                else if (RandomNum < 52)//���� �ð� ����
                {
                    increaseGameTime += 1;
                    StartCoroutine(MySqlSystem.instance.SetIncreaseGameTime(increaseGameTime, TreeName));
                }
                else if (RandomNum < 64)//�� ���� Ȯ��
                {
                    chestnutAppearanceRate += 1;
                    StartCoroutine(MySqlSystem.instance.SetChestnutAppearanceRate(chestnutAppearanceRate, TreeName));
                }
                else if (RandomNum < 76)//���� ������
                {
                    MonsterRegenerationRate += 1;
                    StartCoroutine(MySqlSystem.instance.SetMonsterRegenerationRate(MonsterRegenerationRate, TreeName));
                }
                else if (RandomNum < 88)//���� �̵��ӵ�
                {
                    birdMovementSpeed += 1;
                    StartCoroutine(MySqlSystem.instance.SetBirdMovementSpeed(birdMovementSpeed, TreeName));
                }
                else if (RandomNum < 101)//�ֽ� ������ max��ġ ����
                {
                    WhistleOverallGaugeReduction += 10;
                    StartCoroutine(MySqlSystem.instance.SetWhistleOverallGaugeReduction(WhistleOverallGaugeReduction, TreeName));
                }
            }
            else if (doubleTheChestnutHarvest == 1 && FevertimeAutomation == 0)
            {
                if (RandomNum < 11)//���Ȯ��
                {
                    chestnutHarvest += 10;
                    StartCoroutine(MySqlSystem.instance.SetChestnutHarvest(chestnutHarvest, TreeName));
                }
                else if (RandomNum < 21)//�ǹ�Ÿ�� �ð� ����
                {
                    FeverTimeIncrease += 1;
                    StartCoroutine(MySqlSystem.instance.SetFeverTimeIncrease(FeverTimeIncrease, TreeName));
                }
                else if (RandomNum < 31)//�ǹ�Ÿ�� ��������
                {
                    IncreasedFeverTimeRewards += 10;
                    StartCoroutine(MySqlSystem.instance.SetIncreasedFeverTimeRewards(IncreasedFeverTimeRewards, TreeName));
                }
                else if (RandomNum < 41)//�ǹ�Ÿ�� �ڵ�ȭ
                {
                    FevertimeAutomation = 1;
                    StartCoroutine(MySqlSystem.instance.SetFeverTimeAutomation(1, TreeName));
                }
                else if (RandomNum < 42)//�������� �ʿ��� ��ᷮ ����
                {
                    ReductionOfLevelUpFertilizerRequirement += 5;
                    StartCoroutine(MySqlSystem.instance.SetReductionOfLevelUpFertilizerRequirement(ReductionOfLevelUpFertilizerRequirement, TreeName));
                }
                else if (RandomNum < 52)//���� �ð� ����
                {
                    increaseGameTime += 1;
                    StartCoroutine(MySqlSystem.instance.SetIncreaseGameTime(increaseGameTime, TreeName));
                }
                else if (RandomNum < 64)//�� ���� Ȯ��
                {
                    chestnutAppearanceRate += 1;
                    StartCoroutine(MySqlSystem.instance.SetChestnutAppearanceRate(chestnutAppearanceRate, TreeName));
                }
                else if (RandomNum < 76)//���� ������
                {
                    MonsterRegenerationRate += 1;
                    StartCoroutine(MySqlSystem.instance.SetMonsterRegenerationRate(MonsterRegenerationRate, TreeName));
                }
                else if (RandomNum < 88)//���� �̵��ӵ�
                {
                    birdMovementSpeed += 1;
                    StartCoroutine(MySqlSystem.instance.SetBirdMovementSpeed(birdMovementSpeed, TreeName));
                }
                else if (RandomNum < 100)//�ֽ� ������ max��ġ ����
                {
                    WhistleOverallGaugeReduction += 10;
                    StartCoroutine(MySqlSystem.instance.SetWhistleOverallGaugeReduction(WhistleOverallGaugeReduction, TreeName));
                }
            }
            else if(doubleTheChestnutHarvest == 0 && FevertimeAutomation == 1)
            {
                if (RandomNum < 12)//���Ȯ��
                {
                    chestnutHarvest += 10;
                    StartCoroutine(MySqlSystem.instance.SetChestnutHarvest(chestnutHarvest, TreeName));
                }
                else if (RandomNum < 13)//���Ȯ �ι�
                {
                    doubleTheChestnutHarvest = 1;
                    StartCoroutine(MySqlSystem.instance.SetDoubleTheChestnutHarvest(1, TreeName));
                }
                else if (RandomNum < 25)//�ǹ�Ÿ�� �ð� ����
                {
                    FeverTimeIncrease += 1;
                    StartCoroutine(MySqlSystem.instance.SetFeverTimeIncrease(FeverTimeIncrease, TreeName));
                }
                else if (RandomNum < 37)//�ǹ�Ÿ�� ��������
                {
                    IncreasedFeverTimeRewards += 10;
                    StartCoroutine(MySqlSystem.instance.SetIncreasedFeverTimeRewards(IncreasedFeverTimeRewards, TreeName));
                }
                else if (RandomNum < 38)//�������� �ʿ��� ��ᷮ ����
                {
                    ReductionOfLevelUpFertilizerRequirement += 5;
                    StartCoroutine(MySqlSystem.instance.SetReductionOfLevelUpFertilizerRequirement(ReductionOfLevelUpFertilizerRequirement, TreeName));
                }
                else if (RandomNum < 48)//���� �ð� ����
                {
                    increaseGameTime += 1;
                    StartCoroutine(MySqlSystem.instance.SetIncreaseGameTime(increaseGameTime, TreeName));
                }
                else if (RandomNum < 61)//�� ���� Ȯ��
                {
                    chestnutAppearanceRate += 1;
                    StartCoroutine(MySqlSystem.instance.SetChestnutAppearanceRate(chestnutAppearanceRate, TreeName));
                }
                else if (RandomNum < 74)//���� ������
                {
                    MonsterRegenerationRate += 1;
                    StartCoroutine(MySqlSystem.instance.SetMonsterRegenerationRate(MonsterRegenerationRate, TreeName));
                }
                else if (RandomNum < 87)//���� �̵��ӵ�
                {
                    birdMovementSpeed += 1;
                    StartCoroutine(MySqlSystem.instance.SetBirdMovementSpeed(birdMovementSpeed, TreeName));
                }
                else if (RandomNum < 101)//�ֽ� ������ max��ġ ����
                {
                    WhistleOverallGaugeReduction += 10;
                    StartCoroutine(MySqlSystem.instance.SetWhistleOverallGaugeReduction(WhistleOverallGaugeReduction, TreeName));
                }
            }
            else if(doubleTheChestnutHarvest == 1 && FevertimeAutomation == 1)
            {
                if (RandomNum < 12)//���Ȯ��
                {
                    chestnutHarvest += 10;
                    StartCoroutine(MySqlSystem.instance.SetChestnutHarvest(chestnutHarvest, TreeName));
                }
                else if (RandomNum < 24)//�ǹ�Ÿ�� �ð� ����
                {
                    FeverTimeIncrease += 1;
                    StartCoroutine(MySqlSystem.instance.SetFeverTimeIncrease(FeverTimeIncrease, TreeName));
                }
                else if (RandomNum < 36)//�ǹ�Ÿ�� ��������
                {
                    IncreasedFeverTimeRewards += 10;
                    StartCoroutine(MySqlSystem.instance.SetIncreasedFeverTimeRewards(IncreasedFeverTimeRewards, TreeName));
                }
                else if (RandomNum < 38)//�������� �ʿ��� ��ᷮ ����
                {
                    ReductionOfLevelUpFertilizerRequirement += 5;
                    StartCoroutine(MySqlSystem.instance.SetReductionOfLevelUpFertilizerRequirement(ReductionOfLevelUpFertilizerRequirement, TreeName));
                }
                else if (RandomNum < 48)//���� �ð� ����
                {
                    increaseGameTime += 1;
                    StartCoroutine(MySqlSystem.instance.SetIncreaseGameTime(increaseGameTime, TreeName));
                }
                else if (RandomNum < 61)//�� ���� Ȯ��
                {
                    chestnutAppearanceRate += 1;
                    StartCoroutine(MySqlSystem.instance.SetChestnutAppearanceRate(chestnutAppearanceRate, TreeName));
                }
                else if (RandomNum < 74)//���� ������
                {
                    MonsterRegenerationRate += 1;
                    StartCoroutine(MySqlSystem.instance.SetMonsterRegenerationRate(MonsterRegenerationRate, TreeName));
                }
                else if (RandomNum < 87)//���� �̵��ӵ�
                {
                    birdMovementSpeed += 1;
                    StartCoroutine(MySqlSystem.instance.SetBirdMovementSpeed(birdMovementSpeed, TreeName));
                }
                else if (RandomNum < 101)//�ֽ� ������ max��ġ ����
                {
                    WhistleOverallGaugeReduction += 10;
                    StartCoroutine(MySqlSystem.instance.SetWhistleOverallGaugeReduction(WhistleOverallGaugeReduction, TreeName));
                }
            }
        }
    }
    /// <summary>
    /// 인벤토리로 옮기는 함수
    /// </summary>
    public void SetTreeInventoryPanel()
    {
        TreeSetManager.treeName = TreeName;
        if (GameObject.FindGameObjectWithTag("Canvas") != null)
        {
            SetInventoryPanel = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(10).transform.GetChild(0).transform.GetChild(0).GetComponent<Transform>();
        }
        Vector2 screenPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(SetInventoryPanel.transform.parent.GetComponent<RectTransform>(), Input.mousePosition, Camera.main, out screenPoint);
        SetInventoryPanel.localPosition = screenPoint;
        SetInventoryPanel.parent.gameObject.SetActive(true);
    }
    /*public void TouchTree()
    {
        TreeSetManager.treeName = TreeName;
        LobbySceneUIManager.treeName.text = "TreeName:" + TreeName.ToString();
        LobbySceneUIManager.treePosition.text = "1-" + TreePosition.ToString();
        LobbySceneUIManager.treeLevel.text = "Lv:" + TreeLevel.ToString();
        LobbySceneUIManager.treeImage.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        LobbySceneUIManager.treeStatusText.text = TreeInfoStatus();
        LobbySceneUIManager.treeStatusText1.text = TreeInfoStatus1();
        LobbySceneUIManager.treeLevelUpPoint.text = needFertilizerNum.ToString();
        if (MySqlSystem.fertilizerPoint >= needFertilizerNum)
        {
            LobbySceneUIManager.treeLevelUpPoint.color = Color.green;
        }
        else
        {
            LobbySceneUIManager.treeLevelUpPoint.color = Color.red;
        }
        LobbySceneUIManager.nft_id = nft_id;
        StatusImage();
        LobbySceneUIManager.treeImage.GetComponent<ChangeStatusChestnut>().randomTreeStatus = this.GetComponent<RandomTreeStatus>();
        StartCoroutine(LobbySceneUIManager.treeImage.GetComponent<ChangeStatusChestnut>().LobbyLevelCStheck());
        StartCoroutine(LobbySceneUIManager.treeImage.GetComponent<ChangeStatusChestnut>().LobbyStatusPointCheck());
    }*/
    /// <summary>
    /// 버튼 클릭시 
    /// </summary>
   /* public void ButtonDown()
    {
        isClick = true;
    }
    /// <summary>
    /// 버튼 땠을때 
    /// </summary>
    public void ButtonUp()
    {
        isClick = false;
        if (clickTime >= minClickTime)
        {
            TouchTree();
            SetTreeInventoryPanel();
        }
        else
        {
            GameStartButton();
        }
    }*/
    /// <summary>
    /// 게임 시작시 데이터 전송
    /// </summary>
    /*public void GameStartButton()
    {
        GameLogicManager.treeLevel = TreeLevel;
        GameLogicManager.chestnutHarvest = chestnutHarvest;
        GameLogicManager.doubleTheChestnutHarvest = doubleTheChestnutHarvest;
        GameLogicManager.FeverTimeIncrease = FeverTimeIncrease;
        GameLogicManager.IncreasedFeverTimeRewards = IncreasedFeverTimeRewards;
        GameLogicManager.FevertimeAutomation = FevertimeAutomation;
        GameLogicManager.ReductionOfLevelUpFertilizerRequirement = ReductionOfLevelUpFertilizerRequirement;
        GameLogicManager.increaseGameTime = increaseGameTime;
        GameLogicManager.chestnutAppearanceRate = chestnutAppearanceRate * 0.1f;
        GameLogicManager.MonsterRegenerationRate = MonsterRegenerationRate*0.1f;
        GameLogicManager.birdMovementSpeed = birdMovementSpeed;
        GameLogicManager.WhistleOverallGaugeReduction = WhistleOverallGaugeReduction;
        GameStartPanel = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(14).transform.GetChild(0).GetComponent<Transform>();
        GameStartPanel.gameObject.SetActive(true);
    }*/
    string TreeInfoStatus()
    {
        statusCount = 0;
        status = null;
        if (chestnutHarvest != 0)
        {
            status +="C.PointBounus:";
            statusCount++;
            if (status != null)
            {
                status += "\n";
            }
        }
        if (doubleTheChestnutHarvest != 0)
        {
            status += "DoubleCpoint:";
            statusCount++;
            if (status != null)
            {
                status += "\n";
            }
        }
        if (FeverTimeIncrease != 0)
        {
            status += "FeverTimeBounus:";
            statusCount++;
            if (status != null)
            {
                status += "\n";
            }
        }
        if (IncreasedFeverTimeRewards != 0)
        {
            status +="FeverRewardsBounus:";
            statusCount++;
            if (status != null)
            {
                status += "\n";
            }
        }
        if (FevertimeAutomation != 0)
        {
            status +="FeverAutomation:";
            statusCount++;
            if (status != null)
            {
                status += "\n";
            }
        }
        if (ReductionOfLevelUpFertilizerRequirement != 0)
        {
            status +="FertilizerReduce:";
            statusCount++;
            if (status != null)
            {
                status += "\n";
            }
        }
        if (increaseGameTime != 0)
        {
            status +="GameTimeBounus:";
            statusCount++;
            if (status != null)
            {
                status += "\n";
            }
        }
        if (chestnutAppearanceRate != 0)
        {
            status +="C.SpawnReduce:";
            statusCount++;
            if (status != null)
            {
                status += "\n";
            }
        }
        if (MonsterRegenerationRate != 0)
        {
            status += "M.SpawnReduce:";
            statusCount++;
            if (status != null)
            {
                status += "\n";
            }
        }
        if (birdMovementSpeed != 0)
        {
            status +="BirdSpeedDown:";
            statusCount++;
            if (status != null)
            {
                status += "\n";
            }
        }
        if (WhistleOverallGaugeReduction != 0)
        {
            status += "WhistlePointDown:";
            statusCount++;
            if (status != null)
            {
                status += "\n";
            }
        }
        return status;
    }
    string TreeInfoStatus1()
    {

        status = null;
        if (chestnutHarvest != 0)
        {
            status += chestnutHarvest.ToString();
            if (status != null)
            {
                status += "\n";
            }
        }
        if (doubleTheChestnutHarvest != 0)
        {
            status += "On";
            if (status != null)
            {
                status += "\n";
            }
        }
        if (FeverTimeIncrease != 0)
        {
            status += FeverTimeIncrease.ToString();
            if (status != null)
            {
                status += "\n";
            }
        }
        if (IncreasedFeverTimeRewards != 0)
        {
            status += IncreasedFeverTimeRewards.ToString();
            if (status != null)
            {
                status += "\n";
            }
        }
        if (FevertimeAutomation != 0)
        {
            status +="On";
            if (status != null)
            {
                status += "\n";
            }
        }
        if (ReductionOfLevelUpFertilizerRequirement != 0)
        {
            status +=ReductionOfLevelUpFertilizerRequirement.ToString();
            if (status != null)
            {
                status += "\n";
            }
        }
        if (increaseGameTime != 0)
        {
            status += increaseGameTime.ToString();
            if (status != null)
            {
                status += "\n";
            }
        }
        if (chestnutAppearanceRate != 0)
        {
            status += (chestnutAppearanceRate * 0.1f).ToString();
            if (status != null)
            {
                status += "\n";
            }
        }
        if (MonsterRegenerationRate != 0)
        {
            status += (MonsterRegenerationRate * 0.1f).ToString();
            if (status != null)
            {
                status += "\n";
            }
        }
        if (birdMovementSpeed != 0)
        {
            status += birdMovementSpeed.ToString();
            if (status != null)
            {
                status += "\n";
            }
        }
        if (WhistleOverallGaugeReduction != 0)
        {
            status += WhistleOverallGaugeReduction.ToString();
            if (status != null)
            {
                status += "\n";
            }
        }
        return status;
    }
    void StatusImage()
    {
        for (int j = 1; j < LobbySceneUIManager.statusImage.Length; j++)
        {
            LobbySceneUIManager.statusImage[j].gameObject.SetActive(false);
        }
        for (int x = 1; x < statusCount+1; x++)
        {
            LobbySceneUIManager.statusImage[x].gameObject.SetActive(true);
        }
        int i = 1;
        while(i < statusCount + 1)
        {
            InfiniteLoopDetector.Run();
            if (chestnutHarvest != 0)
            {
                LobbySceneUIManager.statusImage[i].sprite = statusImage3;
                i++;
            }
            if (doubleTheChestnutHarvest != 0)
            {
                LobbySceneUIManager.statusImage[i].sprite = statusImage4;
                i++;
            }
            if (FeverTimeIncrease != 0)
            {
                LobbySceneUIManager.statusImage[i].sprite = statusImage6;
                i++;
            }
            if (IncreasedFeverTimeRewards != 0)
            {
                LobbySceneUIManager.statusImage[i].sprite = statusImage7;
                i++;
            }
            if (FevertimeAutomation != 0)
            {
                LobbySceneUIManager.statusImage[i].sprite = statusImage5;
                i++;
            }
            if (ReductionOfLevelUpFertilizerRequirement != 0)
            {
                LobbySceneUIManager.statusImage[i].sprite = statusImage10;
                i++;
            }
            if (increaseGameTime != 0)
            {
                LobbySceneUIManager.statusImage[i].sprite = statusImage8;
                i++;
            }
            if (chestnutAppearanceRate != 0)
            {
                LobbySceneUIManager.statusImage[i].sprite = statusImage2;
                i++;
            }
            if (MonsterRegenerationRate != 0)
            {
                LobbySceneUIManager.statusImage[i].sprite = statusImage9;
                i++;
            }
            if (birdMovementSpeed != 0)
            {
                LobbySceneUIManager.statusImage[i].sprite = statusImage1;
                i++;
            }
            if (WhistleOverallGaugeReduction != 0)
            {
                LobbySceneUIManager.statusImage[i].sprite = statusImage11;
                i++;
            }
        }
    }
}
