using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class LobbyTutorial : MonoBehaviour
{
    [Header("Tutorial")]
    [SerializeField] Transform TotorialCanvas;
    [SerializeField] RectTransform  highLightTR;    //1
    [SerializeField] Transform      PointAnimation; //2
    [SerializeField] Transform      guidePanel;     //3
    [SerializeField] Transform      SkipBTN;        //4
    [SerializeField] RectTransform  HighLightRedBox;//5
    [SerializeField] Transform      TutorialBTN;    //6
    [SerializeField] Text           TutorialTitle;  //7
    [SerializeField] Transform TutorialTitlePanel;  //7
    [SerializeField] Text guidePanelText;
    [SerializeField] Transform GuidePanelNotice;
    [SerializeField] Transform TutorialStartPanel;

    [SerializeField] Transform CenterPos;
    [SerializeField] Transform DownPos;

    string[] guideText;
    [SerializeField] TutorialCheck tutorialCheck;

    public GameObject[] treeObject;

    [SerializeField] Transform Canvas;
    [Header("0")]
    [SerializeField]Transform InventoryTreeBTN;
    [SerializeField]int InventoryTreeBTNX;
    [SerializeField]int InventoryTreeBTNY;
    [SerializeField] Transform inventoryPanel;
    [Header("1")]
    [SerializeField]Transform SetTreeNoticeOKBTN;
    [SerializeField]int SetTreeNoticeOKBTNX;
    [SerializeField]int SetTreeNoticeOKBTNY;
    [SerializeField]Transform SetTreeNoticePanel;
    [Header("2")]
    [SerializeField]Transform SetTreeButtonNoOne;
    [SerializeField]int SetTreeButtonNoOneX;
    [SerializeField]int SetTreeButtonNoOneY;
    [SerializeField]Transform SetTreePosBTN;
    [Header("3")]
    [SerializeField]Transform FirstTreePos;
    [SerializeField]int FirstTreePosX;
    [SerializeField]int FirstTreePosY;
    [Header("4")]
    //Transform FirstTreePos;
    [Header("5")]
    [SerializeField]Transform SetTreeBTNInLobby;
    [SerializeField]int SetTreeBTNInLobbyX;
    [SerializeField]int SetTreeBTNInLobbyY;
    [SerializeField]Transform SetTreeAndTreeInfoPanel;
    [Header("6")]
    [SerializeField]Transform ChestnutPoint;
    [SerializeField]int ChestnutPointX;
    [SerializeField]int ChestnutPointY;
    [Header("7")]
    //Transform InventoryTreeBTN;
    //int InventoryTreeBTNX;
    //int InventoryTreeBTNY;
    //Transform inventoryPanel;
    [Header("8")]
    [SerializeField] Transform TreeInfoBTN;
    [SerializeField]int TreeInfoBTNX;
    [SerializeField]int TreeInfoBTNY;
    [Header("9")]
    [SerializeField]Transform TreeInfoFirstTreeBTN;
    [SerializeField]int TreeInfoFirstTreeBTNX;
    [SerializeField]int TreeInfoFirstTreeBTNY;
    [SerializeField]Transform TreeInfoPanel;
    [Header("10")]
    [SerializeField]Transform SetTreeBTNInLobbyInfoBTN;
    [SerializeField]int SetTreeBTNInLobbyInfoBTNX;
    [SerializeField]int SetTreeBTNInLobbyInfoBTNY;
    //Transform SetTreeAndTreeInfoPanel;
    [Header("11")]
    [SerializeField] Transform TreeInfomationPanel;
    [Header("12")]
    [SerializeField]Transform TreeInfomationLevelUpBTN;
    [SerializeField]int TreeInfomationLevelUpBTNX;
    [SerializeField]int TreeInfomationLevelUpBTNY;
    //Transform TreeInfomationPanel;
    [Header("13")]
    [SerializeField]Transform TreeInformationTreeImage;
    [SerializeField]int TreeInformationTreeImageX;
    [SerializeField]int TreeInformationTreeImageY;
    //Transform TreeInfomationPanel;
    [SerializeField] Transform LevelUpEffect;
    [Header("14")]
    //Transform TreeInfomationPanel;
    [Header("15")]
    Transform TreeInfomationLevelPanel;
    [SerializeField]int TreeInfomationLevelPanelX;
    [SerializeField]int TreeInfomationLevelPanelY;
    [SerializeField]//Transform TreeInfomationPanel;
    [Header("16")]
    Transform TreeInfomationStatusChangePotionBTN;
    [SerializeField]int TreeInfomationStatusChangePotionBTNX;
    [SerializeField]int TreeInfomationStatusChangePotionBTNY;
    //Transform TreeInfomationPanel;
    [Header("17")]
    [SerializeField]Transform TreeInfomationFirstStatus;
    [SerializeField]int TreeInfomationFirstStatusX;
    [SerializeField]int TreeInfomationFirstStatusY;
    //Transform TreeInfomationPanel;
    [Header("18")]
    //Transform TreeInfomationFirstStatus;
    //int TreeInfomationFirstStatusX;
    //int TreeInfomationFirstStatusY;
    //Transform TreeInfomationPanel;
    Transform StampEffect;
    [Header("19")]
    //Transform TreeInfomationPanel;
    [Header("20")]
    [SerializeField] Transform InventoryBTN;
    [SerializeField]int InventoryBTNX;
    [SerializeField]int InventoryBTNY;
    [Header("21")]
    [SerializeField]Transform InventoryItemBTN;
    [SerializeField]int InventoryItemBTNX;
    [SerializeField]int InventoryItemBTNY;
    //Transform inventoryPanel;
    [SerializeField]Transform inventoryItemPanel;
    [SerializeField]Transform inventoryTreePanel;
    [Header("22")]
    Transform HeartPotionBTN;
    [SerializeField]int HeartPotionBTNX;
    [SerializeField]int HeartPotionBTNY;
    //Transform inventoryPanel;
    [Header("23")]
    //Transform inventoryPanel;
    [Header("24")]
    [SerializeField] Transform TreeInfoMationPercentageBTN;
    [SerializeField]int TreeInfoMationPercentageBTNX;
    [SerializeField]int TreeInfoMationPercentageBTNY;
    //Transform TreeInfomationPanel;
    [Header("25")]
    [SerializeField]Transform PersentagePanel;
    [Header("26")]
    //Transform TreeInfomationPanel;
    Transform treeStatusPersntageButton;//나무 특성 확률 표
    [Header("27")]
    Transform ShopBTN;
    int ShopBTNX;
    int ShopBTNY;


    int tutorialCount = -1;
    int gameTutorialCount = -1;
    private void Awake()
    {
        treeObject = GameObject.FindGameObjectsWithTag("Tree");
    }
    private void Start()
    {
        tutorialCheck = GameObject.FindGameObjectWithTag("Main").GetComponent<TutorialCheck>();
        if (tutorialCheck != null)
        {
            Debug.Log(UpdateData.instance.version);
            Debug.Log(Application.version);
            if (!tutorialCheck.isDidTutorial.LobbyTutorial&& UpdateData.instance.version == Application.version)
            {
                GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(20).gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("TotorialCanvas").transform.GetChild(0).gameObject.SetActive(true);
                PositionReset();
                TutorialStart();
                HighLightReset();
                GuideTextSet();
                HighLightRedBoxSizeReset();
                StartCoroutine(TreePositionReset());
            }
            else if (tutorialCheck.isDidTutorial.LobbyTutorial && !tutorialCheck.isDidTutorial.GameyTutorial && UpdateData.instance.version == Application.version)
            {
                GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(20).gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("TotorialCanvas").transform.GetChild(0).gameObject.SetActive(true);
                PositionReset();
                HighLightReset();
                GuideTextSet();
                HighLightRedBoxSizeReset();
                TutorialGamePlayBTN();
            }
            else
            {
                GameObject.FindGameObjectWithTag("TotorialCanvas").transform.GetChild(0).GetComponent<Transform>().gameObject.SetActive(false);
            }
        }
    }
    /// <summary>
    /// Transform 초기화
    /// </summary>
    void PositionReset()
    {
        TotorialCanvas = GameObject.FindGameObjectWithTag("TotorialCanvas").GetComponent<Transform>();
        GuidePanelNotice = GameObject.FindGameObjectWithTag("TotorialCanvas").transform.GetChild(0).GetChild(5).GetComponent<Transform>();
        HighLightRedBox = GameObject.FindGameObjectWithTag("TotorialCanvas").transform.GetChild(0).GetChild(4).GetComponent<RectTransform>();
        highLightTR = TotorialCanvas.GetChild(0).GetChild(0).GetComponent<RectTransform>();
        PointAnimation = TotorialCanvas.GetChild(0).GetChild(1).GetComponent<Transform>();
        guidePanel = TotorialCanvas.GetChild(0).GetChild(2).GetComponent<Transform>();
        guidePanelText = TotorialCanvas.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>();
        TutorialTitle = TotorialCanvas.GetChild(0).GetChild(7).GetChild(0).GetComponent<Text>();
        TutorialTitlePanel = TotorialCanvas.GetChild(0).GetChild(7).GetComponent<Transform>();
        TutorialStartPanel = TotorialCanvas.GetChild(0).GetChild(8).GetComponent<Transform>();
        CenterPos = TotorialCanvas.GetChild(0).GetChild(9).GetComponent<Transform>();
        DownPos = TotorialCanvas.GetChild(0).GetChild(10).GetComponent<Transform>();
        Canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Transform>();
        SkipBTN = GameObject.FindGameObjectWithTag("TotorialCanvas").transform.GetChild(0).GetChild(3).GetComponent<Transform>();
        TutorialBTN = GameObject.FindGameObjectWithTag("TotorialCanvas").transform.GetChild(0).GetChild(6).GetComponent<Transform>();
        LevelUpEffect = GameObject.FindGameObjectWithTag("HighCanvas").transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Transform>();
        StampEffect = GameObject.FindGameObjectWithTag("HighCanvas").transform.GetChild(0).GetComponent<Transform>();
        inventoryItemPanel = Canvas.GetChild(3).GetChild(0).GetChild(1).GetChild(2).GetComponent<Transform>();
        inventoryTreePanel = Canvas.GetChild(3).GetChild(0).GetChild(1).GetChild(0).GetComponent<Transform>();

        InventoryTreeBTN = Canvas.GetChild(3).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Transform>();
        inventoryPanel = Canvas.GetChild(3).GetChild(0).GetComponent<Transform> ();

        SetTreeNoticeOKBTN = Canvas.GetChild(8).GetChild(0).GetChild(0).GetComponent<Transform> ();
        SetTreeNoticePanel = Canvas.GetChild(8).GetChild(0).GetComponent<Transform> ();

        SetTreeButtonNoOne = Canvas.GetChild(9).GetChild(0).GetChild(0).GetComponent<Transform>();
        SetTreePosBTN = Canvas.GetChild(9).GetChild(0).GetComponent<Transform>();

        FirstTreePos = GameObject.FindGameObjectWithTag("TreeSetPoint").transform.GetChild(0).GetComponent<Transform>();

        SetTreeBTNInLobby = Canvas.GetChild(10).GetChild(0).GetChild(0).GetChild(0).GetComponent <Transform> ();
        SetTreeAndTreeInfoPanel = Canvas.GetChild(10).GetChild(0).GetComponent <Transform> ();

        ChestnutPoint = Canvas.GetChild(0).GetChild(1).GetChild(1).GetComponent<Transform> ();

        TreeInfoBTN = Canvas.GetChild(1).GetChild(0).GetChild(2).GetComponent<Transform> ();

        TreeInfoPanel = Canvas.GetChild(4).GetChild(0).GetComponent<Transform>();

        SetTreeBTNInLobbyInfoBTN = Canvas.GetChild(10).GetChild(0).GetChild(0).GetChild(1).GetComponent<Transform>();

        TreeInfomationPanel = Canvas.GetChild(5).GetChild(0).GetComponent<Transform> ();

        TreeInfomationLevelUpBTN = Canvas.GetChild(5).GetChild(0).GetChild(1).GetChild(0).GetComponent<Transform>();

        TreeInformationTreeImage = Canvas.GetChild(5).GetChild(0).GetChild(0).GetChild(4).GetComponent<Transform> ();

        TreeInfomationLevelPanel = Canvas.GetChild(5).GetChild(0).GetChild(0).GetChild(2).GetComponent <Transform> ();

        TreeInfomationStatusChangePotionBTN = Canvas.GetChild(5).GetChild(0).GetChild(1).GetChild(2).GetComponent<Transform>();

        TreeInfomationFirstStatus = Canvas.GetChild(5).GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetComponent <Transform> ();

        InventoryBTN = Canvas.GetChild(1).GetChild(0).GetChild(1).GetComponent <Transform> ();

        InventoryItemBTN = Canvas.GetChild(3).GetChild(0).GetChild(0).GetChild(2).GetComponent<Transform>();

        HeartPotionBTN = Canvas.GetChild(3).GetChild(0).GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetComponent<Transform>();

        TreeInfoMationPercentageBTN = Canvas.GetChild(5).GetChild(0).GetChild(0).GetChild(5).GetComponent<Transform> ();

        PersentagePanel = GameObject.FindGameObjectWithTag("HighCanvas").transform.GetChild(4).GetChild(0).GetComponent<Transform>();

        ShopBTN = Canvas.GetChild(1).GetChild(0).GetChild(3).GetComponent<Transform>();
    }
    void HighLightReset()
    {
        InventoryTreeBTNX = 250;
        InventoryTreeBTNY = 250;
        SetTreeButtonNoOneX = 300;
        SetTreeButtonNoOneY = 300;
        ChestnutPointX = 250;
        ChestnutPointY = 90;
        TreeInfoBTNX = 250;
        TreeInfoBTNY = 250;
        TreeInfoFirstTreeBTNX = 730;
        TreeInfoFirstTreeBTNY = 300;
        TreeInfomationLevelUpBTNX = 180;
        TreeInfomationLevelUpBTNY = 120;
        TreeInfomationStatusChangePotionBTNX = 180;
        TreeInfomationStatusChangePotionBTNY = 120;
        TreeInfomationFirstStatusX = 680;
        TreeInfomationFirstStatusY = 110;
        InventoryBTNX = 250;
        InventoryBTNY = 250;
        InventoryItemBTNX = 190;
        InventoryItemBTNY = 130;
        TreeInfoMationPercentageBTNX = 60;
        TreeInfoMationPercentageBTNY = 60;
        ShopBTNX = 250;
        ShopBTNY = 250;
    }
    void HighLightRedBoxSizeReset()
    {
        SetTreeNoticeOKBTNX = 280;
        SetTreeNoticeOKBTNY = 130;
        FirstTreePosX = 300;
        FirstTreePosY = 300;
        SetTreeBTNInLobbyX = 250;
        SetTreeBTNInLobbyY = 90;
        SetTreeBTNInLobbyInfoBTNX = 250;
        SetTreeBTNInLobbyInfoBTNY = 90;
        TreeInformationTreeImageX = 530;
        TreeInformationTreeImageY = 530;
    }
    void GuideTextSet()
    {
        guideText = new string[28];
        guideText[0] = "Select The Tree.";
        guideText[1] = "Press the OK Button.";
        guideText[2] = "Touch the shovel icon to plant a tree in the selected area.";
        guideText[3] = "The Tree has been planted well in the selected area.";
        guideText[4] = "Press on the Tree for more than a second.";
        guideText[5] = "Press theSet Tree Button.";
        guideText[6] = "Retrieving the tree will cost you 500 chestnuts.";
        guideText[7] = "You can check the trees recovered from your inventory.";
        guideText[8] = "Press on the tree information button.";
        guideText[9] = "You can briefly check the information of the planted trees and trees.";
        guideText[10] = "After pressing and letting go of the tree, press the ‘Tree Info’ button .";
        guideText[11] = "You can check the tree’s planted location, number, level and its traits.";
        guideText[12] = "if you press on the level up button, ";
        guideText[13] = "The fertilizer is used and the tree level goes up.";
        guideText[14] = "A tree gains a new trait every 10 levels.";
        guideText[15] = "The maximum level for a tree is 50";
        guideText[16] = "Press on the Trait change potion.";
        guideText[17] = "Please select the trait you wish to change.";
        guideText[18] = "You can buy the trait change potion in the shop.";
        guideText[19] = "Press the inventory button.";
        guideText[20] = "Press the inventory button.";
        guideText[21] = "Try pressing and using the stamina potion.";
        guideText[22] = "You can use items or equip equipment in the inventory .";
        guideText[23] = "Press the information icon.";
        guideText[24] = "The types of traits could be seen through the tree information icon.";
        guideText[25] = "Press the Shop button";
        guideText[26] = "Select the tree";
        guideText[27] = "The game will start by pressing the OK button";
    }
    void GuidePosChange(string centerOrDown)
    {
        switch (centerOrDown)
        {
            case "Center":
                guidePanel.position = CenterPos.position;
                break;
            case "Down":
                guidePanel.position = DownPos.position;
                break;
        }
    }
    /// <summary>
    /// 튜토리얼에서 사용하는 판넬 및 버튼 OnOff관리 함수
    /// </summary>
    /// <param name="highLightOnOff"></param>
    /// <param name="pointAnimationOnOff"></param>
    /// <param name="guidePanelOnOff"></param>
    /// <param name="skipBTNOnOff"></param>
    /// <param name="highLightRedBoxOnOff"></param>
    /// <param name="tutorialBTNOnOff"></param>
    /// <param name="tutorialTitleOnOff"></param>
    void TutorialOnOff(bool highLightOnOff,bool pointAnimationOnOff,bool guidePanelOnOff,bool skipBTNOnOff,bool highLightRedBoxOnOff,bool tutorialBTNOnOff,bool tutorialTitleOnOff)
    {
        highLightTR.gameObject.SetActive(highLightOnOff);
        PointAnimation.gameObject.SetActive(pointAnimationOnOff);
        guidePanel.gameObject.SetActive(guidePanelOnOff);
        SkipBTN.gameObject.SetActive(skipBTNOnOff);
        HighLightRedBox.gameObject.SetActive(highLightRedBoxOnOff);
        TutorialBTN.gameObject.SetActive(tutorialBTNOnOff);
        TutorialTitlePanel.gameObject.SetActive(tutorialTitleOnOff);
    }
    /// <summary>
    /// 하이라이트 크기, 위치 조절 함수
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="xsize"></param>
    /// <param name="ysize"></param>
    void HighLightSizeAndTransformChange(Transform pos, float xsize, float ysize)
    {
        Vector2 HighLightSize = new Vector2(xsize, ysize);
        highLightTR.sizeDelta = HighLightSize;
        highLightTR.transform.DOMove(pos.position, 0.1f);
    }
    void HighLightRedBoxSizeAndTransformChange(Transform pos, float xsize, float ysize)
    {
        Vector2 HighLightSize = new Vector2(xsize, ysize);
        HighLightRedBox.sizeDelta = HighLightSize;
        HighLightRedBox.transform.DOMove(pos.position, 0.1f);
        Debug.Log(HighLightRedBox.transform.position);
        Debug.Log(pos.position);
        Debug.Log(HighLightSize);
    }
    /// <summary>
    /// 나무 튜토리얼 중이라면 나무 위치값 전부 0으로 초기화 
    /// </summary>
    IEnumerator TreePositionReset()
    {
        yield return new WaitForSeconds(1);
        GameObject[] treeObject = GameObject.FindGameObjectsWithTag("Tree");
        for (int i = 0; i < treeObject.Length; i++)
        {
            treeObject[i].GetComponent<TreeStatus>().TreePosition = 0;
            StartCoroutine(MySqlSystem.instance.SetTreePosition(0, treeObject[i].GetComponent<TreeStatus>().TreeName));
        }
        TreeSetManager.instance.SetTreePos();
    }
    void GuideTextChange(string guideText)
    {
        guidePanelText.text = guideText;
    }
    public void TutorialStart()
    {
        TutorialStartPanel.gameObject.SetActive(true);
    }
    public void TutorialStartPanelOnOff(bool OnOFF)
    {
        TutorialStartPanel.gameObject.SetActive(OnOFF);
        
    }
    public void NoticeOn()
    {
        GuidePanelNotice.gameObject.SetActive(true);
    }
    public void NoticeOff()
    {
        GuidePanelNotice.gameObject.SetActive(false);
    }
    public void TutorialPlayBTN()
    {
        if (!tutorialCheck.isDidTutorial.LobbyTutorial)
        {
            tutorialCount++;
            switch (tutorialCount)
            {
                case 0:
                    Debug.Log("0");
                    TutorialOnOff(true, false, true, true, false, true, true);
                    GuideTextChange(guideText[0]);
                    inventoryPanel.gameObject.SetActive(true);
                    HighLightSizeAndTransformChange(InventoryTreeBTN, InventoryTreeBTNX, InventoryTreeBTNY);
                    TutorialTitle.text = "Tutorial" + "\n" + "Tree Planting";
                    GuidePosChange("Down");
                    break;
                case 1:
                    TutorialOnOff(false, false, true, true, true, true, true);
                    HighLightRedBoxSizeAndTransformChange(SetTreeNoticeOKBTN, SetTreeNoticeOKBTNX, SetTreeNoticeOKBTNY);
                    inventoryPanel.gameObject.SetActive(false);
                    SetTreeNoticePanel.gameObject.SetActive(true);
                    GuideTextChange(guideText[1]);
                    break;
                case 2:
                    TutorialOnOff(true, false, true, true, false, true, true);
                    SetTreeNoticePanel.gameObject.SetActive(false);
                    SetTreePosBTN.gameObject.SetActive(true);
                    HighLightSizeAndTransformChange(SetTreeButtonNoOne, SetTreeButtonNoOneX, SetTreeButtonNoOneY);
                    GuideTextChange(guideText[2]);
                    break;
                case 3:
                    TutorialOnOff(false, false, true, true, true, true, true);
                    treeObject[0].GetComponent<TreeStatus>().TreePosition = 1;
                    MySqlSystem.instance.SetTreePosition(1, treeObject[0].GetComponent<TreeStatus>().TreeName);
                    TreeSetManager.instance.SetTreePos();
                    SetTreePosBTN.gameObject.SetActive(false);
                    HighLightRedBoxSizeAndTransformChange(FirstTreePos, FirstTreePosX, FirstTreePosY);
                    GuideTextChange(guideText[3]);
                    break;
                case 4:
                    TutorialOnOff(false, true, true, true, false, true, true);
                    TutorialTitle.text = "Tutorial" + "\n" + " Set Tree";
                    PointAnimation.position = FirstTreePos.position;
                    GuideTextChange(guideText[4]);
                    break;
                case 5:
                    TutorialOnOff(false, false, true, true, true, true, true);
                    SetTreeAndTreeInfoPanel.position = FirstTreePos.position;
                    HighLightRedBoxSizeAndTransformChange(SetTreeBTNInLobby, SetTreeBTNInLobbyX, SetTreeBTNInLobbyY);
                    SetTreeAndTreeInfoPanel.gameObject.SetActive(true);
                    GuideTextChange(guideText[5]);
                    break;
                case 6:
                    TutorialOnOff(true, false, true, true, false, true, true);
                    SetTreeAndTreeInfoPanel.gameObject.SetActive(false);
                    HighLightSizeAndTransformChange(ChestnutPoint, ChestnutPointX, ChestnutPointY);
                    GuideTextChange(guideText[6]);
                    break;
                case 7:
                    TutorialOnOff(true, false, true, true, false, true, true);
                    treeObject[0].GetComponent<TreeStatus>().TreePosition = 0;
                    MySqlSystem.instance.SetTreePosition(0, treeObject[0].GetComponent<TreeStatus>().TreeName);
                    TreeSetManager.instance.SetTreePos();
                    inventoryPanel.gameObject.SetActive(true);
                    HighLightSizeAndTransformChange(InventoryTreeBTN, InventoryTreeBTNX, InventoryTreeBTNY);
                    GuideTextChange(guideText[7]);
                    break;
                case 8:
                    TutorialOnOff(true, false, true, true, false, true, true);
                    treeObject[0].GetComponent<TreeStatus>().TreePosition = 1;
                    TutorialTitle.text = "Tutorial" + "\n" + "Tree Infomation";
                    MySqlSystem.instance.SetTreePosition(1, treeObject[0].GetComponent<TreeStatus>().TreeName);
                    TreeSetManager.instance.SetTreePos();
                    TreeInfoFirstTreeBTN = Canvas.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Transform>();
                    inventoryPanel.gameObject.SetActive(false);
                    HighLightSizeAndTransformChange(TreeInfoBTN, TreeInfoBTNX, TreeInfoBTNY);
                    GuideTextChange(guideText[8]);
                    GuidePosChange("Center");
                    break;
                case 9:
                    TutorialOnOff(true, false, true, true, false, true, true);
                    TreeInfoPanel.gameObject.SetActive(true);
                    HighLightSizeAndTransformChange(TreeInfoFirstTreeBTN, TreeInfoFirstTreeBTNX, TreeInfoFirstTreeBTNY);
                    GuideTextChange(guideText[9]);
                    GuidePosChange("Down");
                    break;
                case 10:
                    TutorialOnOff(false, false, true, true, true, true, true);
                    SetTreeAndTreeInfoPanel.position = FirstTreePos.position;
                    TreeInfoPanel.gameObject.SetActive(false);
                    HighLightRedBoxSizeAndTransformChange(SetTreeBTNInLobbyInfoBTN, SetTreeBTNInLobbyInfoBTNX, SetTreeBTNInLobbyInfoBTNY);
                    SetTreeAndTreeInfoPanel.gameObject.SetActive(true);
                    GuideTextChange(guideText[10]);
                    break;
                case 11:
                    TutorialOnOff(false, false, true, true, false, true, true);
                    SetTreeAndTreeInfoPanel.gameObject.SetActive(false);
                    TreeInfomationPanel.gameObject.SetActive(true);
                    GuideTextChange(guideText[11]);
                    break;
                case 12:
                    TutorialOnOff(true, false, true, true, false, true, true);
                    HighLightSizeAndTransformChange(TreeInfomationLevelUpBTN, TreeInfomationLevelUpBTNX, TreeInfomationLevelUpBTNY);
                    GuideTextChange(guideText[12]);
                    break;
                case 13:
                    TutorialOnOff(false, false, true, true, true, true, true);
                    HighLightRedBoxSizeAndTransformChange(TreeInformationTreeImage, TreeInformationTreeImageX, TreeInformationTreeImageY);
                    GuideTextChange(guideText[13]);
                    LevelUpEffect.gameObject.SetActive(true);
                    break;
                case 14:
                    TutorialOnOff(false, false, true, true, false, true, true);
                    GuideTextChange(guideText[14]);
                    break;
                case 15:
                    TutorialOnOff(false, false, true, true, true, true, true);
                    HighLightRedBoxSizeAndTransformChange(TreeInfomationLevelPanel, TreeInfomationLevelPanelX, TreeInfomationLevelPanelY);
                    GuideTextChange(guideText[15]);
                    break;
                case 16:
                    TutorialOnOff(true, false, true, true, false, true, true);
                    TutorialTitle.text = "Tutorial" + "\n" + " Change Status";
                    HighLightSizeAndTransformChange(TreeInfomationStatusChangePotionBTN, TreeInfomationStatusChangePotionBTNX, TreeInfomationStatusChangePotionBTNY);
                    GuideTextChange(guideText[16]);
                    break;
                case 17:
                    TutorialOnOff(true, false, true, true, false, true, true);
                    HighLightSizeAndTransformChange(TreeInfomationFirstStatus, TreeInfomationFirstStatusX, TreeInfomationFirstStatusY);
                    GuideTextChange(guideText[17]);
                    GuidePosChange("Center");
                    break;
                case 18:
                    TutorialOnOff(false, false, true, true, false, true, true);
                    HighLightRedBoxSizeAndTransformChange(TreeInfomationFirstStatus, TreeInfomationFirstStatusX, TreeInfomationFirstStatusY);
                    GuidePosChange("Down");
                    StampEffect.gameObject.SetActive(true);
                    StartCoroutine(DelayStamp());
                    break;
                case 19:
                    TutorialOnOff(false, false, true, true, false, true, true);
                    GuideTextChange(guideText[18]);
                    break;
                case 20:
                    TutorialOnOff(true, false, true, true, false, true, true);
                    TreeInfomationPanel.gameObject.SetActive(false);
                    GuideTextChange(guideText[19]);
                    HighLightSizeAndTransformChange(InventoryBTN, InventoryBTNX, InventoryBTNY);
                    GuidePosChange("Center");
                    TutorialTitle.text = "Tutorial" + "\n" + "Inventory";
                    break;
                case 21:
                    TutorialOnOff(true, false, true, true, false, true, true);
                    HighLightSizeAndTransformChange(InventoryItemBTN, InventoryItemBTNX, InventoryItemBTNY);
                    inventoryPanel.gameObject.SetActive(true);
                    GuideTextChange(guideText[20]);
                    GuidePosChange("Down");
                    break;
                case 22:
                    TutorialOnOff(true, false, true, true, false, true, true);
                    HighLightSizeAndTransformChange(HeartPotionBTN, HeartPotionBTNX, HeartPotionBTNY);
                    GuideTextChange(guideText[21]);
                    inventoryTreePanel.gameObject.SetActive(false);
                    inventoryItemPanel.gameObject.SetActive(true);
                    break;
                case 23:
                    TutorialOnOff(false, false, true, true, false, true, true);
                    GuideTextChange(guideText[22]);
                    break;
                case 24:
                    TutorialOnOff(false, false, true, true, true, true, true);
                    TutorialTitle.text = "Tutorial" + "\n" + "Tree Status";
                    inventoryPanel.gameObject.SetActive(false);
                    HighLightRedBoxSizeAndTransformChange(TreeInfoMationPercentageBTN, TreeInfoMationPercentageBTNX, TreeInfoMationPercentageBTNY);
                    TreeInfomationPanel.gameObject.SetActive(true);
                    GuideTextChange(guideText[23]);
                    break;
                case 25:
                    TutorialOnOff(false, false, false, false, false, true, true);
                    PersentagePanel.gameObject.SetActive(true);
                    GuideTextChange(guideText[24]);
                    break;
                case 26:
                    TutorialOnOff(false, false, true, true, false, true, true);
                    PersentagePanel.gameObject.SetActive(false);
                    break;
                case 27:
                    TutorialOnOff(true, false, true, true, false, true, true);
                    GuideTextChange(guideText[25]);
                    TreeInfomationPanel.gameObject.SetActive(false);
                    HighLightSizeAndTransformChange(ShopBTN, ShopBTNX, ShopBTNY);
                   
                    break;
                case 28:
                    SaveTutorialClear();
                    GameObject.FindGameObjectWithTag("TotorialCanvas").transform.GetChild(0).GetComponent<Transform>().gameObject.SetActive(false);
                    break;
            }
        }
    }
    IEnumerator DelayStamp()
    {
        yield return new WaitForSeconds(1.35f);
        TutorialOnOff(false, false, false, true, true, true, true);
    }
    public void TutorialGamePlayBTN()
    {
        if (!tutorialCheck.isDidTutorial.GameyTutorial && tutorialCheck.isDidTutorial.LobbyTutorial)
        {
            gameTutorialCount++;
            switch (gameTutorialCount)
            {
                case 0:
                    TutorialOnOff(false, true, true, true, false, true, true);
                    PointAnimation.position = FirstTreePos.position;
                    treeObject[0].GetComponent<TreeStatus>().TreePosition = 1;
                    MySqlSystem.instance.SetTreePosition(1, treeObject[0].GetComponent<TreeStatus>().TreeName);
                    TreeSetManager.instance.SetTreePos();
                    TutorialTitle.text = "Tutorial" + "\n" + " Game Start";
                    GuideTextChange(guideText[26]);
                    break;
                case 1:
                    TutorialOnOff(true, false, true, true, false, true, false);
                    HighLightSizeAndTransformChange(GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(14).GetChild(0).GetChild(0).transform, 260, 100);
                    treeObject[0].GetComponent<InClick>().GameStartButton();
                    GuideTextChange(guideText[27]);

                    break;
                case 2:
                    GameObject.FindGameObjectWithTag("TotorialCanvas").transform.GetChild(0).GetComponent<Transform>().gameObject.SetActive(false);
                    break;
            }
        }
    }
    public void SaveTutorialClear()
    {
        if (tutorialCheck.isDidTutorial.LobbyTutorial)
        {
            tutorialCheck.Game1TutorialSave();
        }
        else
        {
            tutorialCheck.LobbyTutorialSave();
        }
    }
    public void TutorialOff()
    {
        GameObject.FindGameObjectWithTag("TotorialCanvas").transform.GetChild(0).gameObject.SetActive(false);
    }
    public void TutorialPanelOff()
    {
        GameObject.FindGameObjectWithTag("TotorialCanvas").transform.GetChild(0).gameObject.SetActive(false);
    }
}
