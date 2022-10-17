using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGam : MonoBehaviour
{
    private bool isClick;
    private int count;
    public static QuitGam instance;
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            instance = null;
        }
    }

    public void Init()
    {
        Debug.Log("ButtonManager::Init");
        this.isClick = false;
    }


    private void Update()
    {
        if(Application.platform == RuntimePlatform.Android)
        {

            if (Input.GetKey(KeyCode.Escape) && !this.isClick)
            {
                this.StartCoroutine(this.CrQuitTimer());
                this.isClick = true;

                this.count++;
                Debug.Log(this.count);

                if (this.count >= 2)
                {
                    this.count = 0;
                    this.isClick = false;
                    Application.Quit();
                }
            }
        }
    }

    IEnumerator CrQuitTimer()
    {
        yield return new WaitForSeconds(0.1f);
        this.isClick = false;

        yield return new WaitForSeconds(0.3f);
        this.isClick = false;
        this.count = 0;

    }

}
