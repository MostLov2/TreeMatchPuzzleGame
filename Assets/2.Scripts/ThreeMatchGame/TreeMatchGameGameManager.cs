using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TreeMatchGameBalance
{
    public int balance_id;
    public int game_time;
    public int drop_min;
    public int drop_max;
    public int fer_min;
    public int fer_max;
}
public class TreeMatchGameGameManager : MonoBehaviour
{
    public TreeMatchGameBalance treeMatchGameBalance;
    public static float TimeCount;
    Text timeCountText;
    public static float StartTime;
    public Transform GameOverEffect;
    public Transform GameOverEffect1;
    public int GameOverCount = 0;
    public int treeLevel;
    public int gameTime;
    public int chestPointMax;
    public int chestPointMin;
    public int fertilizerPointMax;
    public int fertilizerPointMin;
    public static TreeMatchGameGameManager instance;

    private void Awake()
    {
        instance = this;
        TimeCount = 0;
        StartCoroutine(TreeMatchPuzzleBalance());
        timeCountText = GameObject.FindGameObjectWithTag("MiddleCanvas").transform.GetChild(1).GetChild(5).GetComponent<Text>();
        GameOverEffect = GameObject.FindGameObjectWithTag("MoreHighCanvas").transform.GetChild(0).GetComponent<Transform>();
        GameOverEffect1 = GameObject.FindGameObjectWithTag("MoreHighCanvas").transform.GetChild(1).GetComponent<Transform>();
        treeLevel = GameLogicManager.treeLevel;


        timeCountText.text = TimeCount.ToString();
    }
    private void Update()
    {
        if (CountDownInPuzzle.isGameStart)
        {
            if(TimeCount <= 0)
            {
                StartCoroutine(GameOverCountDelay());
            }
            else
            {
                TimeCount -= Time.deltaTime;
                timeCountText.text = TimeCount.ToString("00");
            }
        }
    }
    IEnumerator GameOverCountDelay()
    {
        GameObject.FindGameObjectWithTag("FeverCountCanvas").transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        ThreeMatchPuzzleFeverTime.instance.isFeverCountOff = true;
        GameObject.FindGameObjectWithTag("FeverCountCanvas").transform.GetChild(0).gameObject.SetActive(false);
        ThreeMatchPuzzleFeverTime.instance.isFevertimeStart = true;
        GameObject.FindGameObjectWithTag("FeverTimeCanvas").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("FeverTimeCanvas").transform.GetChild(1).gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("FeverTimeCanvas").transform.GetChild(2).gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("FeverTimeCanvas").transform.GetChild(3).gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("FeverTimeCanvas").transform.GetChild(4).gameObject.SetActive(true);
        CountDownInPuzzle.isGameStart = false;
    }
    /// <summary>
    /// 램덤한 엔딩 애니메이션 재생 함수
    /// </summary>
    /// <returns></returns>
    public IEnumerator GameOverEffectOff()
    {
        int randomNum = Random.Range(1, 2);
        if(randomNum == 0)
        {
            GameOverEffect.gameObject.SetActive(true);
        }
        else
        {
            GameOverEffect1.gameObject.SetActive(true);
        }
        if (randomNum == 0)
        {
            yield return new WaitForSeconds(5);
            GameOverEffect.gameObject.SetActive(false);
            TreeMatchPuzzelGameUIManager.instance.SettingGameOverPanel(TreeMatchGameScoreManager.chestnutPoint+ (int)((TreeMatchGameScoreManager.chestnutPoint/100)*(10*GameLogicManager.chestnutHarvest)), TreeMatchGameScoreManager.fertilizerPoint);
        }
        else
        {
            yield return new WaitForSeconds(2f);
            GameOverEffect1.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// 게임의 밸런스를 DB에서 가져오는 함수
    /// </summary>
    /// <returns></returns>
    public IEnumerator TreeMatchPuzzleBalance()
    {
        WWWForm www = new WWWForm();
        www.AddField("status", "satoBalanceCheck");
        using (UnityWebRequest w = UnityWebRequest.Post("https://conbox.kr/Order", www))
        {
            yield return w.SendWebRequest();
            while (!w.isDone)
            {
                InfiniteLoopDetector.Run();
                Debug.Log("작동안함");
            }
            //Debug.Log(w.downloadHandler.text);
            string str = w.downloadHandler.text;
            string[] charsToRemove = new string[] { "[", "]" };
            foreach (var c in charsToRemove)
            {
                str = str.Replace(c, string.Empty);
            }
            //Debug.Log(str);
            treeMatchGameBalance = JsonUtility.FromJson<TreeMatchGameBalance>(str);
            gameTime = treeMatchGameBalance.game_time;
            chestPointMax = treeMatchGameBalance.drop_max;
            chestPointMin = treeMatchGameBalance.drop_min;
            fertilizerPointMax = treeMatchGameBalance.fer_max;
            fertilizerPointMin = treeMatchGameBalance.fer_min;
            TimeCount = 0;
            EagleGauge.eagleGauge = 0;
            TimeCount = gameTime + (int)(TileManager.instance.treeLevel / 5) * 5+ GameLogicManager.increaseGameTime;
        }
    }

}
