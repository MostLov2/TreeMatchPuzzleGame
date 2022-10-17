using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    int         hp = 3;
    Animator    anim;
    AudioClip[] clip;//1.잠자리채123
    private void Awake()
    {
        clip =      new AudioClip[3];
        clip[0] =   Resources.Load<AudioClip>("Sound/Spray");
        clip[1] =   Resources.Load<AudioClip>("Sound/Spray1");
        clip[2] =   Resources.Load<AudioClip>("Sound/Spray2");
    }
    private void OnEnable()
    {
        GetComponent<CircleCollider2D>().enabled = true;
        anim = GetComponent<Animator>();
        StartCoroutine(DisappearWorm());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spray"))
        {
            hp-= 1+GameLogicManager.sprayDamge;
            anim.SetBool("IsHit", true);
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
                anim.SetTrigger("IsDead");
                UIManager.SparrowGauge += 10;
                GetComponent<CircleCollider2D>().enabled = false;
                StartCoroutine(DeadWorm());
            }
        }
        if (collision.CompareTag("SparrowFly"))
        {
            gameObject.SetActive(false);
        }
    }
   
    IEnumerator DeadWorm()
    {
        yield return new WaitForSeconds(1.5f);
        GameObject effect = EffectOpManager.instance.SetObject("WormDead");
        effect.transform.position = transform.position;
        gameObject.SetActive(false);
    }
    IEnumerator DisappearWorm()
    {
        yield return new WaitForSeconds(3.6f);
        GameObject effect = EffectOpManager.instance.SetObject("WormDead");
        effect.transform.position = transform.position;
        gameObject.SetActive(false);
    }
}
