using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeInfomation : MonoBehaviour
{
    public int positioinNum;
    public string treeName;
    public int treeLevel;
    public int needFreilizer;
    public int statusPotionNum;
    public string statusWord;
    public string statusValue;
    TreeStatus treeStatus;
    Tree tree;
    LevelUp levelUp;
    CreatStatus creatStatus;
    [SerializeField] Transform InfoPanel;
    Transform Stamp;
    Transform DustEffect;
    [SerializeField]AudioClip[] clip;

    [Header("UpdatePanelUpdateObject")]
    [SerializeField]Text needFertilizerQuntityTextInButton;
    [SerializeField]Text treePositionText;
    [SerializeField]Text treeNameText;
    [SerializeField]Text treeLevelText;
    [SerializeField]Text statusChangePotionQuntityPotion;
    [SerializeField]Text statusWordText;
    [SerializeField]Text statusValueText;
    [SerializeField]Image[] treeStatusImages;
    [SerializeField] Transform ButtonOnOff;
    [SerializeField] Button[] treeStatusChangeButtons;
    public static int InfoTreeName;
    [SerializeField] Transform treeStatusChangeOffButton;
    [SerializeField] int count;
    Image[] statusImages;
    Transform treeInfoPanel;
    [SerializeField]
    Sprite birdMovementSpeedImage;
    [SerializeField]
    Sprite chestnutAppearanceRateImage;
    [SerializeField]
    Sprite chestnutHarvestImage;
    [SerializeField]
    Sprite doubleTheChestnutHarvestImage;
    [SerializeField]
    Sprite fevertimeAutomationImage;
    [SerializeField]
    Sprite feverTimeIncreaseImage;
    [SerializeField]
    Sprite increasedFeverTimeRewardsImage;
    [SerializeField]
    Sprite increaseGameTimeImage;
    [SerializeField]
    Sprite monsterRegenerationRateImage;
    [SerializeField]
    Sprite reductionOfLevelUpFertilizerRequirementImage;
    [SerializeField]
    Sprite whistleOverallGaugeReductionImage;
    public GameObject[] LevelupCylinderYellow;
    public GameObject LevelupCylinderWhite;
    public GameObject LevelupCylinderGreen;
    public GameObject LevelupCylinderBlue;
    public GameObject LevelupCylinderPurple;
    public GameObject LevelupCylinderRed;
    Transform NotEnoughStatusPotion;

    public  static TreeInfomation instance;
    private void Awake()
    {
        InfoPanel = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(5);
        needFertilizerQuntityTextInButton =             InfoPanel.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>();
        treeNameText =                                  InfoPanel.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
        treePositionText =                              InfoPanel.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();
        treeLevelText =                                 InfoPanel.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>();
        statusChangePotionQuntityPotion =               InfoPanel.GetChild(0).GetChild(1).GetChild(2).GetChild(1).GetComponent<Text>();
        statusWordText =                                InfoPanel.GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>();
        statusValueText =                               InfoPanel.GetChild(0).GetChild(0).GetChild(3).GetChild(1).GetComponent<Text>();
        treeStatusImages =                              InfoPanel.GetChild(0).GetChild(0).GetChild(3).GetChild(2).GetComponentsInChildren<Image>();
        treeStatusChangeButtons =                       InfoPanel.GetChild(0).GetChild(0).GetChild(3).GetChild(3).GetComponentsInChildren<Button>();
        Stamp = GameObject.FindGameObjectWithTag("HighCanvas").transform.GetChild(0).GetComponent<Transform>();
        DustEffect = GameObject.FindGameObjectWithTag("HighCanvas").transform.GetChild(2).GetComponent<Transform>();
        NotEnoughStatusPotion = GameObject.FindGameObjectWithTag("HighCanvas").transform.GetChild(3).GetComponent<Transform>();
        for (int i = 0; i < treeStatusChangeButtons.Length; i++)
        {
            treeStatusChangeButtons[i].gameObject.SetActive(false);
        }
        treeStatusChangeOffButton =                     InfoPanel.GetChild(0).GetChild(3).GetComponent<Transform>();
        treeInfoPanel =                                 GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(5).transform.GetChild(0).GetComponent<Transform>();
        ButtonOnOff =                                   GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(1).transform.GetChild(0).GetComponent<Transform>();
        statusImages =                                  GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(5).transform.GetChild(0).transform.GetChild(0).transform.GetChild(3).transform.GetChild(2).GetComponentsInChildren<Image>();
        birdMovementSpeedImage =                        Resources.Load<Sprite>("StatusColor/birdMovementSpeed");
        chestnutAppearanceRateImage =                   Resources.Load<Sprite>("StatusColor/chestnutAppearanceRate");
        chestnutHarvestImage =                          Resources.Load<Sprite>("StatusColor/chestnutHarvest");
        doubleTheChestnutHarvestImage =                 Resources.Load<Sprite>("StatusColor/doubleTheChestnutHarvest");
        fevertimeAutomationImage =                      Resources.Load<Sprite>("StatusColor/FevertimeAutomation");
        feverTimeIncreaseImage =                        Resources.Load<Sprite>("StatusColor/FeverTimeIncrease");
        increasedFeverTimeRewardsImage =                Resources.Load<Sprite>("StatusColor/IncreasedFeverTimeRewards");
        increaseGameTimeImage =                         Resources.Load<Sprite>("StatusColor/increaseGameTime");
        monsterRegenerationRateImage =                  Resources.Load<Sprite>("StatusColor/MonsterRegenerationRate");
        reductionOfLevelUpFertilizerRequirementImage =  Resources.Load<Sprite>("StatusColor/ReductionOfLevelUpFertilizerRequirement");
        whistleOverallGaugeReductionImage =             Resources.Load<Sprite>("StatusColor/WhistleOverallGaugeReduction");
        if (GameObject.FindGameObjectWithTag("HighCanvas") != null)
        {
            LevelupCylinderYellow = GameObject.FindGameObjectsWithTag("0.LevelupCylinderYellow");
            for (int i = 0; i < LevelupCylinderYellow.Length; i++)
            {
                LevelupCylinderYellow[i].gameObject.SetActive(false);
                LevelupCylinderYellow[i].GetComponent<DestroyEffect>().enabled = true;
                LevelupCylinderYellow[i].transform.position = LobbySceneUIManager.treeImage.transform.GetChild(10).position;
            }
            LevelupCylinderWhite = GameObject.FindGameObjectWithTag("1.LevelupCylinderWhite");
            LevelupCylinderGreen = GameObject.FindGameObjectWithTag("2.LevelupCylinderGreen");
            LevelupCylinderBlue = GameObject.FindGameObjectWithTag("3.LevelupCylinderBlue");
            LevelupCylinderPurple = GameObject.FindGameObjectWithTag("4.LevelupCylinderPurple");
            LevelupCylinderRed = GameObject.FindGameObjectWithTag("5.LevelupCylinderRed");
            LevelupCylinderWhite.SetActive(false);
            LevelupCylinderWhite.GetComponent<DestroyEffect>().enabled = true;
            LevelupCylinderWhite.transform.position = LobbySceneUIManager.treeImage.transform.GetChild(10).position;
            LevelupCylinderGreen.SetActive(false);
            LevelupCylinderGreen.GetComponent<DestroyEffect>().enabled = true;
            LevelupCylinderGreen.transform.position = LobbySceneUIManager.treeImage.transform.GetChild(10).position;
            LevelupCylinderBlue.SetActive(false);
            LevelupCylinderBlue.GetComponent<DestroyEffect>().enabled = true;
            LevelupCylinderBlue.transform.position = LobbySceneUIManager.treeImage.transform.GetChild(10).position;
            LevelupCylinderPurple.SetActive(false);
            LevelupCylinderPurple.GetComponent<DestroyEffect>().enabled = true;
            LevelupCylinderPurple.transform.position = LobbySceneUIManager.treeImage.transform.GetChild(10).position;
            LevelupCylinderRed.SetActive(false);
            LevelupCylinderRed.GetComponent<DestroyEffect>().enabled = true;
            LevelupCylinderRed.transform.position = LobbySceneUIManager.treeImage.transform.GetChild(10).position;
        }
        instance = this;
    }
    public void FindTree()
    {
        for (int i = 0; i < TreeSetManager.treeObject.Length; i++)
        {
            if(TreeSetManager.treeObject[i].GetComponent<TreeStatus>().TreeName == InfoTreeName)
            {
                treeStatus =    TreeSetManager.treeObject[i].GetComponent<TreeStatus>();
                levelUp =       TreeSetManager.treeObject[i].GetComponent<LevelUp>();
                tree =          TreeSetManager.treeObject[i].GetComponent<Tree>();
                creatStatus =   TreeSetManager.treeObject[i].GetComponent<CreatStatus>();
            }

        }
    }
    #region TreeInfoButton
    /// <summary>
    /// 레벨 업 및 레벨 업에 따른 UI 업데이트 
    /// </summary>
    public void LevelUp()
    {
        levelUp.HowManyNeedFertilizer();
        if (MySqlSystem.fertilizerPoint >= treeStatus.needFertilizerNum - treeStatus.reductionOfLevelUpFertilizerRequirement && MySqlSystem.fertilizerPoint - (treeStatus.needFertilizerNum - treeStatus.reductionOfLevelUpFertilizerRequirement) >= 0 && treeStatus.TreeLevel <= 49)
        {
            SoundManager.instance.PlaySFX(clip, 1, 1, 1);
            LevelUpEffect();
            levelUp.TreeLevelUp();
            ChangeTreeLevel();
            tree.UpdateData();
            UpdateInfoPanel();
            
        }
    }
    /// <summary>
    /// 나무 팔기 버튼 nft id 필요
    /// </summary>
    public void SellTreeButton()
    {
        for (int i = 0; i < TreeSetManager.treeObject.Length; i++)
        {
            Destroy(TreeSetManager.treeObject[i].gameObject);
        }
        MySqlSystem.chestnutPoint = 0;
        MySqlSystem.jewelryPoint = 0;
        MySqlSystem.energy = 0;
        MySqlSystem.fertilizerPoint = 0;
        MySqlSystem.sprayLevelPoint = 0;
        MySqlSystem.dragonflyStickLevelPoint = 0;
        MySqlSystem.instance.loginTime = null;
        MySqlSystem.instance.energySpendTime = null;
        LoadingSceneController.LoadingScene("LoginScene");
        Application.OpenURL("https://conbox.kr/nft/" + treeStatus.nft_id);
    }
    /// <summary>
    /// 특성 변경 버튼 업데이트 해야할 것 UI업데이트 및 아이템 수량 감소 
    /// </summary>
    public void ChangeStatusButton()
    {
        if (ItemManager.statusPotionCount >= 1)
        {
            tree.UpdateData();
            UpdateInfoPanel();
            treeStatusChangeOffButton.gameObject.SetActive(true);
            for (int i = 0; i < count; i++)
            {
                treeStatusChangeButtons[i].gameObject.SetActive(true);
            }
            SetChangeStatusButton();
        }
        else
        {
            NotEnoughStatusPotion.gameObject.SetActive(true);
        }
    }
    public  void NotEnoughStatusPotionPanelOffButton()
    {
        NotEnoughStatusPotion.gameObject.SetActive(false);
    }
    public void ChangeStatusOffButton()
    {
        treeStatusChangeOffButton.gameObject.SetActive(false);
        for (int i = 0; i < treeStatusChangeButtons.Length; i++)
        {
            treeStatusChangeButtons[i].gameObject.SetActive(false);
        }
    }
    void ResetStatusButtonOnOff()
    {
        for (int i = 0; i < treeStatusChangeButtons.Length; i++)
        {
            treeStatusChangeButtons[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < count; i++)
        {
            treeStatusChangeButtons[i].gameObject.SetActive(true);
        }
    }
    /// <summary>
    /// 업데이트 할 내역을 넣을 함수 
    /// 나무 레벨 나무 특성 나무 특성 값 나무 특성 이미지 수정 
    /// </summary>
    public void UpdateInfoPanel()
    {
        FindTree();
        ChangePosition();
        ChangeName();
        ChangeTreeLevel();
        ChangeTreeStatusImage();
        ChangeNeedFertilizerQuntity();
        ChangeStatusChangePotionQuntity();
        ChangeStatusWord();
        ChangeStatusValue();
        ChangeStatusOffButton();
        StartCoroutine(LobbySceneUIManager.treeImage.GetComponent<InfoTreeStatusChestnut>().LobbyLevelCStheck());
        StartCoroutine(LobbySceneUIManager.treeImage.GetComponent<InfoTreeStatusChestnut>().LobbyStatusPointCheck());
    }
    /// <summary>
    /// TreeInfoPanelOff Button
    /// </summary>
    public void TreeInfoPanelOffButton()
    {
        treeInfoPanel.gameObject.SetActive(false);
        ButtonOnOff.gameObject.SetActive(true);
    }
    void ChangeName()
    {
        treeNameText.text = "TreeName: " + treeStatus.TreeName.ToString();
    }
    void ChangePosition()
    {
        positioinNum = treeStatus.TreePosition;
        treePositionText.text ="1-" + treeStatus.TreePosition.ToString();
    }
    #endregion
    void ChangeTreeLevel()
    {
        treeLevel = treeStatus.TreeLevel;
        treeLevelText.text ="LV" +  treeStatus.TreeLevel.ToString();
    }
    void ChangeTreeStatusImage()
    {
        count = 0;
        if (treeStatus.chestnutHarvest != 0)
        {
            count++;
        }
        if (treeStatus.doubleTheChestnutHarvest != 0)
        {
            count++;
        }
        if (treeStatus.feverTimeIncrease != 0)
        {
            count++;
        }
        if (treeStatus.increasedFeverTimeRewards != 0)
        {
            count++;
        }
        if (treeStatus.fevertimeAutomation != 0)
        {
            count++;
        }
        if (treeStatus.reductionOfLevelUpFertilizerRequirement != 0)
        {
            count++;
        }
        if (treeStatus.increaseGameTime != 0)
        {
            count++;
        }
        if (treeStatus.chestnutAppearanceRate != 0)
        {
            count++;
        }
        if (treeStatus.monsterRegenerationRate != 0)
        {
            count++;
        }
        if (treeStatus.birdMovementSpeed != 0)
        {
            count++;
        }
        if (treeStatus.whistleOverallGaugeReduction != 0)
        {
            count++;
        }
        for (int y = 0; y < count; y++)
        {
            statusImages[y].enabled = true;
        }
        for (int j = 1; j < statusImages.Length; j++)
        {
            statusImages[j].gameObject.SetActive(false);
        }
        for (int x = 1; x < count + 1; x++)
        {
            statusImages[x].gameObject.SetActive(true);
        }
        int i = 1;
        while (i < count + 1)
        {
            InfiniteLoopDetector.Run();
            if (treeStatus.chestnutHarvest != 0)
            {
                statusImages[i].sprite = chestnutHarvestImage;
                i++;
            }
            if (treeStatus.doubleTheChestnutHarvest != 0)
            {
                statusImages[i].sprite = doubleTheChestnutHarvestImage;
                i++;
            }
            if (treeStatus.feverTimeIncrease != 0)
            {
                statusImages[i].sprite = feverTimeIncreaseImage;
                i++;
            }
            if (treeStatus.increasedFeverTimeRewards != 0)
            {
                statusImages[i].sprite = increasedFeverTimeRewardsImage;
                i++;
            }
            if (treeStatus.fevertimeAutomation != 0)
            {
                statusImages[i].sprite = fevertimeAutomationImage;
                i++;
            }
            if (treeStatus.reductionOfLevelUpFertilizerRequirement != 0)
            {
                statusImages[i].sprite = reductionOfLevelUpFertilizerRequirementImage;
                i++;
            }
            if (treeStatus.increaseGameTime != 0)
            {
                statusImages[i].sprite = increaseGameTimeImage;
                i++;
            }
            if (treeStatus.chestnutAppearanceRate != 0)
            {
                statusImages[i].sprite = chestnutAppearanceRateImage;
                i++;
            }
            if (treeStatus.monsterRegenerationRate != 0)
            {
                statusImages[i].sprite = monsterRegenerationRateImage;
                i++;
            }
            if (treeStatus.birdMovementSpeed != 0)
            {
                statusImages[i].sprite = birdMovementSpeedImage;
                i++;
            }
            if (treeStatus.whistleOverallGaugeReduction != 0)
            {
                statusImages[i].sprite = whistleOverallGaugeReductionImage;
                i++;
            }
        }
        
    }

    void ChangeNeedFertilizerQuntity()
    {
        needFreilizer = treeStatus.needFertilizerNum;
        needFertilizerQuntityTextInButton.text = treeStatus.needFertilizerNum.ToString();
        if (needFreilizer <= MySqlSystem.fertilizerPoint)
        {
            needFertilizerQuntityTextInButton.color = Color.green;
        }
        else
        {
            needFertilizerQuntityTextInButton.color = Color.red;
        }
    }
    void ChangeStatusChangePotionQuntity()
    {
        statusPotionNum = ItemManager.statusPotionCount;
        statusChangePotionQuntityPotion.text = ItemManager.statusPotionCount.ToString();
    }
    void ChangeStatusWord()
    {
        statusWord = treeStatus.statusString;
        statusWordText.text = treeStatus.statusString;
    }
    void ChangeStatusValue()
    {
        statusValue = treeStatus.statusPointString;
        statusValueText.text = treeStatus.statusPointString;
    }
    void SetChangeStatusButton()
    {
        int i = 0;
        while (i < count)
        {
            for (int y = 0; y < treeStatusChangeButtons.Length; y++)
            {
                treeStatusChangeButtons[y].onClick.RemoveAllListeners();
            }
            InfiniteLoopDetector.Run();
            if (treeStatus.chestnutHarvest > 0)
            {
                treeStatusChangeButtons[i].onClick.AddListener(() =>
                {
                    if (treeStatus.chestnutHarvest > 0)
                    {
                        StartCoroutine(StampEffect());
                        treeStatus.chestnutHarvest -= 1;
                        ItemManager.statusPotionCount--;
                        StartCoroutine(ItemManager.instance.SetIStatusResetPotion());
                        StartCoroutine(MySqlSystem.instance.SetChestnutHarvest(treeStatus.chestnutHarvest, treeStatus.TreeName));
                        StartCoroutine(WaitStatusChange());
                    }
                });
                i++;
            }
            if (treeStatus.doubleTheChestnutHarvest > 0)
            {
                treeStatusChangeButtons[i].onClick.AddListener(() =>
                {
                    if (treeStatus.doubleTheChestnutHarvest > 0)
                    {
                        StartCoroutine(StampEffect());
                        treeStatus.doubleTheChestnutHarvest -= 1;
                        ItemManager.statusPotionCount--;
                        StartCoroutine(ItemManager.instance.SetIStatusResetPotion());
                        StartCoroutine(MySqlSystem.instance.SetDoubleTheChestnutHarvest(treeStatus.doubleTheChestnutHarvest, treeStatus.TreeName));
                        StartCoroutine(WaitStatusChange());
                    }
                });
                i++;
            }
            if (treeStatus.feverTimeIncrease > 0)
            {
                treeStatusChangeButtons[i].onClick.AddListener(() =>
                {
                    if (treeStatus.feverTimeIncrease > 0)
                    {
                        StartCoroutine(StampEffect());
                        treeStatus.feverTimeIncrease -= 1;
                        ItemManager.statusPotionCount--;
                        StartCoroutine(ItemManager.instance.SetIStatusResetPotion());
                        StartCoroutine(MySqlSystem.instance.SetFeverTimeIncrease(treeStatus.feverTimeIncrease, treeStatus.TreeName));
                        StartCoroutine(WaitStatusChange());
                    }
                });
                i++;
            }
            if (treeStatus.increasedFeverTimeRewards > 0)
            {
                treeStatusChangeButtons[i].onClick.AddListener(() =>
                {
                    if (treeStatus.increasedFeverTimeRewards > 0)
                    {
                        StartCoroutine(StampEffect());
                        treeStatus.increasedFeverTimeRewards -= 1;
                        ItemManager.statusPotionCount--;
                        StartCoroutine(ItemManager.instance.SetIStatusResetPotion());
                        StartCoroutine(MySqlSystem.instance.SetIncreasedFeverTimeRewards(treeStatus.increasedFeverTimeRewards, treeStatus.TreeName));
                        StartCoroutine(WaitStatusChange());
                    }
                });
                i++;
            }
            if (treeStatus.fevertimeAutomation > 0)
            {
                treeStatusChangeButtons[i].onClick.AddListener(() =>
                {
                    if (treeStatus.fevertimeAutomation > 0)
                    {
                        StartCoroutine(StampEffect());
                        treeStatus.fevertimeAutomation -= 1;
                        ItemManager.statusPotionCount--;
                        StartCoroutine(ItemManager.instance.SetIStatusResetPotion());
                        StartCoroutine(MySqlSystem.instance.SetFeverTimeAutomation(treeStatus.fevertimeAutomation, treeStatus.TreeName));
                        StartCoroutine(WaitStatusChange());

                    }
                });
                i++;
            }
            if (treeStatus.reductionOfLevelUpFertilizerRequirement > 0)
            {
                treeStatusChangeButtons[i].onClick.AddListener(() =>
                {
                    if (treeStatus.reductionOfLevelUpFertilizerRequirement > 0)
                    {
                        StartCoroutine(StampEffect());
                        treeStatus.reductionOfLevelUpFertilizerRequirement -= 1;
                        ItemManager.statusPotionCount--;
                        StartCoroutine(ItemManager.instance.SetIStatusResetPotion());
                        StartCoroutine(MySqlSystem.instance.SetReductionOfLevelUpFertilizerRequirement(treeStatus.reductionOfLevelUpFertilizerRequirement, treeStatus.TreeName));
                        StartCoroutine(WaitStatusChange());
                    }
                });
                i++;
            }
            if (treeStatus.increaseGameTime > 0)
            {
                treeStatusChangeButtons[i].onClick.AddListener(() =>
                {
                    if (treeStatus.increaseGameTime > 0)
                    {
                        StartCoroutine(StampEffect());
                        treeStatus.increaseGameTime -= 1;
                        ItemManager.statusPotionCount--;
                        StartCoroutine(ItemManager.instance.SetIStatusResetPotion());
                        StartCoroutine(MySqlSystem.instance.SetIncreaseGameTime(treeStatus.increaseGameTime, treeStatus.TreeName));
                        StartCoroutine(WaitStatusChange());
                    }
                });
                i++;
            }
            if (treeStatus.chestnutAppearanceRate > 0)
            {
                treeStatusChangeButtons[i].onClick.AddListener(() =>
                {
                    if (treeStatus.chestnutAppearanceRate > 0)
                    {
                        StartCoroutine(StampEffect());
                        treeStatus.chestnutAppearanceRate -= 1;
                        ItemManager.statusPotionCount--;
                        StartCoroutine(ItemManager.instance.SetIStatusResetPotion());
                        StartCoroutine(MySqlSystem.instance.SetChestnutAppearanceRate(treeStatus.chestnutAppearanceRate, treeStatus.TreeName));
                        StartCoroutine(WaitStatusChange());
                    }
                });
                i++;
            }
            if (treeStatus.monsterRegenerationRate > 0)
            {
                treeStatusChangeButtons[i].onClick.AddListener(() =>
                {
                    if (treeStatus.monsterRegenerationRate > 0)
                    {
                        StartCoroutine(StampEffect());
                        treeStatus.monsterRegenerationRate -= 1;
                        ItemManager.statusPotionCount--;
                        StartCoroutine(ItemManager.instance.SetIStatusResetPotion());
                        StartCoroutine(MySqlSystem.instance.SetMonsterRegenerationRate(treeStatus.monsterRegenerationRate, treeStatus.TreeName));
                        StartCoroutine(WaitStatusChange());
                    }
                });
                i++;
            }
            if (treeStatus.birdMovementSpeed > 0)
            {
                treeStatusChangeButtons[i].onClick.AddListener(() =>
                {
                    if (treeStatus.birdMovementSpeed > 0)
                    {
                        StartCoroutine(StampEffect());
                        treeStatus.birdMovementSpeed -= 1;
                        ItemManager.statusPotionCount--;
                        StartCoroutine(ItemManager.instance.SetIStatusResetPotion());
                        StartCoroutine(MySqlSystem.instance.SetBirdMovementSpeed(treeStatus.birdMovementSpeed, treeStatus.TreeName));
                        StartCoroutine(WaitStatusChange());
                    }
                });
                i++;
            }
            if (treeStatus.whistleOverallGaugeReduction > 0)
            {
                treeStatusChangeButtons[i].onClick.AddListener(() =>
                {
                    if (treeStatus.whistleOverallGaugeReduction > 0)
                    {
                        StartCoroutine(StampEffect());
                        treeStatus.whistleOverallGaugeReduction -= 1;
                        ItemManager.statusPotionCount--;
                        StartCoroutine(ItemManager.instance.SetIStatusResetPotion());
                        StartCoroutine(MySqlSystem.instance.SetWhistleOverallGaugeReduction(treeStatus.whistleOverallGaugeReduction, treeStatus.TreeName));
                        StartCoroutine(WaitStatusChange());
                    }
                });
                i++;
            }
        }
    }
    IEnumerator WaitStatusChange()
    {
        ChangeStatusOffButton();
        yield return new WaitForSeconds(0.7f);
        treeStatus.useStatusPoint++;
        creatStatus.CreateStatus();
        tree.UpdateData();
        UpdateInfoPanel();
        SetChangeStatusButton();
    }
    IEnumerator StampEffect()
    {
        Stamp.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.55f);
        SoundManager.instance.PlaySFX(clip, 0, 1, 1);
        DustEffect.gameObject.SetActive(true);
        DustEffect.position = Stamp.position;
    }
    public void LevelUpEffect()
    {
        
        if (treeStatus.TreeLevel == 9)
        {
            LevelupCylinderWhite.SetActive(true);
        }
        else if (treeStatus.TreeLevel == 19)
        {
            LevelupCylinderGreen.SetActive(true);
        }
        else if (treeStatus.TreeLevel == 29)
        {
            LevelupCylinderBlue.SetActive(true);
        }
        else if (treeStatus.TreeLevel == 39)
        {
            LevelupCylinderPurple.SetActive(true);
        }
        else if (treeStatus.TreeLevel == 49)
        {
            LevelupCylinderRed.SetActive(true);
        }
        else
        {
            for (int i = 0; i < LevelupCylinderYellow.Length; i++)
            {
                if (!LevelupCylinderYellow[i].activeSelf)
                {
                    LevelupCylinderYellow[i].SetActive(true);
                    i = LevelupCylinderYellow.Length - 1;
                }
            }
        }
    }

}
