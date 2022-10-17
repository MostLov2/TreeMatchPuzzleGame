using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    //----------------------Bool-------------------------------
    public static bool      isMiniGameStart;//60�� ���ѽð��� �ٵǾ����� Ȯ��
    //----------------------Object-------------------------------
    GameObject[]            egg;
    GameObject[]            worm;
    GameObject[]            chestnuts;//�̴ϰ��� ���۽� �����ִ� ���� �� ���� ���� ��ġ�� �ű�� ���� �迭 ����
    //----------------------Count-------------------------------
    public  static float    timeCount = 5;//���� ī��Ʈ 
    Text                    countT;//�̴ϰ��� 5�� ī��Ʈ
    //----------------------GameUi-------------------------------
    Transform               DragonStick;//���ڸ�ä
    Transform               Spray;//��������
    Transform               itemUI;//������ ui
    Text                    timeT;//���ѽð� �ؽ�Ʈ 
    //----------------------Collider-------------------------------
    BoxCollider2D           touchCol;//touch���� �ݶ��̴�
    //----------------------Panel-------------------------------
    Transform               gameoverPanel;//���� ������ �ߴ� â
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
                itemUI.gameObject.SetActive(false);//ui����
                touchCol.enabled = false;//�ݶ��̴� ����
                countT.enabled = true;
                if (timeCount > 1)
                {
                    countT.text = timeCount.ToString("0");//ī��Ʈ
                }
                if (timeCount <= 1)
                {
                    countT.text = "Start!!";//ī��Ʈ
                }
                timeCount -= Time.deltaTime;
                if (timeCount <= 0)
                {
                    UIManager.gameTime -= Time.deltaTime;//���� ���۽� ���ѽð� �پ���
                    countT.enabled = false;
                    touchCol.enabled = true;//��ġ���� �ݶ��̴� �ѱ�
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
