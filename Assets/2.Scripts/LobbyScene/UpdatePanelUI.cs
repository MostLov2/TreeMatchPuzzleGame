using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[System.Serializable]
public class LocalSaveInMessageBox
{
    public bool isUpdateOn =    false;
    public string               saveTime;
}
public class UpdatePanelUI : MonoBehaviour
{
    [SerializeField] Text                        updatePanelText;
    [SerializeField] Text                        VersionText;
    [SerializeField] Transform                   UpdatePanelOnOff;
    [SerializeField] Transform                   updatePanelOnOff;
    [SerializeField] Toggle                      updatePanelToggle;
    public LocalSaveInMessageBox localSaveInMessageBox = new LocalSaveInMessageBox();
    public static bool checkUpdatePanelOnOff = false;
    public static string        version;
    public static UpdatePanelUI instance;
    public string filePath;
    private void Awake()
    {
        
        instance = this;
        UpdatePanelOnOff =      GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(20).transform.GetChild(0).GetComponent<Transform>();
        updatePanelText =       GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(20).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).GetComponent<Text>();
        VersionText =           GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(20).transform.GetChild(0).transform.GetChild(2).GetComponent<Text>();
        updatePanelOnOff =      GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(21).transform.GetChild(0).GetComponent<Transform>();
        updatePanelToggle =     GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(20).transform.GetChild(0).transform.GetChild(0).transform.GetChild(3).GetComponent<Toggle>();

        updatePanelText.text =  UpdateData.instance.content;
        VersionText.text =      "Version" + UpdateData.instance.version;

        filePath = Application.persistentDataPath + "/UpdatePanel.txt";
        Load();
    }

    /// <summary>
    /// UpdatePanelLoadSetting
    /// </summary>
    public void Load()
    {
        if (localSaveInMessageBox.saveTime != null)
        {
                Debug.Log("did");
            if (!File.Exists(filePath))
            {
                UpdatePanelOnOff.gameObject.SetActive(true);
                return;
            }
            string jdata = File.ReadAllText(filePath);
            localSaveInMessageBox = JsonUtility.FromJson<LocalSaveInMessageBox>(jdata);
        }
        else
        {
            localSaveInMessageBox.isUpdateOn = false;
            localSaveInMessageBox.saveTime = DateTime.Now.ToString();
            string str = JsonUtility.ToJson(localSaveInMessageBox);
            File.WriteAllText(filePath, str);
            string jdata1 = File.ReadAllText(filePath);
            localSaveInMessageBox = JsonUtility.FromJson<LocalSaveInMessageBox>(jdata1);
        }
        
        UpdatePanelOnOrOff();
    }
    void UpdatePanelOnOrOff()
    {
        DateTime dateTime = DateTime.Now;
        TimeSpan timedif = dateTime - DateTime.Parse(localSaveInMessageBox.saveTime);
        if (localSaveInMessageBox.isUpdateOn && timedif.Days == 0)
        {
            UpdatePanelOnOff.gameObject.SetActive(false);
        }
        else if (timedif.Days > 0)
        {
            UpdatePanelOnOff.gameObject.SetActive(true);
        }
    }
    /// <summary>
    /// 로컬 파일에 업데이트 판넬 하루동안 끌지 확인
    /// </summary>
    public void Save()
    {
        if (updatePanelToggle.isOn)
        {
            UpdatePanelOnOff.gameObject.SetActive(false);
            localSaveInMessageBox.isUpdateOn = true;
            localSaveInMessageBox.saveTime = DateTime.Now.ToString();
            string str = JsonUtility.ToJson(localSaveInMessageBox);
            File.WriteAllText(filePath, str);
        }
        else
        {
            UpdatePanelOnOff.gameObject.SetActive(false);
            localSaveInMessageBox.isUpdateOn = false;
        }
    }

  

    /// <summary>
    /// 버젼 안 맞을시 구글 스토어 실행
    /// </summary>
    public void GoogleStoreDownload()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.Qfora.SatoBam");
        SceneManager.LoadScene("LoginScene");
    }
    /// <summary>
    /// 판넬 업데이트 내역 및 버젼 뜨게 설정
    /// </summary>
    public void PanelUpDate()
    {
        if (UpdateData.instance.version != version)
        {
            updatePanelOnOff.gameObject.SetActive(true);
            UpdatePanelOnOff.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// SETTING PANEL에서 업데이트 내역 나오게 설정
    /// </summary>
    public void UpdatePanelOn()
    {
        UpdatePanelOnOff.gameObject.SetActive(true);
    }
    public void UpdateButton()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.Qfora.SatoBam");
    }
}
