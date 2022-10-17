//using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;

public class AutoLogin
{
    public string id = "";
    public string password = "";
}
public class ButtonManager : MonoBehaviour
{
    public AutoLogin autoLogin = new AutoLogin();
    public Button ExitButton;
    public Button LoginButton;
    public Transform LoginPanel;
    public static ButtonManager instance;
    public string filePath;
    private void Awake()
    {
        instance = this; 
        LoginPanel = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(1).transform.GetChild(0).GetComponent<Transform>();
        filePath = Application.persistentDataPath + "/AutoLogin.txt";
        
        ExitButton.onClick.AddListener(() =>
        {
            LoginPanel.gameObject.SetActive(false);
        });
        LoginButton.onClick.AddListener(() =>
        {
            Debug.Log(autoLogin.id);
            Debug.Log(autoLogin.password);
            AutoLoad();
        });
    }
    public void AutoSave(string inputid,string inputpassword)
    {
        Debug.Log(inputid);
        Debug.Log(inputpassword);
        autoLogin.id = inputid;
        autoLogin.password = inputpassword;
        string idPass = JsonUtility.ToJson(autoLogin);
        File.WriteAllText(filePath, idPass);
    }
    public void AutoLoad()
    {
        if (autoLogin.id == "")
        {
            LoginPanel.gameObject.SetActive(true);
        }
        if (!File.Exists(filePath)) { return; }
        string jdata = File.ReadAllText(filePath);
        autoLogin = JsonUtility.FromJson<AutoLogin>(jdata);
        Debug.Log(jdata);
        if (autoLogin.id != "")
        {
            StartCoroutine(Main.instance.Web.LoginOnOff(autoLogin.id, autoLogin.password));
        }
    }
}
