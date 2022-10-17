using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public static string loginID;
    public static string password;
    public InputField UsernameInput;
    public InputField PasswordInput;
    public Button LoginButton;
    public Button RegisterButton;
    public Button ManualButton;
    
    void Awake()
    {
        
        LoginButton.onClick.AddListener(() =>
        {
            ButtonManager.instance.AutoSave(UsernameInput.text, PasswordInput.text);
            StartCoroutine(Main.instance.Web.LoginOnOff(UsernameInput.text, PasswordInput.text));
            loginID = UsernameInput.text;
            Debug.Log("Login");
            //SoundManager.instance.DestroyClip();
        });
        RegisterButton.onClick.AddListener(() =>
        {
            Application.OpenURL("https://conbox.kr/gameusers");
        });
        ManualButton.onClick.AddListener(() =>
        {
            Application.OpenURL("https://www.topbam.kr/manual");
        });

    }
}
