using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

[System.Serializable]
public class UpdatePanel : MonoBehaviour
{
    public static UpdatePanel instance;
    private void Awake()
    {
        instance = this;
        VersionUpdateLate();
    }
    void VersionUpdateLate()
    {
        UpdatePanelUI.version = Application.version;
        UpdatePanelUI.instance.PanelUpDate();
    }
}




