using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeStatusChestnut : MonoBehaviour
{
    TreeStatus treeStatus;
    Transform Level2;
    Transform Level3;
    Transform Level4;
    Transform Level5;
    Transform Level6;
    [SerializeField]Sprite birdMovementSpeedImage;
    [SerializeField]Sprite chestnutAppearanceRateImage;
    [SerializeField]Sprite chestnutHarvestImage;
    [SerializeField]Sprite doubleTheChestnutHarvestImage;
    [SerializeField]Sprite FevertimeAutomationImage;
    [SerializeField]Sprite FeverTimeIncreaseImage;
    [SerializeField]Sprite IncreasedFeverTimeRewardsImage;
    [SerializeField]Sprite increaseGameTimeImage;
    [SerializeField]Sprite MonsterRegenerationRateImage;
    [SerializeField]Sprite ReductionOfLevelUpFertilizerRequirementImage;
    [SerializeField]Sprite WhistleOverallGaugeReductionImage;
    [SerializeField]SpriteRenderer[] Level2Point;
    [SerializeField]SpriteRenderer[] Level3Point;
    [SerializeField]SpriteRenderer[] Level4Point;
    [SerializeField]SpriteRenderer[] Level5Point;
    [SerializeField]SpriteRenderer[] Level6Point;
    [SerializeField] Image[] Level2PointImage;
    [SerializeField] Image[] Level3PointImage;
    [SerializeField] Image[] Level4PointImage;
    [SerializeField] Image[] Level5PointImage;
    [SerializeField] Image[] Level6PointImage;

    Transform Level3LeavesOnOff;
    Transform Level4LeavesOnOff;
    Transform Level5LeavesOnOff;
    Transform Level6LeavesOnOff;

    public List<int> haveStatusPoint = new List<int>();
    List<int> count = new List<int>();
    List<int> statusPoint = new List<int>();
    private void Awake()
    {
        treeStatus = GetComponent<TreeStatus>();
    }
    public IEnumerator LobbyLevelCStheck()
    {
        Level2 = transform.GetChild(1).GetComponent<Transform>();
        Level3 = transform.GetChild(2).GetComponent<Transform>();
        Level4 = transform.GetChild(3).GetComponent<Transform>();
        Level5 = transform.GetChild(4).GetComponent<Transform>();
        Level6 = transform.GetChild(5).GetComponent<Transform>();

        birdMovementSpeedImage = Resources.Load<Sprite>("StatusChestnut/birdMovementSpeed");
        chestnutAppearanceRateImage = Resources.Load<Sprite>("StatusChestnut/chestnutAppearanceRate");
        chestnutHarvestImage = Resources.Load<Sprite>("StatusChestnut/chestnutHarvest");
        doubleTheChestnutHarvestImage = Resources.Load<Sprite>("StatusChestnut/doubleTheChestnutHarvest");
        FevertimeAutomationImage = Resources.Load<Sprite>("StatusChestnut/FevertimeAutomation");
        FeverTimeIncreaseImage = Resources.Load<Sprite>("StatusChestnut/FeverTimeIncrease");
        IncreasedFeverTimeRewardsImage = Resources.Load<Sprite>("StatusChestnut/IncreasedFeverTimeRewards");
        increaseGameTimeImage = Resources.Load<Sprite>("StatusChestnut/increaseGameTime");
        MonsterRegenerationRateImage = Resources.Load<Sprite>("StatusChestnut/MonsterRegenerationRate");
        ReductionOfLevelUpFertilizerRequirementImage = Resources.Load<Sprite>("StatusChestnut/ReductionOfLevelUpFertilizerRequirement");
        WhistleOverallGaugeReductionImage = Resources.Load<Sprite>("StatusChestnut/WhistleOverallGaugeReduction");

        Level2Point = transform.GetChild(1).GetComponentsInChildren<SpriteRenderer>();
        Level3Point = transform.GetChild(2).GetComponentsInChildren<SpriteRenderer>();
        Level4Point = transform.GetChild(3).GetComponentsInChildren<SpriteRenderer>();
        Level5Point = transform.GetChild(4).GetComponentsInChildren<SpriteRenderer>();
        Level6Point = transform.GetChild(5).GetComponentsInChildren<SpriteRenderer>();

        Level2PointImage = transform.GetChild(1).GetComponentsInChildren<Image>();
        Level3PointImage = transform.GetChild(2).GetComponentsInChildren<Image>();
        Level4PointImage = transform.GetChild(3).GetComponentsInChildren<Image>();
        Level5PointImage = transform.GetChild(4).GetComponentsInChildren<Image>();
        Level6PointImage = transform.GetChild(5).GetComponentsInChildren<Image>();

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
    public void CheckStatusPoint()
    {
        haveStatusPoint.Clear();
        haveStatusPoint.Add(treeStatus.chestnutHarvest);
        haveStatusPoint.Add(treeStatus.doubleTheChestnutHarvest);
        haveStatusPoint.Add(treeStatus.feverTimeIncrease);
        haveStatusPoint.Add(treeStatus.increasedFeverTimeRewards);
        haveStatusPoint.Add(treeStatus.fevertimeAutomation);
        haveStatusPoint.Add(treeStatus.reductionOfLevelUpFertilizerRequirement);
        haveStatusPoint.Add(treeStatus.increaseGameTime);
        haveStatusPoint.Add(treeStatus.chestnutAppearanceRate);
        haveStatusPoint.Add(treeStatus.monsterRegenerationRate);
        haveStatusPoint.Add(treeStatus.birdMovementSpeed);
        haveStatusPoint.Add(treeStatus.whistleOverallGaugeReduction);
        StartCoroutine(StatusPointCheck());
    }
    public void SetInventoryTree()
    {
        transform.localScale = new Vector3(30, 30, 30);
        transform.GetChild(0).gameObject.SetActive(false);
        treeStatus.treeImage .enabled = false;
        treeStatus.treeImageSprite .enabled = true;
        this.GetComponent<SpriteRenderer>().sortingOrder = 33;
        for (int i = 0; i < Level2Point.Length; i++)
        {
            Level2Point[i].sortingOrder = 34;
            Level2PointImage[i].enabled = true;
            Level2Point[i].enabled = false;
        }
        for (int i = 0; i < Level3Point.Length; i++)
        {
            Level3Point[i].sortingOrder = 34;
            Level3PointImage[i].enabled = true;
            Level3Point[i].enabled = false;
        }
        for (int i = 0; i < Level4Point.Length; i++)
        {
            Level4Point[i].sortingOrder = 34;
            Level4PointImage[i].enabled = true;
            Level4Point[i].enabled = false;
        }
        for (int i = 0; i < Level5Point.Length; i++)
        {
            Level5Point[i].sortingOrder = 34;
            Level5PointImage[i].enabled = true;
            Level5Point[i].enabled = false;
        }
        for (int i = 0; i < Level6Point.Length; i++)
        {
            Level6Point[i].sortingOrder = 34;
            Level6PointImage[i].enabled = true;
            Level6Point[i].enabled = false;
        }
        Level3LeavesOnOff.GetComponent<SpriteRenderer>().sortingOrder = 35;
        Level4LeavesOnOff.GetComponent<SpriteRenderer>().sortingOrder = 35;
        Level5LeavesOnOff.GetComponent<SpriteRenderer>().sortingOrder = 35;
        Level6LeavesOnOff.GetComponent<SpriteRenderer>().sortingOrder = 35;
        Level3LeavesOnOff.GetComponent<Image>().enabled = true;
        Level4LeavesOnOff.GetComponent<Image>().enabled = true;
        Level5LeavesOnOff.GetComponent<Image>().enabled = true;
        Level6LeavesOnOff.GetComponent<Image>().enabled = true;
        Level3LeavesOnOff.GetComponent<SpriteRenderer>().enabled = false;
        Level4LeavesOnOff.GetComponent<SpriteRenderer>().enabled = false;
        Level5LeavesOnOff.GetComponent<SpriteRenderer>().enabled = false;
        Level6LeavesOnOff.GetComponent<SpriteRenderer>().enabled = false;
    }
    public void SetFarmTree()
    {
        transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        transform.GetChild(0).gameObject.SetActive(true);
        treeStatus.treeImage.enabled = true;
        treeStatus.treeImageSprite.enabled = false;
        this.GetComponent<SpriteRenderer>().sortingOrder = 20;
        for (int i = 0; i < Level2Point.Length; i++)
        {
            Level2Point[i].sortingOrder = 21;
            Level2PointImage[i].enabled = false;
            Level2Point[i].enabled = true;
        }
        for (int i = 0; i < Level3Point.Length; i++)
        {
            Level3Point[i].sortingOrder = 21;
            Level3PointImage[i].enabled = false;
            Level3Point[i].enabled = true;
        }
        for (int i = 0; i < Level4Point.Length; i++)
        {
            Level4Point[i].sortingOrder = 21;
            Level4PointImage[i].enabled = false;
            Level4Point[i].enabled = true;
        }
        for (int i = 0; i < Level5Point.Length; i++)
        {
            Level5Point[i].sortingOrder = 21;
            Level5PointImage[i].enabled = false;
            Level5Point[i].enabled = true;
        }
        for (int i = 0; i < Level6Point.Length; i++)
        {
            Level6Point[i].sortingOrder = 21;
            Level6PointImage[i].enabled = false;
            Level6Point[i].enabled = true;
        }
        Level3LeavesOnOff.GetComponent<SpriteRenderer>().sortingOrder = 22;
        Level4LeavesOnOff.GetComponent<SpriteRenderer>().sortingOrder = 22;
        Level5LeavesOnOff.GetComponent<SpriteRenderer>().sortingOrder = 22;
        Level6LeavesOnOff.GetComponent<SpriteRenderer>().sortingOrder = 22;
        Level3LeavesOnOff.GetComponent<Image>().enabled = false;
        Level4LeavesOnOff.GetComponent<Image>().enabled = false;
        Level5LeavesOnOff.GetComponent<Image>().enabled = false;
        Level6LeavesOnOff.GetComponent<Image>().enabled = false;
        Level3LeavesOnOff.GetComponent<SpriteRenderer>().enabled = true;
        Level4LeavesOnOff.GetComponent<SpriteRenderer>().enabled = true;
        Level5LeavesOnOff.GetComponent<SpriteRenderer>().enabled = true;
        Level6LeavesOnOff.GetComponent<SpriteRenderer>().enabled = true;
    }

    /// <summary>
    /// 레벨에 맞춰서 밤 스폰 포인트 ON
    /// </summary>
    public IEnumerator LevelCStheck()
    {
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
    public IEnumerator StatusPointCheck()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        count.Clear();
        statusPoint.Clear();
        for (int i = 0; i < haveStatusPoint.Count; i++)
        {
            if (haveStatusPoint[i] != 0 && haveStatusPoint[i] < 2)
            {
                count.Add(i);
                statusPoint.Add(haveStatusPoint[i]);
            }
            else if (haveStatusPoint[i] != 0 && haveStatusPoint[i] >= 2)
            {
                for (int j = 0; j < haveStatusPoint[i]; j++)
                {
                    count.Add(i);
                    statusPoint.Add(haveStatusPoint[i]);
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
            if (treeStatus.TreeLevel <= 9)
            {
                i = count.Count;
            }
            else if (treeStatus.TreeLevel <= 18)
            {
                Debug.Log(i);
                if (count[i] == 0 && statusPoint[i] > 0)
                {
                    statusPoint[i]--;
                    Level2Point[i].sprite = chestnutHarvestImage;
                    Level2PointImage[i].sprite = chestnutHarvestImage;
                    i++;
                }
                else if (count[i] == 1 && statusPoint[i] > 0)
                {
                    statusPoint[i]--;
                    Level2Point[i].sprite = doubleTheChestnutHarvestImage;
                    Level2PointImage[i].sprite = doubleTheChestnutHarvestImage;
                    i++;
                }
                else if (count[i] == 2 && statusPoint[i] > 0)
                {
                    statusPoint[i]--;
                    Level2Point[i].sprite = FeverTimeIncreaseImage;
                    Level2PointImage[i].sprite = FeverTimeIncreaseImage;
                    i++;
                }
                else if (count[i] == 3 && statusPoint[i] > 0)
                {
                    statusPoint[i]--;
                    Level2Point[i].sprite = IncreasedFeverTimeRewardsImage;
                    Level2PointImage[i].sprite = IncreasedFeverTimeRewardsImage;
                    i++;
                }
                else if (count[i] == 4 && statusPoint[i] > 0)
                {
                    statusPoint[i]--;
                    Level2Point[i].sprite = FevertimeAutomationImage;
                    Level2PointImage[i].sprite = FevertimeAutomationImage;
                    i++;
                }
                else if (count[i] == 5 && statusPoint[i] > 0)
                {
                    statusPoint[i]--;
                    Level2Point[i].sprite = ReductionOfLevelUpFertilizerRequirementImage;
                    Level2PointImage[i].sprite = ReductionOfLevelUpFertilizerRequirementImage;
                    i++;
                }
                else if (count[i] == 6 && statusPoint[i] > 0)
                {
                    statusPoint[i]--;
                    Level2Point[i].sprite = increaseGameTimeImage;
                    Level2PointImage[i].sprite = increaseGameTimeImage;
                    i++;
                }
                else if (count[i] == 7 && statusPoint[i] > 0)
                {
                    statusPoint[i]--;
                    Level2Point[i].sprite = chestnutAppearanceRateImage;
                    Level2PointImage[i].sprite = chestnutAppearanceRateImage;
                    i++;
                }
                else if (count[i] == 8 && statusPoint[i] > 0)
                {
                    statusPoint[i]--;
                    Level2Point[i].sprite = MonsterRegenerationRateImage;
                    Level2PointImage[i].sprite = MonsterRegenerationRateImage;
                    i++;
                }
                else if (count[i] == 9 && statusPoint[i] > 0)
                {
                    statusPoint[i]--;
                    Level2Point[i].sprite = birdMovementSpeedImage;
                    Level2PointImage[i].sprite = birdMovementSpeedImage;
                    i++;
                }
                else if (count[i] == 10 && statusPoint[i] > 0)
                {
                    statusPoint[i]--;
                    Level2Point[i].sprite = WhistleOverallGaugeReductionImage;
                    Level2PointImage[i].sprite = WhistleOverallGaugeReductionImage;
                    i++;
                }


            }
            else if (treeStatus.TreeLevel <= 28)
            {
                if (count[i] == 0 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level3Point[i].sprite = chestnutHarvestImage;
                    Level3PointImage[i].sprite = chestnutHarvestImage;
                    i++;
                }
                else if (count[i] == 1 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level3Point[i].sprite = doubleTheChestnutHarvestImage;
                    Level3PointImage[i].sprite = doubleTheChestnutHarvestImage;
                    i++;
                }
                else if (count[i] == 2 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level3Point[i].sprite = FeverTimeIncreaseImage;
                    Level3PointImage[i].sprite = FeverTimeIncreaseImage;
                    i++;
                }
                else if (count[i] == 3 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level3Point[i].sprite = IncreasedFeverTimeRewardsImage;
                    Level3PointImage[i].sprite = IncreasedFeverTimeRewardsImage;
                    i++;
                }
                else if (count[i] == 4 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level3Point[i].sprite = FevertimeAutomationImage;
                    Level3PointImage[i].sprite = FevertimeAutomationImage;
                    i++;
                }
                else if (count[i] == 5 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level3Point[i].sprite = ReductionOfLevelUpFertilizerRequirementImage;
                    Level3PointImage[i].sprite = ReductionOfLevelUpFertilizerRequirementImage;
                    i++;
                }
                else if (count[i] == 6 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level3Point[i].sprite = increaseGameTimeImage;
                    Level3PointImage[i].sprite = increaseGameTimeImage;
                    i++;
                }
                else if (count[i] == 7 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level3Point[i].sprite = chestnutAppearanceRateImage;
                    Level3PointImage[i].sprite = chestnutAppearanceRateImage;
                    i++;
                }
                else if (count[i] == 8 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level3Point[i].sprite = MonsterRegenerationRateImage;
                    Level3PointImage[i].sprite = MonsterRegenerationRateImage;
                    i++;
                }
                else if (count[i] == 9 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level3Point[i].sprite = birdMovementSpeedImage;
                    Level3PointImage[i].sprite = birdMovementSpeedImage;
                    i++;
                }
                else if (count[i] == 10 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level3Point[i].sprite = WhistleOverallGaugeReductionImage;
                    Level3PointImage[i].sprite = WhistleOverallGaugeReductionImage;
                    i++;
                }
            }
            else if (treeStatus.TreeLevel <= 38)
            {
                if (count[i] == 0 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level4Point[i].sprite = chestnutHarvestImage;
                    Level4PointImage[i].sprite = chestnutHarvestImage;
                    i++;
                }
                else if (count[i] == 1 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level4Point[i].sprite = doubleTheChestnutHarvestImage;
                    Level4PointImage[i].sprite = doubleTheChestnutHarvestImage;
                    i++;
                }
                else if (count[i] == 2 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level4Point[i].sprite = FeverTimeIncreaseImage;
                    Level4PointImage[i].sprite = FeverTimeIncreaseImage;
                    i++;
                }
                else if (count[i] == 3 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level4Point[i].sprite = IncreasedFeverTimeRewardsImage;
                    Level4PointImage[i].sprite = IncreasedFeverTimeRewardsImage;
                    i++;
                }
                else if (count[i] == 4 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level4Point[i].sprite = FevertimeAutomationImage;
                    Level4PointImage[i].sprite = FevertimeAutomationImage;
                    i++;
                }
                else if (count[i] == 5 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level4Point[i].sprite = ReductionOfLevelUpFertilizerRequirementImage;
                    Level4PointImage[i].sprite = ReductionOfLevelUpFertilizerRequirementImage;
                    i++;
                }
                else if (count[i] == 6 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level4Point[i].sprite = increaseGameTimeImage;
                    Level4PointImage[i].sprite = increaseGameTimeImage;
                    i++;
                }
                else if (count[i] == 7 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level4Point[i].sprite = chestnutAppearanceRateImage;
                    Level4PointImage[i].sprite = chestnutAppearanceRateImage;
                    i++;
                }
                else if (count[i] == 8 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level4Point[i].sprite = MonsterRegenerationRateImage;
                    Level4PointImage[i].sprite = MonsterRegenerationRateImage;
                    i++;
                }
                else if (count[i] == 9 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level4Point[i].sprite = birdMovementSpeedImage;
                    Level4PointImage[i].sprite = birdMovementSpeedImage;
                    i++;
                }
                else if (count[i] == 10 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level4Point[i].sprite = WhistleOverallGaugeReductionImage;
                    Level4PointImage[i].sprite = WhistleOverallGaugeReductionImage;
                    i++;
                }
            }
            else if (treeStatus.TreeLevel <= 48)
            {
                if (count[i] == 0 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level5Point[i].sprite = chestnutHarvestImage;
                    Level5PointImage[i].sprite = chestnutHarvestImage;
                    i++;
                }
                else if (count[i] == 1 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level5Point[i].sprite = doubleTheChestnutHarvestImage;
                    Level5PointImage[i].sprite = doubleTheChestnutHarvestImage;
                    i++;
                }
                else if (count[i] == 2 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level5Point[i].sprite = FeverTimeIncreaseImage;
                    Level5PointImage[i].sprite = FeverTimeIncreaseImage;
                    i++;
                }
                else if (count[i] == 3 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level5Point[i].sprite = IncreasedFeverTimeRewardsImage;
                    Level5PointImage[i].sprite = IncreasedFeverTimeRewardsImage;
                    i++;
                }
                else if (count[i] == 4 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level5Point[i].sprite = FevertimeAutomationImage;
                    Level5PointImage[i].sprite = FevertimeAutomationImage;
                    i++;
                }
                else if (count[i] == 5 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level5Point[i].sprite = ReductionOfLevelUpFertilizerRequirementImage;
                    Level5PointImage[i].sprite = ReductionOfLevelUpFertilizerRequirementImage;
                    i++;
                }
                else if (count[i] == 6 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level5Point[i].sprite = increaseGameTimeImage;
                    Level5PointImage[i].sprite = increaseGameTimeImage;
                    i++;
                }
                else if (count[i] == 7 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level5Point[i].sprite = chestnutAppearanceRateImage;
                    Level5PointImage[i].sprite = chestnutAppearanceRateImage;
                    i++;
                }
                else if (count[i] == 8 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level5Point[i].sprite = MonsterRegenerationRateImage;
                    Level5PointImage[i].sprite = MonsterRegenerationRateImage;
                    i++;
                }
                else if (count[i] == 9 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level5Point[i].sprite = birdMovementSpeedImage;
                    Level5PointImage[i].sprite = birdMovementSpeedImage;
                    i++;
                }
                else if (count[i] == 10 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level5Point[i].sprite = WhistleOverallGaugeReductionImage;
                    Level5PointImage[i].sprite = WhistleOverallGaugeReductionImage;
                    i++;
                }
            }
            else
            {
                if (count[i] == 0 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level6Point[i].sprite = chestnutHarvestImage;
                    Level6PointImage[i].sprite = chestnutHarvestImage;
                    i++;
                }
                else if (count[i] == 1 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level6Point[i].sprite = doubleTheChestnutHarvestImage;
                    Level6PointImage[i].sprite = doubleTheChestnutHarvestImage;
                    i++;
                }
                else if (count[i] == 2 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level6Point[i].sprite = FeverTimeIncreaseImage;
                    Level6PointImage[i].sprite = FeverTimeIncreaseImage;
                    i++;
                }
                else if (count[i] == 3 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level6Point[i].sprite = IncreasedFeverTimeRewardsImage;
                    Level6PointImage[i].sprite = IncreasedFeverTimeRewardsImage;
                    i++;
                }
                else if (count[i] == 4 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level6Point[i].sprite = FevertimeAutomationImage;
                    Level6PointImage[i].sprite = FevertimeAutomationImage;
                    i++;
                }
                else if (count[i] == 5 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level6Point[i].sprite = ReductionOfLevelUpFertilizerRequirementImage;
                    Level6PointImage[i].sprite = ReductionOfLevelUpFertilizerRequirementImage;
                    i++;
                }
                else if (count[i] == 6 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level6Point[i].sprite = increaseGameTimeImage;
                    Level6PointImage[i].sprite = increaseGameTimeImage;
                    i++;
                }
                else if (count[i] == 7 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level6Point[i].sprite = chestnutAppearanceRateImage;
                    Level6PointImage[i].sprite = chestnutAppearanceRateImage;
                    i++;
                }
                else if (count[i] == 8 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level6Point[i].sprite = MonsterRegenerationRateImage;
                    Level6PointImage[i].sprite = MonsterRegenerationRateImage;
                    i++;
                }
                else if (count[i] == 9 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level6Point[i].sprite = birdMovementSpeedImage;
                    Level6PointImage[i].sprite = birdMovementSpeedImage;
                    i++;
                }
                else if (count[i] == 10 && statusPoint[i] != 0)
                {
                    statusPoint[i]--;
                    Level6Point[i].sprite = WhistleOverallGaugeReductionImage;
                    Level6PointImage[i].sprite = WhistleOverallGaugeReductionImage;
                    i++;
                }
            }
        }
    }
}
