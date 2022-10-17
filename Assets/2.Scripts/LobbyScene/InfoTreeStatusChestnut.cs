using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoTreeStatusChestnut : MonoBehaviour
{
    
    public TreeStatus treeStatus;
    public ChangeStatusChestnut changeStatusChestnut;
    [SerializeField]Transform Level2;
    [SerializeField]Transform Level3;
    [SerializeField]Transform Level4;
    [SerializeField]Transform Level5;
    [SerializeField]Transform Level6;
    [SerializeField]Sprite chestnutHarvestSprite;
    [SerializeField]Sprite doubleTheChestnutHarvestSprite;
    [SerializeField]Sprite FeverTimeIncreaseSprite;
    [SerializeField]Sprite IncreasedFeverTimeRewardsSprite;
    [SerializeField]Sprite FevertimeAutomationSprite;
    [SerializeField]Sprite ReductionOfLevelUpFertilizerRequirementSprite;
    [SerializeField]Sprite increaseGameTimeSprite;
    [SerializeField]Sprite chestnutAppearanceRateSprite;
    [SerializeField]Sprite MonsterRegenerationRateSprite;
    [SerializeField]Sprite birdMovementSpeedSprite;
    [SerializeField]Sprite WhistleOverallGaugeReductionSprite;
    [SerializeField]SpriteRenderer[] Level2Point;
    [SerializeField]SpriteRenderer[] Level3Point;
    [SerializeField]SpriteRenderer[] Level4Point;
    [SerializeField]SpriteRenderer[] Level5Point;
    [SerializeField]SpriteRenderer[] Level6Point;
    [SerializeField]Transform Level3LeavesOnOff;
    [SerializeField]Transform Level4LeavesOnOff;
    [SerializeField]Transform Level5LeavesOnOff;
    [SerializeField]Transform Level6LeavesOnOff;
    public SpriteRenderer treeImage;

    List<int> count = new List<int>();
    List<int> statusPoint = new List<int>();

    Sprite treeLevel9;
    Sprite treeLevel19;
    Sprite treeLevel29;
    Sprite treeLevel39;
    Sprite treeLevel49;
    Sprite treeLevel50;
    private void Awake()
    {
        treeLevel9 = Resources.Load<Sprite>("TreeImage/tree1");
        treeLevel19 = Resources.Load<Sprite>("TreeImage/tree2");
        treeLevel29 = Resources.Load<Sprite>("TreeImage/tree3");
        treeLevel39 = Resources.Load<Sprite>("TreeImage/tree4");
        treeLevel49 = Resources.Load<Sprite>("TreeImage/tree5");
        treeLevel50 = Resources.Load<Sprite>("TreeImage/tree6");
        //treeImage =GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(5).GetChild(0).GetChild(0).GetChild(4).GetComponent<SpriteRenderer>();
    }

    void TreeImageChangeFollowedTreeLevel()
    {
        if (treeStatus != null)
        {
            treeImage.sprite = treeStatus.treeImage.sprite;
        }
        else
        {
            Debug.Log("TreeStatus is null");
        }
    }
    /// <summary>
    /// 레벨에 맞춰서 밤 스폰 포인트 ON
    /// </summary>
    public IEnumerator LobbyLevelCStheck()
    {
        for (int i = 0; i < TreeSetManager.treeObject.Length; i++)
        {
            if (TreeSetManager.treeObject[i].GetComponent<TreeStatus>().TreeName == TreeInfomation.InfoTreeName)
            {
                treeStatus = TreeSetManager.treeObject[i].GetComponent<TreeStatus>();
                changeStatusChestnut = TreeSetManager.treeObject[i].GetComponent<ChangeStatusChestnut>();
            }
        }
        TreeImageChangeFollowedTreeLevel();
        Level2 = transform.GetChild(1).GetComponent<Transform>();
        Level3 = transform.GetChild(2).GetComponent<Transform>();
        Level4 = transform.GetChild(3).GetComponent<Transform>();
        Level5 = transform.GetChild(4).GetComponent<Transform>();
        Level6 = transform.GetChild(5).GetComponent<Transform>();

        chestnutHarvestSprite = Resources.Load<Sprite>("StatusChestnut/chestnutHarvest");
        doubleTheChestnutHarvestSprite = Resources.Load<Sprite>("StatusChestnut/doubleTheChestnutHarvest");
        FeverTimeIncreaseSprite = Resources.Load<Sprite>("StatusChestnut/FeverTimeIncrease");
        IncreasedFeverTimeRewardsSprite = Resources.Load<Sprite>("StatusChestnut/IncreasedFeverTimeRewards");
        FevertimeAutomationSprite = Resources.Load<Sprite>("StatusChestnut/FevertimeAutomation");
        ReductionOfLevelUpFertilizerRequirementSprite = Resources.Load<Sprite>("StatusChestnut/ReductionOfLevelUpFertilizerRequirement");
        increaseGameTimeSprite = Resources.Load<Sprite>("StatusChestnut/increaseGameTime");
        chestnutAppearanceRateSprite = Resources.Load<Sprite>("StatusChestnut/chestnutAppearanceRate");
        MonsterRegenerationRateSprite = Resources.Load<Sprite>("StatusChestnut/MonsterRegenerationRate");
        birdMovementSpeedSprite = Resources.Load<Sprite>("StatusChestnut/birdMovementSpeed");
        WhistleOverallGaugeReductionSprite = Resources.Load<Sprite>("StatusChestnut/WhistleOverallGaugeReduction");

        Level2Point = transform.GetChild(1).GetComponentsInChildren<SpriteRenderer>();
        Level3Point = transform.GetChild(2).GetComponentsInChildren<SpriteRenderer>();
        Level4Point = transform.GetChild(3).GetComponentsInChildren<SpriteRenderer>();
        Level5Point = transform.GetChild(4).GetComponentsInChildren<SpriteRenderer>();
        Level6Point = transform.GetChild(5).GetComponentsInChildren<SpriteRenderer>();

        Level3LeavesOnOff = transform.GetChild(6).transform.GetChild(0).GetComponent<Transform>();
        Level4LeavesOnOff = transform.GetChild(7).transform.GetChild(0).GetComponent<Transform>();
        Level5LeavesOnOff = transform.GetChild(8).transform.GetChild(0).GetComponent<Transform>();
        Level6LeavesOnOff = transform.GetChild(9).transform.GetChild(0).GetComponent<Transform>();
        if (treeStatus.TreeLevel <= 9)
        {
            Level2.gameObject.SetActive(false);
            Level3.gameObject.SetActive(false);
            Level4.gameObject.SetActive(false);
            Level5.gameObject.SetActive(false);
            Level6.gameObject.SetActive(false);
            Level3LeavesOnOff.gameObject.SetActive(false);
            Level4LeavesOnOff.gameObject.SetActive(false);
            Level5LeavesOnOff.gameObject.SetActive(false);
            Level6LeavesOnOff.gameObject.SetActive(false);
        }
        else if (treeStatus.TreeLevel <= 19)
        {
            Level2.gameObject.SetActive(true);
            Level3.gameObject.SetActive(false);
            Level4.gameObject.SetActive(false);
            Level5.gameObject.SetActive(false);
            Level6.gameObject.SetActive(false);
            Level3LeavesOnOff.gameObject.SetActive(false);
            Level4LeavesOnOff.gameObject.SetActive(false);
            Level5LeavesOnOff.gameObject.SetActive(false);
            Level6LeavesOnOff.gameObject.SetActive(false);
        }
        else if (treeStatus.TreeLevel <= 29)
        {
            Level2.gameObject.SetActive(false);
            Level3.gameObject.SetActive(true);
            Level4.gameObject.SetActive(false);
            Level5.gameObject.SetActive(false);
            Level6.gameObject.SetActive(false);
            Level3LeavesOnOff.gameObject.SetActive(true);
            Level4LeavesOnOff.gameObject.SetActive(false);
            Level5LeavesOnOff.gameObject.SetActive(false);
            Level6LeavesOnOff.gameObject.SetActive(false);
        }
        else if (treeStatus.TreeLevel <= 39)
        {
            Level2.gameObject.SetActive(false);
            Level3.gameObject.SetActive(false);
            Level4.gameObject.SetActive(true);
            Level5.gameObject.SetActive(false);
            Level6.gameObject.SetActive(false);
            Level3LeavesOnOff.gameObject.SetActive(false);
            Level4LeavesOnOff.gameObject.SetActive(true);
            Level5LeavesOnOff.gameObject.SetActive(false);
            Level6LeavesOnOff.gameObject.SetActive(false);
        }
        else if (treeStatus.TreeLevel <= 49)
        {
            Level2.gameObject.SetActive(false);
            Level3.gameObject.SetActive(false);
            Level4.gameObject.SetActive(false);
            Level5.gameObject.SetActive(true);
            Level6.gameObject.SetActive(false);
            Level3LeavesOnOff.gameObject.SetActive(false);
            Level4LeavesOnOff.gameObject.SetActive(false);
            Level5LeavesOnOff.gameObject.SetActive(true);
            Level6LeavesOnOff.gameObject.SetActive(false);
        }
        else
        {
            Level2.gameObject.SetActive(false);
            Level3.gameObject.SetActive(false);
            Level4.gameObject.SetActive(false);
            Level5.gameObject.SetActive(false);
            Level6.gameObject.SetActive(true);
            Level3LeavesOnOff.gameObject.SetActive(false);
            Level4LeavesOnOff.gameObject.SetActive(false);
            Level5LeavesOnOff.gameObject.SetActive(false);
            Level6LeavesOnOff.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(Time.deltaTime);
    }
    /// <summary>
    /// 특성 포인트가 있는지 확인 하고 확인후 List 포인트 넣기
    /// </summary>
    public IEnumerator LobbyStatusPointCheck()
    {
        Level2 = transform.GetChild(1).GetComponent<Transform>();
        Level3 = transform.GetChild(2).GetComponent<Transform>();
        Level4 = transform.GetChild(3).GetComponent<Transform>();
        Level5 = transform.GetChild(4).GetComponent<Transform>();
        Level6 = transform.GetChild(5).GetComponent<Transform>();

        chestnutHarvestSprite = Resources.Load<Sprite>("StatusChestnut/chestnutHarvest");
        doubleTheChestnutHarvestSprite = Resources.Load<Sprite>("StatusChestnut/doubleTheChestnutHarvest");
        FeverTimeIncreaseSprite = Resources.Load<Sprite>("StatusChestnut/FeverTimeIncrease");
        IncreasedFeverTimeRewardsSprite = Resources.Load<Sprite>("StatusChestnut/IncreasedFeverTimeRewards");
        FevertimeAutomationSprite = Resources.Load<Sprite>("StatusChestnut/FevertimeAutomation");
        ReductionOfLevelUpFertilizerRequirementSprite = Resources.Load<Sprite>("StatusChestnut/ReductionOfLevelUpFertilizerRequirement");
        increaseGameTimeSprite = Resources.Load<Sprite>("StatusChestnut/increaseGameTime");
        chestnutAppearanceRateSprite = Resources.Load<Sprite>("StatusChestnut/chestnutAppearanceRate");
        MonsterRegenerationRateSprite = Resources.Load<Sprite>("StatusChestnut/MonsterRegenerationRate");
        birdMovementSpeedSprite = Resources.Load<Sprite>("StatusChestnut/birdMovementSpeed");
        WhistleOverallGaugeReductionSprite = Resources.Load<Sprite>("StatusChestnut/WhistleOverallGaugeReduction");

        Level2Point = transform.GetChild(1).GetComponentsInChildren<SpriteRenderer>();
        Level3Point = transform.GetChild(2).GetComponentsInChildren<SpriteRenderer>();
        Level4Point = transform.GetChild(3).GetComponentsInChildren<SpriteRenderer>();
        Level5Point = transform.GetChild(4).GetComponentsInChildren<SpriteRenderer>();
        Level6Point = transform.GetChild(5).GetComponentsInChildren<SpriteRenderer>();

        Level3LeavesOnOff = transform.GetChild(6).transform.GetChild(0).GetComponent<Transform>();
        Level4LeavesOnOff = transform.GetChild(7).transform.GetChild(0).GetComponent<Transform>();
        Level5LeavesOnOff = transform.GetChild(8).transform.GetChild(0).GetComponent<Transform>();
        Level6LeavesOnOff = transform.GetChild(9).transform.GetChild(0).GetComponent<Transform>();
        yield return new WaitForSeconds(Time.deltaTime);
        count.Clear();
        statusPoint.Clear();
        for (int i = 0; i < changeStatusChestnut.haveStatusPoint.Count; i++)
        {
            if (changeStatusChestnut.haveStatusPoint[i] != 0 && changeStatusChestnut.haveStatusPoint[i] < 2)
            {
                count.Add(i);
                statusPoint.Add(changeStatusChestnut.haveStatusPoint[i]);
            }
            else if (changeStatusChestnut.haveStatusPoint[i] != 0 && changeStatusChestnut.haveStatusPoint[i] >= 2)
            {
                for (int j = 0; j < changeStatusChestnut.haveStatusPoint[i]; j++)
                {
                    count.Add(i);
                    statusPoint.Add(changeStatusChestnut.haveStatusPoint[i]);
                }
            }
        }
        SetChestnut();
    }
    /// <summary>
    /// 특성에 맞춰서 스프라이트에 밤 넣기 
    /// </summary>
    void SetChestnut()
    {
        int i = 0;
        while (i < count.Count)
        {
            InfiniteLoopDetector.Run();
            if (treeStatus.TreeLevel <= 19)
            {
                if (count[i] == 0 && statusPoint[i] > 0)
                {
                    statusPoint[i]--;
                    Level2Point[i].sprite = chestnutHarvestSprite;
                    i++;
                }
                else if (count[i] == 1 && statusPoint[i] > 0)
                {
                    statusPoint[i]--;
                    Level2Point[i].sprite = doubleTheChestnutHarvestSprite;
                    i++;
                }
                else if (count[i] == 2 && statusPoint[i] > 0)
                {
                    statusPoint[i]--;
                    Level2Point[i].sprite = FeverTimeIncreaseSprite;
                    i++;
                }
                else if (count[i] == 3 && statusPoint[i] > 0)
                {
                    statusPoint[i]--;
                    Level2Point[i].sprite = IncreasedFeverTimeRewardsSprite;
                    i++;
                }
                else if (count[i] == 4 && statusPoint[i] > 0)
                {
                    statusPoint[i]--;
                    Level2Point[i].sprite = FevertimeAutomationSprite;
                    i++;
                }
                else if (count[i] == 5 && statusPoint[i] > 0)
                {
                    statusPoint[i]--;
                    Level2Point[i].sprite = ReductionOfLevelUpFertilizerRequirementSprite;
                    i++;
                }
                else if (count[i] == 6 && statusPoint[i] > 0)
                {
                    statusPoint[i]--;
                    Level2Point[i].sprite = increaseGameTimeSprite;
                    i++;
                }
                else if (count[i] == 7 && statusPoint[i] > 0)
                {
                    statusPoint[i]--;
                    Level2Point[i].sprite = chestnutAppearanceRateSprite;
                    i++;
                }
                else if (count[i] == 8 && statusPoint[i] > 0)
                {
                    statusPoint[i]--;
                    Level2Point[i].sprite = MonsterRegenerationRateSprite;
                    i++;
                }
                else if (count[i] == 9 && statusPoint[i] > 0)
                {
                    statusPoint[i]--;
                    Level2Point[i].sprite = birdMovementSpeedSprite;
                    i++;
                }
                else if (count[i] == 10 && statusPoint[i] > 0)
                {
                    statusPoint[i]--;
                    Level2Point[i].sprite = WhistleOverallGaugeReductionSprite;
                    i++;
                }


            }
            else if (treeStatus.TreeLevel <= 29)
            {
                if (count[i] == 0 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level3Point[i].sprite = chestnutHarvestSprite;
                    i++;
                }
                else if (count[i] == 1 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level3Point[i].sprite = doubleTheChestnutHarvestSprite;
                    i++;
                }
                else if (count[i] == 2 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level3Point[i].sprite = FeverTimeIncreaseSprite;
                    i++;
                }
                else if (count[i] == 3 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level3Point[i].sprite = IncreasedFeverTimeRewardsSprite;
                    i++;
                }
                else if (count[i] == 4 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level3Point[i].sprite = FevertimeAutomationSprite;
                    i++;
                }
                else if (count[i] == 5 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level3Point[i].sprite = ReductionOfLevelUpFertilizerRequirementSprite;
                    i++;
                }
                else if (count[i] == 6 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level3Point[i].sprite = increaseGameTimeSprite;
                    i++;
                }
                else if (count[i] == 7 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level3Point[i].sprite = chestnutAppearanceRateSprite;
                    i++;
                }
                else if (count[i] == 8 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level3Point[i].sprite = MonsterRegenerationRateSprite;
                    i++;
                }
                else if (count[i] == 9 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level3Point[i].sprite = birdMovementSpeedSprite;
                    i++;
                }
                else if (count[i] == 10 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level3Point[i].sprite = WhistleOverallGaugeReductionSprite;
                    i++;
                }
            }
            else if (treeStatus.TreeLevel <= 39)
            {
                if (count[i] == 0 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level4Point[i].sprite = chestnutHarvestSprite;
                    i++;
                }
                else if (count[i] == 1 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level4Point[i].sprite = doubleTheChestnutHarvestSprite;
                    i++;
                }
                else if (count[i] == 2 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level4Point[i].sprite = FeverTimeIncreaseSprite;
                    i++;
                }
                else if (count[i] == 3 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level4Point[i].sprite = IncreasedFeverTimeRewardsSprite;
                    i++;
                }
                else if (count[i] == 4 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level4Point[i].sprite = FevertimeAutomationSprite;
                    i++;
                }
                else if (count[i] == 5 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level4Point[i].sprite = ReductionOfLevelUpFertilizerRequirementSprite;
                    i++;
                }
                else if (count[i] == 6 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level4Point[i].sprite = increaseGameTimeSprite;
                    i++;
                }
                else if (count[i] == 7 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level4Point[i].sprite = chestnutAppearanceRateSprite;
                    i++;
                }
                else if (count[i] == 8 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level4Point[i].sprite = MonsterRegenerationRateSprite;
                    i++;
                }
                else if (count[i] == 9 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level4Point[i].sprite = birdMovementSpeedSprite;
                    i++;
                }
                else if (count[i] == 10 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level4Point[i].sprite = WhistleOverallGaugeReductionSprite;
                    i++;
                }
            }
            else if (treeStatus.TreeLevel <= 49)
            {
                if (count[i] == 0 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level5Point[i].sprite = chestnutHarvestSprite;
                    i++;
                }
                else if (count[i] == 1 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level5Point[i].sprite = doubleTheChestnutHarvestSprite;
                    i++;
                }
                else if (count[i] == 2 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level5Point[i].sprite = FeverTimeIncreaseSprite;
                    i++;
                }
                else if (count[i] == 3 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level5Point[i].sprite = IncreasedFeverTimeRewardsSprite;
                    i++;
                }
                else if (count[i] == 4 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level5Point[i].sprite = FevertimeAutomationSprite;
                    i++;
                }
                else if (count[i] == 5 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level5Point[i].sprite = ReductionOfLevelUpFertilizerRequirementSprite;
                    i++;
                }
                else if (count[i] == 6 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level5Point[i].sprite = increaseGameTimeSprite;
                    i++;
                }
                else if (count[i] == 7 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level5Point[i].sprite = chestnutAppearanceRateSprite;
                    i++;
                }
                else if (count[i] == 8 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level5Point[i].sprite = MonsterRegenerationRateSprite;
                    i++;
                }
                else if (count[i] == 9 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level5Point[i].sprite = birdMovementSpeedSprite;
                    i++;
                }
                else if (count[i] == 10 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level5Point[i].sprite = WhistleOverallGaugeReductionSprite;
                    i++;
                }
            }
            else
            {
                if (count[i] == 0 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Debug.Log(i);
                    Level6Point[i].sprite = chestnutHarvestSprite;
                    i++;
                }
                else if (count[i] == 1 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level6Point[i].sprite = doubleTheChestnutHarvestSprite;
                    i++;
                }
                else if (count[i] == 2 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level6Point[i].sprite = FeverTimeIncreaseSprite;
                    i++;
                }
                else if (count[i] == 3 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level6Point[i].sprite = IncreasedFeverTimeRewardsSprite;
                    i++;
                }
                else if (count[i] == 4 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level6Point[i].sprite = FevertimeAutomationSprite;
                    i++;
                }
                else if (count[i] == 5 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level6Point[i].sprite = ReductionOfLevelUpFertilizerRequirementSprite;
                    i++;
                }
                else if (count[i] == 6 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level6Point[i].sprite = increaseGameTimeSprite;
                    i++;
                }
                else if (count[i] == 7 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level6Point[i].sprite = chestnutAppearanceRateSprite;
                    i++;
                }
                else if (count[i] == 8 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level6Point[i].sprite = MonsterRegenerationRateSprite;
                    i++;
                }
                else if (count[i] == 9 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level6Point[i].sprite = birdMovementSpeedSprite;
                    i++;
                }
                else if (count[i] == 10 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level6Point[i].sprite = WhistleOverallGaugeReductionSprite;
                    i++;
                }
            }
        }
    }
}
