using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    static string       nextScene;
    public Text         LoadCount;
    public Text         LoadExplanation;
    public GameObject[] monster;
    string[] explanation;
    /// <summary>
    /// 다음 씬을 지정후에 로딩창을 띄우는 함수
    /// </summary>
    /// <param name="sceneName"></param>
    public static void LoadingScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }
    private void Start()
    {
        explanation =       new string[4];
        explanation[0] =    "You can use chestnuts to " + "\n" + "buy good items in the shop.";
        explanation[1] =    "If you don’t attack the egg and the caterpillar  " + "\n" + "it will turn into a Weevil. Please get them quickly.";
        explanation[2] =    "by exploding the chestnut block around the monster  " + "\n" + "you can attack the monster.";
        explanation[3] =    "A special block will appear " + "\n" + " when matching 5 blocks or more.";
        StartCoroutine(LoadSceneProcess());
        StartCoroutine(RandomExplanation());
        MoneterOn();
    }
    /// <summary>
    /// 로딩창 게이지 텍스트 업데이트 함수
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0f;
        while (!op.isDone)
        {
            InfiniteLoopDetector.Run();
            yield return null;
            if (op.progress < 0.9f)
            {
                LoadCount.text= (op.progress*100).ToString("0") + " %";
            }
            else
            {
                timer += Time.unscaledDeltaTime*0.5f;
                float Loadgauge = Mathf.Lerp(0.9f, 1f, timer);
                LoadCount.text = (Loadgauge*100).ToString("0") + " %";
                if (Loadgauge >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
        
    }
    IEnumerator RandomExplanation()
    {
        int randomNum = UnityEngine.Random.Range(0, 1);
        LoadExplanation.text = explanation[randomNum].ToString();
        yield return new WaitForSeconds(0.4f);
    }
    void MoneterOn()
    {
        int num = UnityEngine.Random.Range(1, 4);

        if(num == 1)
        {
            monster[0].SetActive(true);
            monster[1].SetActive(false);
            monster[2].SetActive(false);
        }
        if (num == 2)
        {
            monster[0].SetActive(false);
            monster[1].SetActive(true);
            monster[2].SetActive(false);
        }
        if (num >= 3)
        {
            monster[0].SetActive(false);
            monster[1].SetActive(false);
            monster[2].SetActive(true);
        }
    }
}
