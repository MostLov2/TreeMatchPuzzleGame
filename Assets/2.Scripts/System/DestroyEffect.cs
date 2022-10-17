using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestroyEffect : MonoBehaviour
{
    public float destroyTime;
    public bool fadeOutOff = false;
    public AudioClip[] clip;
    private void OnEnable()
    {
        if (!fadeOutOff)
        {
            StartCoroutine(EffectFadeoutOnOff());
        }
        else
        {
            StartCoroutine(EffectOnOff());
        }
    }
    private void OnDisable()
    {
        if(clip.Length  > 0)
        {
            SoundManager.instance.PlaySFX(clip, 0, -60f, 1);
        }
    }
    IEnumerator EffectFadeoutOnOff()
    {
        if (GetComponent<SpriteRenderer>() != null)
        {
            GetComponent<SpriteRenderer>().color = new Color(255/255, 255 / 255, 255 / 255, 255 / 255);
            GetComponent<SpriteRenderer>().DOColor(new Color(255 / 255, 255 / 255, 255 / 255, 0 / 255),destroyTime);
        }
        yield return new WaitForSeconds(destroyTime);
        gameObject.SetActive(false);
    }
    IEnumerator EffectOnOff()
    {
        yield return new WaitForSeconds(destroyTime);
        gameObject.SetActive(false);
    }

}
