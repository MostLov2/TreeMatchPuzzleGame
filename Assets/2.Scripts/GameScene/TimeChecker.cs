using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChecker : MonoBehaviour
{
    [SerializeField]AudioClip[] clip;
    private void OnEnable()
    {
        SoundManager.instance.PlaySFX(clip, 0, 1, 1);
        StartCoroutine(OFF());
    }
    IEnumerator OFF()
    {
        yield return new WaitForSeconds(5f);
        SoundManager.instance.PlaySFX(clip, 1, 1, 1);
    }
}
