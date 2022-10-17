using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InClick : MonoBehaviour
{
    private float clickTime;
    public float minClickTime = 1;
    private bool isClick;
    TreeStatus treeStatus;
    Transform SetInventoryPanel;
    Transform GameStartPanel;
    private void Awake()
    {
        treeStatus = GetComponent<TreeStatus>();
    }
    public void IsClick()
    {
        if (isClick)
        {
            clickTime += Time.deltaTime;
            if (clickTime < 1)
            {
                treeStatus.treeImage.color = new Color((255 - clickTime * 100) / 255f, (255 - clickTime * 100) / 255f, (255 - clickTime * 100) / 255f);
            }
            else
            {
                treeStatus.treeImage.color = new Color((255 - 1 * 100) / 255f, (255 - 1 * 100) / 255f, (255 - 1 * 100) / 255f, 200 / 255f);

            }
        }
        else
        {
            treeStatus.treeImage.color = Color.white;
            clickTime = 0;
        }
    }
    public void ButtonDown()
    {
        isClick = true;
    }

    public void ButtonUp()
    {
        isClick = false;
        if (clickTime >= minClickTime)
        {
            TouchTree();
            SetTreeInventoryPanel();
        }
        else
        {
            if (!GameObject.FindGameObjectWithTag("Main").GetComponent<TutorialCheck>().isDidTutorial.LobbyTutorial)
            {
                TouchTree();
                SetTreeInventoryPanel();
            }
            else
            {
                GameStartButton();
            }
        }
    }
    public void TouchTree()
    {
        TreeInfomation.InfoTreeName = treeStatus.TreeName;
        TreeSetManager.treeName = treeStatus.TreeName;
        TreeInfomation.instance.FindTree();
        TreeInfomation.instance.UpdateInfoPanel();
    }
    public void SetTreeInventoryPanel()
    {
        if (GameObject.FindGameObjectWithTag("Canvas") != null)
        {
            SetInventoryPanel = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(10).transform.GetChild(0).transform.GetChild(0).GetComponent<Transform>();
        }
        Vector2 screenPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(SetInventoryPanel.transform.parent.GetComponent<RectTransform>(), Input.mousePosition, Camera.main, out screenPoint);
        SetInventoryPanel.localPosition = screenPoint;
        SetInventoryPanel.parent.gameObject.SetActive(true);
    }
    public void GameStartButton()
    {
        GameLogicManager.treeLevel = treeStatus.TreeLevel;
        GameLogicManager.chestnutHarvest = treeStatus.chestnutHarvest;
        GameLogicManager.doubleTheChestnutHarvest = treeStatus.doubleTheChestnutHarvest;
        GameLogicManager.FeverTimeIncrease = treeStatus.feverTimeIncrease;
        GameLogicManager.IncreasedFeverTimeRewards = treeStatus.increasedFeverTimeRewards *10;
        GameLogicManager.FevertimeAutomation = treeStatus.fevertimeAutomation;
        GameLogicManager.ReductionOfLevelUpFertilizerRequirement = treeStatus.reductionOfLevelUpFertilizerRequirement;
        GameLogicManager.increaseGameTime = treeStatus.increaseGameTime;
        GameLogicManager.chestnutAppearanceRate = treeStatus.chestnutAppearanceRate * 0.1f;
        GameLogicManager.MonsterRegenerationRate = treeStatus.monsterRegenerationRate * 0.1f;
        GameLogicManager.birdMovementSpeed = treeStatus.birdMovementSpeed;
        GameLogicManager.WhistleOverallGaugeReduction = treeStatus.whistleOverallGaugeReduction*10;
        GameStartPanel = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(14).transform.GetChild(0).GetComponent<Transform>();
        GameStartPanel.gameObject.SetActive(true);
    }
}
