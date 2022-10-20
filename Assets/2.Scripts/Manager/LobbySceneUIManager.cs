using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Windows;

public class LobbySceneUIManager : MonoBehaviour
{
    //-----------ScorePanel----------------------------------
    [Header("ScorePanel")]
    Text                                    heartText;
    Text                                    chestnutText;
    Text                                    fertilizerText;
    Text                                    farmName;
    //-----------MenuPanel----------------------------------
    [Header("MenuPanel")]
    Transform                               ButtonPanel;
    [SerializeField]Transform               BlackPanel;
    [SerializeField]Text                    BlackPanelText;
    //-----------MyPagePanel----------------------------------
    [Header("MyPagePanel")]
    Transform                               MyPagePanel;
    Text                                    chestnutMypageText;
    //-----------InventoryPanel----------------------------------
    [Header("InventoryPanel")]
    Transform                               InventoryPanel;
    Image                                   inventoryTreeButton;
    Image                                   inventoryEquipmentButton;
    Image                                   inventoryDocumentButton;
    Transform                               treeInventoryPanel;
    Transform                               equipmentInventoryPanel;
    Transform                               documentInventoryPanel;
    //-----------TreeInfoPanel----------------------------------
    [Header("TreeInfoPanel")]
    Transform                               TreeInfoPanel;
    Text                                    FarmName;
    Transform                               InfoContent;
    GameObject                              infoSlot;
    //-----------TreeInformation----------------------------------
    [Header("TreeInformation")]
    Transform                               TreeInformation;
    public static Text                      treeName;
    public static Text                      treePosition;
    public static Text                      treeLevel;
    public static Text                      treeStatusText;
    public static Text                      treeStatusText1;
    public static Text                      treeLevelUpPoint;
    public static SpriteRenderer            treeImage;
    public static Image[]                   statusImage;
    //-----------SettingPanel----------------------------------
    [Header("SettingPanel")]
    Transform                               SettingPanel;
    Button                                  bgmButtonOn;
    Button                                  bgmButtonOff;
    Button                                  effectButtonOn;
    Button                                  effectButtonOff;
    private Slider                          BGMS;
    private Slider                          EffectS;
    public static float                     saveBGMVolumn;
    public static float                     saveEffectVolumn;
    //-----------SetTreePanel----------------------------------
    [Header("SetTreePanel")]
    Transform                               SetTreeNoticPanel;
    Transform                               SetPosButtonPanel;
    Transform                               SetInventoryPanel;
    //-----------ShopPanel----------------------------------
    [Header("ShopPanel")]
    Transform                               ShopPanel;
    [Header("UpDate")]
    Text                                    energyTime;
    Text                                    dragonflystickPay;
    Text                                    spayPay;
    Text                                    dragonflystickLevelShop;
    Text                                    spayLevelShop;
    [SerializeField]Text                                    dragonflystickLevel;
    Image                                   dragonflyStickImage;
    [SerializeField]Text                                    spayLevel;
    [SerializeField]Text                                    dragonflystickDmg;
    [SerializeField] Text                                    spayDmg;
    Text                                    spayShopDmgText;
    Text                                    dragonflystickShopDmgText;
    [Header("Notice")]
    Transform                               noticPanel;
    Transform                               notEnoughMoneyPanel;
    Transform                               GameStartPanel;
    Transform                               NotEnoughFertilizer;
    Transform                               NotEnoughEnergy;
    [Header("GameObject")]
    [SerializeField]GameObject[]            treeObject;
    [SerializeField]GameObject[]            Slot;
    [Header("Sprite")]
    Sprite                                  TreeOn;
    Sprite                                  TreeOff;
    Sprite                                  EquipmentOn;
    Sprite                                  EquipmentOff;
    Sprite                                  ItemOn;
    Sprite                                  ItemOff;
    [Header("Weapon")]
    int                                     sprayDMG;
    int                                     dragonDMG;
    int                                     sprayPay;
    int                                     dragonPay;
    string                                  sprayShopDMG;
    string                                  dragonShopDMG;
    [Header("TreeInfo")]
    public static string                    nft_id;
    [Header("Misc")]
    [SerializeField] AudioMixer             audioMixer;
    [Header("Manager")]
    MySqlSystem mysql;
    [Header("TreeStatusPersentage")]
    [SerializeField]Transform treeStatusPersentage;
    TutorialCheck tutorialCheck;
    Sprite[] BoomRange;
    public AutoLogin autoLogin = new AutoLogin();
    public static LobbySceneUIManager instance;
    private void Awake()
    {
        instance = this;
        BoomRange = Resources.LoadAll<Sprite>("BoomRange");
        tutorialCheck = GameObject.FindGameObjectWithTag("Main").GetComponent<TutorialCheck>();
        //-----------ScorePanel----------------------------------
        heartText =                 GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
        chestnutText =              GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).GetComponent<Text>();
        fertilizerText =            GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(0).transform.GetChild(1).transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();

