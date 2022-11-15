using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AllDestoryBlock : MonoBehaviour
{
    public static AllDestoryBlock Instance;
    public Sprite spray;
    public Sprite stick;
    public Image turnItem1;
    public Image turnItem2;

    private void Awake()
    {
        Instance = this;
        spray = Resources.Load<Sprite>("AllDestroyBlock/Spray");
        stick = Resources.Load<Sprite>("AllDestroyBlock/Stick");
        turnItem1 = GameObject.FindGameObjectWithTag("MiddleCanvas").transform.GetChild(5).GetChild(2).GetChild(0).GetChild(0).GetComponent<Image>();
        turnItem2 = GameObject.FindGameObjectWithTag("MiddleCanvas").transform.GetChild(5).GetChild(2).GetChild(0).GetChild(1).GetComponent<Image>();
    }
    /// <summary>
    /// stickOrSpray true = stick false  = spray
    /// </summary>
    /// <param name="stickOrSpray"></param>
    /// <param name="stickOrSpray1"></param>
    public void DestroyAllBlock(bool stickOrSpray, bool stickOrSpray1)
    {
        if (stickOrSpray)
        {
            turnItem1.sprite = stick;
        }
        else
        {
            turnItem1.sprite = spray;

        }
        if (stickOrSpray1)
        {
            turnItem2.sprite = stick;
        }
        else
        {
            turnItem2.sprite = spray;
        }
        GameObject.FindGameObjectWithTag("MiddleCanvas").transform.GetChild(5).gameObject.SetActive(true);
        for (int i = 0; i < TileManager.instance.tiles.Length; i++)
        {
            TileManager.instance.tiles[i].GetComponent<Tile>().block.transform.DOMove(turnItem1.transform.parent.position, 0.3f);
        }
        StartCoroutine(DelayEffect());
    }
    IEnumerator DelayEffect()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < TileManager.instance.tiles.Length; i++)
        {
            TileManager.instance.tiles[i].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
            TileManager.instance.tiles[i].GetComponent<Tile>().block.SetActive(false);
            TileManager.instance.tiles[i].GetComponent<Tile>().isEmpty = true;
            TileManager.instance.tiles[i].GetComponent<Tile>().block = null;
        }
    }
}
