using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchGameTutorial : MonoBehaviour
{
    TutorialCheck tutorialCheck;
    Transform HelpPanel;
    private void Awake()
    {
        tutorialCheck = GameObject.FindGameObjectWithTag("Main").GetComponent<TutorialCheck>();
        HelpPanel = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(10).GetChild(0).GetComponent<Transform>();
    }
    private void Start()
    {
        if (!tutorialCheck.isDidTutorial.GameyTutorial)
        {
            HelpPanel.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            HelpPanel.gameObject.SetActive(false);
        }
    }
    public void GameTutorialEnd()
    {
        tutorialCheck.Game1TutorialSave();
        Time.timeScale = 1;
    }
}
