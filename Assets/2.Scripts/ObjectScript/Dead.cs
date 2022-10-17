using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Dead : MonoBehaviour
{
    public void IsDead(Animator animator, AudioClip[] clip, int trackNum)
    {
        animator.SetTrigger("IsDead");
        SoundManager.instance.PlaySFX(clip, trackNum, 1, 1);
    }

    public void NinjaRabbitDeadLeaf()
    {
        GameObject deadLeaf =  OPManager.instance.SetObject("DeadLeaf");
        deadLeaf.transform.position = transform.GetChild(0).transform.position;
        Vector3 pos;
        pos = transform.GetChild(0).transform.position;
        StartCoroutine(LeafDelay(pos,deadLeaf));        
        StartCoroutine(DeadDelay(2));
    }
    public void NinjaRabbitDeadTelpo()
    {
        GameObject NinjaRabbitDead = EffectOpManager.instance.SetObject("NinjaRabbitDead");
        NinjaRabbitDead.transform.position = transform.position;
    }
    IEnumerator DeadDelay(float DelayTime)
    {
        yield return new WaitForSeconds(DelayTime);
        gameObject.SetActive(false);
    }
    IEnumerator LeafDelay(Vector3 pos,GameObject deadLeaf)
    {
        pos += new Vector3(-0.8f, -1.25f, 0);
        deadLeaf.transform.DOMoveX(pos.x, 1).SetEase(Ease.OutQuad);
        deadLeaf.transform.DOMoveY(pos.y, 1).SetEase(Ease.InQuad);
        yield return new WaitForSeconds(1);
        pos += new Vector3(0.8f, -1.25f, 0);
        deadLeaf.transform.DOMoveX(pos.x, 1).SetEase(Ease.OutQuad);
        deadLeaf.transform.DOMoveY(pos.y, 1).SetEase(Ease.InQuad);
    }

}
