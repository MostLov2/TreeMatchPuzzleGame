using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] Camera mainCam;
    [SerializeField] Camera miniGameCam;
    [SerializeField] Camera tutorialCam;
    [SerializeField] Canvas canvas;
    private void Awake()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        miniGameCam = GameObject.FindGameObjectWithTag("MiniGameCam").GetComponent<Camera>();
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        StartCoroutine(Upcol());
    }
    IEnumerator Upcol()
    {
        while (true)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            CamChange();
        }
    }
    void CamChange()
    {
        if (MiniGameManager.isMiniGameStart)
        {
            mainCam.depth = 0;
            miniGameCam.depth = 2;
            mainCam.gameObject.GetComponent<AudioListener>().enabled = false;
            miniGameCam.gameObject.GetComponent<AudioListener>().enabled = true;
            canvas.worldCamera = miniGameCam;
        }
        if (!MiniGameManager.isMiniGameStart)
        {
            miniGameCam.depth = 1;
            mainCam.depth = 2;
            mainCam.gameObject.GetComponent<AudioListener>().enabled = true;
            miniGameCam.gameObject.GetComponent<AudioListener>().enabled = false;
            canvas.worldCamera = mainCam;
        }
    }
}
