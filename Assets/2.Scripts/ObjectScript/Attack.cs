using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Attack : MonoBehaviour
{
    Animals animals;
    Vector3 Pos;
    public bool isAttack = false;
    public int count = 0;
    private void Awake()
    {
        animals = GetComponent<Animals>();
    }
    public void AttackChestnutLeft()
    {
        count = 1;
        Pos = transform.position;
        Pos += new Vector3(10, 0, 0);
        transform.DOMoveX(Pos.x, 0.1f).SetEase(Ease.OutQuad);
        transform.DOMoveY(Pos.y, 0.1f).SetEase(Ease.InQuad);
        isAttack = true;
        SoundManager.instance.PlaySFX(animals.clip, 3, 1, 1);
    }
    public void AttackChestnutRight()
    {
        Pos += new Vector3(-10, 0, 0);
        transform.DOMoveX(Pos.x, 0.1f).SetEase(Ease.InQuad);
        transform.DOMoveY(Pos.y, 0.1f).SetEase(Ease.OutQuad);
        isAttack = false;
        Debug.Log("Work");
        count = 0;
    }
    public void CountNum()
    {
       
    }
}
