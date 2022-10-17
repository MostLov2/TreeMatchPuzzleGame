using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownInPuzzle : MonoBehaviour
{
    Text countDownText;
    public Transform highCountDownPanel;
    
    public static bool isGameStart = false;
    public AudioClip[] clip;
    public static int StartCount = 0;
    private void Awake()
    {
        countDownText = GetComponent<Text>();
        highCountDownPanel = GameObject.FindGameObjectWithTag("HighCanvas").transform.GetChild(0).GetComponent<Transform>();
        
        isGameStart = false;
    }
    public void FadeOutPanel()
    {
        StartCount++;
        highCountDownPanel.GetComponent<Animator>().SetTrigger("FadeOut");
    }
    public void Ready()
    {
        countDownText.text = "Ready";
        countDownText.fontSize = 200;
        Time.timeScale = 0;
        isGameStart = false;
        SoundManager.instance.PlaySFX(clip, 0, 1, 1);
    }
    public void One()
    {
        countDownText.text = "1";
        countDownText.fontSize = 300;
    }
    public void Two()
    {
        countDownText.text = "2";
        countDownText.fontSize = 300;
    }
    public void Tree()
    {
        countDownText.text = "3";
        countDownText.fontSize = 300;
    }
    public void EndCountDown()
    {
        Time.timeScale = 1;
        isGameStart = true;
        gameObject.SetActive(false);
    }
    
}
