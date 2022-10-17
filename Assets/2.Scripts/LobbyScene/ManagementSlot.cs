using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagementSlot : MonoBehaviour
{
    //--------------SlotInfo---------------------
    public int managementSlotTreeName;
    string status;
    string status1;
    public int PositionNum;
    //--------------treeObject---------------------
    [SerializeField]
    GameObject[] treeObject;
    int count;
    //--------------Panle---------------------
    Transform InfoPanel;
    Transform InformationPanel;
    //--------------content---------------------
    Transform content;
    //----------------Status Image----------------------------
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
    public Button ThisButton;
    private void Awake()
    {
        InfoPanel = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(5).transform.GetChild(0).GetComponent<Transform>();
        InformationPanel = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(4).transform.GetChild(0).GetComponent<Transform>();
        content = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(4).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Transform>();
        treeObject = GameObject.FindGameObjectsWithTag("Tree");
        birdMovementSpeedImage = Resources.Load<Sprite>("StatusColor/birdMovementSpeed");
        chestnutAppearanceRateImage = Resources.Load<Sprite>("StatusColor/chestnutAppearanceRate");
        chestnutHarvestImage = Resources.Load<Sprite>("StatusColor/chestnutHarvest");
        doubleTheChestnutHarvestImage = Resources.Load<Sprite>("StatusColor/doubleTheChestnutHarvest");
        fevertimeAutomationImage = Resources.Load<Sprite>("StatusColor/FevertimeAutomation");
        feverTimeIncreaseImage = Resources.Load<Sprite>("StatusColor/FeverTimeIncrease");
        increasedFeverTimeRewardsImage = Resources.Load<Sprite>("StatusColor/IncreasedFeverTimeRewards");
        increaseGameTimeImage = Resources.Load<Sprite>("StatusColor/increaseGameTime");
        monsterRegenerationRateImage = Resources.Load<Sprite>("StatusColor/MonsterRegenerationRate");
        reductionOfLevelUpFertilizerRequirementImage = Resources.Load<Sprite>("StatusColor/ReductionOfLevelUpFertilizerRequirement");
        whistleOverallGaugeReductionImage = Resources.Load<Sprite>("StatusColor/WhistleOverallGaugeReduction");
        ThisButton = GetComponent<Button>();
        ClickThisButton();
    }
    void ClickThisButton()
    {
        ThisButton.onClick.AddListener(() =>
        {
            TreeInfomation.InfoTreeName = managementSlotTreeName;
            TreeInfomation.instance.FindTree();
            TreeInfomation.instance.UpdateInfoPanel();
            StartCoroutine(LobbySceneUIManager.treeImage.GetComponent<InfoTreeStatusChestnut>().LobbyLevelCStheck());
            StartCoroutine(LobbySceneUIManager.treeImage.GetComponent<InfoTreeStatusChestnut>().LobbyStatusPointCheck());
            PanelOnOff();
        });
    }
    private void Update()
    {
        ChangeInfo();
        transform.localScale = Vector3.one;
        if (PositionNum == 0)
        {
            transform.SetParent(GameObject.Find("UIManager").transform);
        }
        if (PositionNum != 0)
        {
            transform.SetParent(content);
        }
    }
    /// <summary>
    /// 나무 정보창 슬롯 정보 업데이트 
    /// </summary>
    private void ChangeInfo()
    {
        for (int i = 0; i < treeObject.Length; i++)
        {
            if (treeObject[i] != null)
            {
                if (treeObject[i].GetComponent<TreeStatus>().TreeName == managementSlotTreeName)
                {
                    transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = treeObject[i].GetComponent<SpriteRenderer>().sprite;
                    transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = "TreeName:" + treeObject[i].GetComponent<TreeStatus>().TreeName + "\n" + "Lv " + treeObject[i].GetComponent<TreeStatus>().TreeLevel;
                    PositionNum = treeObject[i].GetComponent<TreeStatus>().TreePosition;
                }
            }
        }
    }
    /// <summary>
    /// 나무 정보창 슬롯 클릭시 정보창 업데이트
    /// </summary>
    /*public void ManagementSlotButton()
    {
        for (int i = 0; i < treeObject.Length; i++)
        {
            if (treeObject[i].GetComponent<TreeStatus>().TreeName == managementSlotTreeName)
            {
                managementSlotTreeName = treeObject[i].GetComponent<TreeStatus>().TreeName;
                TreeSetManager.treeName = managementSlotTreeName;
                LobbySceneUIManager.treeName.text = "TreeName:" + treeObject[i].GetComponent<TreeStatus>().TreeName.ToString();
                LobbySceneUIManager.treePosition.text = "1-" + treeObject[i].GetComponent<TreeStatus>().TreePosition.ToString();
                LobbySceneUIManager.treeLevel.text = "Lv:" + treeObject[i].GetComponent<TreeStatus>().TreeLevel.ToString();
                LobbySceneUIManager.treeImage.sprite = treeObject[i].GetComponent<SpriteRenderer>().sprite;
                LobbySceneUIManager.treeStatusText.text = TreeInfoStatus(i);
                LobbySceneUIManager.treeStatusText1.text = TreeInfoStatus1(i);
                LobbySceneUIManager.nft_id = treeObject[i].GetComponent<TreeStatus>().nft_id;
                LobbySceneUIManager.treeImage.GetComponent<InfoTreeStatusChestnut>().TreeStatus = treeObject[i].GetComponent<TreeStatus>();
                LobbySceneUIManager.treeLevelUpPoint.text = treeObject[i].GetComponent<TreeStatus>().needFertilizerNum.ToString();
                if (MySqlSystem.fertilizerPoint >= treeObject[i].GetComponent<TreeStatus>().needFertilizerNum)
                {
                    LobbySceneUIManager.treeLevelUpPoint.color = Color.green;
                }
                else
                {
                    LobbySceneUIManager.treeLevelUpPoint.color = Color.red;
                }
                StartCoroutine(LobbySceneUIManager.treeImage.GetComponent<InfoTreeStatusChestnut>().LobbyLevelCStheck());
                StartCoroutine(LobbySceneUIManager.treeImage.GetComponent<InfoTreeStatusChestnut>().LobbyStatusPointCheck());
                StatusImage(i);
            }
            
        }
    }*/
    public void PanelOnOff()
    {
        InfoPanel.gameObject.SetActive(true);
    }
    /// <summary>
    /// 나무 정보창 특성 업데이트
    /// </summary>
    /// <param name="treeNum"></param>
    /// <returns></returns>
    /*    public string TreeInfoStatus(int treeNum)
        {
            count = 0;
            status = null;
            if(treeObject[treeNum].GetComponent<TreeStatus>().chestnutHarvest !=0)
            {
                status += "ChsetnutPointBounus:";
                count++;
                if (status != null)
                {
                    status += "\n";
                }
            {
                status += "DoubleChestnutpoint:";
                count++;6464654646
                if (status != null)
                {
                    status += "\n";
                }
            }
            if (treeObject[treeNum].GetComponent<TreeStatus>().FeverTimeIncrease !=0)
            {
                status += "FeverTimeBounus:";
                count++;
                if (status != null)
                {
                    status += "\n";
                }
            }
            if (treeObject[treeNum].GetComponent<TreeStatus>().IncreasedFeverTimeRewards !=0)
            {
                status += "FeverRewardsBounus:";
                count++;
                if (status != null)
                {
                    status += "\n";
                }
            }
            if (treeObject[treeNum].GetComponent<TreeStatus>().FevertimeAutomation !=0)
            {
                status += "FeverAutomation:";
                count++;
                if (status != null)
                {
                    status += "\n";
                }
            }
            if (treeObject[treeNum].GetComponent<TreeStatus>().ReductionOfLevelUpFertilizerRequirement !=0)
            {
                status += "FertilizerReduce:";
                count++;
                if (status != null)
                {
                    status += "\n";
                }
            }
            if (treeObject[treeNum].GetComponent<TreeStatus>().increaseGameTime !=0)
            {
                status +="GameTimeBounus:";
                count++;
                if (status != null)
                {
                    status += "\n";
                }
            }
            if (treeObject[treeNum].GetComponent<TreeStatus>().chestnutAppearanceRate !=0)
            {
                status += "C.SpawnReduce:";
                count++;
                if (status != null)
                {
                    status += "\n";
                }
            }
            if (treeObject[treeNum].GetComponent<TreeStatus>().MonsterRegenerationRate !=0)
            {
                status += "M.SpawnReduce:";
                count++;
                if (status != null)
                {
                    status += "\n";
                }
            }
            if (treeObject[treeNum].GetComponent<TreeStatus>().birdMovementSpeed !=0)
            {
                status += "BirdSpeedDown:";
                count++;
                if (status != null)
                {
                    status += "\n";
                }
            }
            if (treeObject[treeNum].GetComponent<TreeStatus>().WhistleOverallGaugeReduction !=0)
            {
                status += "WhistlePointDown:";
                count++;
                if (status != null)
                {
                    status += "\n";
                }
            }
            return status;
        }
        public string TreeInfoStatus1(int treeNum)
        {
            status = null;
            if (treeObject[treeNum].GetComponent<TreeStatus>().chestnutHarvest != 0)
            {
                status += treeObject[treeNum].GetComponent<TreeStatus>().chestnutHarvest.ToString();
                if (status != null)
                {
                    status += "\n";
                }
            }
            if (treeObject[treeNum].GetComponent<TreeStatus>().doubleTheChestnutHarvest != 0)
            {
                status += "On";
                if (status != null)
                {
                    status += "\n";
                }
            }
            if (treeObject[treeNum].GetComponent<TreeStatus>().feverTimeIncrease != 0)
            {
                status += treeObject[treeNum].GetComponent<TreeStatus>().feverTimeIncrease.ToString();
                if (status != null)
                {
                    status += "\n";
                }
            }
            if (treeObject[treeNum].GetComponent<TreeStatus>().IncreasedFeverTimeRewards != 0)
            {
                status +=treeObject[treeNum].GetComponent<TreeStatus>().IncreasedFeverTimeRewards.ToString();
                if (status != null)
                {
                    status += "\n";
                }
            }
            if (treeObject[treeNum].GetComponent<TreeStatus>().FevertimeAutomation != 0)
            {
                status += "On" ;
                if (status != null)
                {
                    status += "\n";
                }
            }
            if (treeObject[treeNum].GetComponent<TreeStatus>().ReductionOfLevelUpFertilizerRequirement != 0)
            {
                status += treeObject[treeNum].GetComponent<TreeStatus>().ReductionOfLevelUpFertilizerRequirement.ToString();
                if (status != null)
                {
                    status += "\n";
                }
            }
            if (treeObject[treeNum].GetComponent<TreeStatus>().increaseGameTime != 0)
            {
                status += treeObject[treeNum].GetComponent<TreeStatus>().increaseGameTime.ToString();
                if (status != null)
                {
                    status += "\n";
                }
            }
            if (treeObject[treeNum].GetComponent<TreeStatus>().chestnutAppearanceRate != 0)
            {
                status +=(treeObject[treeNum].GetComponent<TreeStatus>().chestnutAppearanceRate * 0.1f).ToString();
                if (status != null)
                {
                    status += "\n";
                }
            }
            if (treeObject[treeNum].GetComponent<TreeStatus>().MonsterRegenerationRate != 0)
            {
                status +=(treeObject[treeNum].GetComponent<TreeStatus>().MonsterRegenerationRate * 0.1f).ToString();
                if (status != null)
                {
                    status += "\n";
                }
            }
            if (treeObject[treeNum].GetComponent<TreeStatus>().birdMovementSpeed != 0)
            {
                status += treeObject[treeNum].GetComponent<TreeStatus>().birdMovementSpeed.ToString();
                if (status != null)
                {
                    status += "\n";
                }
            }
            if (treeObject[treeNum].GetComponent<TreeStatus>().WhistleOverallGaugeReduction != 0)
            {
                status += treeObject[treeNum].GetComponent<TreeStatus>().WhistleOverallGaugeReduction.ToString();
                if (status != null)
                {
                    status += "\n";
                }
            }
            return status;
        }*/
        /*void StatusImage(int treeNum)
        {
            for (int j = 1; j < LobbySceneUIManager.statusImage.Length; j++)
            {
                LobbySceneUIManager.statusImage[j].gameObject.SetActive(false);
            }
            for (int x = 1; x < count+1; x++)
            {
                LobbySceneUIManager.statusImage[x].gameObject.SetActive(true);
            }
            int i = 1;
            while(i<count +1)
            {
                if (treeObject[treeNum].GetComponent<TreeStatus>().chestnutHarvest != 0)
                {
                    LobbySceneUIManager.statusImage[i].sprite = chestnutHarvestImage;
                    i++;
                }
                if (treeObject[treeNum].GetComponent<TreeStatus>().doubleTheChestnutHarvest != 0)
                {
                    LobbySceneUIManager.statusImage[i].sprite = doubleTheChestnutHarvestImage;
                    i++;
                }
                if (treeObject[treeNum].GetComponent<TreeStatus>().FeverTimeIncrease != 0)
                {
                    LobbySceneUIManager.statusImage[i].sprite = feverTimeIncreaseImage;
                    i++;
                }
                if (treeObject[treeNum].GetComponent<TreeStatus>().IncreasedFeverTimeRewards != 0)
                {
                    LobbySceneUIManager.statusImage[i].sprite = increasedFeverTimeRewardsImage;
                    i++;
                }
                if (treeObject[treeNum].GetComponent<TreeStatus>().FevertimeAutomation != 0)
                {
                    LobbySceneUIManager.statusImage[i].sprite = fevertimeAutomationImage;
                    i++;
                }
                if (treeObject[treeNum].GetComponent<TreeStatus>().ReductionOfLevelUpFertilizerRequirement != 0)
                {
                    LobbySceneUIManager.statusImage[i].sprite = reductionOfLevelUpFertilizerRequirementImage;
                    i++;
                }
                if (treeObject[treeNum].GetComponent<TreeStatus>().increaseGameTime != 0)
                {
                    LobbySceneUIManager.statusImage[i].sprite = increaseGameTimeImage;
                    i++;
                }
                if (treeObject[treeNum].GetComponent<TreeStatus>().chestnutAppearanceRate != 0)
                {
                    LobbySceneUIManager.statusImage[i].sprite = chestnutAppearanceRateImage;
                    i++;
                }
                if (treeObject[treeNum].GetComponent<TreeStatus>().MonsterRegenerationRate != 0)
                {
                    LobbySceneUIManager.statusImage[i].sprite = monsterRegenerationRateImage;
                    i++;
                }
                if (treeObject[treeNum].GetComponent<TreeStatus>().birdMovementSpeed != 0)
                {
                    LobbySceneUIManager.statusImage[i].sprite = birdMovementSpeedImage;
                    i++;
                }
                if (treeObject[treeNum].GetComponent<TreeStatus>().WhistleOverallGaugeReduction != 0)
                {
                    LobbySceneUIManager.statusImage[i].sprite = whistleOverallGaugeReductionImage;
                    i++;
                }
            }
        }
    }*/
}
