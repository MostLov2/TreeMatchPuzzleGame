using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeStatus : MonoBehaviour
{
    //----------------Tree status----------------------------
    public int TreeLevel;
    public int TreeName;
    public string nft_id;
    public string nft_name;
    public int sizeNum = 1;
    public int needFertilizerNum;
    public int chestnutHarvest = 0;
    public int doubleTheChestnutHarvest = 0;
    public int feverTimeIncrease = 0;
    public int increasedFeverTimeRewards = 0;
    public int fevertimeAutomation = 0;
    public int reductionOfLevelUpFertilizerRequirement = 0;
    public int increaseGameTime = 0;
    public int chestnutAppearanceRate = 0;
    public int monsterRegenerationRate = 0;
    public int birdMovementSpeed = 0;
    public int whistleOverallGaugeReduction = 0;
    public int TreePosition = 0;
    public int useStatusPoint = 0;
    public string statusString;
    public string statusPointString;
    public SpriteRenderer treeImage;
    public Image treeImageSprite;
    //----------------Tree Sprite----------------------------
    private void Awake()
    {
        treeImage=  GetComponent<SpriteRenderer>();
        treeImageSprite =  GetComponent<Image>();
    }
}
