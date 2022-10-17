using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    //-----------------hp---------------------------
    int                 hp = 1;//알 체력
    //-----------------AudioClip---------------------------
    AudioClip[]         clip;//사운드 클립 배열
    //-----------------eggCount---------------------------
    public static int   eggCount = 0;//알이 몇개 소환 되어있는지 확인
    //-----------------Animator---------------------------
    Animator eggAnimator;//알 애니메이터
    //-----------------Time---------------------------
    float               bornWorm = 0.45f;
    float               brokenEgg = 3f;
    float               unActveEgg = 1f;
    private void Awake()
    {
        clip =          new AudioClip[4];
        clip[0] =       Resources.Load<AudioClip>("Sound/Spray");
        clip[1] =       Resources.Load<AudioClip>("Sound/Spray1");
        clip[2] =       Resources.Load<AudioClip>("Sound/Spray2");
        clip[3] =       Resources.Load<AudioClip>("Sound/hatching_sound");
        eggAnimator =   GetComponent<Animator>();
    }
    private void OnEnable()
    {
        hp = 1;//재생성 되었을때 체력 초기화
        eggCount++;//알이 소환 되었을때 카운트 측정
        eggAnimator.SetBool("IsBorn", false);//죽을때 깨진 알들 다시 붙여놓기
        StartCoroutine(BornWorm());//애벌레 소환
        StartCoroutine(Upcol());
    }
    private void OnDisable()
    {
        eggCount--;//알이 사라질때 알의 수 줄이기
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ContactSpray(collision);
        ContactSparrowFly(collision);
    }
    IEnumerator Upcol()
    {
        while (true)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            if (MiniGameManager.isMiniGameStart)
            {
                gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 스프레이에 닿았을 경우
    /// </summary>
    /// <param name="collision"></param>
    void ContactSpray(Collider2D collision)
    {
        if (collision.CompareTag("Spray"))//스프레이 콜라이더 접촉시 
        {
            hp -= 1+ GameLogicManager.sprayDamge;//체력 감소
            int randomSound = Random.Range(0, 2);
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
            if (hp <= 0)
            {
                GameObject effect = EffectOpManager.instance.SetObject("EggDead");
                effect.transform.position = transform.position;
                SoundManager.instance.PlaySFX(clip, 3, 1, 1);
                UIManager.SparrowGauge += 10;//죽었을 경우 휘슬 게이지 10 증가
                gameObject.SetActive(false);//죽었을때 오브젝트 비활성화
            }
        }
    }
    /// <summary>
    /// 독수리에 닿았을 경우
    /// </summary>
    /// <param name="collision"></param>
    void ContactSparrowFly(Collider2D collision)
    {
        if (collision.CompareTag("SparrowFly"))//참새 콜라이더 접촉시
        {
            gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// bornWorm이후에 벌레 소환
    /// </summary>
    /// <returns></returns>
    IEnumerator BornWorm()//알이 소환되고 애벌레가 소환 되는 함수
    {
        if (!MiniGameManager.isMiniGameStart)//미니게임이 아닐경우에만 되게 설정
        {
            yield return new WaitForSeconds(brokenEgg);//3초 후에 알이 부서짐
            eggAnimator.SetBool("IsBorn", true);//알 애니메이션
            yield return new WaitForSeconds(bornWorm);//1초후에 애벌레 생성
            GameObject effect = EffectOpManager.instance.SetObject("EggDead");
            effect.transform.position = transform.position;
            GameObject egg = OPManager.instance.SetObject("Worm");//애벌레 소환
            egg.transform.position = transform.position;//위치는 알 위치
            yield return new WaitForSeconds(unActveEgg);//1초 후에 알 비활성화
            gameObject.SetActive(false);//오브젝트 비활성화
        }
    }
}
