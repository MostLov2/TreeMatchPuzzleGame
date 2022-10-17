using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColOnOFF : MonoBehaviour
{
    float waitTime = 0;
    float delaytime = 10;
    private void Update()
    {
        waitTime += Time.deltaTime;
        GetComponent<BoxCollider2D>().enabled = false;
        if (waitTime > delaytime * Time.deltaTime)
        {
            GetComponent<BoxCollider2D>().enabled = true;
            waitTime = 0;
        }
    }
}
