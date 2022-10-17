using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameOverSnake : MonoBehaviour
{
    public Transform[] point;
    public Transform[] setPoint;
    public void SnakeEnd()
    {
        StartCoroutine(CreateChestnut());
    }
    IEnumerator CreateChestnut()
    {
        for (int i = 0; i < 400; i++)
        {
            yield return new WaitForSeconds(0.01f);
            CreatBlockEffect();
        }
    }
    public void CreatBlockEffect()
    {
        int RandomNum = Random.Range(0, 5);
        int RandomPointNum = Random.Range(0, 5);
        int RandomPointUpNum = Random.Range(0, 7);
        GameObject effect = OPBlock.instance.SetObject(RandomNum);
        effect.transform.position = setPoint[RandomPointNum].transform.position;
        effect.transform.SetParent(transform);
        effect.transform.DOMoveX(point[0].position.x, 1).SetEase(Ease.InSine);
        effect.transform.DOMoveY(point[0].position.y, 1).SetEase(Ease.OutSine);
        GameObject effect1 = OPBlock.instance.SetObject(RandomNum);
        effect1.transform.position = setPoint[RandomPointNum].transform.position;
        effect1.transform.SetParent(transform);
        effect1.transform.DOMoveY(point[1].position.y, 1).SetEase(Ease.OutSine);
        effect1.transform.DOMoveX(point[1].position.x, 1).SetEase(Ease.InSine);
        GameObject effect2 = OPBlock.instance.SetObject(RandomNum);
        effect2.transform.position = setPoint[RandomPointNum].transform.position;
        effect2.transform.SetParent(transform);
        effect2.transform.DOMoveX(point[2].position.x, 1).SetEase(Ease.InSine);
        effect2.transform.DOMoveY(point[2].position.y, 1).SetEase(Ease.OutSine);
        GameObject effect3 = OPBlock.instance.SetObject(RandomNum);
        effect3.transform.position = setPoint[RandomPointNum].transform.position;
        effect3.transform.SetParent(transform);
        effect3.transform.DOMoveX(point[3].position.x, 1).SetEase(Ease.InSine);
        effect3.transform.DOMoveY(point[3].position.y, 1).SetEase(Ease.OutSine);
        GameObject effect4 = OPBlock.instance.SetObject(RandomNum);
        effect4.transform.position = setPoint[RandomPointNum].transform.position;
        effect4.transform.SetParent(transform);
        effect4.transform.DOMoveX(point[4].position.x, 1).SetEase(Ease.InSine);
        effect4.transform.DOMoveY(point[4].position.y, 1).SetEase(Ease.OutSine);
        GameObject effect5 = OPBlock.instance.SetObject(RandomNum);
        effect5.transform.position = setPoint[RandomPointNum].transform.position;
        effect5.transform.SetParent(transform);
        effect5.transform.DOMoveX(point[5].position.x, 1).SetEase(Ease.InSine);
        effect5.transform.DOMoveY(point[5].position.y, 1).SetEase(Ease.OutSine);
        GameObject effect6 = OPBlock.instance.SetObject(RandomNum);
        effect6.transform.position = setPoint[RandomPointNum].transform.position;
        effect6.transform.SetParent(transform);
        effect6.transform.DOMoveX(point[6].position.x, 1).SetEase(Ease.InSine);
        effect6.transform.DOMoveY(point[6].position.y, 1).SetEase(Ease.OutSine);
        StartCoroutine(DestroyEffect(effect));
        StartCoroutine(DestroyEffect(effect1));
        StartCoroutine(DestroyEffect(effect2));
        StartCoroutine(DestroyEffect(effect3));
        StartCoroutine(DestroyEffect(effect4));
        StartCoroutine(DestroyEffect(effect5));
        StartCoroutine(DestroyEffect(effect6));
    }
    IEnumerator DestroyEffect(GameObject effect)
    {
        yield return new WaitForSeconds(1);
        GameObject parentCanvas = GameObject.FindGameObjectWithTag("MiddleCanvas");
        effect.transform.SetParent(parentCanvas.transform);
        effect.SetActive(false);
    }
}
