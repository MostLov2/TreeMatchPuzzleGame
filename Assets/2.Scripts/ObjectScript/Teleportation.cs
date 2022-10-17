using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Teleportation : MonoBehaviour
{
    Animals animals;
    private void Awake()
    {
        animals = GetComponent<Animals>();
    }
    public void TeleportPoint()
    {
        int RandomNum = Random.Range(0, animals.MovePoint.Length);
        transform.position = animals.MovePoint[RandomNum].transform.position;
    }
    public void TelpoEffect()
    {
        GameObject telpoEffect = EffectOpManager.instance.SetObject("Telpo");
        telpoEffect.transform.position = transform.GetChild(0).transform.position;
        SoundManager.instance.PlaySFX(animals.clip, 4, 1, 1);
    }
    public void TelpoWood()
    {
        SoundManager.instance.PlaySFX(animals.clip, 5, 1, 1);
        GameObject telpoEffect = OPManager.instance.SetObject("TelpoWood");
        telpoEffect.transform.position = transform.position;
        telpoEffect.transform.rotation = transform.rotation;
        telpoEffect.transform.DOMove(transform.position += new Vector3(0, -3, 0), 1, false);
        telpoEffect.transform.DORotate(new Vector3(0, 0, -90), 1);
    }
}
