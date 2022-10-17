using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeImageChange : MonoBehaviour
{
    TreeStatus treeStatus;
    Sprite treeLevel9;
    Sprite treeLevel19;
    Sprite treeLevel29;
    Sprite treeLevel39;
    Sprite treeLevel49;
    Sprite treeLevel50;
    private void Awake()
    {
        treeStatus = GetComponent<TreeStatus>();
        treeLevel9 = Resources.Load<Sprite>("TreeImage/tree1");
        treeLevel19 = Resources.Load<Sprite>("TreeImage/tree2");
        treeLevel29 = Resources.Load<Sprite>("TreeImage/tree3");
        treeLevel39 = Resources.Load<Sprite>("TreeImage/tree4");
        treeLevel49 = Resources.Load<Sprite>("TreeImage/tree5");
        treeLevel50 = Resources.Load<Sprite>("TreeImage/tree6");
    }
    /// <summary>
    /// 레벨에 따른 나무 이미지 변경
    /// </summary>
    public void TreeImageChangeFollowedTreeLevel()
    {
        if (treeStatus != null)
        {
            Sprite treeImage = null;
            if (treeStatus.TreeLevel <= 9)
            {
                treeImage = treeLevel9;
            }
            else if (treeStatus.TreeLevel <= 19)
            {
                treeImage = treeLevel19;
            }
            else if (treeStatus.TreeLevel <= 29)
            {
                treeImage = treeLevel29;
            }
            else if (treeStatus.TreeLevel <= 39)
            {
                treeImage = treeLevel39;
            }
            else if (treeStatus.TreeLevel <= 49)
            {
                treeImage = treeLevel49;
            }
            else
            {
                treeImage = treeLevel50;
            }
            treeStatus.treeImage.sprite = treeImage;
            treeStatus.treeImageSprite.sprite = treeImage;
        }
        else
        {
            Debug.Log("TreeStatus is null");
        }
    }

}