        //-----------MenuPanel-----------------------------------
        ButtonPanel =               GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(1).transform.GetChild(0).GetComponent<Transform>();
        BlackPanel =                GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).GetComponent<Transform>();
        BlackPanelText =            GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
        //-----------MyPagePanel----------------------------------
        MyPagePanel =               GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(2).transform.GetChild(0).GetComponent<Transform>();
        chestnutMypageText =        GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(2).transform.GetChild(0).transform.GetChild(1).GetComponent<Text>();
        //-----------InventoryPanel----------------------------------
        InventoryPanel =            GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(3).transform.GetChild(0).GetComponent<Transform>();
        inventoryTreeButton =       GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(3).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        inventoryEquipmentButton =  GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(3).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<Image>();
        inventoryDocumentButton =   GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(3).transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).GetComponent<Image>();
        treeInventoryPanel =        GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(3).transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<Transform>();
        equipmentInventoryPanel =   GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(3).transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).GetComponent<Transform>();
        documentInventoryPanel =    GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(3).transform.GetChild(0).transform.GetChild(1).transform.GetChild(2).GetComponent<Transform>();
        //-----------TreeInfoPanel----------------------------------
        TreeInfoPanel =             GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(4).transform.GetChild(0).GetComponent<Transform>();
        FarmName =                  GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(4).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<Text>();
        InfoContent =               GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(4).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Transform>();
        infoSlot =                  Resources.Load<GameObject>("Inventory/ManagementSlot");
        //-----------TreeInformation----------------------------------
        TreeInformation =           GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(5).transform.GetChild(0).GetComponent<Transform>();
        treeName =                  GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(5).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
        treePosition =              GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(5).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<Text>();
        treeLevel =                 GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(5).transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();
        treeStatusText =            GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(5).transform.GetChild(0).transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).GetComponent<Text>();
        treeStatusText1 =           GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(5).transform.GetChild(0).transform.GetChild(0).transform.GetChild(3).transform.GetChild(1).GetComponent<Text>();
        treeLevelUpPoint =          GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(5).transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
        statusImage =               GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(5).transform.GetChild(0).transform.GetChild(0).transform.GetChild(3).transform.GetChild(2).GetComponentsInChildren<Image>();
        treeImage =                 GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(5).transform.GetChild(0).transform.GetChild(0).transform.GetChild(4).GetComponent<SpriteRenderer>();
        //-----------SettingPanel----------------------------------
        SettingPanel =              GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(7).transform.GetChild(0).GetComponent<Transform>();
        bgmButtonOn =               GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(7).transform.GetChild(0).transform.GetChild(0).transform.GetChild(4).GetComponent<Button>();
        bgmButtonOff =              GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(7).transform.GetChild(0).transform.GetChild(0).transform.GetChild(3).GetComponent<Button>();
        effectButtonOn =            GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(7).transform.GetChild(0).transform.GetChild(1).transform.GetChild(4).GetComponent<Button>();
        effectButtonOff =           GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(7).transform.GetChild(0).transform.GetChild(1).transform.GetChild(3).GetComponent<Button>();
        BGMS =                      GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(7).transform.GetChild(0).transform.GetChild(0).GetComponent<Slider>();
        EffectS =                   GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(7).transform.GetChild(0).transform.GetChild(1).GetComponent<Slider>();
        //-----------SetTreePanel----------------------------------
        SetTreeNoticPanel =         GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(8).transform.GetChild(0).GetComponent<Transform>();
        SetPosButtonPanel =         GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(9).transform.GetChild(0).GetComponent<Transform>();
        SetInventoryPanel =         GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(10).transform.GetChild(0).GetComponent<Transform>();
        //-----------ShopPanel--------------------------------------
        ShopPanel =                 GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(6).transform.GetChild(0).GetComponent<Transform>();
        //-----------Update-----------------------------------------
        farmName =                  GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(0).transform.GetChild(1).transform.GetChild(3).transform.GetChild(0).GetComponent<Text>();
        energyTime =                GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).GetComponent<Text>();
        dragonflystickPay =         GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(6).transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();
        spayPay =                   GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(6).transform.GetChild(0).transform.GetChild(1).transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();
        dragonflystickLevelShop =   GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(6).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<Text>();
        spayLevelShop =             GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(6).transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<Text>();
        dragonflystickLevel =       GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(3).transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<Text>();
        spayLevel =                 GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(3).transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<Text>();
        dragonflystickDmg =         GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(3).transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).GetComponent<Text>();
        dragonflyStickImage =         GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(3).transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).transform.GetChild(2).GetComponent<Image>();  
        spayDmg =                   GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(3).transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).transform.GetChild(1).GetComponent<Text>();
        spayShopDmgText =           GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(6).transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).GetComponent<Text>();
        dragonflystickShopDmgText = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(6).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<Text>();
        //-----------Notice-----------------------------------------
        noticPanel =                GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(12).transform.GetChild(0).GetComponent<Transform>();
        notEnoughMoneyPanel =       GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(13).transform.GetChild(0).GetComponent<Transform>();
        GameStartPanel =            GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(14).transform.GetChild(0).GetComponent<Transform>();
        NotEnoughFertilizer =       GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(16).transform.GetChild(0).GetComponent<Transform>();
        NotEnoughEnergy =           GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(17).transform.GetChild(0).GetComponent<Transform>();
        //-----------Object-----------------------------------------
        treeObject =                GameObject.FindGameObjectsWithTag("Tree");
        Slot =                      GameObject.FindGameObjectsWithTag("ManagementSlot");
        treeInventoryPanel.gameObject.SetActive(true);
        equipmentInventoryPanel.gameObject.SetActive(false);
        documentInventoryPanel.gameObject.SetActive(false);
        //-----------Resources-----------------------------------------
        TreeOn =                    Resources.Load<Sprite>("Button/button inventory tree click");
        TreeOff =                   Resources.Load<Sprite>("Button/button inventory tree");
        EquipmentOn =               Resources.Load<Sprite>("Button/button inventory tool click");
        EquipmentOff =              Resources.Load<Sprite>("Button/button inventory tool");
        ItemOn =                Resources.Load<Sprite>("Button/KakaoTalk_20220518_152115533");
        ItemOff =               Resources.Load<Sprite>("Button/KakaoTalk_20220518_152115533_01");
        //----------Sprite---------------------------------------------
        inventoryTreeButton.sprite =        TreeOn;
        inventoryEquipmentButton.sprite =   EquipmentOff;
        inventoryDocumentButton.sprite =    ItemOff;
        //----------TreeStatusPersentage---------------------------------------------
        treeStatusPersentage = GameObject.FindGameObjectWithTag("HighCanvas").transform.GetChild(4).GetChild(0).GetComponent<Transform>();

        if (GameObject.Find("Main").GetComponent<MySqlSystem>() != null)
        {
            mysql = GameObject.Find("Main").GetComponent<MySqlSystem>();
        }

        TreeInfoCreater();
    }
    private void Update()
    {
        EnergyUpdate();
        ScoreUpdate();
        MusicOnOff();
    }
    /// <summary>
    /// 에너지 실시간 업데이트
    /// </summary>
    void EnergyUpdate()
    {
        if (MySqlSystem.energy >= 10)
        {
            energyTime.text = null;
        }
        else
        {
            energyTime.text = (MySqlSystem.minute + ":" + MySqlSystem.second.ToString("00"));
        }
    }
    /// <summary>
    /// 밤 에너지 비료 상점 및 인벤토리 무기공격력 레벨 가격 업데이트
    /// </summary>
    void ScoreUpdate()
    {
        heartText.text = MySqlSystem.energy.ToString("0")+" / 10";
        chestnutText.text = string.Format("{0:n0}", MySqlSystem.chestnutPoint);
        fertilizerText.text = string.Format("{0:n0}", MySqlSystem.fertilizerPoint);
        farmName.text = MySqlSystem.localID + "'s Chestnut Farm #1";
        chestnutMypageText.text = string.Format("{0:n0}", MySqlSystem.chestnutPoint);
        SprayWeaponDmg();
        DagonflyStickWeaponDmg();
        if(MySqlSystem.sprayLevelPoint < 5)
        {
            spayPay.text = string.Format("{0:n0}", sprayPay);
        }
        else if (MySqlSystem.sprayLevelPoint >= 5)
        {
            spayPay.text ="Sold Out";
        }
        if (MySqlSystem.dragonflyStickLevelPoint < 5)
        {
            dragonflystickPay.text = string.Format("{0:n0}", dragonPay);
        }
        else if (MySqlSystem.dragonflyStickLevelPoint >= 5)
        {
            dragonflystickPay.text = "Sold Out";
        }


        if (MySqlSystem.sprayLevelPoint < 5)
        {
            spayLevel.text = "Lv. " + MySqlSystem.sprayLevelPoint.ToString();
        }
        else if (MySqlSystem.sprayLevelPoint >= 5)
        {
            spayLevel.text = "Lv. Max";
        }
        if (MySqlSystem.dragonflyStickLevelPoint < 5)
        {
            dragonflystickLevel.text = "Lv. " + MySqlSystem.dragonflyStickLevelPoint.ToString();
        }
        else if (MySqlSystem.dragonflyStickLevelPoint >= 5)
        {
            dragonflystickLevel.text = "Lv. Max";
        }


        if (MySqlSystem.sprayLevelPoint < 5)
        {
            spayLevelShop.text = "Lv. " + MySqlSystem.sprayLevelPoint.ToString();
        }
        else if (MySqlSystem.sprayLevelPoint >= 5)
        {
            spayLevelShop.text = "Lv. Max";
        }
        if (MySqlSystem.dragonflyStickLevelPoint < 5)
        {
            dragonflystickLevelShop.text = "Lv. " + MySqlSystem.dragonflyStickLevelPoint.ToString();
        }
        else if (MySqlSystem.dragonflyStickLevelPoint >= 5)
        {
            dragonflystickLevelShop.text = "Lv. Max";
        }
        spayShopDmgText.text = "DMG:" + sprayShopDMG;
        dragonflystickShopDmgText.text = "DMG:" + dragonShopDMG;
        dragonflyStickImage.sprite = BoomRange[MySqlSystem.dragonflyStickLevelPoint];
        if(MySqlSystem.sprayLevelPoint != 5)
        {
            spayDmg.text = "Destroy " + MySqlSystem.sprayLevelPoint+1.ToString() + "Block";
        }
        else
        {
            spayDmg.text = "Destroy " + "8" + "Block";
        }
        
        dragonflystickDmg.text = "DMG: " + dragonDMG.ToString();
    }
    /// <summary>
    /// 스프레이 데미지 업데이트
    /// </summary>
    void SprayWeaponDmg()
    {
        if (MySqlSystem.sprayLevelPoint == 0)
        {
            sprayDMG = 2;
            sprayShopDMG = "2 -> 3";
            sprayPay = 1000;
        }
        if (MySqlSystem.sprayLevelPoint == 1)
        {
            sprayDMG = 3;
            sprayShopDMG = "3 -> 4";
            sprayPay = 5000;
        }
        if (MySqlSystem.sprayLevelPoint == 2)
        {
            sprayDMG = 4;
            sprayShopDMG = "4 -> 7";
            sprayPay = 10000;
        }
        if (MySqlSystem.sprayLevelPoint == 3)
        {
            sprayDMG = 7;
            sprayShopDMG = "7 -> 10";
            sprayPay = 20000;
        }
        if (MySqlSystem.sprayLevelPoint == 4)
        {
            sprayDMG = 10;
            sprayShopDMG = "10 -> 20";
            sprayPay = 30000;
        }
        if (MySqlSystem.sprayLevelPoint == 5)
        {
            sprayShopDMG = "20";
            sprayDMG = 20;
        }
    }
    /// <summary>
    /// 잠자리채 실시간 업데이트
    /// </summary>
    void DagonflyStickWeaponDmg()
    {
        if (MySqlSystem.dragonflyStickLevelPoint == 0)
        {
            dragonDMG = 15;
            dragonShopDMG = "15 -> 18";
            dragonPay = 1000;
        }
        if (MySqlSystem.dragonflyStickLevelPoint == 1)
        {
            dragonDMG = 18;
            dragonShopDMG = "18 -> 22";
            dragonPay = 5000;
        }
        if (MySqlSystem.dragonflyStickLevelPoint == 2)
        {
            dragonDMG = 22;
            dragonShopDMG = "22 -> 33";
            dragonPay = 10000;
        }
        if (MySqlSystem.dragonflyStickLevelPoint == 3)
        {
            dragonDMG = 33;
            dragonShopDMG = "33 -> 44";
            dragonPay = 20000;
        }
        if (MySqlSystem.dragonflyStickLevelPoint == 4)
        {
            dragonDMG = 44;
            dragonShopDMG = "44 -> 55";
            dragonPay = 50000;
        }
        if (MySqlSystem.dragonflyStickLevelPoint == 5)
        {
            dragonShopDMG = "55";
            dragonDMG = 55;
        }
    }
    /// <summary>
    /// 세팅 버튼 ON
    /// </summary>
    public void LobbySettingButton()
    {
        SettingPanel.gameObject.SetActive(true);
        MyPagePanel.gameObject.SetActive(false);
        InventoryPanel.gameObject.SetActive(false);
        TreeInfoPanel.gameObject.SetActive(false);
        ShopPanel.gameObject.SetActive(false);
        BlackPanelText.text = "Settings";
        BlackPanel.gameObject.SetActive(true);
    }
    /// <summary>
    /// 세팅 버튼 끄기
    /// </summary>
    public void LobbySettingQuitButton()
    {
        SettingPanel.gameObject.SetActive(false);
        BlackPanel.gameObject.SetActive(false);
    }
    /// <summary>
    /// 마이 페이지 ON
    /// </summary>
    public void MyPageButton()
    {
        SettingPanel.gameObject.SetActive(false);
        MyPagePanel.gameObject.SetActive(true);
        InventoryPanel.gameObject.SetActive(false);
        TreeInfoPanel.gameObject.SetActive(false);
        ShopPanel.gameObject.SetActive(false);
        BlackPanelText.text = "My Page";
        BlackPanel.gameObject.SetActive(true);
    }
    /// <summary>
    /// 마이 페이지 OFF
    /// </summary>
    public void MyPageQuitButton()
    {
        MyPagePanel.gameObject.SetActive(false);
        BlackPanel.gameObject.SetActive(false);
    }
    /// <summary>
    /// 인벤토리 판넬 ON
    /// </summary>
    public void InventoryPanelButton()
    {
        SettingPanel.gameObject.SetActive(false);
        MyPagePanel.gameObject.SetActive(false);
        InventoryPanel.gameObject.SetActive(true);
        TreeInfoPanel.gameObject.SetActive(false);
        ShopPanel.gameObject.SetActive(false);
        //BlackPanelText.text = "Inventory";
        BlackPanel.gameObject.SetActive(false);
    }
    /// <summary>
    /// 인벤토리 판넬 OFF
    /// </summary>
    public void InventoryPanelQuitButton()
    {
        InventoryPanel.gameObject.SetActive(false);
        BlackPanel.gameObject.SetActive(false);
    }
    /// <summary>
    /// 나무 정보창 ON
    /// </summary>
    public void TreeInfoPanelButton()
    {
        SettingPanel.gameObject.SetActive(false);
        MyPagePanel.gameObject.SetActive(false);
        InventoryPanel.gameObject.SetActive(false);
        TreeInfoPanel.gameObject.SetActive(true);
        ShopPanel.gameObject.SetActive(false);
        BlackPanelText.text = "Trees";
        BlackPanel.gameObject.SetActive(true);
    }
    /// <summary>
    /// 나무 정보창 OFF
    /// </summary>
    public void TreeInfoPanelQuitButton()
    {
        TreeInfoPanel.gameObject.SetActive(false);
        BlackPanel.gameObject.SetActive(false);
    }
    /// <summary>
    /// 상점창 ON
    /// </summary>
    public void ShopPanelButton()
    {
        Debug.Log("Work");
        SettingPanel.gameObject.SetActive(false);
        MyPagePanel.gameObject.SetActive(false);
        InventoryPanel.gameObject.SetActive(false);
        TreeInfoPanel.gameObject.SetActive(false);
        for (int i = 0; i < treeObject.Length; i++)
        {
            treeObject[i].transform.SetParent(GameObject.Find("Main").GetComponent<Transform>());
            treeObject[i].transform.position = GameObject.Find("Main").GetComponent<Transform>().position;
        }
        SceneManager.LoadScene("ShopScene");
    }
    /// <summary>
    /// 상점창 OFF
    /// </summary>
    public void ShopPanelOffButton()
    {
        ShopPanel.gameObject.SetActive(false);
        BlackPanel.gameObject.SetActive(false);
    }
    /// <summary>
    /// 나무 특성 및 정보창 ON
    /// </summary>
    public void TreeInformationButton()
    {
        TreeInformation.gameObject.SetActive(true);
        ButtonPanel.gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(10).transform.GetChild(0).GetComponent<Transform>().gameObject.SetActive(false);
    }
    /// <summary>
    /// 나무 특성 및 정보창 OFF
    /// </summary>
    public void TreeInformationQuitButton()
    {
        TreeInformation.gameObject.SetActive(false);
        ButtonPanel.gameObject.SetActive(true);
        BlackPanel.gameObject.SetActive(false);
    }
    /// <summary>
    /// 인벤토리 나무창 ON
    /// </summary>
    public void InventoryTreeButton()
    {
        inventoryTreeButton.sprite = TreeOn;
        inventoryEquipmentButton.sprite = EquipmentOff;
        inventoryDocumentButton.sprite = ItemOff;
        treeInventoryPanel.gameObject.SetActive(true);
        equipmentInventoryPanel.gameObject.SetActive(false);
        documentInventoryPanel.gameObject.SetActive(false);
    }
    /// <summary>
    /// 인벤토리 장비창 ON
    /// </summary>
    public void InventoryEquipmentButton()
    {
        inventoryTreeButton.sprite = TreeOff;
        inventoryEquipmentButton.sprite = EquipmentOn;
        inventoryDocumentButton.sprite = ItemOff;
        treeInventoryPanel.gameObject.SetActive(false);
        equipmentInventoryPanel.gameObject.SetActive(true);
        documentInventoryPanel.gameObject.SetActive(false);
    }
    /// <summary>
    /// 인벤토리 땅문서 창 ON
    /// </summary>
    public void InventoryDocumentButton()
    {
        inventoryTreeButton.sprite = TreeOff;
        inventoryEquipmentButton.sprite = EquipmentOff;
        inventoryDocumentButton.sprite = ItemOn;
        treeInventoryPanel.gameObject.SetActive(false);
        equipmentInventoryPanel.gameObject.SetActive(false);
        documentInventoryPanel.gameObject.SetActive(true);
    }
    /// <summary>
    /// 나무 심을지 확인 하는 판넬 OFF
    /// </summary>
    public void SetTreeOffButton()
    {
        SetTreeNoticPanel.gameObject.SetActive(false);
        InventoryPanel.gameObject.SetActive(true);
        ButtonPanel.gameObject.SetActive(true);
        BlackPanel.gameObject.SetActive(true);
        BlackPanelText.text = "Inventory";
    }
    /// <summary>
    /// 나무를 심기 
    /// </summary>
    public void SetTreeOKButton()
    {
        SetTreeNoticPanel.gameObject.SetActive(false);
        InventoryPanel.gameObject.SetActive(false);
        SetPosButtonPanel.gameObject.SetActive(true);
        ButtonPanel.gameObject.SetActive(false);
    }
    /// <summary>
    /// 나무 뽑기 저장
    /// </summary>
    public void SetInventoryPanelOffButton()
    {
        SetInventoryPanel.gameObject.SetActive(false);
        ButtonPanel.gameObject.SetActive(true);
    }
    /// <summary>
    /// 나무 정보 선택 버튼 생성
    /// </summary>
    public void TreeInfoCreater()
    {
        for (int i = 0; i < treeObject.Length; i++)
        {
            GameObject treeInfoSlot = Instantiate<GameObject>(infoSlot, transform);
            treeInfoSlot.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = treeObject[i].GetComponent<SpriteRenderer>().sprite;
            treeInfoSlot.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text ="TreeName:"+ treeObject[i].GetComponent<TreeStatus>().TreeName+"\n" + "Lv "+ treeObject[i].GetComponent<TreeStatus>().TreeLevel;
            treeInfoSlot.GetComponent<ManagementSlot>().managementSlotTreeName = treeObject[i].GetComponent<TreeStatus>().TreeName;
        }
    }
    /// <summary>
    /// 스프레이 레벨업 버튼
    /// </summary>
    public void SprayLevelUp()
    {
        if(MySqlSystem.chestnutPoint >= sprayPay &&MySqlSystem.sprayLevelPoint <5)
        {
            MySqlSystem.sprayLevelPoint++;
            MySqlSystem.chestnutPoint -= sprayPay;
            StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint));
            StartCoroutine(MySqlSystem.instance.SetSprayLevel(MySqlSystem.sprayLevelPoint));
        }
        if (MySqlSystem.chestnutPoint < sprayPay)
        {
            NotEnoughMoneyOn();
        }
    }
    //잠자리채 레벨업 버튼
    public void DraogonFlyLevelUp()
    {
        if (MySqlSystem.chestnutPoint >= dragonPay && MySqlSystem.dragonflyStickLevelPoint < 5)
        {
            MySqlSystem.dragonflyStickLevelPoint++;
            MySqlSystem.chestnutPoint -= dragonPay;
            StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint));
            StartCoroutine(MySqlSystem.instance.SetDragonflyStickLevel(MySqlSystem.dragonflyStickLevelPoint));
        }
        if (MySqlSystem.chestnutPoint < dragonPay)
        {
            NotEnoughMoneyOn();
        }
    }
    /// <summary>
    /// 알림창 On
    /// </summary>
    public void Notic()
    {
        noticPanel.gameObject.SetActive(true);
        ButtonPanel.gameObject.SetActive(false);
    }
    /// <summary>
    /// 알림창 OFF
    /// </summary>
    public void NoticOff()
    {
        noticPanel.gameObject.SetActive(false);
        ButtonPanel.gameObject.SetActive(true);
    }
    /// <summary>
    /// 큐로즈 교환 버튼
    /// </summary>
    public void ChangeQroze()
    {
        Application.OpenURL("https://conbox.kr/gamelogins");
        for (int i = 0; i < treeObject.Length; i++)
        {
            Destroy(treeObject[i].gameObject);
        }
        for (int i = 0; i < Slot.Length; i++)
        {
            Destroy(Slot[i].gameObject);
        }
        LoadingSceneController.LoadingScene("LoginScene");
    }
    /// <summary>
    /// 큐로즈 사용 후 밤 저장 버튼
    /// </summary>
    public void SetChestnut()
    {
        MyPagePanel.gameObject.SetActive(false);
        BlackPanel.gameObject.SetActive(false);
    }
    /// <summary>
    /// 무기 업그레이드 돈이 부족할때 뜨는 판넬 On
    /// </summary>
    public void NotEnoughMoneyOn()
    {
        notEnoughMoneyPanel.gameObject.SetActive(true);
        ButtonPanel.gameObject.SetActive(false);
    }
    /// <summary>
    /// 돈부족 판넬 OFF
    /// </summary>
    public void NotEnoughMoneyOff()
    {
        notEnoughMoneyPanel.gameObject.SetActive(false);
        ButtonPanel.gameObject.SetActive(true);
    }

    /// <summary>
    /// 소리 음소거 확인 버튼
    /// </summary>
    void MusicOnOff()
    {
        if(BGMS.value <= 0.0001)
        {
            bgmButtonOff.gameObject.SetActive(false);
            bgmButtonOn.gameObject.SetActive(true);
        }
        else
        {
            bgmButtonOff.gameObject.SetActive(true);
            bgmButtonOn.gameObject.SetActive(false);
        }
        if (EffectS.value <= 0.0001)
        {
            effectButtonOff.gameObject.SetActive(false);
            effectButtonOn.gameObject.SetActive(true);
        }
        else
        {
            effectButtonOff.gameObject.SetActive(true);
            effectButtonOn.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// BGM OFF
    /// </summary>
    public void BGMOFF()
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(-40) * 20);
        saveBGMVolumn = BGMS.value;
        BGMS.value = -40;
        bgmButtonOff.gameObject.SetActive(false);
        bgmButtonOn.gameObject.SetActive(true);
    }
    /// <summary>
    /// BGM ON
    /// </summary>
    public void BGMON()
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(saveBGMVolumn) * 20);
        BGMS.value = saveBGMVolumn;
        if (BGMS.value > 0.0001)
        {
            bgmButtonOff.gameObject.SetActive(true);
            bgmButtonOn.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// Effect Off
    /// </summary>
    public void EffectOFF()
    {
        audioMixer.SetFloat("Effect", Mathf.Log10(-40) * 20);
        saveEffectVolumn = EffectS.value;
        EffectS.value = -40;
        effectButtonOff.gameObject.SetActive(false);
        effectButtonOn.gameObject.SetActive(true);
    }
    /// <summary>
    /// Effect On
    /// </summary>
    public void EffectON()
    {
        audioMixer.SetFloat("Effect", Mathf.Log10(saveEffectVolumn) * 20);
        EffectS.value = saveEffectVolumn;
        if (EffectS.value > 0.0001)
        {
            effectButtonOff.gameObject.SetActive(true);
            effectButtonOn.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// BGM 볼륨 슬라이더 조정
    /// </summary>
    /// <param name="val"></param>
    public void BGMVolume(float val)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(val) * 20);
    }
    /// <summary>
    /// Effect 불륨 슬라이더 조정
    /// </summary>
    /// <param name="val"></param>
    public void EffectVolume(float val)//����Ʈ ���� ����
    {
        audioMixer.SetFloat("Effect", Mathf.Log10(val) * 20);
    }
    /// <summary>
    /// 게임 스타트 OK버튼
    /// </summary>
    public void GameStartButtonPanelOk()
    {
        if(MySqlSystem.energy > 0)
        {
            MySqlSystem.energy--;
            StartCoroutine(mysql.SetEnergy(0));
            for (int i = 0; i < TreeSetManager.treeObject.Length; i++)
            {
                TreeSetManager.treeObject[i].transform.SetParent(GameObject.Find("Main").transform);
                TreeSetManager.treeObject[i].transform.position = GameObject.Find("Main").transform.position;
            }
            if (MySqlSystem.energy == 9)
            {
                StartCoroutine(MySqlSystem.instance.EnergyTimeS());
            }
            //LoadingSceneController.LoadingScene("GameScene");
            if (!tutorialCheck.isDidTutorial.GameyTutorial)
            {
                tutorialCheck.Game1TutorialSave();
            }
            LoadingSceneController.LoadingScene("ThreeMatchGameScene");
        }
        else
        {
            NotEnoughEnergy.gameObject.SetActive(true);
            GameStartPanel.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// 게임 판넬 OFF
    /// </summary>
    public void GameStartButtonNo()
    {
        GameStartPanel.gameObject.SetActive(false);
    }
    /// <summary>
    /// 나무 팔기 버튼
    /// </summary>
    public void SellButton()
    {
        for (int i = 0; i < treeObject.Length; i++)
        {
            Destroy(treeObject[i].gameObject);
        }
        for (int i = 0; i < Slot.Length; i++)
        {
            Destroy(Slot[i].gameObject);
        }
        MySqlSystem.chestnutPoint = 0;
        MySqlSystem.jewelryPoint = 0;
        MySqlSystem.energy = 0;
        MySqlSystem.fertilizerPoint = 0;
        MySqlSystem.sprayLevelPoint = 0;
        MySqlSystem.dragonflyStickLevelPoint = 0;
        MySqlSystem.instance.loginTime = null;
        MySqlSystem.instance.energySpendTime = null;
        //SoundManager.instance.DestroyClip();
        LoadingSceneController.LoadingScene("LoginScene");
        Application.OpenURL("https://conbox.kr/nft/"+ nft_id);
    }
    ///<summary>
    ///로그아웃 버튼
    ///</summary>
    public void LogoutButton()
    {
        LoadingSceneController.LoadingScene("LoginScene");
        MySqlSystem.chestnutPoint = 0;
        MySqlSystem.jewelryPoint = 0;
        MySqlSystem.energy = 0;
        MySqlSystem.fertilizerPoint = 0;
        MySqlSystem.sprayLevelPoint = 0;
        MySqlSystem.dragonflyStickLevelPoint = 0;
        MySqlSystem.instance.loginTime = null;
        MySqlSystem.instance.energySpendTime = null;
        //SoundManager.instance.DestroyClip();
        AutoSaveOff();
        for (int i = 0; i < Slot.Length; i++)
        {
            Destroy(Slot[i].gameObject);
        }
        for (int i = 0; i < treeObject.Length; i++)
        {
            Destroy(treeObject[i].gameObject);
        }
    }
    public void AutoSaveOff()
    {
        autoLogin.id = "";
        autoLogin.password = "";
        string idPass = JsonUtility.ToJson(autoLogin);
        string filePath = Application.persistentDataPath + "/AutoLogin.txt";
        System.IO.File.WriteAllText(filePath, idPass);
    }
    public void NotEnoughFertilizerOff()
    {
        NotEnoughFertilizer.gameObject.SetActive(false);
        TreeInformation.gameObject.SetActive(true);
    }
    public void NotEnoughEnergyOff()
    {
        NotEnoughEnergy.gameObject.SetActive(false);
    }
    public void SetInventoryPanelOff()
    {
        SetInventoryPanel.gameObject.SetActive(false);
    }
    public void TreeStatusPersentagePanelOn()
    {
        treeStatusPersentage.gameObject.SetActive(true);
    }
    public void TreeStatusPersentagePanelOff()
    {
        treeStatusPersentage.gameObject.SetActive(false);
    }
}
