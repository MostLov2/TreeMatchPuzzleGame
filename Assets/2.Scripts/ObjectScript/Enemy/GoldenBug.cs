using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GoldenChestBugState { IDLE, MOVE, DEAD };

public class GoldenBug : MonoBehaviour
{
    public ChestBugState state;
    Animals animals;
    Idle idle;
    Move move;
    Dead dead;
    Hit hit;
    AudioClip[] clip;
    float waitTime = 0;
    public static int goldenChestBugCount = 0;
    private void Awake()
    {
        animals = GetComponent<Animals>();
        idle = GetComponent<Idle>();
        move = GetComponent<Move>();
        hit = GetComponent<Hit>();
        dead = GetComponent<Dead>();

        clip = new AudioClip[8];
        clip[0] = Resources.Load<AudioClip>("Sound/DragonStick");
        clip[1] = Resources.Load<AudioClip>("Sound/DragonStick1");
        clip[2] = Resources.Load<AudioClip>("Sound/DragonStick2");
        clip[3] = Resources.Load<AudioClip>("Sound/Spray");
        clip[4] = Resources.Load<AudioClip>("Sound/Spray1");
        clip[5] = Resources.Load<AudioClip>("Sound/Spray2");
        clip[6] = Resources.Load<AudioClip>("Sound/ChestnutBugSFX");
        clip[7] = Resources.Load<AudioClip>("Sound/MonsterDeath");
        animals.animator = GetComponent<Animator>();
        animals.spriteRenderer = GetComponent<SpriteRenderer>();
        animals.isMove = Random.RandomRange(0, 101);
        animals.hp = GameLogicManager.goldenBugHp;
        animals.speed = 3;
        animals.MovePoint = GameObject.FindGameObjectsWithTag("ChestNutSpawnPoint");
        animals.spriteRenderer = GetComponent<SpriteRenderer>();
        animals.weaponDMG = 1 + GameLogicManager.sprayDamge;
        animals.animator = GetComponent<Animator>();
        animals.clip = clip;
    }
    private void OnEnable()
    {
        animals.hp = GameLogicManager.goldenBugHp;//재생성시 체력
        GetComponent<CircleCollider2D>().enabled = true;//죽었을때 꺼지는 콜라이더를 재생성시 콜라이더 켜지게 설정
        GetComponent<Rigidbody2D>().isKinematic = false;
        transform.GetChild(0).gameObject.SetActive(true);
        animals.spriteRenderer.color = Color.white;
        animals.spriteRenderer.enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
        animals.spriteRenderer.enabled = true;
        StartCoroutine(UpCol());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ContactSpray(collision);
    }
    IEnumerator UpCol()
    {
        while (true)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            SwitchState();
        }
    }
    void ContactSpray(Collider2D collision)
    {
        if (collision.CompareTag("Spray"))//스프레이 무기콜라이더 접촉했을경우
        {
            if (animals.hp > 0)
            {
                GameObject effect = EffectOpManager.instance.SetObject("ChestnutBugHit");
                effect.transform.position = transform.position;
                animals.hp -= 1 + GameLogicManager.sprayDamge;//맞았을 경우 체력 하나 소모
                hit.GotHit(GameLogicManager.goldenBugHp, animals.hp, animals.spriteRenderer, collision, animals.animator);
            }
            else
            {
                int RandomTrackNum = Random.Range(0, 3);
                dead.IsDead(animals.animator, animals.clip, RandomTrackNum);
                StartCoroutine(DeadPlus());
            }
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
        }
    }
    void SwitchState()
    {
        UpdateState();
        ChangeMovement();

        switch (state)//스테이트 별 상태 체크
        {
            case ChestBugState.IDLE:
                idle.IsIdle(animals.animator);
                break;
            case ChestBugState.MOVE:
                move.Movement(animals.speed, animals.animator, animals.MovePoint);
                break;
            case ChestBugState.DEAD:
                break;
        }
    }
    void UpdateState()
    {
        if (animals.hp <= 0)
        {

            state = ChestBugState.DEAD;
        }
        else
        {
            if (animals.isMove >= 20)
            {
                state = ChestBugState.MOVE;
            }
            else
            {
                state = ChestBugState.IDLE;
            }
        }
    }
    void ChangeMovement()
    {
        waitTime += Time.deltaTime;
        if (waitTime >= 4)
        {
            animals.isMove = Random.RandomRange(0, 101);
            waitTime = 0;
        }
    }
    IEnumerator DeadPlus()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        int RandomNum = Random.Range(0, 2);
        if(RandomNum == 0)
        {
            UIManager.fertilizerPointNum += 50;
        }
        if (RandomNum == 1)
        {
            ChestnutBur.getChestnutPoint += 100;
        }
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(0.767f);
        animals.spriteRenderer.enabled = false;
        GameObject GoldChestOpen = EffectOpManager.instance.SetObject("GoldChestOpen");
        GoldChestOpen.transform.position = transform.position;
        StartCoroutine(GoldenBugEffect(RandomNum));
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
    IEnumerator GoldenBugEffect(int num)
    {
        yield return new WaitForSeconds(2.5f);
        if(num == 0)
        {
            GameObject FertilizertBlast = EffectOpManager.instance.SetObject("FertilizertBlast");
            FertilizertBlast.transform.position = transform.position;
        }
        if(num == 1)
        {
            GameObject ChestnutBlast = EffectOpManager.instance.SetObject("ChestnutBlast");
            ChestnutBlast.transform.position = transform.position;
        }
    }

}
