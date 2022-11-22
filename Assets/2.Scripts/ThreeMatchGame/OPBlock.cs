using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OPBlock : MonoBehaviour
{
    public GameObject redBlockP;
    public GameObject yellowBlockP;
    public GameObject greenBlockP;
    public GameObject blueBlockP;
    public GameObject purpleBlockP;
    public GameObject beeBlockP;
    public GameObject beeHiveBlockP;
    public GameObject eggBlockP;
    public GameObject rabbitBlockP;
    public GameObject squirrelBlockP;
    public GameObject wevvilBlockP;
    public GameObject wormBlockP;
    public GameObject stickBlockP;
    public GameObject sprayBlockP;
    public GameObject eagleBlockP;
    public GameObject firecrackerRedP;
    public GameObject firecrackerYellowP;
    public GameObject firecrackerGreenP;
    public GameObject firecrackerBlueP;
    public GameObject firecrackerPurpleP;

    public GameObject[] redBlock;
    public GameObject[] yellowBlock;
    public GameObject[] greenBlock;
    public GameObject[] blueBlock;
    public GameObject[] purpleBlock;
    public GameObject[] beeBlock;
    public GameObject[] beeHiveBlock;
    public GameObject[] eggBlock;
    public GameObject[] rabbitBlock;
    public GameObject[] squirrelBlock;
    public GameObject[] wevvilBlock;
    public GameObject[] wormBlock;
    public GameObject[] stickBlock;
    public GameObject[] sprayBlock;
    public GameObject[] eagleBlock;
    public GameObject[] firecrackerRed;
    public GameObject[] firecrackerYellow;
    public GameObject[] firecrackerGreen;
    public GameObject[] firecrackerBlue;
    public GameObject[] firecrackerPurple;

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
        firecrackerRedP = Resources.Load<GameObject>("ItemBlockScript/FirecrackerBlockRed");
        firecrackerYellowP = Resources.Load<GameObject>("ItemBlockScript/FirecrackerBlockYellow");
        firecrackerGreenP = Resources.Load<GameObject>("ItemBlockScript/FirecrackerBlockGreen");
        firecrackerBlueP = Resources.Load<GameObject>("ItemBlockScript/FirecrackerBlockBlue");
        firecrackerPurpleP = Resources.Load<GameObject>("ItemBlockScript/FirecrackerBlockPurple");

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
        firecrackerRed = new GameObject[10];
        firecrackerYellow = new GameObject[10];
        firecrackerGreen = new GameObject[10];
        firecrackerBlue = new GameObject[10];
        firecrackerPurple = new GameObject[10];
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
        for (int i = 0; i < firecrackerRed.Length; i++)
        {
            firecrackerRed[i] = Instantiate<GameObject>(firecrackerRedP);
            firecrackerRed[i].transform.SetParent(transform);
            firecrackerRed[i].transform.localScale = Vector3.one;
            firecrackerRed[i].SetActive(false);
        }
        for (int i = 0; i < firecrackerYellow.Length; i++)
        {
            firecrackerYellow[i] = Instantiate<GameObject>(firecrackerYellowP);
            firecrackerYellow[i].transform.SetParent(transform);
            firecrackerYellow[i].transform.localScale = Vector3.one;
            firecrackerYellow[i].SetActive(false);
        }
        for (int i = 0; i < firecrackerGreen.Length; i++)
        {
            firecrackerGreen[i] = Instantiate<GameObject>(firecrackerGreenP);
            firecrackerGreen[i].transform.SetParent(transform);
            firecrackerGreen[i].transform.localScale = Vector3.one;
            firecrackerGreen[i].SetActive(false);
        }
        for (int i = 0; i < firecrackerBlue.Length; i++)
        {
            firecrackerBlue[i] = Instantiate<GameObject>(firecrackerBlueP);
            firecrackerBlue[i].transform.SetParent(transform);
            firecrackerBlue[i].transform.localScale = Vector3.one;
            firecrackerBlue[i].SetActive(false);
        }
        for (int i = 0; i < firecrackerPurple.Length; i++)
        {
            firecrackerPurple[i] = Instantiate<GameObject>(firecrackerPurpleP);
            firecrackerPurple[i].transform.SetParent(transform);
            firecrackerPurple[i].transform.localScale = Vector3.one;
            firecrackerPurple[i].SetActive(false);
        }
    }
    /// <summary>
    /// 0 redBlock 1 yellowBlock 2 GreenBlock 3 BlueBlock 4 purpleBlock 5 beeBlock 6 beeHiveBlock 7 eggBlock 8 rabbitBlock 9 squirrelBLock 10 wevvilBlock 11 wormBlock 12 stickBlock 13 sprayBlock 14 eagleBlock 15 firecrackerBlockRed
    /// 16firecrackerYellow 17firecrackerGreen 18firecrackerBlue 19firecrackerPurple
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
            case 15:
                targetPool = firecrackerRed;
                break;
            case 16:
                targetPool = firecrackerYellow;
                break;
            case 17:
                targetPool = firecrackerGreen;
                break;
            case 18:
                targetPool = firecrackerBlue;
                break;
            case 19:
                targetPool = firecrackerPurple;
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
