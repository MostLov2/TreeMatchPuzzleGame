using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableEffect : MonoBehaviour
{
    public int effectNum;
    private void OnDisable()
    {
        if (CountDownInPuzzle.isGameStart)
        {
            GameObject effect = BlockEffectOPManager.instance.SetObject(effectNum);
            effect.transform.position = this.transform.position;
        }
    }
}
