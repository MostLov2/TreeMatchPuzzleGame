using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum NinjaRabbitState { IDLE, DEAD };
public class NinjaRabbit : MonoBehaviour
{
    public NinjaRabbitState state;
    public Animals animals;
    public Idle idle;
    public Teleportation teleportation;
    public CreateClone createClone;
    public ThrowChestnut throwChestnut;
    public Hit hit;
    public Dead dead;
    public Attack attack;
    AudioClip[] clip;
    public float telepoteTime = 0;
    //int createCloneNum = 0;
    public static int NInjaRabbit = 0;
    private void Awake()
    {
        animals = GetComponent<Animals>();
        idle = GetComponent<Idle>();
        teleportation = GetComponent<Teleportation>();
        createClone = GetComponent<CreateClone>();
        throwChestnut = GetComponent<ThrowChestnut>();
        attack = GetComponent<Attack>();
        hit = GetComponent<Hit>();
        dead = GetComponent<Dead>();
        animals.animator = GetComponent<Animator>();
        animals.hp = 80;
        animals.weaponDMG = 1 + GameLogicManager.DragonflyStickDamge;
        clip = new AudioClip[6];
        clip[0] = Resources.Load<AudioClip>("Sound/DragonStick");
        clip[1] = Resources.Load<AudioClip>("Sound/DragonStick1");
        clip[2] = Resources.Load<AudioClip>("Sound/DragonStick2");
        clip[3] = Resources.Load<AudioClip>("Sound/NinjaRabbitAttack");
        clip[4] = Resources.Load<AudioClip>("Sound/Telpo");
        clip[5] = Resources.Load<AudioClip>("Sound/Wood");
        animals.clip = clip;
        animals.spriteRenderer = GetComponent<SpriteRenderer>();
        animals.MovePoint = GameObject.FindGameObjectsWithTag("ChestNutSpawnPoint");
        StartCoroutine(UpCol());
    }
    private void OnEnable()
    {
        StartCoroutine(TelpoDelay());
        animals.hp = 80;
        NInjaRabbit++;
        GetComponent<CircleCollider2D>().enabled = true;

        animals.spriteRenderer.enabled = true;
        StartCoroutine(UpCol());
    }
    IEnumerator TelpoDelay()
    {
        yield return new WaitForSeconds(1.1f);
        animals.animator.SetTrigger("Telpo");
    }
    private void OnDisable()
    {
        NInjaRabbit--;
    }
    IEnumerator UpCol()
    {
        while (!MiniGameManager.isMiniGameStart)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            ChangeTime();
        }
    }
    void SwitchState()
    {
        UpdateState();
        switch (state)
        {
            case NinjaRabbitState.IDLE:
                break;
            case NinjaRabbitState.DEAD:
                break;
            default:
                break;
        }
    }
    void UpdateState()
    {
        if (animals.hp <= 0)
        {
            state = NinjaRabbitState.DEAD;
        }
        else
        {
            state = NinjaRabbitState.IDLE;
        }
    }
    void ChangeTime()
    {
        telepoteTime += Time.deltaTime;
        if(telepoteTime > 4 && attack.isAttack != true&&state != NinjaRabbitState.DEAD)
        {
            animals.animator.SetTrigger("Telpo");
            telepoteTime = 0;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("ChestnutBur") && !attack.isAttack && attack.count == 0&&state !=NinjaRabbitState.DEAD)
        {
            animals.animator.SetTrigger("IsAttack");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("DragonflyStick"))
        {
            if(animals.hp > 0)
            {
                animals.hp -= animals.weaponDMG;
                hit.GotHit(80,animals.hp,animals.spriteRenderer,collision,animals.animator);
                if(animals.hp > 40)
                {
                    int randomNUm = Random.Range(0, 100);
                    if (randomNUm <= 10&& NInjaRabbit <= 5)
                    {
                        createClone.CreateCloneInGame();
                    }
                }
                else
                {
                    Debug.Log("hpdown");
                    int randomNUm = Random.Range(0, 100);
                    if (randomNUm <= 50 && NInjaRabbit <= 5)
                    {
                        createClone.CreateCloneInGame();
                    }
                }
            }
            else
            {
                GetComponent<CircleCollider2D>().enabled = false;
                DropFertilizer();
                int randomNum = Random.Range(0,clip.Length);
                dead.IsDead(animals.animator, clip, randomNum);
                animals.animator.SetTrigger("IsDead");
                UIManager.SparrowGauge += 5;
            }
        }
        if (collision.CompareTag("SparrowFly"))
        {
            GetComponent<CircleCollider2D>().enabled = false;
            DropFertilizer();
            int randomNum = Random.Range(0, clip.Length);
            dead.IsDead(animals.animator, clip, randomNum);
            UIManager.SparrowGauge += 5;
            gameObject.SetActive(false);
        }
    }
    void DropFertilizer()
    {
        int RandomNum = Random.Range(1, 5);
        for (int i = 1; i < RandomNum; i++)
        {
            UIManager.fertilizerPointNum += 1;
        }
    }
}
