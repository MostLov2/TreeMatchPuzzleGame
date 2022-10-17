using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusString : MonoBehaviour
{
    TreeStatus treeStatus;
    StatusPointChange statusPointChange;
    private void Awake()
    {
        treeStatus = GetComponent<TreeStatus>();
        statusPointChange = GetComponent<StatusPointChange>();
    }
    /// <summary>
    /// 특성명 표기를 위한 String
    /// </summary>
    public void StatusStringCreate()
    {
        string statusString = null;
        if(treeStatus != null)
        {
            if (treeStatus.chestnutHarvest != 0)
            {
                statusString += "C.PointBounus:";
                if (statusString != null)
                {
                    statusString += "\n";
                }
            }
            if (treeStatus.doubleTheChestnutHarvest != 0)
            {
                statusString += "DoubleCpoint:";
                if (statusString != null)
                {
                    statusString += "\n";
                }
            }
            if (treeStatus.feverTimeIncrease != 0)
            {
                statusString += "FeverTimeBounus:";
                if (statusString != null)
                {
                    statusString += "\n";
                }
            }
            if (treeStatus.increasedFeverTimeRewards != 0)
            {
                statusString += "FeverRewardsBounus:";
                if (statusString != null)
                {
                    statusString += "\n";
                }
            }
            if (treeStatus.fevertimeAutomation != 0)
            {
                statusString += "FeverAutomation:";
                if (statusString != null)
                {
                    statusString += "\n";
                }
            }
            if (treeStatus.reductionOfLevelUpFertilizerRequirement != 0)
            {
                statusString += "FertilizerReduce:";
                if (statusString != null)
                {
                    statusString += "\n";
                }
            }
            if (treeStatus.increaseGameTime != 0)
            {
                statusString += "GameTimeBounus:";
                if (statusString != null)
                {
                    statusString += "\n";
                }
            }
            if (treeStatus.chestnutAppearanceRate != 0)
            {
                statusString += "C.SpawnReduce:";
                if (statusString != null)
                {
                    statusString += "\n";
                }
            }
            if (treeStatus.monsterRegenerationRate != 0)
            {
                statusString += "M.SpawnReduce:";
                if (statusString != null)
                {
                    statusString += "\n";
                }
            }
            if (treeStatus.birdMovementSpeed != 0)
            {
                statusString += "BirdSpeedDown:";
                if (statusString != null)
                {
                    statusString += "\n";
                }
            }
            if (treeStatus.whistleOverallGaugeReduction != 0)
            {
                statusString += "WhistlePointDown:";
                if (statusString != null)
                {
                    statusString += "\n";
                }
            }
        }
        treeStatus.statusString = statusString;
    }
    /// <summary>
    /// 특성 값 표기를 위한 String
    /// </summary>
    public void StatusPointStringCreate()
    {
        string statusString = null;
        if (treeStatus.chestnutHarvest != 0)
        {
            statusString += statusPointChange.StatusChangePoint("chestnutHarvest");
            if (statusString != null)
            {
                statusString += "\n";
            }
        }
        if (treeStatus.doubleTheChestnutHarvest != 0)
        {
            statusString += "On";
            if (statusString != null)
            {
                statusString += "\n";
            }
        }
        if (treeStatus.feverTimeIncrease != 0)
        {
            statusString += statusPointChange.StatusChangePoint("feverTimeIncrease");
            if (statusString != null)
            {
                statusString += "\n";
            }
        }
        if (treeStatus.increasedFeverTimeRewards != 0)
        {
            statusString += statusPointChange.StatusChangePoint("increasedFeverTimeRewards");
            if (statusString != null)
            {
                statusString += "\n";
            }
        }
        if (treeStatus.fevertimeAutomation != 0)
        {
            statusString += "On";
            if (statusString != null)
            {
                statusString += "\n";
            }
        }
        if (treeStatus.reductionOfLevelUpFertilizerRequirement != 0)
        {
            statusString += statusPointChange.StatusChangePoint("reductionOfLevelUpFertilizerRequirement");
            if (statusString != null)
            {
                statusString += "\n";
            }
        }
        if (treeStatus.increaseGameTime != 0)
        {
            statusString += statusPointChange.StatusChangePoint("increaseGameTime");
            if (statusString != null)
            {
                statusString += "\n";
            }
        }
        if(treeStatus.chestnutAppearanceRate != 0)
        {
            statusString += statusPointChange.StatusChangePoint("chestnutAppearanceRate");
            if (statusString != null)
            {
                statusString += "\n";
            }
        }
        if (treeStatus.monsterRegenerationRate != 0)
        {
            statusString += statusPointChange.StatusChangePoint("monsterRegenerationRate");
            if (statusString != null)
            {
                statusString += "\n";
            }
        }
        if (treeStatus.birdMovementSpeed != 0)
        {
            statusString += statusPointChange.StatusChangePoint("birdMovementSpeed");
            if (statusString != null)
            {
                statusString += "\n";
            }
        }
        if(treeStatus.whistleOverallGaugeReduction != 0)
        {
            statusString += statusPointChange.StatusChangePoint("whistleOverallGaugeReduction");
            if (statusString != null)
            {
                statusString += "\n";
            }
        }
        treeStatus.statusPointString = statusString;
    }

}
