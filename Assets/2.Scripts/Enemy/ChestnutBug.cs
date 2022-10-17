using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�� ��ó�� ������ ����ٰ� �˳��� ���� 2�� ��� 
public class ChestnutBug : MonoBehaviour
{
    //-------------HP----------------------
    public int hp;//ü��
    //------------Point----------------------
    Vector3 point;//���� ��ġ�� �����ϱ����� ������
    //------------AudioClip----------------------
    AudioClip[] clip;//���� Ŭ��
    //------------GameObject----------------------
    GameObject parentChestnutBur;//���� �ڽİ�ü�� �Ͽ� �ߺ��Ǵ� �˼�ȯ�� �������� ����
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
        hp =                                        50;//������� ü��
        GetComponent<CapsuleCollider2D>().enabled = true;//�׾����� ������ �ݶ��̴��� ������� �ݶ��̴� ������ ����
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
    /// �������� �������
    /// </summary>
    /// <param name="collision"></param>
    void ContactSpray(Collider2D collision)
    {
        if (collision.CompareTag("Spray"))//�������� �����ݶ��̴� �����������
        {
            GameObject effect = EffectOpManager.instance.SetObject("ChestnutBugHit");
            effect.transform.position = transform.position;
            hp -= 1 +GameLogicManager.sprayDamge;//�¾��� ��� ü�� �ϳ� �Ҹ�
            StartCoroutine(Damge());
            int randomSound = Random.Range(0, 2);//�������� �ϳ��� ������ ���� ���� ���带 ����ϰ� ����
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
            if (hp <= 0)//�׾��� ���
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
    /// ���ڸ�ä ����� ��
    /// </summary>
    /// <param name="collision"></param>
    void ContactDragonStick(Collider2D collision)
    {
        if (collision.CompareTag("DragonflyStick"))//���ڸ�ä �ݶ��̴� ���˽� �ߵ�
        {
            int randomSound = Random.Range(0, 2);//���� ����
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
            MudOn();//�߸��� ����� ���ݽ� ȭ���� ������ ����Ʈ 
        }
    }
    /// <summary>
    /// ������ ����� �� 
    /// </summary>
    /// <param name="collision"></param>
    void ContactSparrowFly(Collider2D collision)
    {
        if (collision.CompareTag("SparrowFly"))//���� ��ȯ�� ������� ����
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
    /// ����̿� ����� ��
    /// </summary>
    /// <param name="collision"></param>
    void ContactChestnutBur(Collider2D collision)
    {
        if (collision.CompareTag("ChestnutBur"))//�㿡 ����� ���
        {
            parentChestnutBur = collision.gameObject;//���� �ݶ��̴��� ChestnutBur�� ��� ����
            point = collision.transform.position;//���� �ݶ��̴� ��ġ�� ����
            StartCoroutine(BornEgg());//�˻���
        }
    }
    /// <summary>
    /// �߸��� ����� ���ݽ� ���� ����
    /// </summary>
    void MudOn()
    {
        GameObject effect = EffectOpManager.instance.SetObject("MudEffect");
        effect.transform.position = transform.position;
        SoundManager.instance.PlaySFX(clip, 6, 1, 1);
    }
    /// <summary>
    /// �� ���� �Լ� 
    /// </summary>
    /// <returns></returns>
    IEnumerator BornEgg()
    {
        if (!MiniGameManager.isMiniGameStart&&Egg.eggCount<10)//�̴ϰ����� �ƴϰų� eggCount�� 10�� ������ ��� 
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
            if (parentChestnutBur.GetComponent<ChestnutBur>().isBornEggHere == false) 
            {
                GameObject egg = OPManager.instance.SetObject("Egg");//�˼�ȯ
                egg.transform.position = point;//��ġ�� ����� ������ ���� ��ġ
            }
            yield return new WaitForSeconds(3f);
            GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }
    /// <summary>
    /// �¾����� 
    /// </summary>
    /// <returns></returns>
    IEnumerator Damge()
    {
        anim.SetTrigger("IsHit");
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        yield return new WaitForSeconds(1f);
    }
    /// <summary>
    /// �׾����� �ߵ�
    /// </summary>
    /// <returns></returns>
    IEnumerator Dead()
    {
        SoundManager.instance.PlaySFX(clip, 7, 1, 1);
        GetComponent<CapsuleCollider2D>().enabled = false;
        UIManager.SparrowGauge += 30;//�ֽ������� ����
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
