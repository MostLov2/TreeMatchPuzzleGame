using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    //-----------------hp---------------------------
    int                 hp = 1;//�� ü��
    //-----------------AudioClip---------------------------
    AudioClip[]         clip;//���� Ŭ�� �迭
    //-----------------eggCount---------------------------
    public static int   eggCount = 0;//���� � ��ȯ �Ǿ��ִ��� Ȯ��
    //-----------------Animator---------------------------
    Animator eggAnimator;//�� �ִϸ�����
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
        hp = 1;//����� �Ǿ����� ü�� �ʱ�ȭ
        eggCount++;//���� ��ȯ �Ǿ����� ī��Ʈ ����
        eggAnimator.SetBool("IsBorn", false);//������ ���� �˵� �ٽ� �ٿ�����
        StartCoroutine(BornWorm());//�ֹ��� ��ȯ
        StartCoroutine(Upcol());
    }
    private void OnDisable()
    {
        eggCount--;//���� ������� ���� �� ���̱�
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
    /// �������̿� ����� ���
    /// </summary>
    /// <param name="collision"></param>
    void ContactSpray(Collider2D collision)
    {
        if (collision.CompareTag("Spray"))//�������� �ݶ��̴� ���˽� 
        {
            hp -= 1+ GameLogicManager.sprayDamge;//ü�� ����
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
                UIManager.SparrowGauge += 10;//�׾��� ��� �ֽ� ������ 10 ����
                gameObject.SetActive(false);//�׾����� ������Ʈ ��Ȱ��ȭ
            }
        }
    }
    /// <summary>
    /// �������� ����� ���
    /// </summary>
    /// <param name="collision"></param>
    void ContactSparrowFly(Collider2D collision)
    {
        if (collision.CompareTag("SparrowFly"))//���� �ݶ��̴� ���˽�
        {
            gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// bornWorm���Ŀ� ���� ��ȯ
    /// </summary>
    /// <returns></returns>
    IEnumerator BornWorm()//���� ��ȯ�ǰ� �ֹ����� ��ȯ �Ǵ� �Լ�
    {
        if (!MiniGameManager.isMiniGameStart)//�̴ϰ����� �ƴҰ�쿡�� �ǰ� ����
        {
            yield return new WaitForSeconds(brokenEgg);//3�� �Ŀ� ���� �μ���
            eggAnimator.SetBool("IsBorn", true);//�� �ִϸ��̼�
            yield return new WaitForSeconds(bornWorm);//1���Ŀ� �ֹ��� ����
            GameObject effect = EffectOpManager.instance.SetObject("EggDead");
            effect.transform.position = transform.position;
            GameObject egg = OPManager.instance.SetObject("Worm");//�ֹ��� ��ȯ
            egg.transform.position = transform.position;//��ġ�� �� ��ġ
            yield return new WaitForSeconds(unActveEgg);//1�� �Ŀ� �� ��Ȱ��ȭ
            gameObject.SetActive(false);//������Ʈ ��Ȱ��ȭ
        }
    }
}
