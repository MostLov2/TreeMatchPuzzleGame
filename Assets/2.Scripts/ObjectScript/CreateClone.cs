using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CreateClone : MonoBehaviour
{
    public void CreateCloneInGame()
    {
        GameObject ninjaRabbit = OPManager.instance.SetObject("NinjaRabbit");
        ninjaRabbit.transform.position = transform.position;
        ninjaRabbit.GetComponent<SpriteRenderer>().color = Color.black;
        ninjaRabbit.GetComponent<SpriteRenderer>().DOColor(Color.white, 1f);
        ninjaRabbit.transform.DOMove((transform.position + new Vector3(3, 0, 0)), 1f, false);

    }
}
