using System.Collections;
using System.Collections.Generic;
using System.Management.Instrumentation;
using UnityEngine;
using UnityEngine.UI;

public class ComboSystem : MonoBehaviour
{
    public Sprite[] comboText;
    public SpriteRenderer comboImage;
    public static ComboSystem instance;
    public AudioClip[] clip;
    private void Awake()
    {
        instance = this;
        comboText = Resources.LoadAll<Sprite>("ComboText");
        comboImage = GameObject.FindGameObjectWithTag("MiddleCanvas").transform.GetChild(4).GetComponent<SpriteRenderer>();
    }
    /// <summary>
    /// 콤보 발생시 나오는 Text 동작 애니메이션 
    /// </summary>
    public void ComboTextUp()
    {
        if (TileManager.instance.comboCount >= 7)
        {
            comboImage.sprite = comboText[0];
            comboImage.gameObject.SetActive(true);
            SoundManager.instance.PlaySFX(clip, 0, 1, 1);
        }
        else if (TileManager.instance.comboCount >= 4)
        {
            comboImage.sprite = comboText[2];
            comboImage.gameObject.SetActive(true);
            SoundManager.instance.PlaySFX(clip, 1, 1, 1);
        }
        else if (TileManager.instance.comboCount >= 2)
        {
            comboImage.sprite = comboText[1];
            comboImage.gameObject.SetActive(true);
            SoundManager.instance.PlaySFX(clip, 2, 1, 1);
        }
        else
        {
            comboImage.gameObject.SetActive(false);
        }
        
    }
}
