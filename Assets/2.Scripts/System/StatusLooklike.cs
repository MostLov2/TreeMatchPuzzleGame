using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusLooklike : MonoBehaviour
{
    RandomTreeStatus randomTreeStatus;
    Transform Level2;
    Transform Level3;
    Transform Level4;
    Transform Level5;
    Transform Level6;
    Sprite chestnutHarvestSprite;
    Sprite doubleTheChestnutHarvestSprite;
    Sprite FeverTimeIncreaseSprite;
    Sprite IncreasedFeverTimeRewardsSprite;
    Sprite FevertimeAutomationSprite;
    Sprite ReductionOfLevelUpFertilizerRequirementSprite;
    Sprite increaseGameTimeSprite;
    Sprite chestnutAppearanceRateSprite;
    Sprite MonsterRegenerationRateSprite;
    Sprite birdMovementSpeedSprite;
    Sprite WhistleOverallGaugeReductionSprite;
   
    SpriteRenderer[] Level2Point;
    SpriteRenderer[] Level3Point;
    SpriteRenderer[] Level4Point;
    SpriteRenderer[] Level5Point;
    SpriteRenderer[] Level6Point;

    Transform Level3LeavesOnOff;
    Transform Level4LeavesOnOff;
    Transform Level5LeavesOnOff;
    Transform Level6LeavesOnOff;

    List<int> count = new List<int>();
    List<int> statusPoint = new List<int>();
    


    void Awake()
    {
        randomTreeStatus =                              GetComponent<RandomTreeStatus>();

        Level2 =                                        transform.GetChild(1).GetComponent<Transform>();
        Level3 =                                        transform.GetChild(2).GetComponent<Transform>();
        Level4 =                                        transform.GetChild(3).GetComponent<Transform>();
        Level5 =                                        transform.GetChild(4).GetComponent<Transform>();
        Level6 =                                        transform.GetChild(5).GetComponent<Transform>();

        chestnutHarvestSprite =                         Resources.Load<Sprite>("StatusChestnut/chestnutHarvest");
        doubleTheChestnutHarvestSprite =                Resources.Load<Sprite>("StatusChestnut/doubleTheChestnutHarvest");
        FeverTimeIncreaseSprite =                       Resources.Load<Sprite>("StatusChestnut/FeverTimeIncrease");
        IncreasedFeverTimeRewardsSprite =               Resources.Load<Sprite>("StatusChestnut/IncreasedFeverTimeRewards");
        FevertimeAutomationSprite =                     Resources.Load<Sprite>("StatusChestnut/FevertimeAutomation");
        ReductionOfLevelUpFertilizerRequirementSprite = Resources.Load<Sprite>("StatusChestnut/ReductionOfLevelUpFertilizerRequirement");
        increaseGameTimeSprite =                        Resources.Load<Sprite>("StatusChestnut/increaseGameTime");
        chestnutAppearanceRateSprite =                  Resources.Load<Sprite>("StatusChestnut/chestnutAppearanceRate");
        MonsterRegenerationRateSprite =                 Resources.Load<Sprite>("StatusChestnut/MonsterRegenerationRate");
        birdMovementSpeedSprite =                       Resources.Load<Sprite>("StatusChestnut/birdMovementSpeed");
        WhistleOverallGaugeReductionSprite =            Resources.Load<Sprite>("StatusChestnut/WhistleOverallGaugeReduction");

        Level2Point =                                   transform.GetChild(1).GetComponentsInChildren<SpriteRenderer>();
        Level3Point =                                   transform.GetChild(2).GetComponentsInChildren<SpriteRenderer>();
        Level4Point =                                   transform.GetChild(3).GetComponentsInChildren<SpriteRenderer>();
        Level5Point =                                   transform.GetChild(4).GetComponentsInChildren<SpriteRenderer>();
        Level6Point =                                   transform.GetChild(5).GetComponentsInChildren<SpriteRenderer>();

        Level3LeavesOnOff = transform.GetChild(6).transform.GetChild(0).GetComponent<Transform>();
        Level4LeavesOnOff = transform.GetChild(7).transform.GetChild(0).GetComponent<Transform>();
        Level5LeavesOnOff = transform.GetChild(8).transform.GetChild(0).GetComponent<Transform>();
        Level6LeavesOnOff = transform.GetChild(9).transform.GetChild(0).GetComponent<Transform>();
    }
    public void SetInventoryTree()
    {
        transform.localScale = new Vector3(30, 30, 30);
        transform.GetChild(0).gameObject.SetActive(false);
        this.GetComponent<SpriteRenderer>().sortingOrder = 33;
        for (int i = 0; i < Level2Point.Length; i++)
        {
            Level2Point[i].sortingOrder = 34;
        }
        for (int i = 0; i < Level3Point.Length; i++)
        {
            Level3Point[i].sortingOrder = 34;
        }
        for (int i = 0; i < Level4Point.Length; i++)
        {
            Level4Point[i].sortingOrder = 34;
        }
        for (int i = 0; i < Level5Point.Length; i++)
        {
            Level5Point[i].sortingOrder = 34;
        }
        for (int i = 0; i < Level6Point.Length; i++)
        {
            Level6Point[i].sortingOrder = 34;
        }
        Level3LeavesOnOff.GetComponent<SpriteRenderer>().sortingOrder = 35;
        Level4LeavesOnOff.GetComponent<SpriteRenderer>().sortingOrder = 35;
        Level5LeavesOnOff.GetComponent<SpriteRenderer>().sortingOrder = 35;
        Level6LeavesOnOff.GetComponent<SpriteRenderer>().sortingOrder = 35;
    }
    public void SetFarmTree()
    {
        transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        transform.GetChild(0).gameObject.SetActive(true);
        this.GetComponent<SpriteRenderer>().sortingOrder = 20;
        for (int i = 0; i < Level2Point.Length; i++)
        {
            Level2Point[i].sortingOrder = 21;
        }
        for (int i = 0; i < Level3Point.Length; i++)
        {
            Level3Point[i].sortingOrder = 21;
        }
        for (int i = 0; i < Level4Point.Length; i++)
        {
            Level4Point[i].sortingOrder = 21;
        }
        for (int i = 0; i < Level5Point.Length; i++)
        {
            Level5Point[i].sortingOrder = 21;
        }
        for (int i = 0; i < Level6Point.Length; i++)
        {
            Level6Point[i].sortingOrder = 21;
        }
        Level3LeavesOnOff.GetComponent<SpriteRenderer>().sortingOrder = 22;
        Level4LeavesOnOff.GetComponent<SpriteRenderer>().sortingOrder = 22;
        Level5LeavesOnOff.GetComponent<SpriteRenderer>().sortingOrder = 22;
        Level6LeavesOnOff.GetComponent<SpriteRenderer>().sortingOrder = 22;
    }

    /// <summary>
    /// 레벨에 맞춰서 밤 스폰 포인트 ON
    /// </summary>
    public IEnumerator LevelCStheck()
    {
        if (randomTreeStatus.TreeLevel <= 8)
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
        else if (randomTreeStatus.TreeLevel <= 18)
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
        else if (randomTreeStatus.TreeLevel <= 28)
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
        else if (randomTreeStatus.TreeLevel <= 38)
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
        else if (randomTreeStatus.TreeLevel <= 48)
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
    public IEnumerator StatusPointCheck()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        count.Clear();
        statusPoint.Clear();
        for (int i = 0; i < randomTreeStatus.haveStatusPoint.Count; i++)
        {
            if (randomTreeStatus.haveStatusPoint[i] != 0 && randomTreeStatus.haveStatusPoint[i] <2)
            {
                count.Add(i);
                statusPoint.Add(randomTreeStatus.haveStatusPoint[i]);
            }
            else if (randomTreeStatus.haveStatusPoint[i] != 0 && randomTreeStatus.haveStatusPoint[i] >= 2)
            {
                for (int j = 0; j < randomTreeStatus.haveStatusPoint[i]; j++)
                {
                    count.Add(i);
                    statusPoint.Add(randomTreeStatus.haveStatusPoint[i]);
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
            if (randomTreeStatus.TreeLevel <= 18)
            {
                if(count[i] == 0 && statusPoint[i] > 0)
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
            else if (randomTreeStatus.TreeLevel <= 28)
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
            else if (randomTreeStatus.TreeLevel <= 38)
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
            else if (randomTreeStatus.TreeLevel <= 48)
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
