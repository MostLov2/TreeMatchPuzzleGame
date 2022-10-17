using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EagleGauge : MonoBehaviour
{
    public static int eagleGauge = 0;
    public int eagleGaugeInit = 100;
    Transform Eagle;
    public AudioClip[] clip;
    public static EagleGauge instance;
    private void Awake()
    {
        instance = this;
        Eagle = GameObject.FindGameObjectWithTag("MiddleCanvas").transform.GetChild(1).GetChild(3).GetChild(1).GetChild(1).GetComponent<Transform>();   
    }
    public void EagleItem()
    {
        StartCoroutine(CreateEagleBlock());
    }
    public IEnumerator CreateEagleBlock()
    {
        while (eagleGauge >= eagleGaugeInit)
        {
            eagleGauge -= eagleGaugeInit;   
            int RandomBlockSelect = TileManager.instance.randomBlockNumMake();
            Eagle.gameObject.SetActive(true);
            SoundManager.instance.PlaySFX(clip, 0, 1, 1);
            Eagle.transform.DOMove(TileManager.instance.tiles[RandomBlockSelect].transform.position, 1f);
            yield return new WaitForSeconds(1f);
            Eagle.transform.position = Eagle.transform.parent.transform.position;
            Eagle.gameObject.SetActive(false);
            EagleBlockChange(RandomBlockSelect);
        }
    }
    public void EagleBlockChange(int RandomBlockSelect)
    {
        GameObject eagleBlock = OPBlock.instance.SetObject(14);
        eagleBlock.transform.position = TileManager.instance.tiles[RandomBlockSelect].transform.position;
        TileManager.instance.tiles[RandomBlockSelect].GetComponent<Tile>().block.gameObject.SetActive(false);
        TileManager.instance.tiles[RandomBlockSelect].GetComponent<Tile>().block = null;
        TileManager.instance.tiles[RandomBlockSelect].GetComponent<Tile>().block = eagleBlock;
        TileManager.instance.tiles[RandomBlockSelect].GetComponent<Tile>().blockColor = 14;
    }
    
}
