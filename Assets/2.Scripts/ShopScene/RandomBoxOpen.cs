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
            if (randomSilverBoxPoint <= Balance.instance.chestnut_1S)//��
            {
                PanelImageChange("Fake");
                RewardText.text = "NoLuck";
                StartCoroutine(RewardPanelOn(3, 3));
            }
            else if (randomSilverBoxPoint <= Balance.instance.chestnut_2S)//��500
            {
                MySqlSystem.chestnutPoint += 500;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint));
                PanelImageChange("Chestnut");
                RewardText.text = "500";
                StartCoroutine(RewardPanelOn(4, 3));
            }
            else if (randomSilverBoxPoint <= Balance.instance.fertilizer_1S)//���20
            {
                MySqlSystem.fertilizerPoint += 20;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.fertilizerPoint));
                PanelImageChange("Fertilizer");
                RewardText.text = "20";
                StartCoroutine(RewardPanelOn(4, 3));
            }
            else if (randomSilverBoxPoint <= Balance.instance.chestnut_3S)//��750
            {
                MySqlSystem.chestnutPoint += 750;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint));
                PanelImageChange("Chestnut");
                RewardText.text = "750";
                StartCoroutine(RewardPanelOn(4, 3));
            }
            else if (randomSilverBoxPoint <= Balance.instance.chestnut_4S)//��1000
            {
                MySqlSystem.chestnutPoint += 1000;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint));
                PanelImageChange("Chestnut");
                RewardText.text = "1000";
                StartCoroutine(RewardPanelOn(4, 3));
            }
            else if (randomSilverBoxPoint <= Balance.instance.chestnut_5S)//��1500
            {
                MySqlSystem.chestnutPoint += 1500;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint));
                PanelImageChange("Chestnut");
                RewardText.text = "1500";
                StartCoroutine(RewardPanelOn(4, 3));
            }
            else if (randomSilverBoxPoint <= Balance.instance.chestnut_6S)//��2500
            {
                MySqlSystem.chestnutPoint += 2500;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint));
                PanelImageChange("Chestnut");
                RewardText.text = "2500";
                StartCoroutine(RewardPanelOn(4, 3));
            }
            else if (randomSilverBoxPoint <= Balance.instance.fertilizer_2S)//��� 50
            {
                MySqlSystem.fertilizerPoint += 50;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.fertilizerPoint));
                PanelImageChange("Fertilizer");
                RewardText.text = "50";
                StartCoroutine(RewardPanelOn(4, 3));
            }
            else if (randomSilverBoxPoint <= Balance.instance.chestnut_7S)//��5000
            {
                MySqlSystem.chestnutPoint += 5000;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint));
                PanelImageChange("Chestnut");
                RewardText.text = "5000";
                StartCoroutine(RewardPanelOn(4, 3));
            }
            else if (randomSilverBoxPoint <= Balance.instance.chestnut_8S)//��7500
            {
                MySqlSystem.chestnutPoint += 7500;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint));
                PanelImageChange("Chestnut");
                RewardText.text = "7500";
                StartCoroutine(RewardPanelOn(4, 0));
            }
            else if (randomSilverBoxPoint <= Balance.instance.fertilizer_3S)//��� 200
            {
                MySqlSystem.fertilizerPoint += 200;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.fertilizerPoint));
                PanelImageChange("Fertilizer");
                RewardText.text = "200";
                StartCoroutine(RewardPanelOn(4, 1));
            }
            else if (randomSilverBoxPoint <= Balance.instance.chestnut_9S)//��10000
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

            if (randomGoldenBoxPoint <= Balance.instance.chestnut_1)//�� 500
            {
                MySqlSystem.chestnutPoint += 500;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint));
                PanelImageChange("Chestnut");
                RewardText.text = "500";
                StartCoroutine(RewardPanelOn(4, 3));

            }
            else if (randomGoldenBoxPoint <= Balance.instance.fertilizer_1)//��� 20
            {
                MySqlSystem.fertilizerPoint += 10;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.fertilizerPoint));
                PanelImageChange("Fertilizer");
                RewardText.text = "10";
                StartCoroutine(RewardPanelOn(4, 3));
            }
            else if (randomGoldenBoxPoint <= Balance.instance.chestnut_2)//�� 1000
            {
                MySqlSystem.chestnutPoint += 1000;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint));
                PanelImageChange("Chestnut");
                RewardText.text = "1000";
                StartCoroutine(RewardPanelOn(4, 3));
            }
            else if (randomGoldenBoxPoint <= Balance.instance.fertilizer_2)//��� 50
            {
                MySqlSystem.fertilizerPoint += 50;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.fertilizerPoint));
                PanelImageChange("Fertilizer");
                RewardText.text = "50";
                StartCoroutine(RewardPanelOn(4, 3));
            }
            else if (randomGoldenBoxPoint <= Balance.instance.chestnut_3)//�� 2000
            {
                MySqlSystem.chestnutPoint += 2000;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint));
                PanelImageChange("Chestnut");
                RewardText.text = "2000";
                StartCoroutine(RewardPanelOn(4, 3));
            }
            else if (randomGoldenBoxPoint <= Balance.instance.fertilizer_3)//��� 50
            {
                MySqlSystem.fertilizerPoint += 50;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.fertilizerPoint));
                PanelImageChange("Fertilizer");
                RewardText.text = "50";
                StartCoroutine(RewardPanelOn(4, 3));
            }
            else if (randomGoldenBoxPoint <= Balance.instance.chestnut_4)//�� 3000
            {
                MySqlSystem.chestnutPoint += 3000;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint));
                PanelImageChange("Chestnut");
                RewardText.text = "3000";
                StartCoroutine(RewardPanelOn(4, 0));
            }
            else if (randomGoldenBoxPoint <= Balance.instance.fertilizer_4)//��� 100
            {
                MySqlSystem.fertilizerPoint += 100;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.fertilizerPoint));
                PanelImageChange("Fertilizer");
                RewardText.text = "100";
                StartCoroutine(RewardPanelOn(4, 1));
            }
            else if (randomGoldenBoxPoint <= Balance.instance.chestnut_5)//�� 5000
            {
                MySqlSystem.chestnutPoint += 5000;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint));
                PanelImageChange("Chestnut");
                RewardText.text = "5000";
                StartCoroutine(RewardPanelOn(4, 0));
            }
            else if (randomGoldenBoxPoint <= Balance.instance.fertilizer_5)//��� 500
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


