using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusPointChange : MonoBehaviour
{
    TreeStatus treeStatus;
    private void Awake()
    {
        treeStatus = GetComponent<TreeStatus>();
    }
    /// <summary>
    /// 1로 표기된 특성 값을 게임 사용에 맞게 변환 
    /// </summary>
    /// <param name="statusName"></param>
    /// <returns></returns>
    public float StatusChangePoint(string statusName)
    {
        if(treeStatus != null)
        {
            float result = 0;
            switch (statusName)
            {
                case "chestnutHarvest":
                    result = treeStatus.chestnutHarvest * 10;
                    break;
                case "doubleTheChestnutHarvest":
                    result = treeStatus.doubleTheChestnutHarvest;
                    break;
                case "feverTimeIncrease":
                    result = treeStatus.feverTimeIncrease;
                    break;
                case "increasedFeverTimeRewards":
                    result = treeStatus.increasedFeverTimeRewards * 10;
                    break;
                case "fevertimeAutomation":
                    result = treeStatus.fevertimeAutomation;
                    break;
                case "reductionOfLevelUpFertilizerRequirement":
                    result = treeStatus.reductionOfLevelUpFertilizerRequirement * 5;
                    break;
                case "increaseGameTime":
                    result = treeStatus.increaseGameTime;
                    break;
                case "chestnutAppearanceRate":
                    result = treeStatus.chestnutAppearanceRate * 0.1f;
                    break;
                case "monsterRegenerationRate":
                    result = treeStatus.monsterRegenerationRate * 0.1f;
                    break;
                case "birdMovementSpeed":
                    result = treeStatus.birdMovementSpeed;
                    break;
                case "whistleOverallGaugeReduction":
                    result = treeStatus.whistleOverallGaugeReduction *  10;
                    break;
                default:
                    break;
            }
            return result;
        }
        return 0;
    }
}
