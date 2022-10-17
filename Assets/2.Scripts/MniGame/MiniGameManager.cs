using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    //----------------------Bool-------------------------------
    public static bool      isMiniGameStart;//60초 제한시간이 다되었는지 확인
    //----------------------Object-------------------------------
    GameObject[]            egg;
    GameObject[]            worm;
    GameObject[]            chestnuts;//미니게임 시작시 원래있던 밤을 다 꺼서 다음 위치로 옮기기 위해 배열 설정
    //----------------------Count-------------------------------
    public  static float    timeCount = 5;//시작 카운트 
    Text                    countT;//미니게임 5초 카운트
    //----------------------GameUi-------------------------------
    Transform               DragonStick;//잠자리채
    Transform               Spray;//스프레이
    Transform               itemUI;//아이템 ui
    Text                    timeT;//제한시간 텍스트 
    //----------------------Collider-------------------------------
    BoxCollider2D           touchCol;//touch감지 콜라이더
    //----------------------Panel-------------------------------
    Transform               gameoverPanel;//게임 끝나면 뜨는 창
    //----------------------AudioClip-------------------------------
    AudioClip[]             clip;
    private void OnEnable()
    {
        clip =                      new AudioClip[1];
        clip[0] =                   Resources.Load<AudioClip>("Sound/Jingle_1");
        countT =                    GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(4).GetComponent<Text>();//c
        itemUI =                    GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(2).GetComponent<Transform>();//c
        timeT =                     GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(3).GetComponent<Text>();//c
        gameoverPanel =             GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(8).transform.GetChild(0).GetComponent<Transform>();//c
        touchCol =                  GameObject.FindGameObjectWithTag("MiniGameCol").GetComponent<BoxCollider2D>();
        DragonStick =               GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<Transform>();
        Spray =                     GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).GetComponent<Transform>();
        chestnuts =                 GameObject.FindGameObjectsWithTag("ChestnutBur");
        egg =                       GameObject.FindGameObjectsWithTag("Egg");
        worm =                      GameObject.FindGameObjectsWithTag("Worm");
        Time.timeScale =            1;
        UIManager.gameTime =        5 + GameLogicManager.FeverTimeIncrease;


        DragonStick.gameObject.SetActive(false);
        Spray.gameObject.SetActive(false);
        isMiniGameStart = true;


        for (int i = 0; i < chestnuts.Length; i++)
        {
            chestnuts[i].SetActive(false);
        }
        StartCoroutine(Upcol());
    }
    IEnumerator Upcol()
    {
        while (true)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            if (isMiniGameStart)
            {
                for (int i = 0; i < egg.Length; i++)
                {
                    egg[i].SetActive(false);
                }
                for (int i = 0; i < worm.Length; i++)
                {
                    worm[i].SetActive(false);
                }
                itemUI.gameObject.SetActive(false);//ui끄기
                touchCol.enabled = false;//콜라이더 끄기
                countT.enabled = true;
                if (timeCount > 1)
                {
                    countT.text = timeCount.ToString("0");//카운트
                }
                if (timeCount <= 1)
                {
                    countT.text = "Start!!";//카운트
                }
                timeCount -= Time.deltaTime;
                if (timeCount <= 0)
                {
                    UIManager.gameTime -= Time.deltaTime;//게임 시작시 제한시간 줄어들기
                    countT.enabled = false;
                    touchCol.enabled = true;//터치감지 콜라이더 켜기
                    if (UIManager.gameTime < 0)
                    {
                        UIManager.gameTime = 0;
                        Time.timeScale = 0;
                        gameoverPanel.gameObject.SetActive(true);
                        SoundManager.instance.PlaySFX(clip, 0, 1, 1);
                        ChestnutBur.getChestnutPoint += GameLogicManager.IncreasedFeverTimeRewards;
                    }
                }
            }
        }
    }
}
