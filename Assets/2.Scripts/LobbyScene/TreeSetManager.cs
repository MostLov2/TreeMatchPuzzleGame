using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TreeSetManager : MonoBehaviour
{
    //---------------TreeObject-----------------------
    public static GameObject[]      treeObject;
    //---------------TreeSetPoint-----------------------
    public Transform                treeSetPoint;
    public Transform[]              treeSetPoints;
    public List<GameObject>         setTreePoints;
    //---------------TreeSetPoint-----------------------
     [SerializeField]GameObject[]                   slot;
    //---------------Panel-----------------------
     Transform                      SetPosPanel;
     Transform                      SetinventoryPanel;
     Transform                      AlreadySetPanel;
     Transform                      ButtonPanel;
     Transform                      BlackPanel;
     Transform                      NotEnoughChestnutPanel;
    //---------------Button-----------------------
    [SerializeField]Transform       ResetButtons;
    [SerializeField]Transform       ResetOffButton;
    [SerializeField] Button[] ResetButton;
    List<Button> buttonList = new List<Button>();
    [SerializeField]Text StatusResetButton;
    //----------------Count----------------------
    int count = 0;
    //----------------TreeInfo----------------------
    public static int               treeName;
    string                          status;
    //----------------Status Image----------------------------
    Sprite statusImage1;
    Sprite statusImage2;
    Sprite statusImage3;
    Sprite statusImage4;
    Sprite statusImage5;
    Sprite statusImage6;
    Sprite statusImage7;
    Sprite statusImage8;
    Sprite statusImage9;
    Sprite statusImage10;
    Sprite statusImage11;
    //----------------SingleTurn----------------------
    public static TreeSetManager    instance;
    void Awake()
    {
        instance = this;
        if(instance == null)
            Destroy(gameObject);
        statusImage1 =              Resources.Load<Sprite>("StatusColor/birdMovementSpeed");
        statusImage2 =              Resources.Load<Sprite>("StatusColor/chestnutAppearanceRate");
        statusImage3 =              Resources.Load<Sprite>("StatusColor/chestnutHarvest");
        statusImage4 =              Resources.Load<Sprite>("StatusColor/doubleTheChestnutHarvest");
        statusImage5 =              Resources.Load<Sprite>("StatusColor/FevertimeAutomation");
        statusImage6 =              Resources.Load<Sprite>("StatusColor/FeverTimeIncrease");
        statusImage7 =              Resources.Load<Sprite>("StatusColor/IncreasedFeverTimeRewards");
        statusImage8 =              Resources.Load<Sprite>("StatusColor/increaseGameTime");
        statusImage9 =              Resources.Load<Sprite>("StatusColor/MonsterRegenerationRate");
        statusImage10 =             Resources.Load<Sprite>("StatusColor/ReductionOfLevelUpFertilizerRequirement");
        statusImage11 =             Resources.Load<Sprite>("StatusColor/WhistleOverallGaugeReduction");
        treeObject =                GameObject.FindGameObjectsWithTag("Tree");
        treeSetPoint =              GameObject.FindGameObjectWithTag("TreeSetPoint").transform;
        treeSetPoints =             treeSetPoint.GetComponentsInChildren<Transform>();
        SetPosPanel =               GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(9).transform.GetChild(0);
        SetinventoryPanel =         GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(10).transform.GetChild(0);
        AlreadySetPanel =           GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(11).transform.GetChild(0);
        ButtonPanel =               GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(1).transform.GetChild(0);
        BlackPanel =                GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).GetComponent<Transform>();
        NotEnoughChestnutPanel =    GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(19).transform.GetChild(0).GetComponent<Transform>();
        ResetButtons =              GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(5).GetChild(0).GetChild(0).GetChild(3).GetChild(3).GetComponent<Transform>();
        ResetOffButton =            GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(5).GetChild(0).GetChild(3).GetComponent<Transform>();
        ResetButton =               GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(5).GetChild(0).GetChild(0).GetChild(3).GetChild(3).GetComponentsInChildren<Button>();
        StatusResetButton =         GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(5).GetChild(0).GetChild(1).GetChild(2).GetChild(1).GetComponent<Text>();

        for (int i = 0; i < ResetButton.Length; i++)
        {
            buttonList.Add(ResetButton[i]);
        }
        for (int i = 1; i < treeSetPoints.Length; i++)
        {
            setTreePoints.Add(treeSetPoints[i].gameObject);
        }

        SetTreePos();
    }
    public void UpdateStatePotion()
    {
        StatusResetButton.text = ItemManager.statusPotionCount.ToString();
        StartCoroutine(ItemManager.instance.SetIStatusResetPotion());
    }
    /// <summary>
    /// 나무 위치 인벤토리 LobbyScene으로 변경 함수
    /// </summary>*/
    public void SetTreePos()
    {
        for (int i = 0; i < treeObject.Length; i++)
        {
            if (treeObject[i].GetComponent<TreeStatus>().TreePosition != 0)
            {
                treeObject[i].GetComponent<ChangeStatusChestnut>().SetFarmTree();
                SetPosition(i);
            }
            if (!treeObject[i].transform.parent.CompareTag("Slot"))
            {
                if (treeObject[i].GetComponent<TreeStatus>().TreePosition == 0)
                {
                    SetInventory1(i);
                }
            }
        }
    }
    /// <summary>
    /// LobbyScene위치 변경
    /// </summary>
    /// <param name="i"></param>
    public void SetPosition(int i)
    {
        treeObject[i].transform.position = transform.position;
        treeObject[i].transform.SetParent(setTreePoints[treeObject[i].GetComponent<TreeStatus>().TreePosition - 1].transform);
        treeObject[i].transform.position = setTreePoints[treeObject[i].GetComponent<TreeStatus>().TreePosition - 1].transform.position;
        if (treeObject[i].GetComponent<TreeStatus>().TreePosition == 1 || treeObject[i].GetComponent<TreeStatus>().TreePosition == 2)
        {
            treeObject[i].transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        }
        else if (treeObject[i].GetComponent<TreeStatus>().TreePosition == 3 || treeObject[i].GetComponent<TreeStatus>().TreePosition == 4 || treeObject[i].GetComponent<TreeStatus>().TreePosition == 5)
        {
            treeObject[i].transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }
        if (treeObject[i].GetComponent<TreeStatus>().TreePosition == 6 || treeObject[i].GetComponent<TreeStatus>().TreePosition == 7||treeObject[i].GetComponent<TreeStatus>().TreePosition == 8)
        {
            treeObject[i].transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        }
        if (treeObject[i].GetComponent<TreeStatus>().TreePosition == 9 || treeObject[i].GetComponent<TreeStatus>().TreePosition == 10)
        {
            treeObject[i].transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        }
    }
    /// <summary>
    /// 인벤토리 이동 함수
    /// </summary>
    /// <param name="i"></param>
    void SetInventory1(int i)
    {
        if (slot[count].transform.childCount == 2)
        {
            treeObject[i].transform.SetParent(slot[count].transform);
            treeObject[i].transform.position = slot[count].transform.position;
            treeObject[i].GetComponent<ChangeStatusChestnut>().SetInventoryTree();
            count = 0;
            
        }
        else if(slot[count].transform.childCount == 3&&count <=21 )
        {
            count++;
            SetInventory1(i);
        }
    }
    /// <summary>
    /// 나무 위치 DB저장
    /// </summary>
    public void SetInventory()
    {
        if(MySqlSystem.chestnutPoint >= 500)
        {
            for (int i = 0; i < treeObject.Length; i++)
            {
                if (treeObject[i].GetComponent<TreeStatus>().TreeName == treeName)
                {
                    FindTree(0);
                    SetTreePos();
                    treeObject[i].GetComponent<TreePositionChange>().ChangeTreePosition(0);
                    treeObject[i].GetComponent<ChangeStatusChestnut>().SetInventoryTree();
                    StartCoroutine(MySqlSystem.instance.SetTreePosition(0, treeName));
                    StartCoroutine(MySqlSystem.instance.GetTree1());
                    MySqlSystem.chestnutPoint -= 500;
                    StartCoroutine(MySqlSystem.instance.Setchestnut(MySqlSystem.chestnutPoint));
                    SetinventoryPanel.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            NotEnoughChestnutPanel.gameObject.SetActive(true);
        }
    }
    public void NotEnoughChestnutPanelOff()
    {
        NotEnoughChestnutPanel.gameObject.SetActive(false);
    }
    /// <summary>
    /// 이미 심어져 있는 곳은 못심게 막는 함수
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    bool ButtonOnOff(int num)
    {
        List<int> list = new List<int>();   
        List<int> nolist = new List<int>();
        list.Clear();
        nolist.Clear();
        for (int i = 0; i < treeObject.Length; i++)
        {
            if(treeObject[i].GetComponent<TreeStatus>().TreePosition == num)
            {
                list.Add(treeObject[i].GetComponent<TreeStatus>().TreePosition);
            }
            if (treeObject[i].GetComponent<TreeStatus>().TreePosition != num)
            {
                nolist.Add(treeObject[i].GetComponent<TreeStatus>().TreePosition);
            }
        }
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == num)
            {
                AlreadySetPanel.gameObject.SetActive(true);
                return true;
            }
        }
        for (int i = 0; i < nolist.Count; i++)
        {
            if (nolist[i] == num)
            {
                AlreadySetPanel.gameObject.SetActive(true);
                return false;
            }
        }
        return false;
    }
    /// <summary>
    /// 못심게 하는 판넬 off
    /// </summary>
    public void AlreadySetPanelOff()
    {
        AlreadySetPanel.gameObject.SetActive(false);
    }
    #region SetButton
    public void Button1()
    {
        if (!ButtonOnOff(1))
        {
            SetPosPanel.gameObject.SetActive(false);
            FindTree(1);
            StartCoroutine(MySqlSystem.instance.SetTreePosition(1, treeName));
            StartCoroutine(MySqlSystem.instance.GetTree1());
            BlackPanel.gameObject.SetActive(false);
            SetTreePos();
        }
    }
    public void Button2()
    {
        if (!ButtonOnOff(2))
        {
            SetPosPanel.gameObject.SetActive(false);
            FindTree(2);
            StartCoroutine(MySqlSystem.instance.SetTreePosition(2, treeName));
            StartCoroutine(MySqlSystem.instance.GetTree1());
            BlackPanel.gameObject.SetActive(false);
            SetTreePos();
        }
    }
    public void Button3()
    {
        if (!ButtonOnOff(3))
        {
            SetPosPanel.gameObject.SetActive(false);
            FindTree(3);
            StartCoroutine(MySqlSystem.instance.SetTreePosition(3, treeName));
            StartCoroutine(MySqlSystem.instance.GetTree1());
            BlackPanel.gameObject.SetActive(false);
            SetTreePos();
        }
    }
    public void Button4()
    {
        if (!ButtonOnOff(4))
        {
            SetPosPanel.gameObject.SetActive(false);
            FindTree(4);
            StartCoroutine(MySqlSystem.instance.SetTreePosition(4, treeName));
            StartCoroutine(MySqlSystem.instance.GetTree1());
            BlackPanel.gameObject.SetActive(false);
            SetTreePos();
        }
    }
    public void Button5()
    {
        if (!ButtonOnOff(5))
        {
            SetPosPanel.gameObject.SetActive(false);
            FindTree(5);
            StartCoroutine(MySqlSystem.instance.SetTreePosition(5, treeName));
            StartCoroutine(MySqlSystem.instance.GetTree1());
            BlackPanel.gameObject.SetActive(false);
            SetTreePos();
        }
    }
    public void Button6()
    {
        if (!ButtonOnOff(6))
        {
            SetPosPanel.gameObject.SetActive(false);
            FindTree(6);
            StartCoroutine(MySqlSystem.instance.SetTreePosition(6, treeName));
            StartCoroutine(MySqlSystem.instance.GetTree1());
            BlackPanel.gameObject.SetActive(false);
            SetTreePos();
        }
    }
    public void Button7()
    {
        if (!ButtonOnOff(7))
        {
            SetPosPanel.gameObject.SetActive(false);
            FindTree(7);
            StartCoroutine(MySqlSystem.instance.SetTreePosition(7, treeName));
            StartCoroutine(MySqlSystem.instance.GetTree1());
            BlackPanel.gameObject.SetActive(false);
            SetTreePos();
        }
    }
    public void Button8()
    {
        if (!ButtonOnOff(8))
        {
            SetPosPanel.gameObject.SetActive(false);
            FindTree(8);
            StartCoroutine(MySqlSystem.instance.SetTreePosition(8, treeName));
            StartCoroutine(MySqlSystem.instance.GetTree1());
            BlackPanel.gameObject.SetActive(false);
            SetTreePos();
        }
    }
    public void Button9()
    {
        if (!ButtonOnOff(9))
        {
            SetPosPanel.gameObject.SetActive(false);
            FindTree(9);
            StartCoroutine(MySqlSystem.instance.SetTreePosition(9, treeName));
            StartCoroutine(MySqlSystem.instance.GetTree1());
            BlackPanel.gameObject.SetActive(false);
            SetTreePos();
        }
    }
    public void Button10()
    {
        if(!ButtonOnOff(10))
        {
            SetPosPanel.gameObject.SetActive(false);
            FindTree(10);
            StartCoroutine(MySqlSystem.instance.SetTreePosition(10, treeName));
            StartCoroutine(MySqlSystem.instance.GetTree1());
            BlackPanel.gameObject.SetActive(false);
            SetTreePos();
        }
    }
    #endregion
    /// <summary>
    /// 나무 위치 변경 이후에 SortingOrder값 변경하는 함수
    /// </summary>
    /// <param name="n"></param>
    void FindTree(int n)
    {
        ButtonPanel.gameObject.SetActive(true);
        for (int i = 0; i < treeObject.Length; i++)
        {
            if(treeObject[i].GetComponent<TreeStatus>().TreeName == treeName)
            {
                treeObject[i].GetComponent<TreeStatus>().TreePosition = n;
                treeObject[i].GetComponent<SpriteRenderer>().sortingOrder = n;
                treeObject[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = n+1;
                treeObject[i].transform.GetChild(0).transform.GetChild(0).GetComponent<Canvas>().sortingOrder = n+2;
            }
        }
    }
    public void ResetTreeStatusButton()
    {
        if (ItemManager.statusPotionCount > 0)
        {
            int ButtonCount = 0;
            Debug.Log(treeName);
            for (int i = 0; i < treeObject.Length; i++)
            {
                if (treeObject[i].GetComponent<TreeStatus>().TreeName == treeName)
                {
                    Debug.Log(i);
                    Debug.Log(treeName);
                    SetStatus(i, ButtonCount);
                }
            }
        }
            
    }
    public void ResetTreeStatusOffButton()
    {
        ResetButtons.gameObject.SetActive(false);
        ResetOffButton.gameObject.SetActive(false);
    }

    void SetStatus(int treeNum, int ButtonCount)
    {

        ResetButtons.gameObject.SetActive(true);
        ResetOffButton.gameObject.SetActive(true);
    }
}
