using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ShopBuyButton : MonoBehaviour
{
    [Header("Canvas")]
    Transform MiddleCanvas;
    [SerializeField] Transform highCanvas;
    [SerializeField] Transform moreHighCanvas;
    [Header("Text")]
    [SerializeField] Text NetStickLevelText;
    [SerializeField] Text NetStickLevelUpText;
    [SerializeField] Text NetStickDMGText;
    [SerializeField] Text NetStickDMGUpText;
    [SerializeField] Text NetStickPriceText;
    [SerializeField] Text SprayLevelText;
    [SerializeField] Text SprayLevelUpText;
    [SerializeField] Text SprayDMGText;
    [SerializeField] Text SprayDMGUpText;
    [SerializeField] Text SprayPriceText;
    [SerializeField] Image NetStickDMGImage;
    [SerializeField] Image NetStickDMGUpImage;
    [SerializeField] Sprite[] BoomRangeImages;

    [Header("Panel")]
    [SerializeField] Transform NotEnoughChestnutPanel;
    [SerializeField] Transform NotEnoughGoldKeyPanel;
    [SerializeField] Transform NotDevelopPanel;

    [Header("Price")]
    private int sprayPrice;
    private int netStickPrice;

    private void Awake()
    {
        MiddleCanvas =          GameObject.FindGameObjectWithTag("MiddleCanvas").GetComponent<Transform>();
        highCanvas =            GameObject.FindGameObjectWithTag("HighCanvas").GetComponent<Transform>();

        NetStickLevelText =     MiddleCanvas.transform.GetChild(1).transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).transform.GetChild(3).GetComponent<Text>();
        NetStickLevelUpText =   MiddleCanvas.transform.GetChild(1).transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).transform.GetChild(4).GetComponent<Text>();
        NetStickDMGText =       MiddleCanvas.transform.GetChild(1).transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).transform.GetChild(5).GetComponent<Text>();
        NetStickDMGUpText =     MiddleCanvas.transform.GetChild(1).transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).transform.GetChild(6).GetComponent<Text>();
        NetStickDMGImage =       MiddleCanvas.transform.GetChild(1).transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).transform.GetChild(5).GetChild(0).GetComponent<Image>();
        NetStickDMGUpImage =     MiddleCanvas.transform.GetChild(1).transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).transform.GetChild(6).GetChild(0).GetComponent<Image>();
        NetStickPriceText =     MiddleCanvas.transform.GetChild(1).transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
        SprayLevelText =        MiddleCanvas.transform.GetChild(1).transform.GetChild(1).transform.GetChild(1).transform.GetChild(1).transform.GetChild(3).GetComponent<Text>();
        SprayLevelUpText =      MiddleCanvas.transform.GetChild(1).transform.GetChild(1).transform.GetChild(1).transform.GetChild(1).transform.GetChild(4).GetComponent<Text>();
        SprayDMGText =          MiddleCanvas.transform.GetChild(1).transform.GetChild(1).transform.GetChild(1).transform.GetChild(1).transform.GetChild(5).GetComponent<Text>();
        SprayDMGUpText =        MiddleCanvas.transform.GetChild(1).transform.GetChild(1).transform.GetChild(1).transform.GetChild(1).transform.GetChild(6).GetComponent<Text>();
        SprayPriceText =        MiddleCanvas.transform.GetChild(1).transform.GetChild(1).transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
        BoomRangeImages =       Resources.LoadAll<Sprite>("BoomRange");

        NotEnoughChestnutPanel =    highCanvas.transform.GetChild(1).GetComponent<Transform>();
        NotEnoughGoldKeyPanel =     highCanvas.transform.GetChild(2).GetComponent<Transform>();
        NotDevelopPanel =           highCanvas.transform.GetChild(3).GetComponent<Transform>();

    }
    private void Start()
    {
         SprayWeaponDmg();
        DagonflyStickWeaponDmg();
    }
    /// <summary>
    /// 스프레이 데미지 업데이트
    /// </summary>
    void SprayWeaponDmg()
    {
        if (MySqlSystem.sprayLevelPoint == 0)
        {
            SprayDMGText.text = "2";
            SprayDMGUpText.text = "3";
            SprayPriceText.text = "1,000";
            SprayLevelText.text = MySqlSystem.sprayLevelPoint.ToString();
            SprayLevelUpText.text = (MySqlSystem.sprayLevelPoint + 1).ToString();
            sprayPrice = 1000;
        }
        if (MySqlSystem.sprayLevelPoint == 1)
        {
            SprayDMGText.text = "3";
            SprayDMGUpText.text = "7";
            SprayPriceText.text = "5,000";
            SprayLevelText.text = MySqlSystem.sprayLevelPoint.ToString();
            SprayLevelUpText.text = (MySqlSystem.sprayLevelPoint + 1).ToString();
            sprayPrice = 5000;
        }
        if (MySqlSystem.sprayLevelPoint == 2)
        {
            SprayDMGText.text = "4";
            SprayDMGUpText.text = "7";
            SprayPriceText.text = "10,000";
            SprayLevelText.text = MySqlSystem.sprayLevelPoint.ToString();
            SprayLevelUpText.text = (MySqlSystem.sprayLevelPoint + 1).ToString();
            sprayPrice = 10000;
        }
        if (MySqlSystem.sprayLevelPoint == 3)
        {
            SprayDMGText.text = "7";
            SprayDMGUpText.text = "10";
            SprayPriceText.text = "20,000";
            SprayLevelText.text = MySqlSystem.sprayLevelPoint.ToString();
            SprayLevelUpText.text = (MySqlSystem.sprayLevelPoint + 1).ToString();
            sprayPrice = 20000;
        }
        if (MySqlSystem.sprayLevelPoint == 4)
        {
            SprayDMGText.text = "10";
            SprayDMGUpText.text = "20";
            SprayPriceText.text = "50,000";
            SprayLevelText.text = MySqlSystem.sprayLevelPoint.ToString();
            SprayLevelUpText.text = (MySqlSystem.sprayLevelPoint + 1).ToString();
            sprayPrice = 50000;
        }
        if (MySqlSystem.sprayLevelPoint == 5)
        {
            SprayDMGText.text = "20";
            SprayDMGUpText.text = "Max";
            SprayPriceText.text = "Max";
            SprayLevelText.text = MySqlSystem.sprayLevelPoint.ToString();
            SprayLevelUpText.text = "Max";
        }

    }
    /// <summary>
    /// 잠자리채 실시간 업데이트
    /// </summary>
    void DagonflyStickWeaponDmg()
    {
        if (MySqlSystem.dragonflyStickLevelPoint == 0)
        {
            NetStickDMGText.text = "15";
            NetStickDMGUpText.text = "18";
            NetStickDMGImage.sprite = BoomRangeImages[MySqlSystem.dragonflyStickLevelPoint];
            NetStickDMGUpImage.sprite = BoomRangeImages[MySqlSystem.dragonflyStickLevelPoint+1];
            NetStickPriceText.text = "1,000";
            NetStickLevelText.text = MySqlSystem.dragonflyStickLevelPoint.ToString();
            NetStickLevelUpText.text = (MySqlSystem.dragonflyStickLevelPoint + 1).ToString();
            netStickPrice = 1000;


        }
        if (MySqlSystem.dragonflyStickLevelPoint == 1)
        {
            NetStickDMGText.text = "18";
            NetStickDMGUpText.text = "22";
            NetStickDMGImage.sprite = BoomRangeImages[MySqlSystem.dragonflyStickLevelPoint];
            NetStickDMGUpImage.sprite = BoomRangeImages[MySqlSystem.dragonflyStickLevelPoint + 1];
            NetStickPriceText.text = "5,000";
            NetStickLevelText.text = MySqlSystem.dragonflyStickLevelPoint.ToString();
            NetStickLevelUpText.text = (MySqlSystem.dragonflyStickLevelPoint + 1).ToString();
            netStickPrice = 5000;
        }
        if (MySqlSystem.dragonflyStickLevelPoint == 2)
        {
            NetStickDMGText.text = "22";
            NetStickDMGUpText.text = "33";
            NetStickDMGImage.sprite = BoomRangeImages[MySqlSystem.dragonflyStickLevelPoint];
            NetStickDMGUpImage.sprite = BoomRangeImages[MySqlSystem.dragonflyStickLevelPoint + 1];
            NetStickPriceText.text = "10,000";
            NetStickLevelText.text = MySqlSystem.dragonflyStickLevelPoint.ToString();
            NetStickLevelUpText.text = (MySqlSystem.dragonflyStickLevelPoint + 1).ToString();
            netStickPrice = 10000;
        }
        if (MySqlSystem.dragonflyStickLevelPoint == 3)
        {
            NetStickDMGText.text = "33";
            NetStickDMGUpText.text = "44";
            NetStickDMGImage.sprite = BoomRangeImages[MySqlSystem.dragonflyStickLevelPoint];
            NetStickDMGUpImage.sprite = BoomRangeImages[MySqlSystem.dragonflyStickLevelPoint + 1];
            NetStickPriceText.text = "20,000";
            NetStickLevelText.text = MySqlSystem.dragonflyStickLevelPoint.ToString();
            NetStickLevelUpText.text = (MySqlSystem.dragonflyStickLevelPoint + 1).ToString();
            netStickPrice = 20000;
        }
        if (MySqlSystem.dragonflyStickLevelPoint == 4)
        {
            NetStickDMGText.text = "44";
            NetStickDMGUpText.text = "55";
            NetStickDMGImage.sprite = BoomRangeImages[MySqlSystem.dragonflyStickLevelPoint];
            NetStickDMGUpImage.sprite = BoomRangeImages[MySqlSystem.dragonflyStickLevelPoint + 1];
            NetStickPriceText.text = "50,000";
            NetStickLevelText.text = MySqlSystem.dragonflyStickLevelPoint.ToString();
            NetStickLevelUpText.text = (MySqlSystem.dragonflyStickLevelPoint + 1).ToString();
            netStickPrice = 50000;
        }
        if (MySqlSystem.dragonflyStickLevelPoint == 5)
        {
            NetStickDMGText.text = "55";
            NetStickDMGImage.sprite = BoomRangeImages[MySqlSystem.dragonflyStickLevelPoint];
            NetStickDMGUpImage.sprite = BoomRangeImages[MySqlSystem.dragonflyStickLevelPoint];
            NetStickDMGUpText.text = "Max";
            NetStickPriceText.text = "Max";
            NetStickLevelText.text = MySqlSystem.dragonflyStickLevelPoint.ToString();
            NetStickLevelUpText.text = "Max";
        }
    }
    public void ShopBuyItemFertilizerButton()
    {
        if(MySqlSystem.chestnutPoint >= 100)
        {
            MySqlSystem.chestnutPoint -= 100;
            StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint));
            MySqlSystem.fertilizerPoint += 1;
            StartCoroutine(MySqlSystem.instance.SetFertilizer(MySqlSystem.fertilizerPoint));
        }
        else
        {
            NotEnoughChestnutPanel.gameObject.SetActive(true);
        }
    }
    public void ShopBuyItemStateChangeButton()
    {
        if (MySqlSystem.chestnutPoint >= 10000)
        {
            MySqlSystem.chestnutPoint -= 10000;
            StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint));
            ItemManager.statusPotionCount++;
            StartCoroutine(ItemManager.instance.SetIStatusResetPotion());
        }
        else
        {
            NotEnoughChestnutPanel.gameObject.SetActive(true);
        }
    }
    public void ShopBuyItemHeartButton()
    {
        if(MySqlSystem.chestnutPoint >= 5000)
        {
            MySqlSystem.chestnutPoint -= 5000;
            StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint));
            ItemManager.heartPotionCount++;
            StartCoroutine(ItemManager.instance.SetIHeartPotion());
        }
        else
        {
            NotEnoughChestnutPanel.gameObject.SetActive(true);
        }
    }
    public void ShopUpgradeDragonflyStickButton()
    {
        if (MySqlSystem.dragonflyStickLevelPoint <= 4) 
        { 
            if (netStickPrice > MySqlSystem.chestnutPoint)
            {
                NotEnoughChestnutPanel.gameObject.SetActive(true);
            }
            else
            {
                MySqlSystem.chestnutPoint -= netStickPrice;
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint));
                MySqlSystem.dragonflyStickLevelPoint += 1;
                StartCoroutine(MySqlSystem.instance.SetDragonflyStickLevel(MySqlSystem.dragonflyStickLevelPoint));
            }
        }
        DagonflyStickWeaponDmg();
    }
    public void ShopUpgradeSprayStickButton()
    {
        if(MySqlSystem.sprayLevelPoint <= 4)
        {
            if (sprayPrice > MySqlSystem.chestnutPoint)
            {
                NotEnoughChestnutPanel.gameObject.SetActive(true);
            }
            else
            {
                MySqlSystem.sprayLevelPoint++;
                MySqlSystem.chestnutPoint -= sprayPrice;
                StartCoroutine(MySqlSystem.instance.SetSprayLevel(MySqlSystem.sprayLevelPoint));
                StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint));
            }
            SprayWeaponDmg();
        }
    }
}
