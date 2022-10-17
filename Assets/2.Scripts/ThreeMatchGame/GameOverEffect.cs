using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverEffect : MonoBehaviour
{
    public void GameOver()
    {
        TreeMatchPuzzelGameUIManager.instance.SettingGameOverPanel(TreeMatchGameScoreManager.chestnutPoint, TreeMatchGameScoreManager.fertilizerPoint);
    }
}
