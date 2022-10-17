using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicManager : MonoBehaviour//게임 시작전 나무 레벨 나무 특성을 받아오는 함수
{
    //------------------TreeStatus-----------------------
    public static int               treeLevel;//나무레벨
    public static int               chestnutHarvest = 0;//밤 수확률 증가
    public static int               doubleTheChestnutHarvest = 0;//밤 수확 두배
    public static int               FeverTimeIncrease = 0;//피버타임 시간 증가
    public static int               IncreasedFeverTimeRewards = 0;//피버타임 보상 증가
    public static int               FevertimeAutomation = 0;//피버타임 자동화
    public static int               ReductionOfLevelUpFertilizerRequirement = 0;//비료 필요량 감소
    public static int               increaseGameTime = 0;//게임 시간 증가
    public static float             chestnutAppearanceRate = 0;//밤 등장 확률
    public static float             MonsterRegenerationRate = 0;//몬스터 리젠률
    public static int               birdMovementSpeed = 0;//새의 이동속도
    public static int               WhistleOverallGaugeReduction = 0;//휘슬게이지 MAX 수치 감소
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
    /// 무기 레벨 변경시 게임 적용 함수
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
    /// 나무 레벨별 게임 적용 함수
    /// </summary>
    public void ChangeLogic() //나무 레벨에 따른 설정 변경 게임 시작 버튼 클릭시 사용하게 설정
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
