using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatStatus : MonoBehaviour
{
    TreeStatus treeStatus;
    private void Awake()
    {
        treeStatus = GetComponent<TreeStatus>();
    }
    public void CreateStatus()
    {
        if(treeStatus != null)
        {
            if (treeStatus.useStatusPoint >= 1)
            {
                treeStatus.useStatusPoint--;
                int RandomNum = Random.Range(1, 101);
                if (treeStatus.doubleTheChestnutHarvest == 0 && treeStatus.fevertimeAutomation == 0)
                {
                    if (RandomNum < 10)//���Ȯ��
                    {
                        treeStatus.chestnutHarvest += 1;
                        StartCoroutine(MySqlSystem.instance.SetChestnutHarvest(treeStatus.chestnutHarvest, treeStatus.TreeName));
                    }
                    else if (RandomNum < 11)//���Ȯ �ι�
                    {
                        treeStatus.doubleTheChestnutHarvest += 1;
                        StartCoroutine(MySqlSystem.instance.SetDoubleTheChestnutHarvest(treeStatus.doubleTheChestnutHarvest, treeStatus.TreeName));
                    }
                    else if (RandomNum < 21)//�ǹ�Ÿ�� �ð� ����
                    {
                        treeStatus.feverTimeIncrease += 1;
                        StartCoroutine(MySqlSystem.instance.SetFeverTimeIncrease(treeStatus.feverTimeIncrease, treeStatus.TreeName));
                    }
                    else if (RandomNum < 31)//�ǹ�Ÿ�� ��������
                    {
                        treeStatus.increasedFeverTimeRewards += 1;
                        StartCoroutine(MySqlSystem.instance.SetIncreasedFeverTimeRewards(treeStatus.increasedFeverTimeRewards, treeStatus.TreeName));
                    }
                    else if (RandomNum < 41)//�ǹ�Ÿ�� �ڵ�ȭ
                    {
                        treeStatus.fevertimeAutomation += 1;
                        StartCoroutine(MySqlSystem.instance.SetFeverTimeAutomation(treeStatus.fevertimeAutomation, treeStatus.TreeName));
                    }
                    else if (RandomNum < 42)//�������� �ʿ��� ��ᷮ ����
                    {
                        treeStatus.reductionOfLevelUpFertilizerRequirement += 1;
                        StartCoroutine(MySqlSystem.instance.SetReductionOfLevelUpFertilizerRequirement(treeStatus.reductionOfLevelUpFertilizerRequirement, treeStatus.TreeName));
                    }
                    else if (RandomNum < 52)//���� �ð� ����
                    {
                        treeStatus.increaseGameTime += 1;
                        StartCoroutine(MySqlSystem.instance.SetIncreaseGameTime(treeStatus.increaseGameTime, treeStatus.TreeName));
                    }
                    else if (RandomNum < 64)//�� ���� Ȯ��
                    {
                        treeStatus.chestnutAppearanceRate += 1;
                        StartCoroutine(MySqlSystem.instance.SetChestnutAppearanceRate(treeStatus.chestnutAppearanceRate, treeStatus.TreeName));
                    }
                    else if (RandomNum < 76)//���� ������
                    {
                        treeStatus.monsterRegenerationRate += 1;
                        StartCoroutine(MySqlSystem.instance.SetMonsterRegenerationRate(treeStatus.monsterRegenerationRate, treeStatus.TreeName));
                    }
                    else if (RandomNum < 88)//���� �̵��ӵ�
                    {
                        treeStatus.birdMovementSpeed += 1;
                        StartCoroutine(MySqlSystem.instance.SetBirdMovementSpeed(treeStatus.birdMovementSpeed, treeStatus.TreeName));
                    }
                    else if (RandomNum < 101)//�ֽ� ������ max��ġ ����
                    {
                        treeStatus.whistleOverallGaugeReduction += 1;
                        StartCoroutine(MySqlSystem.instance.SetWhistleOverallGaugeReduction(treeStatus.whistleOverallGaugeReduction, treeStatus.TreeName));
                    }
                }
                else if (treeStatus.doubleTheChestnutHarvest == 1 && treeStatus.fevertimeAutomation == 0)
                {
                    if (RandomNum < 11)//���Ȯ��
                    {
                        treeStatus.chestnutHarvest += 1;
                        StartCoroutine(MySqlSystem.instance.SetChestnutHarvest(treeStatus.chestnutHarvest, treeStatus.TreeName));
                    }
                    else if (RandomNum < 21)//�ǹ�Ÿ�� �ð� ����
                    {
                        treeStatus.feverTimeIncrease += 1;
                        StartCoroutine(MySqlSystem.instance.SetFeverTimeIncrease(treeStatus.feverTimeIncrease, treeStatus.TreeName));
                    }
                    else if (RandomNum < 31)//�ǹ�Ÿ�� ��������
                    {
                        treeStatus.increasedFeverTimeRewards += 1;
                        StartCoroutine(MySqlSystem.instance.SetIncreasedFeverTimeRewards(treeStatus.increasedFeverTimeRewards, treeStatus.TreeName));
                    }
                    else if (RandomNum < 41)//�ǹ�Ÿ�� �ڵ�ȭ
                    {
                        treeStatus.fevertimeAutomation += 1;
                        StartCoroutine(MySqlSystem.instance.SetFeverTimeAutomation(treeStatus.fevertimeAutomation, treeStatus.TreeName));
                    }
                    else if (RandomNum < 42)//�������� �ʿ��� ��ᷮ ����
                    {
                        treeStatus.reductionOfLevelUpFertilizerRequirement += 1;
                        StartCoroutine(MySqlSystem.instance.SetReductionOfLevelUpFertilizerRequirement(treeStatus.reductionOfLevelUpFertilizerRequirement, treeStatus.TreeName));
                    }
                    else if (RandomNum < 52)//���� �ð� ����
                    {
                        treeStatus.increaseGameTime += 1;
                        StartCoroutine(MySqlSystem.instance.SetIncreaseGameTime(treeStatus.increaseGameTime, treeStatus.TreeName));
                    }
                    else if (RandomNum < 64)//�� ���� Ȯ��
                    {
                        treeStatus.chestnutAppearanceRate += 1;
                        StartCoroutine(MySqlSystem.instance.SetChestnutAppearanceRate(treeStatus.chestnutAppearanceRate, treeStatus.TreeName));
                    }
                    else if (RandomNum < 76)//���� ������
                    {
                        treeStatus.monsterRegenerationRate += 1;
                        StartCoroutine(MySqlSystem.instance.SetMonsterRegenerationRate(treeStatus.monsterRegenerationRate, treeStatus.TreeName));
                    }
                    else if (RandomNum < 88)//���� �̵��ӵ�
                    {
                        treeStatus.birdMovementSpeed += 1;
                        StartCoroutine(MySqlSystem.instance.SetBirdMovementSpeed(treeStatus.birdMovementSpeed, treeStatus.TreeName));
                    }
                    else if (RandomNum < 100)//�ֽ� ������ max��ġ ����
                    {
                        treeStatus.whistleOverallGaugeReduction += 1;
                        StartCoroutine(MySqlSystem.instance.SetWhistleOverallGaugeReduction(treeStatus.whistleOverallGaugeReduction, treeStatus.TreeName));
                    }
                }
                else if (treeStatus.doubleTheChestnutHarvest == 0 && treeStatus.fevertimeAutomation == 1)
                {
                    if (RandomNum < 12)//���Ȯ��
                    {
                        treeStatus.chestnutHarvest += 1;
                        StartCoroutine(MySqlSystem.instance.SetChestnutHarvest(treeStatus.chestnutHarvest, treeStatus.TreeName));
                    }
                    else if (RandomNum < 13)//���Ȯ �ι�
                    {
                        treeStatus.doubleTheChestnutHarvest = 1;
                        StartCoroutine(MySqlSystem.instance.SetDoubleTheChestnutHarvest(1, treeStatus.TreeName));
                    }
                    else if (RandomNum < 25)//�ǹ�Ÿ�� �ð� ����
                    {
                        treeStatus.feverTimeIncrease += 1;
                        StartCoroutine(MySqlSystem.instance.SetFeverTimeIncrease(treeStatus.feverTimeIncrease, treeStatus.TreeName));
                    }
                    else if (RandomNum < 37)//�ǹ�Ÿ�� ��������
                    {
                        treeStatus.increasedFeverTimeRewards += 1;
                        StartCoroutine(MySqlSystem.instance.SetIncreasedFeverTimeRewards(treeStatus.increasedFeverTimeRewards, treeStatus.TreeName));
                    }
                    else if (RandomNum < 38)//�������� �ʿ��� ��ᷮ ����
                    {
                        treeStatus.reductionOfLevelUpFertilizerRequirement += 1;
                        StartCoroutine(MySqlSystem.instance.SetReductionOfLevelUpFertilizerRequirement(treeStatus.reductionOfLevelUpFertilizerRequirement, treeStatus.TreeName));
                    }
                    else if (RandomNum < 48)//���� �ð� ����
                    {
                        treeStatus.increaseGameTime += 1;
                        StartCoroutine(MySqlSystem.instance.SetIncreaseGameTime(treeStatus.increaseGameTime, treeStatus.TreeName));
                    }
                    else if (RandomNum < 61)//�� ���� Ȯ��
                    {
                        treeStatus.chestnutAppearanceRate += 1;
                        StartCoroutine(MySqlSystem.instance.SetChestnutAppearanceRate(treeStatus.chestnutAppearanceRate, treeStatus.TreeName));
                    }
                    else if (RandomNum < 74)//���� ������
                    {
                        treeStatus.monsterRegenerationRate += 1;
                        StartCoroutine(MySqlSystem.instance.SetMonsterRegenerationRate(treeStatus.monsterRegenerationRate, treeStatus.TreeName));
                    }
                    else if (RandomNum < 87)//���� �̵��ӵ�
                    {
                        treeStatus.birdMovementSpeed += 1;
                        StartCoroutine(MySqlSystem.instance.SetBirdMovementSpeed(treeStatus.birdMovementSpeed, treeStatus.TreeName));
                    }
                    else if (RandomNum < 101)//�ֽ� ������ max��ġ ����
                    {
                        treeStatus.whistleOverallGaugeReduction += 1;
                        StartCoroutine(MySqlSystem.instance.SetWhistleOverallGaugeReduction(treeStatus.whistleOverallGaugeReduction, treeStatus.TreeName));
                    }
                }
                else if (treeStatus.doubleTheChestnutHarvest == 1 && treeStatus.fevertimeAutomation == 1)
                {
                    if (RandomNum < 12)//���Ȯ��
                    {
                        treeStatus.chestnutHarvest += 1;
                        StartCoroutine(MySqlSystem.instance.SetChestnutHarvest(treeStatus.chestnutHarvest, treeStatus.TreeName));
                    }
                    else if (RandomNum < 24)//�ǹ�Ÿ�� �ð� ����
                    {
                        treeStatus.feverTimeIncrease += 1;
                        StartCoroutine(MySqlSystem.instance.SetFeverTimeIncrease(treeStatus.feverTimeIncrease, treeStatus.TreeName));
                    }
                    else if (RandomNum < 36)//�ǹ�Ÿ�� ��������
                    {
                        treeStatus.increasedFeverTimeRewards += 1;
                        StartCoroutine(MySqlSystem.instance.SetIncreasedFeverTimeRewards(treeStatus.increasedFeverTimeRewards, treeStatus.TreeName));
                    }
                    else if (RandomNum < 38)//�������� �ʿ��� ��ᷮ ����
                    {
                        treeStatus.reductionOfLevelUpFertilizerRequirement += 1;
                        StartCoroutine(MySqlSystem.instance.SetReductionOfLevelUpFertilizerRequirement(treeStatus.reductionOfLevelUpFertilizerRequirement, treeStatus.TreeName));
                    }
                    else if (RandomNum < 48)//���� �ð� ����
                    {
                        treeStatus.increaseGameTime += 1;
                        StartCoroutine(MySqlSystem.instance.SetIncreaseGameTime(treeStatus.increaseGameTime, treeStatus.TreeName));
                    }
                    else if (RandomNum < 61)//�� ���� Ȯ��
                    {
                        treeStatus.chestnutAppearanceRate += 1;
                        StartCoroutine(MySqlSystem.instance.SetChestnutAppearanceRate(treeStatus.chestnutAppearanceRate, treeStatus.TreeName));
                    }
                    else if (RandomNum < 74)//���� ������
                    {
                        treeStatus.monsterRegenerationRate += 1;
                        StartCoroutine(MySqlSystem.instance.SetMonsterRegenerationRate(treeStatus.monsterRegenerationRate, treeStatus.TreeName));
                    }
                    else if (RandomNum < 87)//���� �̵��ӵ�
                    {
                        treeStatus.birdMovementSpeed += 1;
                        StartCoroutine(MySqlSystem.instance.SetBirdMovementSpeed(treeStatus.birdMovementSpeed, treeStatus.TreeName));
                    }
                    else if (RandomNum < 101)//�ֽ� ������ max��ġ ����
                    {
                        treeStatus.whistleOverallGaugeReduction += 1;
                        StartCoroutine(MySqlSystem.instance.SetWhistleOverallGaugeReduction(treeStatus.whistleOverallGaugeReduction, treeStatus.TreeName));
                    }
                }
            }
        }
        else
        {
            Debug.LogError("TreeStatus is not in CreateStatus");
        }
    }
}
