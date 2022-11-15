using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ThreeMatchPuzzleFeverTime : MonoBehaviour
{
    public Sprite[] popImages;
    public float popGauge = 0;
    public float popGaugeInit = 100;
    public Image popSnakeImage;
    public Image popGaugeImage;
    public bool isFevertimeStart;
    public bool isFeverCountOff = false;
    public Text feverTimeText;
    Animator ani;
    float curTime = 1;
    public float fevertime;
    public int endCount;
    public int chestnutPointInFevertime;
    public static ThreeMatchPuzzleFeverTime instance;
    public int autoCount = 0;
    private void Awake()
    {
        instance = this;
        autoCount = 0;
        chestnutPointInFevertime = 0; 
        endCount = 0;
        fevertime = 10 + GameLogicManager.FeverTimeIncrease;
        popImages = Resources.LoadAll<Sprite>("FeverTimeSnake");
        popSnakeImage = GameObject.FindGameObjectWithTag("FeverTimeCanvas").transform.GetChild(3).GetComponent<Image>();
        ani = GameObject.FindGameObjectWithTag("FeverTimeCanvas").transform.GetChild(3).GetComponent<Animator>();
        popGaugeImage = GameObject.FindGameObjectWithTag("FeverTimeCanvas").transform.GetChild(1).GetChild(0).GetComponent<Image>();
        feverTimeText = GameObject.FindGameObjectWithTag("FeverTimeCanvas").transform.GetChild(2).GetChild(3).GetComponent<Text>();
        isFevertimeStart = false;
    }
    private void Update()
    {
        if (isFevertimeStart)
        {
            if (fevertime >= 0&& isFeverCountOff)
            {
                feverTimeText.text = fevertime.ToString("00");
                fevertime -= Time.deltaTime;
            }
            ControlPopGauge();
            ControlSnakeScale();
            if (GameLogicManager.FevertimeAutomation > 0 && popGauge <= 100)
            {
                TouchPopUDown();
            }
        }
        popGaugeImage.fillAmount = Mathf.Lerp(popGaugeImage.fillAmount, (float)popGauge / popGaugeInit, Time.deltaTime);
    }
    void ControlPopGauge()
    {
        curTime -= Time.deltaTime;
        if (curTime <= 0&&popGauge >= 0)
        {
            popGauge-=2;
            curTime = 1;
            if (popSnakeImage.transform.localScale.x >= 1)
            {
                popSnakeImage.transform.localScale -= new Vector3(0.02f, 0.02f, 0.02f);
            }
        }
    }
    void ControlSnakeScale()
    {
        if (popGauge >= 80)
        {
            popSnakeImage.sprite = popImages[4];
        }
        else if (popGauge >= 60)
        {
            popSnakeImage.sprite = popImages[3];
        }
        else if (popGauge >= 40)
        {
            popSnakeImage.sprite = popImages[2];
        }
        else if (popGauge >= 20)
        {
            popSnakeImage.sprite = popImages[1];
        }
        else if (popGauge >= 0)
        {
            popSnakeImage.sprite = popImages[0];
        }
    }
    public void TouchPopUDown()
    {
        if (isFevertimeStart&& popGauge <=100)
        {
            GameObject popEffect = BlockEffectOPManager.instance.SetObject(23);
            if (GameLogicManager.FevertimeAutomation > 0)
            {
                autoCount++;
                popEffect.transform.position = Vector3.zero;
                popGauge += 1f;
                popSnakeImage.transform.localScale += new Vector3(0.001f, 0.001f, 0.001f);
                if(autoCount <= 60)
                {
                    int randomPoint = Random.Range(0, 6);
                    chestnutPointInFevertime += randomPoint;
                }
            }
            else
            {
                popGauge += 3;
                popSnakeImage.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
                int randomPoint = Random.Range(0, 6);
                chestnutPointInFevertime += randomPoint;
            }
           

            
            Vector2 screenPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(popEffect.transform.parent.GetComponent<RectTransform>(), Input.mousePosition, Camera.main, out screenPoint);
            popEffect.transform.localPosition = screenPoint;
            popEffect.gameObject.SetActive(true);
            if(popGauge >= 100||fevertime <= 0)
            {
                isFevertimeStart = false;
                endCount++;
                StartCoroutine(FeverTimeEnd());
                StartCoroutine(DelayEndEffect());
            }
        }
        else
        {
            if (endCount <= 0&& !isFevertimeStart)
            {
                isFevertimeStart = false;
                endCount++;
                StartCoroutine(FeverTimeEnd());
                StartCoroutine(DelayEndEffect());
            }
        }
    }
    IEnumerator DelayEndEffect()
    {
        yield return new WaitForSeconds(3);
        GameObject.FindGameObjectWithTag("FeverTimeCanvas").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("FeverTimeCanvas").transform.GetChild(1).gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("FeverTimeCanvas").transform.GetChild(2).gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("FeverTimeCanvas").transform.GetChild(3).gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("FeverTimeCanvas").transform.GetChild(4).gameObject.SetActive(false);
        StartCoroutine(TreeMatchGameGameManager.instance.GameOverEffectOff());
    }
    IEnumerator FeverTimeEnd()
    {
        popSnakeImage.transform.DOScale(Vector3.one, 0.8f);
        popSnakeImage.transform.GetChild(0).gameObject.SetActive(true);
        popSnakeImage.sprite = popImages[4];
        yield return new WaitForSeconds(0.2f);
        popSnakeImage.sprite = popImages[3];
        yield return new WaitForSeconds(0.2f);
        popSnakeImage.sprite = popImages[2];
        yield return new WaitForSeconds(0.2f);
        popSnakeImage.sprite = popImages[1];
        yield return new WaitForSeconds(0.2f);
        popSnakeImage.sprite = popImages[0];
        TreeMatchGameScoreManager.chestnutPoint += chestnutPointInFevertime + (chestnutPointInFevertime/100)*GameLogicManager.IncreasedFeverTimeRewards;
    }

}
