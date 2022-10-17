using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class RandomBoxOpen : MonoBehaviour
{
    public int randomGoldenBoxPoint;
    public int randomSilverBoxPoint;
    [Header("Canvas")]
    [SerializeField] Transform highCanvas;
    [SerializeField] Transform MoreHighCanvas;

    [Header("Panel")]
    [SerializeField] Transform NotEnoughChestnutPanel;
    [SerializeField] Transform NotEnoughGoldKeyPanel;
    [SerializeField] Transform RewardPanel;
    [SerializeField] Transform BlackPanel;
    [SerializeField] Transform GoldBoxPanel;
    [SerializeField] Transform SilverBoxPanel;

    [Header("Image")]
    [SerializeField] Transform RewardChestnutImage;
    [SerializeField] Transform RewardfertilizerImage;
    [SerializeField] Transform RewardFakeImage;

    [Header("Text")]
    [SerializeField] Text RewardText;

    [Header("Sound")]
    [SerializeField] AudioClip[] clip;

    [Header("Effect")]
    [SerializeField] Transform chestnutEffect;
    [SerializeField] Transform fertilizerEffect;

    public AudioMixer audioMixer;

    private void Awake()
    {

        SoundManager.instance.PlaySFX(clip, 0, 1, 0);

        highCanvas = GameObject.FindGameObjectWithTag("HighCanvas").GetComponent<Transform>();
        MoreHighCanvas = GameObject.FindGameObjectWithTag("MoreHighCanvas(PopUpPanel)").GetComponent<Transform>();

        NotEnoughChestnutPanel = highCanvas.transform.GetChild(1).GetComponent<Transform>();
        NotEnoughGoldKeyPanel = highCanvas.transform.GetChild(2).GetComponent<Transform>();
        RewardPanel = MoreHighCanvas.transform.GetChild(2).GetComponent<Transform>();
        BlackPanel = highCanvas.transform.GetChild(0).GetComponent<Transform>();
        GoldBoxPanel = MoreHighCanvas.transform.GetChild(1).GetComponent<Transform>();
        SilverBoxPanel = MoreHighCanvas.transform.GetChild(0).GetComponent<Transform>();

        RewardChestnutImage = MoreHighCanvas.transform.GetChild(2).GetChild(1).GetComponent<Transform>();
        RewardfertilizerImage = MoreHighCanvas.transform.GetChild(2).GetChild(2).GetComponent<Transform>();
        RewardFakeImage = MoreHighCanvas.transform.GetChild(2).GetChild(3).GetComponent<Transform>();

        RewardText = RewardPanel.transform.GetChild(0).GetComponent<Text>();

        fertilizerEffect = GameObject.FindGameObjectWithTag("Effect").transform.GetChild(0).GetComponent<Transform>();
        chestnutEffect = GameObject.FindGameObjectWithTag("Effect").transform.GetChild(1).GetComponent<Transform>();
    }
    /// <summary>
    /// SilverBoxItemPersentage
    /// </summary>
    public void SilverBoxOpen()
    {
        if (MySqlSystem.chestnutPoint >= 500)
        {
            BlackPanel.gameObject.SetActive(true);
            SilverBoxPanel.gameObject.SetActive(true);
            MySqlSystem.chestnutPoint -= 500;
            StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint));

            randomSilverBoxPoint = Random.Range(1, 1001);
            if (randomSilverBoxPoint <= Balance.instance.chestnut_1S)//参
            {
                PanelImageChange("Fake");
                RewardText.text = "NoLuck";
                StartCoroutine(RewardPanelOn(3, 3));
            }
            else if (randomSilverBoxPoint <= Balance.instance.chestnut_2S)//广500
            {
                MySqlSystem.chestnutPoint += 500;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint));
                PanelImageChange("Chestnut");
                RewardText.text = "500";
                StartCoroutine(RewardPanelOn(4, 3));
            }
            else if (randomSilverBoxPoint <= Balance.instance.fertilizer_1S)//厚丰20
            {
                MySqlSystem.fertilizerPoint += 20;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.fertilizerPoint));
                PanelImageChange("Fertilizer");
                RewardText.text = "20";
                StartCoroutine(RewardPanelOn(4, 3));
            }
            else if (randomSilverBoxPoint <= Balance.instance.chestnut_3S)//广750
            {
                MySqlSystem.chestnutPoint += 750;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint));
                PanelImageChange("Chestnut");
                RewardText.text = "750";
                StartCoroutine(RewardPanelOn(4, 3));
            }
            else if (randomSilverBoxPoint <= Balance.instance.chestnut_4S)//广1000
            {
                MySqlSystem.chestnutPoint += 1000;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint));
                PanelImageChange("Chestnut");
                RewardText.text = "1000";
                StartCoroutine(RewardPanelOn(4, 3));
            }
            else if (randomSilverBoxPoint <= Balance.instance.chestnut_5S)//广1500
            {
                MySqlSystem.chestnutPoint += 1500;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint));
                PanelImageChange("Chestnut");
                RewardText.text = "1500";
                StartCoroutine(RewardPanelOn(4, 3));
            }
            else if (randomSilverBoxPoint <= Balance.instance.chestnut_6S)//广2500
            {
                MySqlSystem.chestnutPoint += 2500;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint));
                PanelImageChange("Chestnut");
                RewardText.text = "2500";
                StartCoroutine(RewardPanelOn(4, 3));
            }
            else if (randomSilverBoxPoint <= Balance.instance.fertilizer_2S)//厚丰 50
            {
                MySqlSystem.fertilizerPoint += 50;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.fertilizerPoint));
                PanelImageChange("Fertilizer");
                RewardText.text = "50";
                StartCoroutine(RewardPanelOn(4, 3));
            }
            else if (randomSilverBoxPoint <= Balance.instance.chestnut_7S)//广5000
            {
                MySqlSystem.chestnutPoint += 5000;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint));
                PanelImageChange("Chestnut");
                RewardText.text = "5000";
                StartCoroutine(RewardPanelOn(4, 3));
            }
            else if (randomSilverBoxPoint <= Balance.instance.chestnut_8S)//广7500
            {
                MySqlSystem.chestnutPoint += 7500;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint));
                PanelImageChange("Chestnut");
                RewardText.text = "7500";
                StartCoroutine(RewardPanelOn(4, 0));
            }
            else if (randomSilverBoxPoint <= Balance.instance.fertilizer_3S)//厚丰 200
            {
                MySqlSystem.fertilizerPoint += 200;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.fertilizerPoint));
                PanelImageChange("Fertilizer");
                RewardText.text = "200";
                StartCoroutine(RewardPanelOn(4, 1));
            }
            else if (randomSilverBoxPoint <= Balance.instance.chestnut_9S)//广10000
            {
                MySqlSystem.chestnutPoint += 10000;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint));
                PanelImageChange("Chestnut");
                RewardText.text = "10000";
                StartCoroutine(RewardPanelOn(4, 0));
            }
        }
        else
        {
            NotEnoughChestnutPanel.gameObject.SetActive(true);
        }
    }
    /// <summary>
    /// GoldenBoxPersentage
    /// </summary>
    public void GoldenBoxOpen()
    {

        if (ItemManager.goldKeyCount > 0)
        {
            BlackPanel.gameObject.SetActive(true);
            GoldBoxPanel.gameObject.SetActive(true);
            ItemManager.goldKeyCount -= 1;
            StartCoroutine(ItemManager.instance.SetIGoldKey());
            randomGoldenBoxPoint = Random.Range(1, 101);

            if (randomGoldenBoxPoint <= Balance.instance.chestnut_1)//广 500
            {
                MySqlSystem.chestnutPoint += 500;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint));
                PanelImageChange("Chestnut");
                RewardText.text = "500";
                StartCoroutine(RewardPanelOn(4, 3));

            }
            else if (randomGoldenBoxPoint <= Balance.instance.fertilizer_1)//厚丰 20
            {
                MySqlSystem.fertilizerPoint += 10;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.fertilizerPoint));
                PanelImageChange("Fertilizer");
                RewardText.text = "10";
                StartCoroutine(RewardPanelOn(4, 3));
            }
            else if (randomGoldenBoxPoint <= Balance.instance.chestnut_2)//广 1000
            {
                MySqlSystem.chestnutPoint += 1000;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint));
                PanelImageChange("Chestnut");
                RewardText.text = "1000";
                StartCoroutine(RewardPanelOn(4, 3));
            }
            else if (randomGoldenBoxPoint <= Balance.instance.fertilizer_2)//厚丰 50
            {
                MySqlSystem.fertilizerPoint += 50;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.fertilizerPoint));
                PanelImageChange("Fertilizer");
                RewardText.text = "50";
                StartCoroutine(RewardPanelOn(4, 3));
            }
            else if (randomGoldenBoxPoint <= Balance.instance.chestnut_3)//广 2000
            {
                MySqlSystem.chestnutPoint += 2000;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint));
                PanelImageChange("Chestnut");
                RewardText.text = "2000";
                StartCoroutine(RewardPanelOn(4, 3));
            }
            else if (randomGoldenBoxPoint <= Balance.instance.fertilizer_3)//厚丰 50
            {
                MySqlSystem.fertilizerPoint += 50;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.fertilizerPoint));
                PanelImageChange("Fertilizer");
                RewardText.text = "50";
                StartCoroutine(RewardPanelOn(4, 3));
            }
            else if (randomGoldenBoxPoint <= Balance.instance.chestnut_4)//广 3000
            {
                MySqlSystem.chestnutPoint += 3000;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint));
                PanelImageChange("Chestnut");
                RewardText.text = "3000";
                StartCoroutine(RewardPanelOn(4, 0));
            }
            else if (randomGoldenBoxPoint <= Balance.instance.fertilizer_4)//厚丰 100
            {
                MySqlSystem.fertilizerPoint += 100;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.fertilizerPoint));
                PanelImageChange("Fertilizer");
                RewardText.text = "100";
                StartCoroutine(RewardPanelOn(4, 1));
            }
            else if (randomGoldenBoxPoint <= Balance.instance.chestnut_5)//广 5000
            {
                MySqlSystem.chestnutPoint += 5000;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint));
                PanelImageChange("Chestnut");
                RewardText.text = "5000";
                StartCoroutine(RewardPanelOn(4, 0));
            }
            else if (randomGoldenBoxPoint <= Balance.instance.fertilizer_5)//厚丰 500
            {
                MySqlSystem.fertilizerPoint += 500;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.fertilizerPoint));
                PanelImageChange("Fertilizer");
                RewardText.text = "500";
                StartCoroutine(RewardPanelOn(4, 1));
            }
        }
        else
        {
            NotEnoughGoldKeyPanel.gameObject.SetActive(true);
        }
    }
    public void NotEnoughChestnutPanelOff()
    {
        NotEnoughChestnutPanel.gameObject.SetActive(false);
    }
    public void NotEnoughGoldKeyPanelOff()
    {
        NotEnoughGoldKeyPanel.gameObject.SetActive(false);
    }
    /// <summary>
    /// Chestnut,Fertilizer,Fake image
    /// </summary>
    /// <param name="type"></param>
    void PanelImageChange(string type)
    {
        switch (type)
        {
            case "Chestnut":
                RewardChestnutImage.gameObject.SetActive(true);
                RewardfertilizerImage.gameObject.SetActive(false);
                RewardFakeImage.gameObject.SetActive(false);
                Debug.Log(type);
                break;
            case "Fertilizer":
                RewardChestnutImage.gameObject.SetActive(false);
                RewardfertilizerImage.gameObject.SetActive(true);
                RewardFakeImage.gameObject.SetActive(false);
                Debug.Log(type);
                break;
            case "Fake":
                RewardChestnutImage.gameObject.SetActive(false);
                RewardfertilizerImage.gameObject.SetActive(false);
                RewardFakeImage.gameObject.SetActive(true);
                Debug.Log(type);
                break;
        }
    }

    IEnumerator RewardPanelOn(int rewardSound,int num)
    {
        audioMixer.SetFloat("BGM", -40);
        yield return new WaitForSeconds(1);
        SoundManager.instance.PlaySFX(clip, 1, 1, 1);
        yield return new WaitForSeconds(2);
        SoundManager.instance.PlaySFX(clip, 2, 1, 1);
        yield return new WaitForSeconds(1f);
        SoundManager.instance.PlaySFX(clip, rewardSound, 1, 1);
        GoldBoxPanel.gameObject.SetActive(false);
        SilverBoxPanel.gameObject.SetActive(false);
        RewardPanel.gameObject.SetActive(true);
        if(num == 0)
        {
            chestnutEffect.gameObject.SetActive(true);
            yield return new WaitForSeconds(1);
            chestnutEffect.gameObject.SetActive(false);
        }
        else if (num == 1)
        {
            fertilizerEffect.gameObject.SetActive(true);
            yield return new WaitForSeconds(1);
            fertilizerEffect.gameObject.SetActive(false);
        }
        else
        {
            chestnutEffect.gameObject.SetActive(false);
            fertilizerEffect.gameObject.SetActive(false);
        }
    }

    public void RewardPanelOff()
    {
        audioMixer.SetFloat("BGM", 0);
        RewardPanel.gameObject.SetActive(false);
        BlackPanel.gameObject.SetActive(false);
    }
}


