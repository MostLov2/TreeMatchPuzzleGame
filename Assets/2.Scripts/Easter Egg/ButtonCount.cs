using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCount : MonoBehaviour
{
    int         count = 0;
    [SerializeField]
    Transform   UncorrectPanel;
    [SerializeField]
    Text        UncorrectText;
    private void Awake()
    {
        UncorrectPanel =    GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(3).transform.GetChild(0).GetComponent<Transform>();
        UncorrectText =     UncorrectPanel.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();  
    }
    public void EasterEgg()
    {
        if(count <= 121)
        {
            count++;
        }
        if(count == 121)
        {
            StartCoroutine(Eagle());
            UncorrectPanel.gameObject.SetActive(true);
            UncorrectText.text = "만든 사람:" + "이진원, 채수윤, 정승용, 김영조, 이찬행"+"서위토 도리무";
        }
        else
        {
            Debug.Log("Finsh");
        }
    }
    IEnumerator Eagle()
    {
        yield return new WaitForSeconds(1f);
        UncorrectPanel.GetComponent<Animator>().SetBool("Fly", false);
    }
    public void EasterEggEnd()
    {
        StartCoroutine(Gone());
    }
    IEnumerator Gone()
    {
        UncorrectPanel.GetComponent<Animator>().SetTrigger("Gone");
        yield return new WaitForSeconds(1f);
        UncorrectPanel.gameObject.SetActive(false);
    }
}
