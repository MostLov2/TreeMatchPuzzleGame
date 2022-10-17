using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRandomNum : MonoBehaviour
{
    public static int CreateNum(int MaxNum)
    {
        int randomNum = Random.Range(0, MaxNum);
        return randomNum;
    }
}
