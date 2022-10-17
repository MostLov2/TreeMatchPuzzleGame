using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
[Serializable]
public class IsDidTutorial
{
    public bool LobbyTutorial = false;
    public bool ShopTutorial = false;
    public bool GameyTutorial = false;
}
public class TutorialCheck : MonoBehaviour
{
    public IsDidTutorial isDidTutorial = new IsDidTutorial();
    public string filePath;
    private void Awake()
    {
        filePath = Application.persistentDataPath + "/TutorialChecker.txt";
        Load();
    }
    public void Load()
    {
        if (!File.Exists(filePath)) { return; }
        string jdata = File.ReadAllText(filePath);
        if(jdata == "")
        {
            string str = JsonUtility.ToJson(isDidTutorial);
            File.WriteAllText(filePath, str);
            Debug.Log(str);
            string jdata1 = File.ReadAllText(filePath);
            isDidTutorial = JsonUtility.FromJson<IsDidTutorial>(jdata1);
        }
        Debug.Log(jdata);
        isDidTutorial = JsonUtility.FromJson<IsDidTutorial>(jdata);
    }
    public void LobbyTutorialSave()
    {
        isDidTutorial.LobbyTutorial = true;
        string str = JsonUtility.ToJson(isDidTutorial);
        File.WriteAllText(filePath, str);
    }
    public void ShopTutorialSave()
    {
        isDidTutorial.ShopTutorial = true;
        string str = JsonUtility.ToJson(isDidTutorial);
        File.WriteAllText(filePath, str);
    }
    public void Game1TutorialSave()
    {
        isDidTutorial.GameyTutorial = true;
        string str = JsonUtility.ToJson(isDidTutorial);
        File.WriteAllText(filePath, str);
    }
}
