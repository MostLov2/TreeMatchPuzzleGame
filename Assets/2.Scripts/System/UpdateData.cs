using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class VersionCheck
{
    public int update_id;
    public string version;
    public string content;
}
public class UpdateData : MonoBehaviour
{
    public VersionCheck versionCheck;
    public int update_id;
    public string version;
    public string content;
    public static UpdateData instance;
    void Awake()
    {
        instance = this;
        
    }
    public IEnumerator UpdatePanelAndVersion()
    {
        WWWForm www = new WWWForm();
        www.AddField("status", "versionCheckSato");
        using (UnityWebRequest w = UnityWebRequest.Post("https://conbox.kr/Order", www))
        {
            yield return w.SendWebRequest();
            while (!w.isDone)
            {
                Debug.Log("작동안함");
            }
            Debug.Log(w.downloadHandler.text);
            string str = w.downloadHandler.text;
            string[] charsToRemove = new string[] { "[", "]" };
            foreach (var c in charsToRemove)
            {
                str = str.Replace(c, string.Empty);
            }
            versionCheck = JsonUtility.FromJson<VersionCheck>(str);
            update_id = versionCheck.update_id;
            version = versionCheck.version;
            content = versionCheck.content;
            content = content.Replace("@", "@" + System.Environment.NewLine);
            string[] charToRemoveGol = new string[] { "@" };
            foreach (var c in charToRemoveGol)
            {
                content = content.Replace(c, string.Empty);
            }
        }
    }
}
