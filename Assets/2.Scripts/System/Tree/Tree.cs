using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    TreeStatus status;
    LevelUp levelUp;
    InClick inClick;
    CreatStatus creatStatus;
    TreeImageChange treeImageChange;
    StatusString statusString;
    StatusPointChange statusPointChange;
    TreePositionChange treePositionChange;
    ChangeStatusChestnut changeStatusChestnut;

    private void Awake()
    {
        status = GetComponent<TreeStatus>();
        levelUp = GetComponent<LevelUp>();
        inClick = GetComponent<InClick>();
        creatStatus = GetComponent<CreatStatus>();
        treeImageChange = GetComponent<TreeImageChange>();
        statusString = GetComponent<StatusString>();
        statusPointChange = GetComponent<StatusPointChange>();
        treePositionChange = GetComponent<TreePositionChange>();
        changeStatusChestnut = GetComponent<ChangeStatusChestnut>();
    }
    public void UpdateData()
    {
        levelUp.HowManyNeedFertilizer();
        treeImageChange.TreeImageChangeFollowedTreeLevel();
        statusString.StatusPointStringCreate();
        statusString.StatusStringCreate();
        changeStatusChestnut.CheckStatusPoint();
        StartCoroutine(changeStatusChestnut.LobbyLevelCStheck());
        StartCoroutine(changeStatusChestnut.LevelCStheck());
    }
    private void Update()
    {
        inClick.IsClick();
    }

}
