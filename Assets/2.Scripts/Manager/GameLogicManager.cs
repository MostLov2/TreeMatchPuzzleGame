using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicManager : MonoBehaviour//���� ������ ���� ���� ���� Ư���� �޾ƿ��� �Լ�
{
    //------------------TreeStatus-----------------------
    public static int               treeLevel;//��������
    public static int               chestnutHarvest = 0;//�� ��Ȯ�� ����
    public static int               doubleTheChestnutHarvest = 0;//�� ��Ȯ �ι�
    public static int               FeverTimeIncrease = 0;//�ǹ�Ÿ�� �ð� ����
    public static int               IncreasedFeverTimeRewards = 0;//�ǹ�Ÿ�� ���� ����
    public static int               FevertimeAutomation = 0;//�ǹ�Ÿ�� �ڵ�ȭ
    public static int               ReductionOfLevelUpFertilizerRequirement = 0;//��� �ʿ䷮ ����
    public static int               increaseGameTime = 0;//���� �ð� ����
    public static float             chestnutAppearanceRate = 0;//�� ���� Ȯ��
    public static float             MonsterRegenerationRate = 0;//���� ������
    public static int               birdMovementSpeed = 0;//���� �̵��ӵ�
    public static int               WhistleOverallGaugeReduction = 0;//�ֽ������� MAX ��ġ ����
    public static int               goldenBugHp = 0;
    //------------------WeaponDMG-----------------------
    public static int               sprayDamge;
    public static int               DragonflyStickDamge;
    //------------------SingleTurn-----------------------
    public static GameLogicManager  instance;
    void Start()
    {
        if (null == instance)
        {
            instance =              this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            instance =              null;
        }
        ChangeLogic();
        WeaponLevelChange();
    }
    /// <summary>
    /// ���� ���� ����� ���� ���� �Լ�
    /// </summary>
    public void WeaponLevelChange()
    {
        if (MySqlSystem.dragonflyStickLevelPoint == 0)
        {
            DragonflyStickDamge = 14;
        }
        if (MySqlSystem.dragonflyStickLevelPoint == 1)
        {
            DragonflyStickDamge = 17;
        }
        if (MySqlSystem.dragonflyStickLevelPoint == 2)
        {
            DragonflyStickDamge = 21;
        }
        if (MySqlSystem.dragonflyStickLevelPoint == 3)
        {
            DragonflyStickDamge = 32;
        }
        if (MySqlSystem.dragonflyStickLevelPoint == 4)
        {
            DragonflyStickDamge = 43;
        }
        if (MySqlSystem.dragonflyStickLevelPoint == 5)
        {
            DragonflyStickDamge = 54;
        }
        if (MySqlSystem.sprayLevelPoint == 0)
        {
            sprayDamge = 1;
        }
        if (MySqlSystem.sprayLevelPoint == 1)
        {
            sprayDamge = 2;
        }
        if (MySqlSystem.sprayLevelPoint == 2)
        {
            sprayDamge = 4;
        }
        if (MySqlSystem.sprayLevelPoint == 3)
        {
            sprayDamge = 6;
        }
        if (MySqlSystem.sprayLevelPoint == 4)
        {
            sprayDamge = 9;
        }
        if (MySqlSystem.sprayLevelPoint == 5)
        {
            sprayDamge = 19;
        }
    }
    /// <summary>
    /// ���� ������ ���� ���� �Լ�
    /// </summary>
    public void ChangeLogic() //���� ������ ���� ���� ���� ���� ���� ��ư Ŭ���� ����ϰ� ����
    {
        if (treeLevel < 10)
        {
            SpawnManager.chestnutBugSpawnWaitTime = 5;
            SpawnManager.squrrielSpawnWaitTime = 5;
            SpawnManager.chestnutbugSpawnCount = 3;
            SpawnManager.squrrielSpawnCount = 5;
            SpawnManager.chestnutSpawnCount = 15;
            ChestnutBur.minRandomShot = 1;
            ChestnutBur.maxRandomShot = 1;
            goldenBugHp = 250;
        }
        else if (treeLevel < 20)
        {
            SpawnManager.chestnutBugSpawnWaitTime = 5;
            SpawnManager.squrrielSpawnWaitTime = 5;
            SpawnManager.chestnutbugSpawnCount = 5;
            SpawnManager.squrrielSpawnCount = 6;
            SpawnManager.chestnutSpawnCount = 15;
            ChestnutBur.minRandomShot = 1;
            ChestnutBur.maxRandomShot = 2;
            goldenBugHp = 260;
        }
        else if (treeLevel < 30)
        {
            SpawnManager.chestnutBugSpawnWaitTime = 5;
            SpawnManager.squrrielSpawnWaitTime = 5;
            SpawnManager.chestnutbugSpawnCount = 7;
            SpawnManager.squrrielSpawnCount = 7;
            SpawnManager.chestnutSpawnCount = 20;
            ChestnutBur.minRandomShot = 1;
            ChestnutBur.maxRandomShot = 3;
            goldenBugHp = 270;
        }
        else if (treeLevel < 40)
        {
            SpawnManager.chestnutBugSpawnWaitTime = 5;
            SpawnManager.squrrielSpawnWaitTime = 5;
            SpawnManager.chestnutbugSpawnCount = 9;
            SpawnManager.squrrielSpawnCount = 8;
            SpawnManager.chestnutSpawnCount = 20;
            ChestnutBur.minRandomShot = 2;
            ChestnutBur.maxRandomShot = 3;
            goldenBugHp = 280;
        }
        else if (treeLevel < 50)
        {
            SpawnManager.chestnutBugSpawnWaitTime = 5;
            SpawnManager.squrrielSpawnWaitTime = 5;
            SpawnManager.chestnutbugSpawnCount = 10;
            SpawnManager.squrrielSpawnCount = 9;
            SpawnManager.chestnutSpawnCount = 25;
            ChestnutBur.minRandomShot = 2;
            ChestnutBur.maxRandomShot = 4;
            goldenBugHp = 290;
        }
        else
        {
            SpawnManager.chestnutBugSpawnWaitTime = 5;
            SpawnManager.squrrielSpawnWaitTime = 5;
            SpawnManager.chestnutbugSpawnCount = 12;
            SpawnManager.squrrielSpawnCount = 10;
            SpawnManager.chestnutSpawnCount = 25;
            ChestnutBur.minRandomShot = 3;
            ChestnutBur.maxRandomShot = 5;
            goldenBugHp = 300;
        }
    }
}
