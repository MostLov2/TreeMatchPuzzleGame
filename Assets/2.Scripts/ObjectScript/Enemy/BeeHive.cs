using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum  BeeHiveState { IDLE, CREATEBEE, DEAD };
public class BeeHive : MonoBehaviour
{
    public BeeState state;
    public Animals animals;
    public Idle idle;
    public Hit hit;
    public Dead dead;
    public static int HiveCount = 0;
    Transform beeCreatePoint;
    AudioClip[] clip;
    public float spawnTime = 0;
    public float stunTime = 8;
    bool TenBeeInHere = false;
    public GameObject hiveLeaves;

    private void Awake()
    {
        beeCreatePoint = transform.GetChild(0);
        animals = GetComponent<Animals>();
        idle = GetComponent<Idle>();
        hit = GetComponent<Hit>();
        dead = GetComponent<Dead>();
        animals.animator = GetComponent<Animator>();
        animals.spriteRenderer = GetComponent<SpriteRenderer>();
        animals.isMove = Random.RandomRange(0, 101);
        animals.hp = 300;
        animals.MovePoint = GameObject.FindGameObjectsWithTag("ChestNutSpawnPoint");
        animals.spriteRenderer = GetComponent<SpriteRenderer>();
        animals.weaponDMG = 1 + GameLogicManager.sprayDamge;
        animals.animator = GetComponent<Animator>();
        clip = new AudioClip[3];
        clip[0] = Resources.Load<AudioClip>("Sound/BeeCreate");
        clip[1] = Resources.Load<AudioClip>("Sound/HitBeeHive1");
        clip[2] = Resources.Load<AudioClip>("Sound/HitBeeHive2");
        HiveCount = 1;
        animals.clip = clip;
        StartCoroutine(UpCol());
    }
    private void OnEnable()
    {
        StartCoroutine(UpCol());
    }
    IEnumerator UpCol()
    {
        while (!MiniGameManager.isMiniGameStart)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            SpawnBee();
            CountBee();
            StartCoroutine(StunPlayer());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DragonflyStick"))
        {
            if (animals.hp > 0)
            {
                animals.animator.SetTrigger("CreateBee");
                animals.hp -= 1+GameLogicManager.DragonflyStickDamge;
                hit.GotHit(300, animals.hp,animals.spriteRenderer, collision, animals.animator);
                hiveLeaves.GetComponent<Animator>().SetTrigger("IsHit");
            }
            else
            {
                int randomTrackNum = Random.Range(1, 3);
                dead.IsDead(animals.animator, clip, randomTrackNum);
                GetComponent<Rigidbody2D>().gravityScale = 10f;
                hiveLeaves.SetActive(false);
            }
        }
    }
    #region CreateBee
    void CreatBee()
    {
        GameObject bee = OPManager.instance.SetObject("Bee");
        bee.transform.position = beeCreatePoint.position;
        SoundManager.instance.PlaySFX(clip, 0, 1, 1);
        StartCoroutine(ShotBee(bee));
    }
    IEnumerator ShotBee(GameObject bee)
    {
        bee.GetComponent<Rigidbody2D>().AddForce(Vector2.left*Time.deltaTime*100,ForceMode2D.Impulse);
        yield return new WaitForSeconds(1.5f);
        bee.GetComponent<Rigidbody2D>().isKinematic = true;
    }
    #endregion
    void CountBee()
    {
        if (Bee.beeCount >= 10)
        {
            TenBeeInHere = true;
        }
        else
        {
            TenBeeInHere = false;
        }
    }
    IEnumerator StunPlayer()
    {
        if (TenBeeInHere)
        {
            stunTime += Time.deltaTime;
            if (stunTime >= 8)
            {
                stunTime = 0;
                GameObject.FindGameObjectWithTag("Player").GetComponent<CrashSystem>().enabled = false;
                GameObject.FindGameObjectWithTag("Stun").transform.GetChild(0).gameObject.SetActive(true);
                GameObject.FindGameObjectWithTag("Stun").transform.position = GameObject.FindGameObjectWithTag("Canvas").transform.position;
                yield return new WaitForSeconds(1);
                GameObject.FindGameObjectWithTag("Player").GetComponent<CrashSystem>().enabled = true;
                GameObject.FindGameObjectWithTag("Stun").transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
    void SpawnBee()
    {
        spawnTime+=Time.deltaTime;
        if(spawnTime >= 8)
        {
            spawnTime=0;
            animals.animator.SetTrigger("CreateBee");
        }
    }
}
