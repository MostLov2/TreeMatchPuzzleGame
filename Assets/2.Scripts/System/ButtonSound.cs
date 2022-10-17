using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] AudioClip[] clip;
    public void ButtonSoundOn(int count)
    {
        SoundManager.instance.PlaySFX(clip, count, 1, 1);
    }
}
