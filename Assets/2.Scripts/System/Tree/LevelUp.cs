using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    TreeStatus treeStatus;
    TreeImageChange treeImageChange;
    public int needFertilizerNum = 0;
    CreatStatus creatStatus;
    StatusPointChange statusPointChange;

    private void Awake()
    {
        treeStatus = GetComponent<TreeStatus>();
        treeImageChange = GetComponent<TreeImageChange>();
        creatStatus = GetComponent<CreatStatus>();
        statusPointChange = GetComponent<StatusPointChange>();
        
    }
    public void TreeLevelUp()
    {
        if (treeStatus != null)
        {
            MySqlSystem.fertilizerPoint -= needFertilizerNum - treeStatus.reductionOfLevelUpFertilizerRequirement;
            StartCoroutine(MySqlSystem.instance.SetFertilizer(MySqlSystem.fertilizerPoint));
            treeStatus.TreeLevel++;
            
            StartCoroutine(MySqlSystem.instance.SetTreeLevel(treeStatus.TreeName, treeStatus.TreeLevel));

            if (treeStatus.TreeLevel % 10 == 0)
            {
                if (treeStatus.TreeLevel != 0)
                {
                    treeStatus.useStatusPoint += 1;
                    treeImageChange.TreeImageChangeFollowedTreeLevel();
                    creatStatus.CreateStatus();
                    StartCoroutine(LobbySceneUIManager.treeImage.GetComponent<InfoTreeStatusChestnut>().LobbyLevelCStheck());
                    StartCoroutine(LobbySceneUIManager.treeImage.GetComponent<InfoTreeStatusChestnut>().LobbyStatusPointCheck());
                }
            }
        }
        else
        {
            Debug.LogError("TreeStatus is not in LevelUpScript");
        }
        
    }
    public void HowManyNeedFertilizer()
    {
        if(treeStatus.TreeLevel%10 == 9)
        {
            treeStatus.sizeNum = ((int)treeStatus.TreeLevel / 10)+2;
        }
        else
        {
            treeStatus.sizeNum = ((int)treeStatus.TreeLevel / 10) + 1;
        }
       
        if (treeStatus.TreeLevel == 1)
        {
            needFertilizerNum = 10 - (int)statusPointChange.StatusChangePoint("reductionOfLevelUpFertilizerRequirement");
            treeStatus.needFertilizerNum = needFertilizerNum;
        }
        else if (treeStatus.TreeLevel == 2)
        {
            needFertilizerNum = 20 - (int)statusPointChange.StatusChangePoint("reductionOfLevelUpFertilizerRequirement");
            treeStatus.needFertilizerNum = needFertilizerNum;
        }
        else if (treeStatus.TreeLevel >= 3)
        {
            needFertilizerNum = 0;
            needFertilizerNum = 10 * (treeStatus.TreeLevel) * treeStatus.sizeNum;
            treeStatus.needFertilizerNum = needFertilizerNum - (int)statusPointChange.StatusChangePoint("reductionOfLevelUpFertilizerRequirement");
        }
    }
    
}
