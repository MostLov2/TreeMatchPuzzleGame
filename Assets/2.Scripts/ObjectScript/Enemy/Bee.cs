using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BeeState { IDLE, MOVE, SHOT, DEAD };

public class Bee : MonoBehaviour
{
    public BeeState state;
    public Animals animals;
    public Idle idle;
    public Move move;
    public Hit hit;
    public Dead dead;
    float waitTime = 0;
    float shotTime = 0;
    public static int beeCount = 0;
    AudioClip[] clip;
    bool isShot = false;
    private void Awake()
    {
        animals = GetComponent<Animals>();
        idle = GetComponent<Idle>();
        move = GetComponent<Move>();
        hit = GetComponent<Hit>();
        dead = GetComponent<Dead>();
        animals.animator = GetComponent<Animator>();
        animals.spriteRenderer = GetComponent<SpriteRenderer>();
        animals.isMove = Random.RandomRange(0, 101);
        animals.hp = 5;
        animals.speed = 20;
        animals.MovePoint = GameObject.FindGameObjectsWithTag("ChestNutSpawnPoint");
        animals.spriteRenderer = GetComponent<SpriteRenderer>();
        animals.weaponDMG = 1 + GameLogicManager.sprayDamge;
        animals.animator = GetComponent<Animator>();
        clip = new AudioClip[3];
        clip[0] = Resources.Load<AudioClip>("Sound/Spray");
        clip[1] = Resources.Load<AudioClip>("Sound/Spray1");
        clip[2] = Resources.Load<AudioClip>("Sound/Spray2");
        animals.clip = clip;
    }
    private void OnEnable()
    {
        animals.hp = 5;
        GetComponent<CircleCollider2D>().enabled = true;//죽었을때 꺼지는 콜라이더를 재생성시 콜라이더 켜지게 설정
        GetComponent<Rigidbody2D>().isKinematic = false;
        beeCount++;
        isShot = false;
        StartCoroutine(UpCol());
    }
    private void OnDisable()
    {
        beeCount--;
    }
    IEnumerator UpCol()
    {
        while (true)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            ChangeMovement();
            SwitchState();
            ShotCountDown();
        }
    }
    void ShotCountDown()
    {
        shotTime += Time.deltaTime;
        if (shotTime >= 1f)
        {
            shotTime = 0f;
            isShot = false;
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
    void SwitchState()
    {
        UpdateState();
        switch (state)//스테이트 별 상태 체크
        {
            case BeeState.SHOT:
                break;
            case BeeState.IDLE:
                idle.IsIdle(animals.animator);
                break;
            case BeeState.MOVE:
                move.Movement(animals.speed, animals.animator, animals.MovePoint);
                break;
            case BeeState.DEAD:
                break;
        }
    }
    void UpdateState()
    {
        if (isShot)
        {
            state = BeeState.SHOT;
        }
        else
        {
            if (animals.hp <= 0)
            {
                state = BeeState.DEAD;
            }
            else
            {
                if (animals.isMove >= 20)
                {
                    state = BeeState.MOVE;
                }
                else
                {
                    state = BeeState.IDLE;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spray"))
        {
            if (animals.hp > 0)
            {
                GameObject effect = EffectOpManager.instance.SetObject("ChestnutBugHit");
                effect.transform.position = transform.position;
                animals.hp -= 1 + GameLogicManager.sprayDamge;//맞았을 경우 체력 하나 소모
                hit.GotHit(5, animals.hp, animals.spriteRenderer, collision, animals.animator);
            }
            else
            {
                int RandomTrackNum = Random.Range(1, 3);
                dead.IsDead(animals.animator, animals.clip, RandomTrackNum);
            }
        }
        if (collision.CompareTag("SparrowFly"))
        {
            int RandomTrackNum = Random.Range(1, 3);
            dead.IsDead(animals.animator, animals.clip, RandomTrackNum);
        }
    }
    void Dead()
    {
        gameObject.SetActive(false);
    }
}
