using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCheck : MonoBehaviour
{
    Transform TreeCheckPanel;

    private void Awake()
    {
        /*TreeCheckPanel = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(18).transform.GetChild(0).GetComponent<Transform>();
        if (MySqlSystem.instance.myObject.users.Length <= 0)
        {
            TreeCheckPanel.gameObject.SetActive(true);
        }*/
    }
    public void BuyTreeButton()
    {
        MySqlSystem.chestnutPoint = 0;
        MySqlSystem.jewelryPoint = 0;
        MySqlSystem.energy = 0;
        MySqlSystem.fertilizerPoint = 0;
        MySqlSystem.sprayLevelPoint = 0;
        MySqlSystem.dragonflyStickLevelPoint = 0;
        MySqlSystem.instance.loginTime = null;
        MySqlSystem.instance.energySpendTime = null;
        //SoundManager.instance.DestroyClip();
        Application.OpenURL("https://conbox.kr/explorer/topbam");
        LoadingSceneController.LoadingScene("LoginScene");
        TreeCheckPanel.gameObject.SetActive(false);
    }
    public void DontBuyTree()
    {
        TreeCheckPanel.gameObject.SetActive(false);
    }
}
