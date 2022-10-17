using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparrowItem : MonoBehaviour
{
    [SerializeField]
    GameObject[]        right;
    [SerializeField]
    GameObject[]        left;
    public static int   sparrowItemCount = 0;
    public static int   randomPointSpawnNum;
    [SerializeField]AudioClip[] clip;
    private void Awake()
    {
        right = GameObject.FindGameObjectsWithTag("SparrowPointRight");
        left =  GameObject.FindGameObjectsWithTag("SparrowPointLeft");

        StartCoroutine(Upcol());
    }
    IEnumerator Upcol()
    {
        while (true)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            if (UIManager.SparrowGauge >= 100-GameLogicManager.WhistleOverallGaugeReduction)
            {
                SpawnSparrow();
                SoundManager.instance.PlaySFX(clip, 0, 1, 1);
                UIManager.SparrowGauge -= 100 - GameLogicManager.WhistleOverallGaugeReduction;
            }
        }
    }
    IEnumerator Acon()
    {
        yield return new WaitForSeconds(1);
        SpawnSparrow();
    }
    private void OnEnable()
    {
        sparrowItemCount++;
    }
    private void OnDisable()
    {
        sparrowItemCount--;
    }
    /// <summary>
    /// 독수리 소환 위치 설정 함수
    /// </summary>
    void SpawnSparrow()
    {
        randomPointSpawnNum = Random.Range(0, 3);
        int randomRightNum = Random.Range(0,right.Length);
        int randomLeftNum = Random.Range(0,left.Length);
        if(randomPointSpawnNum <=1)//왼쪽 오른쪽
        {
            GameObject sparrow = OPManager.instance.SetObject("Sparrow");
            sparrow.GetComponent<SpriteRenderer>().flipX = true;
            sparrow.transform.position = right[randomRightNum].transform.position;
            
        }
        if(randomPointSpawnNum >1)//오른쪽 왼쪽
        {
            GameObject sparrow = OPManager.instance.SetObject("Sparrow");
            sparrow.GetComponent<SpriteRenderer>().flipX = false;
            sparrow.transform.position = left[randomLeftNum].transform.position;
            if (left[randomLeftNum].transform.localPosition.y - transform.localPosition.y >= 0)
            {
                sparrow.GetComponent<Animator>().SetBool("IsFly", true);
            }
            if(left[randomLeftNum].transform.localPosition.y - transform.localPosition.y < 0)
            {
                sparrow.GetComponent<Animator>().SetBool("IsFly", false);
            }
        }
    }
}