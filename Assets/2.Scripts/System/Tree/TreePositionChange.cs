using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePositionChange : MonoBehaviour
{
    TreeStatus status;
    void Awake()
    {
        status = GetComponent<TreeStatus>();
    }
    public void ChangeTreePosition(int positinNum)
    {
        Debug.Log("1");
        if (status != null)
        {
            Debug.Log("2");
            status.TreePosition = positinNum;
        }
        else
        {
            Debug.LogError("Does't set TreeStatuas in TreePositionChange");
        }
    }
}
