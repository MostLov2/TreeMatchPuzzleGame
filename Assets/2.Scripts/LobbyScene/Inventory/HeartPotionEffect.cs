using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HeartPotionEffect : MonoBehaviour
{
    Transform targetPosition;
    private void Awake()
    {
        targetPosition = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetComponent<Transform>();
    }
    private void OnEnable()
    {
       StartCoroutine(EffectDelay());
    }
    IEnumerator EffectDelay()
    {
        int RandomNum = Random.Range(0, 5);
        if(RandomNum == 0)
        {
            transform.DOMoveX(targetPosition.position.x, 1).SetEase(Ease.OutQuad);
            transform.DOMoveY(targetPosition.position.y, 1).SetEase(Ease.InQuad);
        }
        else if(RandomNum == 1)
        {
            transform.DOMoveX(targetPosition.position.x, 1).SetEase(Ease.InQuad);
            transform.DOMoveY(targetPosition.position.y, 1).SetEase(Ease.OutQuad);
        }
        else if (RandomNum == 2)
        {
            transform.DOMoveX(targetPosition.position.x, 1).SetEase(Ease.InSine);
            transform.DOMoveY(targetPosition.position.y, 1).SetEase(Ease.OutSine);
        }
        else if (RandomNum == 3)
        {
            transform.DOMoveX(targetPosition.position.x, 1).SetEase(Ease.OutSine);
            transform.DOMoveY(targetPosition.position.y, 1).SetEase(Ease.InSine);
        }
        else
        {
            transform.DOMove(targetPosition.position, 1f, false);
        }
        yield return new WaitForSeconds(1f);
        GameObject heartExplsionEffect =  LobbyEffectManager.instance.SetObject("HeartExplosionEffect");
        heartExplsionEffect.transform.position = transform.position;
        gameObject.SetActive(false);
    }
   
}
