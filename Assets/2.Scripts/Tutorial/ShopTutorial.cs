using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class ShopTutorial : MonoBehaviour
{
    TutorialCheck tutorialCheck;
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
    [SerializeField] Transform SkipPanel;

    int tutorialCount = -1;

    [Header("0")]
    [SerializeField]Transform ShopItemBTN;
    [SerializeField] int ShopItemBTNX;
    [SerializeField] int ShopItemBTNY;

    [Header("1")]
    [SerializeField]Transform ShopGotchaBTN;
    [SerializeField]int ShopGotchaBTNX;
    [SerializeField]int ShopGotchaBTNY;
    [SerializeField] Transform GotchaPanel;
    [SerializeField] Transform BuyItemPanels;

    [Header("2")]
    //Transform GotchaPanel;

    [Header("3")]
    Transform UpGradeBTN;
    int UpGradeBTNX;
    int UpGradeBTNY;

    [Header("4")]
    Transform WeaponPanel;
    Transform ShopBuyPanel;

    string[] guideText;
    private void Awake()
    {
        tutorialCheck = GameObject.FindGameObjectWithTag("Main").GetComponent<TutorialCheck>();
        if (!tutorialCheck.isDidTutorial.ShopTutorial)
        {
            PositionReset();
            GuideTextSet();
            HighLightReset();
        }
        else
        {
            GameObject.FindGameObjectWithTag("TotorialCanvas").transform.GetChild(0).GetComponent<Transform>().gameObject.SetActive(false);
        }
    }
    private void Start()
    {
        TutorialPlay();
    }
    void HighLightReset()
    {
        ShopItemBTNX = 230;
        ShopItemBTNY = 140;
        ShopGotchaBTNX = 230;
        ShopGotchaBTNY = 140;
        UpGradeBTNX = 280;
        UpGradeBTNY = 130;
    }
    void TutorialOnOff(bool highLightOnOff, bool pointAnimationOnOff, bool guidePanelOnOff, bool skipBTNOnOff, bool highLightRedBoxOnOff, bool tutorialBTNOnOff, bool tutorialTitleOnOff)
    {
        highLightTR.gameObject.SetActive(highLightOnOff);
        PointAnimation.gameObject.SetActive(pointAnimationOnOff);
        guidePanel.gameObject.SetActive(guidePanelOnOff);
        SkipBTN.gameObject.SetActive(skipBTNOnOff);
        HighLightRedBox.gameObject.SetActive(highLightRedBoxOnOff);
        TutorialBTN.gameObject.SetActive(tutorialBTNOnOff);
        TutorialTitlePanel.gameObject.SetActive(tutorialTitleOnOff);
    }
    void GuideTextSet()
    {
        guideText = new string[4];
        guideText[0] = "You can buy Items";
        guideText[1] = "You can make a draw";
        guideText[2] = "You can buy equipments";
        guideText[3] = "You can upgrade you equipmet using chesnut";
    }
    void HighLightSizeAndTransformChange(Transform pos, float xsize, float ysize)
    {
        Vector2 HighLightSize = new Vector2(xsize, ysize);
        highLightTR.sizeDelta = HighLightSize;
        highLightTR.transform.DOMove(pos.position, 0.1f);
    }
    void PositionReset()
    {
        TotorialCanvas = GameObject.FindGameObjectWithTag("TotorialCanvas").GetComponent<Transform>();
        GuidePanelNotice = GameObject.FindGameObjectWithTag("TotorialCanvas").transform.GetChild(0).GetChild(5).GetComponent<Transform>();
        HighLightRedBox = GameObject.FindGameObjectWithTag("TotorialCanvas").transform.GetChild(0).GetChild(4).GetComponent<RectTransform>();
        highLightTR = TotorialCanvas.GetChild(0).GetChild(0).GetComponent<RectTransform>();
        PointAnimation = TotorialCanvas.GetChild(0).GetChild(1).GetComponent<Transform>();
        guidePanel = TotorialCanvas.GetChild(0).GetChild(2).GetComponent<Transform>();
        guidePanelText = TotorialCanvas.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>();
        TutorialTitlePanel = TotorialCanvas.GetChild(0).GetChild(7).GetComponent<Transform>();
        TutorialTitle = TotorialCanvas.GetChild(0).GetChild(7).GetChild(0).GetComponent<Text>();
        TutorialStartPanel = TotorialCanvas.GetChild(0).GetChild(8).GetComponent<Transform>();
        SkipBTN = GameObject.FindGameObjectWithTag("TotorialCanvas").transform.GetChild(0).GetChild(3).GetComponent<Transform>();
        TutorialBTN = GameObject.FindGameObjectWithTag("TotorialCanvas").transform.GetChild(0).GetChild(6).GetComponent<Transform>();
        SkipPanel= GameObject.FindGameObjectWithTag("TotorialCanvas").transform.GetChild(0).GetChild(5).GetComponent<Transform>();

        ShopItemBTN = GameObject.FindGameObjectWithTag("MiddleCanvas").transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<Transform>();

        ShopGotchaBTN = GameObject.FindGameObjectWithTag("MiddleCanvas").transform.GetChild(1).GetChild(0).GetChild(0).GetChild(1).GetComponent<Transform>();
        BuyItemPanels = GameObject.FindGameObjectWithTag("MiddleCanvas").transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Transform>();
        GotchaPanel= GameObject.FindGameObjectWithTag("MiddleCanvas").transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<Transform>();

        UpGradeBTN= GameObject.FindGameObjectWithTag("MiddleCanvas").transform.GetChild(0).GetChild(1).GetComponent<Transform>();

        WeaponPanel = GameObject.FindGameObjectWithTag("MiddleCanvas").transform.GetChild(1).GetChild(1).GetComponent<Transform>();
        ShopBuyPanel = GameObject.FindGameObjectWithTag("MiddleCanvas").transform.GetChild(1).GetChild(0).GetComponent<Transform>();
    }
    void GuideTextChange(string guideText)
    {
        guidePanelText.text = guideText;
    }

    public void TutorialPlay()
    {
        tutorialCount++;
        switch (tutorialCount)
        {
            case 0:
                TutorialOnOff(true, false, true, true, false, true, true);
                GuideTextChange(guideText[0]);
                TutorialTitle.text = "Tutorial" + "\n" + " Shop";
                break;
            case 1:
                TutorialOnOff(true, false, true, true, false, true, true);
                GotchaPanel.gameObject.SetActive(true);
                GuideTextChange(guideText[1]);
                BuyItemPanels.gameObject.SetActive(false);
                GotchaPanel.gameObject.SetActive(true);
                HighLightSizeAndTransformChange(ShopGotchaBTN, ShopGotchaBTNX, ShopGotchaBTNY);
                break;
            case 2:
                TutorialOnOff(false, false, true, true, false, true, true);
                GuideTextChange(guideText[2]);
                break;
            case 3:
                TutorialOnOff(true, false, true, true, false, true, true);
                HighLightSizeAndTransformChange(UpGradeBTN, UpGradeBTNX, UpGradeBTNY);
                TutorialTitle.text = "Tutorial" + "\n" + " Upgrade";
                break;
            case 4:
                TutorialOnOff(false, false, true, true, false, true, true);
                GotchaPanel.gameObject.SetActive(false);
                WeaponPanel.gameObject.SetActive(true);
                ShopBuyPanel.gameObject.SetActive(false);
                GuideTextChange(guideText[3]);
                break;
            case 5:
                tutorialCheck.ShopTutorialSave();
                GameObject.FindGameObjectWithTag("TotorialCanvas").transform.GetChild(0).GetComponent<Transform>().gameObject.SetActive(false);
                break;
        }
    }
    public void PressSkipBTN()
    {
        SkipPanelOnOff(true);
    }
    public void SkipOKBTN()
    {
        SkipPanelOnOff(false);
        GameObject.FindGameObjectWithTag("TotorialCanvas").transform.GetChild(0).gameObject.SetActive(false);
        tutorialCheck.ShopTutorialSave();
    }
    public void SkipPanelOnOff(bool OnOff)
    {
        SkipPanel.gameObject.SetActive(OnOff);
    }
}
