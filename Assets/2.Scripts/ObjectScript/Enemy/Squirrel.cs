using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public enum SquirrelState { IDLE, MOVE, HAVECHESTNUT, DEAD };
public class Squirrel : MonoBehaviour
{
    public SquirrelState state;
    public Animals animals;
    public Idle idle;
    public Move move;
    public HaveChestnut haveChestnut;
    public Hit hit;
    public Dead dead;
    AudioClip[] clip;
    float waitTime = 0;
    public static int squirrelCount = 0;
    private void Awake()
    {
        animals = GetComponent<Animals>();
        idle = GetComponent<Idle>();
        move = GetComponent<Move>();
        haveChestnut = GetComponent<HaveChestnut>();
        hit = GetComponent<Hit>();
        dead = GetComponent<Dead>();
        animals.hp = 100;
        animals.speed = 10f;
        animals.MovePoint = GameObject.FindGameObjectsWithTag("ChestNutSpawnPoint");
        animals.spriteRenderer = GetComponent<SpriteRenderer>();
        animals.weaponDMG = 1 + GameLogicManager.DragonflyStickDamge;
        animals.animator = GetComponent<Animator>();
        clip = new AudioClip[3];
        clip[0] = Resources.Load<AudioClip>("Sound/DragonStick");
        clip[1] = Resources.Load<AudioClip>("Sound/DragonStick1");
        clip[2] = Resources.Load<AudioClip>("Sound/DragonStick2");
        animals.clip = clip; 
        animals.isMove = Random.RandomRange(0, 101);
        StartCoroutine(UpCol());
        RingOnOff(false);
    }
    private void OnEnable()
    {
        StartCoroutine(UpCol());
        animals.spriteRenderer.color = Color.white;
        animals.isMove = Random.RandomRange(0, 101);
        transform.GetChild(0).gameObject.SetActive(false);
        GetComponent<CircleCollider2D>().enabled = true;
        RingOnOff(false);
        animals.hp = 100;
        animals.speed = 10f;
        squirrelCount++;
    }
    private void OnDisable()
    {
        squirrelCount--;
    }
    IEnumerator UpCol()
    {
        while (true)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            ChangeMovement();
            SwitchState();
        }
    }
    #region Hit
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ContactDragonStick(collision);
        ContactWall(collision);
        ContactSparrowFly(collision);
    }
    void ContactDragonStick(Collider2D collision)
    {
        if (collision.CompareTag("DragonflyStick"))
        {
            animals.hp -= 1 + GameLogicManager.DragonflyStickDamge;
            int randomSound = Random.Range(0, 3);
            if (animals.hp >= -100)
            {
                hit.GotHit(100, animals.hp, animals.spriteRenderer, collision, animals.animator);
                GameObject effect = EffectOpManager.instance.SetObject("SquirrelHit");
                effect.transform.position = transform.position;
            }
            if(animals.hp <=0)
            {
                GetComponent<CircleCollider2D>().enabled = false;
                int RandomTrackNum = Random.Range(0, 3);
                dead.IsDead(animals.animator, clip, RandomTrackNum);
                StartCoroutine(DeadPlus());
            }
            if (randomSound == 0)
            {
                SoundManager.instance.PlaySFX(animals.clip, randomSound, 1, 1);
            }
            if (randomSound == 1)
            {
                SoundManager.instance.PlaySFX(animals.clip, randomSound, 1, 1);
            }
            if (randomSound == 2)
            {
                SoundManager.instance.PlaySFX(animals.clip, randomSound, 1, 1);
            }
        }
    }
    void ContactWall(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }
    void ContactSparrowFly(Collider2D collision)
    {
        if (collision.CompareTag("SparrowFly"))
        {
            animals.hp = 0;
        }
    }
    #endregion
    #region State
    void SwitchState()
    {
        UpdateState();
        switch (state)//스테이트 별 상태 체크
        {
            case SquirrelState.IDLE:
                idle.IsIdle(animals.animator);
                break;
            case SquirrelState.MOVE:
                move.Movement(animals.speed,animals.animator,animals.MovePoint);
                break;
            case SquirrelState.HAVECHESTNUT:
                animals.hp = 1;
                haveChestnut.IsHaveChestnut(animals.animator);
                break;
            case SquirrelState.DEAD:
                
                break;
        }
    }
    void UpdateState()
    {
        if (animals.hp <= 0)
        {
            state = SquirrelState.DEAD;
        }
        else
        {
            if (transform.GetChild(0).gameObject.activeSelf)
            {
                state = SquirrelState.HAVECHESTNUT;
            }
            else
            {
                if (animals.isMove >= 20)
                {
                    state = SquirrelState.MOVE;
                }
                else
                {
                    state = SquirrelState.IDLE;
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
    #region Dead
    IEnumerator DeadPlus()
    {
        RingOnOff(true);
        GetComponent<CircleCollider2D>().enabled = false;
        UIManager.SparrowGauge += 20;
        transform.GetChild(0).GetComponent<Transform>().gameObject.SetActive(false);
        
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * Time.deltaTime * 100, ForceMode2D.Impulse);
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        int RandomNum = Random.Range(1, 5);
        for (int i = 1; i < RandomNum; i++)
        {
            UIManager.fertilizerPointNum += 1;
        }
        if (transform.GetChild(0).gameObject.activeSelf)
        {
            int randomShot = Random.Range(ChestnutBur.minRandomShot, ChestnutBur.maxRandomShot);
            for (int i = 0; i < randomShot; i++)
            {
                if (GameLogicManager.doubleTheChestnutHarvest == 1)
                {
                    ChestnutBur.getChestnutPoint += 2;
                }
                else
                {
                    ChestnutBur.getChestnutPoint++;
                }
                GameObject chestnut = OPManager.instance.SetObject("Chestnut");
                chestnut.transform.position = transform.position;
            }
        }
        yield return new WaitForSeconds(0.417f * 2);
        GameObject effect = EffectOpManager.instance.SetObject("ChestBugDead");
        effect.transform.position = transform.position;
        gameObject.SetActive(false);
    }
    void RingOnOff(bool onOff)
    {
        transform.GetChild(1).GetComponent<Transform>().gameObject.SetActive(onOff);

    }
    #endregion

}
