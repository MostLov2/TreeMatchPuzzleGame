using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class TreeMatchPuzzelGameUIManager : MonoBehaviour
{
    public static TreeMatchPuzzelGameUIManager instance;
    Transform settingPanel;
    Transform GameOverPanel;
    Text gameOverPanelChestnutPoint;
    Text gameOverPanelFertilizerPoint;
    [Header("SettingPanel")]
    public AudioMixer audioMixer;
    public Transform SettingPanel;
    public Button bgmButtonOn;
    public Button bgmButtonOff;
    public Button effectButtonOn;
    public Button effectButtonOff;
    public Slider BGMS;
    public Slider EffectS;
    public static float saveBGMVolumn;
    public static float saveEffectVolumn;
    [Header("EagleGauge")]
    public Image eagleGaugeImage; 

    private void Awake()
    {
        instance = this;
        settingPanel = GameObject.FindGameObjectWithTag("HighCanvas").transform.GetChild(2).GetComponent<Transform>();
        GameOverPanel = GameObject.FindGameObjectWithTag("HighCanvas").transform.GetChild(1).GetComponent<Transform>();
        gameOverPanelChestnutPoint = GameObject.FindGameObjectWithTag("HighCanvas").transform.GetChild(1).GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();
        gameOverPanelFertilizerPoint = GameObject.FindGameObjectWithTag("HighCanvas").transform.GetChild(1).GetChild(0).GetChild(0).GetChild(2).GetComponent<Text>();
        SettingPanel = GameObject.FindGameObjectWithTag("HighCanvas").transform.GetChild(2).transform.GetChild(0).GetComponent<Transform>();
        bgmButtonOn = GameObject.FindGameObjectWithTag("HighCanvas").transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetChild(4).GetComponent<Button>();
        bgmButtonOff = GameObject.FindGameObjectWithTag("HighCanvas").transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).transform.GetChild(3).GetComponent<Button>();
        effectButtonOn = GameObject.FindGameObjectWithTag("HighCanvas").transform.GetChild(2).transform.GetChild(0).transform.GetChild(1).transform.GetChild(4).GetComponent<Button>();
        effectButtonOff = GameObject.FindGameObjectWithTag("HighCanvas").transform.GetChild(2).transform.GetChild(0).transform.GetChild(1).transform.GetChild(3).GetComponent<Button>();
        BGMS = GameObject.FindGameObjectWithTag("HighCanvas").transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).GetComponent<Slider>();
        EffectS = GameObject.FindGameObjectWithTag("HighCanvas").transform.GetChild(2).transform.GetChild(0).transform.GetChild(1).GetComponent<Slider>();
        eagleGaugeImage = GameObject.FindGameObjectWithTag("MiddleCanvas").transform.GetChild(1).GetChild(3).GetChild(0).GetComponent<Image>();
    }
    private void Update()
    {
        MusicOnOff();

        if (CountDownInPuzzle.isGameStart)
        {
            eagleGaugeImage.fillAmount = Mathf.Lerp(eagleGaugeImage.fillAmount, (float)EagleGauge.eagleGauge / EagleGauge.instance.eagleGaugeInit, Time.deltaTime);
        }
        
    }
    public void SettingPanelOn()
    {
        if (CountDownInPuzzle.isGameStart)
        {
            settingPanel.gameObject.SetActive(true);
            CountDownInPuzzle.isGameStart = false;
        }
    }
    public void SettingPanelOff()
    {
        settingPanel.gameObject.SetActive(false);
        CountDownInPuzzle.isGameStart = true;
    }
    public void RetryButton()
    {
        CountDownInPuzzle.isGameStart = false;
        CountDownInPuzzle.StartCount = 0;
        SceneManager.LoadScene("ThreeMatchGameScene");
    }
    public void ReturnFarmButton()
    {
        CountDownInPuzzle.isGameStart = false;
        CountDownInPuzzle.StartCount = 0;
        Time.timeScale = 1;
        LoadingSceneController.LoadingScene("LobbySceneRestart");
    }
    public void SettingGameOverPanel(int chestnutPoint, int fertilizerPoint)
    {
        GameOverPanel.gameObject.SetActive(true);
        gameOverPanelChestnutPoint.text = chestnutPoint.ToString();
        gameOverPanelFertilizerPoint.text = fertilizerPoint.ToString();
        MySqlSystem.chestnutPoint += chestnutPoint;
        MySqlSystem.fertilizerPoint += fertilizerPoint;
        StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint));
        StartCoroutine(MySqlSystem.instance.SetFertilizer(MySqlSystem.fertilizerPoint));
    }
    /// <summary>
    /// 소리 음소거 확인 버튼
    /// </summary>
    void MusicOnOff()
    {
        if (BGMS.value <= 0.0001)
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
    public void EffectVolume(float val)
    {
        audioMixer.SetFloat("Effect", Mathf.Log10(val) * 20);
    }
}
