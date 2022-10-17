using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//밤 근처에 있으면 멈췄다가 알낳고 가기 2초 대기 
public class ChestnutBug : MonoBehaviour
{
    //-------------HP----------------------
    public int hp;//체력
    //------------Point----------------------
    Vector3 point;//밤의 위치를 저장하기위한 변수값
    //------------AudioClip----------------------
    AudioClip[] clip;//사운드 클립
    //------------GameObject----------------------
    GameObject parentChestnutBur;//밤의 자식객체로 하여 중복되는 알소환을 막기위한 변수
    //------------Animator----------------------
    Animator anim;
    //---------------SpriteRenderer--------------
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        clip =              new AudioClip[8];
        clip[0] =           Resources.Load<AudioClip>("Sound/DragonStick");
        clip[1] =           Resources.Load<AudioClip>("Sound/DragonStick1");
        clip[2] =           Resources.Load<AudioClip>("Sound/DragonStick2");
        clip[3] =           Resources.Load<AudioClip>("Sound/Spray");
        clip[4] =           Resources.Load<AudioClip>("Sound/Spray1");
        clip[5] =           Resources.Load<AudioClip>("Sound/Spray2");
        clip[6] =           Resources.Load<AudioClip>("Sound/ChestnutBugSFX");
        clip[7] =           Resources.Load<AudioClip>("Sound/MonsterDeath");
        anim =              GetComponent<Animator>();
        spriteRenderer =    GetComponent<SpriteRenderer>();    
    }
    private void OnEnable()
    {
        hp =                                        50;//재생성시 체력
        GetComponent<CapsuleCollider2D>().enabled = true;//죽었을때 꺼지는 콜라이더를 재생성시 콜라이더 켜지게 설정
        GetComponent<Rigidbody2D>().isKinematic =   false;
        spriteRenderer.color =                      Color.white;

        StartCoroutine(UpCol());
    }
    private void OnDisable()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ContactSpray(collision);
        ContactDragonStick(collision);
        ContactSparrowFly(collision);
        ContactChestnutBur(collision);
    }
    /// <summary>
    /// 스프레이 닿았을때
    /// </summary>
    /// <param name="collision"></param>
    void ContactSpray(Collider2D collision)
    {
        if (collision.CompareTag("Spray"))//스프레이 무기콜라이더 접촉했을경우
        {
            GameObject effect = EffectOpManager.instance.SetObject("ChestnutBugHit");
            effect.transform.position = transform.position;
            hp -= 1 +GameLogicManager.sprayDamge;//맞았을 경우 체력 하나 소모
            StartCoroutine(Damge());
            int randomSound = Random.Range(0, 2);//램덤으로 하나의 정수가 나와 램덤 사운드를 재생하게 설정
            if (randomSound == 0)
            {
                SoundManager.instance.PlaySFX(clip, 3, 1, 1);
            }
            if (randomSound == 1)
            {
                SoundManager.instance.PlaySFX(clip, 4, 1, 1);
            }
            if (randomSound == 2)
            {
                SoundManager.instance.PlaySFX(clip, 5, 1, 1);
            }
            if (hp <= 0)//죽었을 경우
            {

                StartCoroutine(Dead());
            }
        }
    }
    IEnumerator UpCol()
    {
        while (true)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
            if (hp <= 13)
            {
                spriteRenderer.color = Color.red;
            }
            else if (hp < 26)
            {
                spriteRenderer.color = new Color(255 / 255f, 85 / 255f, 0 / 255f);
            }
            else if (hp < 36)
            {
                spriteRenderer.color = new Color(255 / 255f, 170 / 255f, 0 / 255f);
            }
            else if (hp < 41)
            {
                spriteRenderer.color = new Color(255 / 255f, 255 / 255f, 0 / 255f);
            }
            else if (hp < 51)
            {
                spriteRenderer.color = Color.white;
            }
        }
    }
    /// <summary>
    /// 잠자리채 닿았을 때
    /// </summary>
    /// <param name="collision"></param>
    void ContactDragonStick(Collider2D collision)
    {
        if (collision.CompareTag("DragonflyStick"))//잠자리채 콜라이더 접촉시 발동
        {
            int randomSound = Random.Range(0, 2);//램덤 사운드
            if (randomSound == 0)
            {
                SoundManager.instance.PlaySFX(clip, randomSound, 1, 1);
            }
            if (randomSound == 1)
            {
                SoundManager.instance.PlaySFX(clip, randomSound, 1, 1);
            }
            if (randomSound == 2)
            {
                SoundManager.instance.PlaySFX(clip, randomSound, 1, 1);
            }
            MudOn();//잘못된 무기로 공격시 화면을 가리는 이팩트 
        }
    }
    /// <summary>
    /// 독수리 닿았을 때 
    /// </summary>
    /// <param name="collision"></param>
    void ContactSparrowFly(Collider2D collision)
    {
        if (collision.CompareTag("SparrowFly"))//참새 소환시 사라지게 설정
        {
            hp = 0;
            gameObject.SetActive(false);
            StartCoroutine(Dead());
            int RandomNum = Random.Range(1, 5);
            for (int i = 1; i < RandomNum; i++)
            {
                UIManager.fertilizerPointNum+=1;
            }
        }
    }
    /// <summary>
    /// 밤송이에 닿았을 때
    /// </summary>
    /// <param name="collision"></param>
    void ContactChestnutBur(Collider2D collision)
    {
        if (collision.CompareTag("ChestnutBur"))//밤에 닿았을 경우
        {
            parentChestnutBur = collision.gameObject;//닿은 콜라이더가 ChestnutBur일 경우 저장
            point = collision.transform.position;//닿은 콜라이더 위치값 저장
            StartCoroutine(BornEgg());//알생성
        }
    }
    /// <summary>
    /// 잘못된 무기로 공격시 방해 패턴
    /// </summary>
    void MudOn()
    {
        GameObject effect = EffectOpManager.instance.SetObject("MudEffect");
        effect.transform.position = transform.position;
        SoundManager.instance.PlaySFX(clip, 6, 1, 1);
    }
    /// <summary>
    /// 알 낳는 함수 
    /// </summary>
    /// <returns></returns>
    IEnumerator BornEgg()
    {
        if (!MiniGameManager.isMiniGameStart&&Egg.eggCount<10)//미니게임이 아니거나 eggCount가 10개 이하일 경우 
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
            if (parentChestnutBur.GetComponent<ChestnutBur>().isBornEggHere == false) 
            {
                GameObject egg = OPManager.instance.SetObject("Egg");//알소환
                egg.transform.position = point;//위치는 저장된 접촉한 밤의 위치
            }
            yield return new WaitForSeconds(3f);
            GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }
    /// <summary>
    /// 맞았을때 
    /// </summary>
    /// <returns></returns>
    IEnumerator Damge()
    {
        anim.SetTrigger("IsHit");
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        yield return new WaitForSeconds(1f);
    }
    /// <summary>
    /// 죽었을때 발동
    /// </summary>
    /// <returns></returns>
    IEnumerator Dead()
    {
        SoundManager.instance.PlaySFX(clip, 7, 1, 1);
        GetComponent<CapsuleCollider2D>().enabled = false;
        UIManager.SparrowGauge += 30;//휘슬게이즈 설정
        int RandomNum = Random.Range(1, 5);
        for (int i = 1; i < RandomNum; i++)
        {
            UIManager.fertilizerPointNum += 1;
        }
        spriteRenderer.color = Color.white;
        GetComponent<CapsuleCollider2D>().enabled = false;
        anim.SetTrigger("IsDead");
        GetComponent<Rigidbody2D>().isKinematic = true;
        yield return new WaitForSeconds(1f);
        GameObject effect = EffectOpManager.instance.SetObject("ChestBugDead");
        effect.transform.position = transform.position;
        gameObject.SetActive(false);
    }
}
