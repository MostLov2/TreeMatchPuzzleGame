using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    //-------------SlotInfo------------------------
    Text                treeLevelT;
    Image               slotImage;
    int                 treeLevel;
    SpriteRenderer      targetImage;
    Transform           targetTransform;
    //-------------Panel------------------------
    Transform           SetTreeNoticPanel;
    //-------------SingleTurn------------------------
    public static Slot  instance;
    //-------------Bool------------------------
    public static bool  isSetOn;
    Transform           BlackPanel;
    Transform           InventoryPanel;
    void Start()
    {
        instance =          this;
        slotImage =         transform.GetChild(0).GetComponent<Image>();
        treeLevelT =        transform.GetChild(1).transform.GetChild(0).GetComponent<Text>();
        targetTransform =   transform.GetChild(1).GetComponent<Transform>();
        BlackPanel = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).GetComponent<Transform>();
        InventoryPanel = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(3).transform.GetChild(0).GetComponent<Transform>();
        SetTreeNoticPanel = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(8).transform.GetChild(0).GetComponent<Transform>();
        if (transform.childCount >= 3)
        {
            targetImage =   transform.GetChild(2).GetComponent<SpriteRenderer>();
            treeLevel =     transform.GetChild(2).GetComponent<TreeStatus>().TreeLevel;

            SetSlot();
        }
    }
    void Update()
    {
        TreeSlotUpdate();
    }
    /// <summary>
    /// Slot 업데이트
    /// </summary>
    void TreeSlotUpdate()
    {
        if (transform.childCount >= 3)
        {
            targetImage = transform.GetChild(2).GetComponent<SpriteRenderer>();
            treeLevel = transform.GetChild(2).GetComponent<TreeStatus>().TreeLevel;
            targetTransform.gameObject.SetActive(true);
            SetSlot();
        }
        else if (transform.childCount < 3)
        {
            targetTransform.gameObject.SetActive(false);
            //slotImage.color = new Color(255, 255, 255, 0);
            treeLevelT.text = null;
        }
    }
    /// <summary>
    /// 나무 심었을때 업데이트
    /// </summary>
    public void SetSlot()
    {
        //slotImage.color = new Color(255, 255, 255, 255);
        //slotImage.sprite = targetImage.sprite;
        treeLevelT.text = "Lv " + treeLevel;
    }
    /// <summary>
    /// Slot 터치시 심을지 물어보는 판넬
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if(transform.childCount >= 3)
            {
                SetTreeNoticPanel.gameObject.SetActive(true);
                isSetOn = true;
                BlackPanel.gameObject.SetActive(false);
                InventoryPanel.gameObject.SetActive(false);
                TreeSetManager.treeName = transform.GetChild(2).GetComponent<TreeStatus>().TreeName;
            }
        }
    }
}
