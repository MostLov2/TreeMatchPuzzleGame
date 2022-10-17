using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public enum ChestBugState { IDLE, MOVE, BORNEGG, DEAD };
public class ChestBugs : MonoBehaviour
{
    public ChestBugState state;
    Animals animals;
    Idle idle;
    Move move;
    BornEgg bornEgg;
    Dead dead;
    Hit hit;
    AudioClip[] clip;
    bool inChestnut;
    public static int chestnutBugCount = 0;
    float waitTime = 0;
    float waitBornTime = 0;
    Vector3 point;
    GameObject parentChestnutBur;
    private void Awake()
    {
        animals = GetComponent<Animals>();
        idle = GetComponent<Idle>();
        move = GetComponent<Move>();
        bornEgg = GetComponent<BornEgg>();
        hit = GetComponent<Hit>();
        dead = GetComponent<Dead>();
        inChestnut = false;

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
        animals.hp = 50;
        animals.speed = 6;
        animals.MovePoint = GameObject.FindGameObjectsWithTag("ChestNutSpawnPoint");
        animals.spriteRenderer = GetComponent<SpriteRenderer>();
        animals.weaponDMG = 1 + GameLogicManager.sprayDamge;
        animals.animator = GetComponent<Animator>();
        animals.clip = clip;
    }
    private void OnEnable()
    {
        animals.hp = 50;//재생성시 체력
        GetComponent<CircleCollider2D>().enabled = true;//죽었을때 꺼지는 콜라이더를 재생성시 콜라이더 켜지게 설정
        GetComponent<Rigidbody2D>().isKinematic = false;
        animals.spriteRenderer.color = Color.white;
        chestnutBugCount++;//나왔을때 카운트

        StartCoroutine(UpCol());
    }
    private void OnDisable()
    {
        chestnutBugCount--;//사라질때 카운트 하나 제거
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ContactSpray(collision);
        ContactDragonStick(collision);
        ContactSparrowFly(collision);
        ContactChestnutBur(collision);
    }
    IEnumerator UpCol()
    {
        while (true)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            SwitchState();
            if (inChestnut)
            {
                waitBornTime += Time.deltaTime;
                if (waitBornTime >= 4f)
                {
                    inChestnut = false;
                    waitBornTime = 0;
                }
            }
        }
    }
    void ContactSpray(Collider2D collision)
    {
        if (collision.CompareTag("Spray"))//스프레이 무기콜라이더 접촉했을경우
        {
            if(animals.hp > 0)
            {
                GameObject effect = EffectOpManager.instance.SetObject("ChestnutBugHit");
                effect.transform.position = transform.position;
                animals.hp -= 1 + GameLogicManager.sprayDamge;//맞았을 경우 체력 하나 소모
                hit.GotHit(50, animals.hp, animals.spriteRenderer, collision, animals.animator);
            }
            else
            {
                int RandomTrackNum = Random.Range(0, 3);
                dead.IsDead(animals.animator, animals.clip, RandomTrackNum
                    );
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
    void ContactChestnutBur(Collider2D collision)
    {
        if (collision.CompareTag("ChestnutBur"))//밤에 닿았을 경우
        {
            transform.position = Vector3.MoveTowards(transform.position, collision.transform.position, Time.deltaTime * animals.speed);
            StartCoroutine(InChestNut());
            parentChestnutBur = collision.gameObject;//닿은 콜라이더가 ChestnutBur일 경우 저장
            point = collision.transform.position;//닿은 콜라이더 위치값 저장
            StartCoroutine(bornEgg.IsBornEgg(parentChestnutBur, point));
        }
    }
    IEnumerator InChestNut()
    {
        yield return new WaitForSeconds(1f);
        inChestnut = true;
    }
    void ContactSparrowFly(Collider2D collision)
    {
        if (collision.CompareTag("SparrowFly"))//참새 소환시 사라지게 설정
        {
            animals.hp = 0;
            int RandomTrackNum = Random.Range(0, 3);
            dead.IsDead(animals.animator, animals.clip, RandomTrackNum);
            StartCoroutine(DeadPlus());
            int RandomNum = Random.Range(1, 5);
            for (int i = 1; i < RandomNum; i++)
            {
                UIManager.fertilizerPointNum += 1;
            }
            gameObject.SetActive(false);
        }
    }
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
    void MudOn()
    {
        GameObject effect = EffectOpManager.instance.SetObject("MudEffect");
        effect.transform.position = transform.position;
        SoundManager.instance.PlaySFX(clip, 6, 1, 1);
    }
    #region State
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
            case ChestBugState.BORNEGG:
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
            if (inChestnut)
            {
                state = ChestBugState.BORNEGG;
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
    #endregion
    IEnumerator DeadPlus()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        UIManager.SparrowGauge += 30;//휘슬게이즈 설정
        int GoldenGauge = Random.Range(1, 11);
        SpawnManager.goldenChestBugGauge += GoldenGauge;
        Debug.Log(SpawnManager.goldenChestBugGauge);
        int RandomNum = Random.Range(1, 5);
        for (int i = 1; i < RandomNum; i++)
        {
            UIManager.fertilizerPointNum += 1;
        }
        animals.spriteRenderer.color = Color.white;
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        yield return new WaitForSeconds(1f);
        GameObject effect = EffectOpManager.instance.SetObject("ChestBugDead");
        effect.transform.position = transform.position;
        gameObject.SetActive(false);
    }

}

