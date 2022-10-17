using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquirrelEffect : MonoBehaviour
{
    float value = 0;
    public float speed;
    public bool isOn;
    public int SwapCount = 0;
    private void OnEnable()
    {
        isOn = true;
        GetComponent<Image>().fillAmount = 0;
        transform.GetChild(0).GetComponent<Image>().fillAmount = 0;
        SwapCount = 0;
        value = 0;
    }
    private void OnDisable()
    {
        isOn = false;
        GetComponent<Image>().fillAmount = 0;
        transform.GetChild(0).GetComponent<Image>().fillAmount = 0;
        SwapCount = 0;
        value = 0;
    }
    private void Update()
    {
        if (isOn)
        {
            if (value <= 1)
            {
                value += Time.deltaTime * speed;
            }
        }
        else
        {
            value = 0;
        }
        GetComponent<Image>().fillAmount = value;
        transform.GetChild(0).GetComponent<Image>().fillAmount = value;
    }
}
