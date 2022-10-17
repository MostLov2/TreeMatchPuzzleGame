using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestnutBur : MonoBehaviour
{
    //------------------hp------------------------
    int                 hp = 1;//체력
    //------------------getChestnutPoint------------------------
    public static int   getChestnutPoint = 0;
    //------------------chestnutburCount------------------------
    public static int   chestnutburCount = 0;//밤의 전체량을 체크하는 변수
    //------------------randomDropNum------------------------
    int                 randomDropNum;
    //------------------bool------------------------
    public bool         isBugHere;
    public bool         isWormHere;
    public bool         isEggHere;
    public bool         isBornEggHere;
    bool                canNotTake = false;
    //------------------randomShot------------------------
    int                 randomShot;
    //------------------AudioClip------------------------
    AudioClip[]         clip;//1.잠자리채123
    //------------------밤 발사 갯수------------------------
    public static int   minRandomShot;
    public static int   maxRandomShot;
    private void Awake()
    {
        clip =      new AudioClip[3];
        clip[0] =   Resources.Load<AudioClip>("Sound/swing01");
        clip[1] =   Resources.Load<AudioClip>("Sound/swing02");
        clip[2] =   Resources.Load<AudioClip>("Sound/DragonStick2");
    }
    private void OnEnable()
    {
        chestnutburCount++;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        randomDropNum = 0;
        hp =            1;

        isBugHere =     false;
        isWormHere =    false;
        isEggHere =     false;
        isBornEggHere = false;
        canNotTake =    false;

        GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<CircleCollider2D>().enabled = true;

        StartCoroutine(Upcol());
    }
    private void OnDisable()
    {
        chestnutburCount--;
    }
    IEnumerator Upcol()
    {
        while (true)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            if(GameLogicManager.FevertimeAutomation == 1)
            {
                randomDropNum = Random.Range(0, 100);
            }
            randomShot = Random.Range(minRandomShot, maxRandomShot);
            if (MiniGameManager.timeCount <= 0)
            {
                if (Input.GetMouseButtonUp(0) && UIManager.gameTime > 0)
                {
                    randomDropNum = Random.Range(0, 100);
                }
                if (randomDropNum > 95)
                {
                    gameObject.SetActive(false);
                    GameObject effect =  EffectOpManager.instance.SetObject("ChestnutPop");
                    effect.transform.position = transform.position;
                    FireChestnut();
                }
            }
            if (UIManager.gameTime < 0)
            {
                randomDropNum = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DragonflyStick")&&!isBugHere&&!isWormHere && !isEggHere)//잠자리채에 닿았을때
        {
            int randomSound = Random.Range(0, 2);
            if(randomSound  == 0)
            {
                SoundManager.instance.PlaySFX(clip, randomSound, 1, 1);
            }
            if(randomSound  == 1)
            {
                SoundManager.instance.PlaySFX(clip, randomSound, 1, 1);
            }
                
            hp -= 1;
            if (hp <= 0)//죽었을 경우
            {
                FireChestnut();
                GetComponent<CircleCollider2D>().enabled = false;
                gameObject.SetActive(false);
                GameObject effect = EffectOpManager.instance.SetObject("ChestnutPop");
                effect.transform.position = transform.position;
            }
        }
        if (collision.CompareTag("CatchPoint"))
        {
            gameObject.SetActive(false);
        }

        if (collision.CompareTag("Worm"))
        {
            isWormHere = true;
            GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(DisappearChestnut());
        }
        if (collision.CompareTag("Egg"))
        {
            isEggHere = true;
            isBornEggHere = true;
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        if (collision.CompareTag("SparrowFly"))
        {
            FireChestnut();
            gameObject.SetActive(false);
            GameObject effect = EffectOpManager.instance.SetObject("ChestnutPop");
            effect.transform.position = transform.position;
        }
        
        
    }
    /// <summary>
    /// 밤송이 채취시 밤 발사 
    /// </summary>
    public void FireChestnut()//밤송이가 체력이 다될 경우 밤을 램덤 발사
    {
        for (int i = 0; i < randomShot; i++)
        {
            if(GameLogicManager.doubleTheChestnutHarvest == 1)
            {
                getChestnutPoint += 2;
            }
            else
            {
                getChestnutPoint ++;
            }
            GameObject chestnut =  OPManager.instance.SetObject("Chestnut");
            chestnut.transform.position = transform.position;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ChestnutBurCheck"))
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            isBugHere = false;
        }
        if (collision.CompareTag("Worm"))
        {
            isWormHere = false;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (collision.CompareTag("Egg"))
        {
            isEggHere = false;
            isBornEggHere = false;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Squirrel")&&!collision.transform.GetChild(0).gameObject.activeSelf&& !isBugHere&& !isWormHere &&!isEggHere)//다람쥐에 접촉시 발동,밤을 들고 있을 경우 발동 안하게 설정
        {
            collision.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.SetActive(false);
            /*takeNum += Time.deltaTime;
            if (takeNum >= 0.35f)
            {
                Debug.Log(takeNum);
                takeNum = 0;
            }*/
        }
        if (collision.CompareTag("NinjaRabbit"))
        {
            if (collision.GetComponent<Attack>().isAttack)
            {
                GetComponent<Rigidbody2D>().gravityScale = 5;
            }
        }
        if (collision.CompareTag("Wall"))//다람쥐가 던져 벽에 닿을 경우 off
        {
            gameObject.SetActive(false);
            GameObject effect = EffectOpManager.instance.SetObject("ChestnutPop");
            effect.transform.position = transform.position;
        }
        if (collision.CompareTag("ChestnutBurCheck"))
        {
            isBugHere = true;
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        if (collision.CompareTag("Worm"))
        {
            isWormHere = true;
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        if ( collision.CompareTag("Egg"))
        {
            isEggHere = true;
            isBornEggHere = true;
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
   /// <summary>
   /// 밤 사라지는 함수
   /// </summary>
   /// <returns></returns>
    IEnumerator DisappearChestnut()
    {
        yield return new WaitForSeconds(2.5f);
        if (isWormHere)
        {
            gameObject.SetActive(false);
        }
    }
}
