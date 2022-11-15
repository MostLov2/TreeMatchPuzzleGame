using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeMatchGameScoreManager : MonoBehaviour
{
    static public int chestnutPoint;
    static public int fertilizerPoint;
    public Text chestnutPointText;
    public Text fertilizerPointText;
    public Text chestnutFeverPointText;
    public Text fertilizerFeverPointText;
    public static TreeMatchGameScoreManager Instance;
    private void Awake()
    {
        Instance = this;
        chestnutPoint = 0;
        fertilizerPoint = 0;
        chestnutPointText = GameObject.FindGameObjectWithTag("MiddleCanvas").transform.GetChild(1).GetChild(0).GetComponent<Text>();
        fertilizerPointText = GameObject.FindGameObjectWithTag("MiddleCanvas").transform.GetChild(1).GetChild(1).GetComponent<Text>();
        chestnutFeverPointText = GameObject.FindGameObjectWithTag("FeverTimeCanvas").transform.GetChild(2).GetChild(1).GetComponent<Text>();
        fertilizerFeverPointText = GameObject.FindGameObjectWithTag("FeverTimeCanvas").transform.GetChild(2).GetChild(2).GetComponent<Text>();
    }
    public void ChestnutPointChangeInText(int score)
    {
        chestnutPoint += score;
        chestnutPointText.text = chestnutPoint.ToString();
        chestnutFeverPointText.text = chestnutPoint.ToString();
    }
    public void FertilizerPointChangeInText(int score)
    {
        fertilizerPoint += score;
        fertilizerPointText.text = fertilizerPoint.ToString();
        fertilizerFeverPointText.text = fertilizerPoint.ToString();
    }
}
