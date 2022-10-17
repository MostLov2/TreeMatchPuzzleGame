using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main instance;
    public MySqlSystem Web;
    void Start()
    {
        instance =  this;
        Web = GameObject.Find("Main").GetComponent<MySqlSystem>();  
    }

}
