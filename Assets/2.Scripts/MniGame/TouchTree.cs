using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchTree : MonoBehaviour
{
    //----------------Time------------------
    //float waitTime = 0;//��� �ð�
    float blinkTime = 0;//�����̴� �ð�
    float soundDelay = 0;
    //----------------bool------------------
    bool isTouch = false;//��ġ ����
    //----------------touchT1------------------
    Text touchT1;//��ġ �̹���
    Text touchT2;
    //----------------AudioClip------------------
    [SerializeField]
    AudioClip[] clip;
    //----------------ChestnutSpawnPoint------------------
    GameObject[] point;
    
    private void Awake()
    {
        clip =      new AudioClip[1];
        clip[0] =   Resources.Load<AudioClip>("Sound/HitWood");

        touchT1 =   GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(5).transform.GetChild(0).GetComponent<Text>();//c
        touchT2 =   GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(5).transform.GetChild(1).GetComponent<Text>();//c

        point =     GameObject.FindGameObjectsWithTag("MiniAconSpawnPoint");

        transform.Rotate(Vector3.zero);
    }
    private void Update()
    {
        ShakeTree();
        
        if (MiniGameManager.timeCount <= 0)
        {
            BlinkText();
            if (Input.GetMouseButtonDown(0) && UIManager.gameTime > 0)
            {
                SoundManager.instance.PlaySFX(clip, 0, 1, 1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                isTouch = true;
                int radomNum = Random.Range(0, point.Length);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetMouseButtonUp(0))
            {
                isTouch = false;
            }
        }
    }
    /// <summary>
    /// 나무 흔들때 애니메이션 및 소리 발생 함수
    /// </summary>
    void ShakeTree()
    {
        if (GameLogicManager.FevertimeAutomation == 1 && MiniGameManager.timeCount < 0)
        {
            StartCoroutine(ShakeOnOff());
            soundDelay += Time.deltaTime;
            if (clip[0].length < soundDelay)
            {
                SoundManager.instance.PlaySFX(clip, 0, 1, 1);
                soundDelay = 0;
            }

        }
        if (MiniGameManager.timeCount < 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(ShakeOnOff());
            
            }
            if (Input.GetMouseButtonUp(0))
            {
                GetComponent<Animator>().SetBool("Move", false);
            }
        }

    }
    /// <summary>
    /// 흔들리는 거 끄는 함수
    /// </summary>
    /// <returns></returns>
    IEnumerator ShakeOnOff()
    {
        GetComponent<Animator>().SetBool("Move", true);
        yield return new WaitForSeconds(0.5f);
        GetComponent<Animator>().SetBool("Move", false);
    }
    /// <summary>
    /// 미니게임 시 번쩍거리는 Touch Text 함수
    /// </summary>
    void BlinkText()
    {
        blinkTime += Time.deltaTime;
        if(blinkTime > 0&&blinkTime < 0.5)
        {
            touchT1.enabled = true;
            touchT2.enabled = true;
        }
        if(blinkTime > 1&&blinkTime < 2)
        {
            touchT1.enabled = false;
            touchT2.enabled = false;
        }
        if(blinkTime > 2.5)
        {
            blinkTime = 0;
        }
    }
}
