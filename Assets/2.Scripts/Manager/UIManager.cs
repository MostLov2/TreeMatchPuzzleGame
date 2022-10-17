using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    //----------------------SocrePanel-----------------------------
    public static int   SparrowGauge = 0;
    public static float gameTime;
    public static int   fertilizerPointNum;
    Text                timeText;
    Text                chestnutScore;
    Text                fertilizerScore;
    Transform           findBugImage;
    Transform           whiStleGauge;
    //----------------------결과창-----------------------------
    Transform           gameoverPanel;
    Text                chestPoint;
    Text                fertilizerPoint;
    Transform           GameoverButton;
    //----------------------Setting-----------------------------
    Transform           settingPanel;
    Transform           helpPanel;
    Transform           soundSetting;
    Transform           JustGoLobbyButton;
    Transform           BGMOffB;
    Transform           BGMONB;
    Transform           EffectOffB;
    Transform           EffectONB;
    Slider              BGMS;
    Slider              EffectS;
    //----------------------GameUI------------------------------
    Transform           itemUI;
    Transform           spray;
    Transform           dragonflyStick;
    //--------------------NoticePanel--------------------------
    Transform           askPanel;
    //--------------------Collider--------------------------
    BoxCollider2D       touchCol;
    //--------------------MiniGame--------------------------
    Text                countT;
    //----------------------Bool---------------------------
    public static bool  isUIOn;
    //----------------------GameStart---------------------------
    Transform           firstTouchPanel;
    //float               timer = 0;
    //----------------------AudioMixer-----------------------------
    [SerializeField]
    AudioMixer          audioMixer = null;
    //----------------------Manager-----------------------------
    [SerializeField]Transform           miniGameManager;
    //----------------------Clip-----------------------------
    [SerializeField] AudioClip[] clip;

    [SerializeField] Transform timeChecker;

    #region FindData
    void Awake()
    {
        gameTime =          60;
        gameTime +=         GameLogicManager.increaseGameTime;
        itemUI =            GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(2).GetComponent<Transform>();
        timeText =          GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(3).transform.GetChild(0).transform.GetChild(1).GetComponent<Text>();
        settingPanel =      GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(7).transform.GetChild(0).GetComponent<Transform>();
        chestnutScore =     GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
        fertilizerScore =   GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(1).transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
        helpPanel =         GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(10).transform.GetChild(0).GetComponent<Transform>();
        chestPoint =        GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(8).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<Text>();
        fertilizerPoint =   GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(8).transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).GetComponent<Text>();
        gameoverPanel =     GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(8).transform.GetChild(0).GetComponent<Transform>();
        countT =            GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(4).GetComponent<Text>();
        askPanel =          GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(9).transform.GetChild(0).GetComponent<Transform>();
        findBugImage =      GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(1).transform.GetChild(1).transform.GetChild(3).transform.GetChild(0).GetComponent<Transform>();
        whiStleGauge =      GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(1).transform.GetChild(1).transform.GetChild(4).transform.GetChild(1).GetComponent<Transform>();
        BGMS =              GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(7).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Slider>();
        EffectS =           GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(7).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<Slider>();
        BGMOffB =           GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(7).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(3).GetComponent<Transform>();
        BGMONB =            GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(7).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(4).GetComponent<Transform>();
        EffectOffB =        GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(7).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).transform.GetChild(3).GetComponent<Transform>();
        EffectONB =         GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(7).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).transform.GetChild(4).GetComponent<Transform>();
        GameoverButton =    GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(9).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<Transform>();
        JustGoLobbyButton = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(9).transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).GetComponent<Transform>();
        firstTouchPanel =   GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(6).transform.GetChild(0).GetComponent<Transform>();
        spray =             GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<Transform>();
        touchCol =          GameObject.FindGameObjectWithTag("MiniGameCol").GetComponent<BoxCollider2D>();
        miniGameManager =   GameObject.FindGameObjectWithTag("MiniGameManager").transform.GetChild(0).GetComponent<Transform>();
        dragonflyStick =    GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).GetComponent<Transform>();
        timeChecker = GameObject.FindGameObjectWithTag("TimeChecker").transform.GetChild(0).GetComponent<Transform>();
        StartCoroutine(Upcol());
    }
    #endregion
    IEnumerator Upcol()
    {
        while (true)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            TimeText();
            ScoreUpdate();
            FindBug();
            WhistleGaugeUpdate();
        }
    }
    private void Update()
    {
        MusicOnOff();
        if(MySqlSystem.energy <= 9)
        {
            MySqlSystem.second -= Time.deltaTime;
        }
    }
    /// <summary>
    /// WhistleGaugeUpdate
    /// </summary>
    void WhistleGaugeUpdate()
    {
        whiStleGauge.GetComponent<Image>().fillAmount = (float)SparrowGauge / 100;
    }
    /// <summary>
    /// 점수 업데이트
    /// </summary>
    void ScoreUpdate()
    {
        chestnutScore.text = string.Format("{0:n0}", ChestnutBur.getChestnutPoint);
        chestPoint.text = string.Format("{0:n0}", ChestnutBur.getChestnutPoint + (ChestnutBur.getChestnutPoint/100)*GameLogicManager.chestnutHarvest);
        fertilizerPoint.text = string.Format("{0:n0}", fertilizerPointNum);
        fertilizerScore.text = string.Format("{0:n0}", fertilizerPointNum);
    }
    /// <summary>
    /// 게임내에 벌레가 있는지 확인
    /// </summary>
    void FindBug()
    {
        if (GameObject.FindGameObjectsWithTag("ChestBug").Length > 0)
        {
            findBugImage.gameObject.SetActive(false);
        }
        else
        {
            findBugImage.gameObject.SetActive(true);
        }
    }
    /// <summary>
    /// 남은 시간 업데이트
    /// </summary>
    void TimeText()
    {
        timeText.text = gameTime.ToString("0");
        if (gameTime > 0&&!MiniGameManager.isMiniGameStart)
        {
            gameTime -= Time.deltaTime;
        }
        if(gameTime >= 0 && gameTime<=4&& !MiniGameManager.isMiniGameStart)
        {
            timeChecker.gameObject.SetActive(true);
        }
        if (gameTime <= 0)
        {
            miniGameManager.gameObject.SetActive(true);
        }

    }
    /// <summary>
    /// 음악 음소거 버튼 업데이트
    /// </summary>
    void MusicOnOff()
    {
        if(BGMS.value <= 0.0001)
        {
            BGMOffB.gameObject.SetActive(false);
            BGMONB.gameObject.SetActive(true);
        }
        else
        {
            BGMOffB.gameObject.SetActive(true);
            BGMONB.gameObject.SetActive(false);
        }
        if (EffectS.value <= 0.0001)
        {
            EffectOffB.gameObject.SetActive(false);
            EffectONB.gameObject.SetActive(true);
        }
        else
        {
            EffectOffB.gameObject.SetActive(true);
            EffectONB.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// BGM음소거 ON
    /// </summary>
    public void BGMOFF()
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(-40) * 20);
        LobbySceneUIManager.saveBGMVolumn = BGMS.value;
        BGMS.value = -40;
        BGMOffB.gameObject.SetActive(false);
        BGMONB.gameObject.SetActive(true);
    }
    /// <summary>
    /// BGM음소거 Off
    /// </summary>
    public void BGMON()
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(LobbySceneUIManager.saveBGMVolumn) * 20);
        BGMS.value = LobbySceneUIManager.saveBGMVolumn;
        if (BGMS.value > 0.0001)
        {
            BGMOffB.gameObject.SetActive(true);
            BGMONB.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// Effect 음소거 OFF
    /// </summary>
    public void EffectOFF()
    {
        audioMixer.SetFloat("Effect", Mathf.Log10(-40) * 20);
        LobbySceneUIManager.saveEffectVolumn = EffectS.value;
        EffectS.value = -40;
        EffectOffB.gameObject.SetActive(false);
        EffectONB.gameObject.SetActive(true);
    }
    /// <summary>
    /// Effect 음소거 On
    /// </summary>
    public void EffectON()
    {
        audioMixer.SetFloat("Effect", Mathf.Log10(LobbySceneUIManager.saveEffectVolumn) * 20);
        EffectS.value = LobbySceneUIManager.saveEffectVolumn;
        if (EffectS.value > 0.0001)
        {
            EffectOffB.gameObject.SetActive(true);
            EffectONB.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// 설정창 On
    /// </summary>
    public void SettingButton()
    { 
        settingPanel.gameObject.SetActive(true);
        Time.timeScale = 0;
        isUIOn = true;
        spray.gameObject.SetActive(false);
        dragonflyStick.gameObject.SetActive(false);
    }
    /// <summary>
    /// 설정창 Off
    /// </summary>
    public void ResumeButton()
    {
        settingPanel.gameObject.SetActive(false);
        isUIOn = false;
        Time.timeScale = 1;
    }
    /// <summary>
    /// 도움말 On
    /// </summary>
    public void HelpButton()
    {
        helpPanel.gameObject.SetActive(true);
    }
    /// <summary>
    /// 도움말 Off
    /// </summary>
    public void HelpExit()
    {
        helpPanel.gameObject.SetActive(false);
    }
    /// <summary>
    /// 다시 시작 버튼
    /// </summary>
    public void RestartButton()
    {
        gameTime =                                  60;
        gameTime +=                                 GameLogicManager.increaseGameTime;

        SpawnManager.firstChestnutSpawnTime =       0;
        Time.timeScale =                            1;
        MiniGameManager.timeCount =                 5;
        ChestnutBur.getChestnutPoint =              0;
        fertilizerPointNum =                        0;

        isUIOn =                                    false;
        touchCol.enabled =                          false;
        countT.enabled =                            false;
        MiniGameManager.isMiniGameStart =           false;
        SoundManager.instance.DestroyClip();

        gameoverPanel.gameObject.SetActive(false);
        itemUI.gameObject.SetActive(true);
        firstTouchPanel.gameObject.SetActive(true);

        OPManager.instance.DestroyObject();
        EffectOpManager.instance.DestroyEffect();

        SceneManager.LoadScene("GameScene");
    }
    /// <summary>
    /// BGM볼륨 슬라이더 조절
    /// </summary>
    /// <param name="val"></param>
    public void BGMVolumn(float val)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(val) * 20);
    }
    /// <summary>
    /// Effect 슬라이더 조절
    /// </summary>
    /// <param name="val"></param>
    public void EffectVolumn(float val)
    {
        audioMixer.SetFloat("Effect", Mathf.Log10(val) * 20);
    }
    /// <summary>
    /// 로비씬 돌아가기 버튼
    /// </summary>
    public void LobbySceneButton()
    {
        SpawnManager.firstChestnutSpawnTime =                       0;
        gameTime =                                                  60;
        gameTime +=                                                 GameLogicManager.increaseGameTime;
        Time.timeScale =                                            1;
        MiniGameManager.timeCount =                                 5;
        ChestnutBur.getChestnutPoint =                              0;
        fertilizerPointNum =                                        0;
        GameLogicManager.treeLevel =                                0;
        GameLogicManager.chestnutHarvest =                          0;
        GameLogicManager.doubleTheChestnutHarvest =                 0;
        GameLogicManager.FeverTimeIncrease =                        0;
        GameLogicManager.IncreasedFeverTimeRewards =                0;
        GameLogicManager.FevertimeAutomation =                      0;
        GameLogicManager.ReductionOfLevelUpFertilizerRequirement =  0;
        GameLogicManager.increaseGameTime =                         0;
        GameLogicManager.chestnutAppearanceRate =                   0;
        GameLogicManager.MonsterRegenerationRate =                  0;
        GameLogicManager.birdMovementSpeed =                        0;
        GameLogicManager.WhistleOverallGaugeReduction =             0;

        MiniGameManager.isMiniGameStart =                           false;
        touchCol.enabled =                                          false;
        isUIOn =                                                    false;
        countT.enabled =                                            false;

        firstTouchPanel.gameObject.SetActive(true);
        gameoverPanel.gameObject.SetActive(false);
        itemUI.gameObject.SetActive(true);



        SoundManager.instance.DestroyClip();
        LoadingSceneController.LoadingScene("LobbySceneRestart");
    }
    /// <summary>
    /// 로비로 돌아갈지 물어보는 판넬 on
    /// </summary>
    public void OnAskLobbyScene()
    {
        askPanel.gameObject.SetActive(true);
    }
    //로비로 돌아갈지 물어보는 판넬 Off
    public void OffAskLobbyScene()
    {
        askPanel.gameObject.SetActive(false);
        if (MiniGameManager.isMiniGameStart&&gameTime<=0)
        {
            gameoverPanel.gameObject.SetActive(true);
        }
    }
    /// <summary>
    /// 게임이 끝났을때 밤의 수를 DB에 저장하는 함수
    /// </summary>
    public void SetScore()
    {
        MySqlSystem.chestnutPoint += (ChestnutBur.getChestnutPoint + (ChestnutBur.getChestnutPoint / 100) * GameLogicManager.chestnutHarvest);
        MySqlSystem.fertilizerPoint += fertilizerPointNum;
        StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint+ ChestnutBur.getChestnutPoint  + (ChestnutBur.getChestnutPoint/100)*GameLogicManager.chestnutHarvest));
        StartCoroutine(MySqlSystem.instance.SetFertilizer(MySqlSystem.fertilizerPoint+ fertilizerPointNum));
    }
    /// <summary>
    /// 게임 종료시 OK버튼
    /// </summary>
    public void GameoverYesButton()
    {
        GameoverButton.gameObject.SetActive(true);
        JustGoLobbyButton.gameObject.SetActive(false);
        gameoverPanel.gameObject.SetActive(false);
    }
    /// <summary>
    /// 게임 종료가 아닌 로비로 돌아가는 버튼
    /// </summary>
    public void JustGoLobby()
    {
        GameoverButton.gameObject.SetActive(false);
        JustGoLobbyButton.gameObject.SetActive(true);
    }
    /// <summary>
    /// 게임 시작 전 화면을 눌러야 시작되는 버튼
    /// </summary>
    public void FirstTouch()
    {
        Time.timeScale = 1;
        firstTouchPanel.gameObject.SetActive(false);
    }
    
}
