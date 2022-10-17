using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseCanvasUi : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] Transform LowCanvas;
    [SerializeField] Transform MiddleCanvas;

    [Header("Text")]
    [SerializeField] Text GoldkeyCount;
    [SerializeField] Text heart;
    [SerializeField] Text heartTime;
    [SerializeField] Text chestnut;
    [SerializeField] Text fertilizer;


    private void Awake()
    {
        LowCanvas =         GameObject.FindGameObjectWithTag("LowCanvas").GetComponent<Transform>();
        MiddleCanvas =      GameObject.FindGameObjectWithTag("MiddleCanvas").GetComponent<Transform>();
        heart =             LowCanvas.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
        heartTime =         LowCanvas.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<Text>();
        chestnut =          LowCanvas.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent <Text>();
        fertilizer =        LowCanvas.transform.transform.GetChild(0).GetChild(0).transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).GetComponent <Text>();
        GoldkeyCount =      MiddleCanvas.transform.GetChild(1).transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
    }
    private void Update()
    {
        PointUpdate();
    }
    void PointUpdate()
    {
        heart.text = MySqlSystem.energy.ToString() + "/" + "10";
        if (MySqlSystem.energy <= 9)
        {
            heartTime.gameObject.SetActive(true);
            if (MySqlSystem.minute != 29)
            {
                heartTime.text = MySqlSystem.minute.ToString() +":" + MySqlSystem.second.ToString("00");
            }
        }
        else
        {
            heartTime.gameObject.SetActive(false);
        }
        chestnut.text = MySqlSystem.chestnutPoint.ToString();
        fertilizer.text = MySqlSystem.fertilizerPoint.ToString();

        GoldkeyCount.text = ItemManager.goldKeyCount.ToString();
    }
}
