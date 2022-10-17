using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OPBlock : MonoBehaviour
{
    GameObject redBlockP;
    GameObject yellowBlockP;
    GameObject greenBlockP;
    GameObject blueBlockP;
    GameObject purpleBlockP;
    GameObject beeBlockP;
    GameObject beeHiveBlockP;
    GameObject eggBlockP;
    GameObject rabbitBlockP;
    GameObject squirrelBlockP;
    GameObject wevvilBlockP;
    GameObject wormBlockP;
    GameObject stickBlockP;
    GameObject sprayBlockP;
    GameObject eagleBlockP;

    GameObject[] redBlock;
    GameObject[] yellowBlock;
    GameObject[] greenBlock;
    GameObject[] blueBlock;
    GameObject[] purpleBlock;
    GameObject[] beeBlock;
    GameObject[] beeHiveBlock;
    GameObject[] eggBlock;
    GameObject[] rabbitBlock;
    GameObject[] squirrelBlock;
    GameObject[] wevvilBlock;
    GameObject[] wormBlock;
    GameObject[] stickBlock;
    GameObject[] sprayBlock;
    GameObject[] eagleBlock;

    GameObject[] targetPool;
    public static OPBlock instance;
    private void Awake()
    {
        instance = this;
        if (instance == null)
            Destroy(gameObject);

        redBlockP = Resources.Load<GameObject>("ChestnutBlockScript/RedBlock");
        yellowBlockP = Resources.Load<GameObject>("ChestnutBlockScript/YellowBlock");
        greenBlockP = Resources.Load<GameObject>("ChestnutBlockScript/GreenBlock");
        blueBlockP = Resources.Load<GameObject>("ChestnutBlockScript/BlueBlock");
        purpleBlockP = Resources.Load<GameObject>("ChestnutBlockScript/PurpleBlock");
        beeBlockP = Resources.Load<GameObject>("MonsterBlockScript/BeeBlock");
        beeHiveBlockP = Resources.Load<GameObject>("MonsterBlockScript/BeeHiveBlock");
        eggBlockP = Resources.Load<GameObject>("MonsterBlockScript/EggBlock");
        rabbitBlockP = Resources.Load<GameObject>("MonsterBlockScript/RabbitBlock");
        squirrelBlockP = Resources.Load<GameObject>("MonsterBlockScript/SquirrelBlock");
        wevvilBlockP = Resources.Load<GameObject>("MonsterBlockScript/WevvilBlock");
        wormBlockP = Resources.Load<GameObject>("MonsterBlockScript/WormBlock");
        stickBlockP = Resources.Load<GameObject>("ItemBlockScript/DragonflyStickItemBlock");
        sprayBlockP = Resources.Load<GameObject>("ItemBlockScript/SprayItemBlock");
        eagleBlockP = Resources.Load<GameObject>("ItemBlockScript/EagleItemBlock");

        redBlock = new GameObject[100];
        yellowBlock = new GameObject[100];
        greenBlock = new GameObject[100];
        blueBlock = new GameObject[100];
        purpleBlock = new GameObject[100];
        beeBlock = new GameObject[10];
        beeHiveBlock = new GameObject[10];
        eggBlock = new GameObject[10];
        rabbitBlock = new GameObject[10];
        squirrelBlock = new GameObject[10];
        wevvilBlock = new GameObject[10];
        wormBlock = new GameObject[10];
        stickBlock = new GameObject[10];
        sprayBlock = new GameObject[10];
        eagleBlock = new GameObject[10];
        GetObject();
    }
    void GetObject()
    {
        for (int i = 0; i < redBlock.Length; i++)
        {
            redBlock[i] = Instantiate<GameObject>(redBlockP);
            redBlock[i].transform.SetParent(transform);
            redBlock[i].transform.localScale = Vector3.one;
            redBlock[i].SetActive(false);
        }
        for (int i = 0; i < yellowBlock.Length; i++)
        {
            yellowBlock[i] = Instantiate<GameObject>(yellowBlockP);
            yellowBlock[i].transform.SetParent(transform);
            yellowBlock[i].transform.localScale = Vector3.one;
            yellowBlock[i].SetActive(false);
        }
        for (int i = 0; i < greenBlock.Length; i++)
        {
            greenBlock[i] = Instantiate<GameObject>(greenBlockP);
            greenBlock[i].transform.SetParent(transform);
            greenBlock[i].transform.localScale = Vector3.one;
            greenBlock[i].SetActive(false);
        }
        for (int i = 0; i < blueBlock.Length; i++)
        {
            blueBlock[i] = Instantiate<GameObject>(blueBlockP);
            blueBlock[i].transform.SetParent(transform);
            blueBlock[i].transform.localScale = Vector3.one;
            blueBlock[i].SetActive(false);
        }
        for (int i = 0; i < purpleBlock.Length; i++)
        {
            purpleBlock[i] = Instantiate<GameObject>(purpleBlockP);
            purpleBlock[i].transform.SetParent(transform);
            purpleBlock[i].transform.localScale = Vector3.one;
            purpleBlock[i].SetActive(false);
        }
        for (int i = 0; i < beeBlock.Length; i++)
        {
            beeBlock[i] = Instantiate<GameObject>(beeBlockP);
            beeBlock[i].transform.SetParent(transform);
            beeBlock[i].transform.localScale = Vector3.one;
            beeBlock[i].SetActive(false);
        }
        for (int i = 0; i < beeHiveBlock.Length; i++)
        {
            beeHiveBlock[i] = Instantiate<GameObject>(beeHiveBlockP);
            beeHiveBlock[i].transform.SetParent(transform);
            beeHiveBlock[i].transform.localScale = Vector3.one;
            beeHiveBlock[i].SetActive(false);
        }
        for (int i = 0; i < eggBlock.Length; i++)
        {
            eggBlock[i] = Instantiate<GameObject>(eggBlockP);
            eggBlock[i].transform.SetParent(transform);
            eggBlock[i].transform.localScale = Vector3.one;
            eggBlock[i].SetActive(false);
        }
        for (int i = 0; i < squirrelBlock.Length; i++)
        {
            squirrelBlock[i] = Instantiate<GameObject>(squirrelBlockP);
            squirrelBlock[i].transform.SetParent(transform);
            squirrelBlock[i].transform.localScale = Vector3.one;
            squirrelBlock[i].SetActive(false);
        }
        for (int i = 0; i < rabbitBlock.Length; i++)
        {
            rabbitBlock[i] = Instantiate<GameObject>(rabbitBlockP);
            rabbitBlock[i].transform.SetParent(transform);
            rabbitBlock[i].transform.localScale = Vector3.one;
            rabbitBlock[i].SetActive(false);
        }
        for (int i = 0; i < wevvilBlock.Length; i++)
        {
            wevvilBlock[i] = Instantiate<GameObject>(wevvilBlockP);
            wevvilBlock[i].transform.SetParent(transform);
            wevvilBlock[i].transform.localScale = Vector3.one;
            wevvilBlock[i].SetActive(false);
        }
        for (int i = 0; i < wormBlock.Length; i++)
        {
            wormBlock[i] = Instantiate<GameObject>(wormBlockP);
            wormBlock[i].transform.SetParent(transform);
            wormBlock[i].transform.localScale = Vector3.one;
            wormBlock[i].SetActive(false);
        }
        for (int i = 0; i < stickBlock.Length; i++)
        {
            stickBlock[i] = Instantiate<GameObject>(stickBlockP);
            stickBlock[i].transform.SetParent(transform);
            stickBlock[i].transform.localScale = Vector3.one;
            stickBlock[i].SetActive(false);
        }
        for (int i = 0; i < sprayBlock.Length; i++)
        {
            sprayBlock[i] = Instantiate<GameObject>(sprayBlockP);
            sprayBlock[i].transform.SetParent(transform);
            sprayBlock[i].transform.localScale = Vector3.one;
            sprayBlock[i].SetActive(false);
        }
        for (int i = 0; i < eagleBlock.Length; i++)
        {
            eagleBlock[i] = Instantiate<GameObject>(eagleBlockP);
            eagleBlock[i].transform.SetParent(transform);
            eagleBlock[i].transform.localScale = Vector3.one;
            eagleBlock[i].SetActive(false);
        }
    }
    /// <summary>
    /// 0 redBlock 1 yellowBlock 2 GreenBlock 3 BlueBlock 4 purpleBlock 5 beeBlock 6 beeHiveBlock 7 eggBlock 8 rabbitBlock 9 squirrelBLock 10 wevvilBlock 11 wormBlock 12 stickBlock 13 sprayBlock 14 eagleBlock
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public GameObject SetObject(int type)
    {
        switch (type)
        {
            case 0:
                targetPool = redBlock;
                break;
            case 1:
                targetPool = yellowBlock;
                break;
            case 2:
                targetPool = greenBlock;
                break;
            case 3:
                targetPool = blueBlock;
                break;
            case 4:
                targetPool = purpleBlock;
                break;
            case 5:
                targetPool = beeBlock;
                break;
            case 6:
                targetPool = beeHiveBlock;
                break;
            case 7:
                targetPool = eggBlock;
                break;
            case 8:
                targetPool = rabbitBlock;
                break;
            case 9:
                targetPool = squirrelBlock;
                break;
            case 10:
                targetPool = wevvilBlock;
                break;
            case 11:
                targetPool = wormBlock;
                break;
            case 12:
                targetPool = stickBlock;
                break;
            case 13:
                targetPool = sprayBlock;
                break;
            case 14:
                targetPool = eagleBlock;
                break;
            default:
                break;
        }
        for (int i = 0; i < targetPool.Length; i++)
        {
            if (!targetPool[i].activeSelf)
            {
                targetPool[i].SetActive(true);
                return targetPool[i];
            }
        }
        return null;
    }
    public void DestroyObject()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.childCount != 0)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}
