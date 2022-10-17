using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEffectOPManager : MonoBehaviour
{
    public GameObject redBlockEffectP;
    public GameObject yellowBlockEffectP;
    public GameObject greenBlockEffectP;
    public GameObject blueBlockEffectP;
    public GameObject purpleBlockEffectP;
    public GameObject mudEffectP;
    public GameObject eggDeadP;
    public GameObject wormDeadP;
    public GameObject EvolutionEffectP;
    public GameObject wevvilDeadP;
    public GameObject squirrelEffectP;
    public GameObject monsterHitEffectP;
    public GameObject beeSkillEffectP;
    public GameObject beeStingSkillEffectP;
    public GameObject rabbitSkillEffectP;
    public GameObject rabbitKnifeEffectP;
    public GameObject woodEffectP;
    public GameObject shurikenWithWoodP;
    public GameObject chestnutEffectP;
    public GameObject fertilizerEffectP;
    public GameObject eagleEffectP;
    public GameObject stickEffectP;
    public GameObject sprayEffectP;

    public GameObject[] redBlockEffect;
    public GameObject[] yellowBlockEffect;
    public GameObject[] greenBlockEffect;
    public GameObject[] blueBlockEffect;
    public GameObject[] purpleBlockEffect;
    public GameObject[] mudEffect;
    public GameObject[] eggDead;
    public GameObject[] wormDead;
    public GameObject[] EvolutionEffect;
    public GameObject[] wevvilDead;
    public GameObject[] squirrelEffect;
    public GameObject[] monsterHitEffect;
    public GameObject[] beeSkillEffect;
    public GameObject[] beeStingSkillEffect;
    public GameObject[] rabbitSkillEffect;
    public GameObject[] rabbitKnifeEffect;
    public GameObject[] woodEffect;
    public GameObject[] shurikenWithWood;
    public GameObject[] chestnutEffect;
    public GameObject[] fertilizerEffect;
    public GameObject[] eagleEffect;
    public GameObject[] stickEffect;
    public GameObject[] sprayEffect;
    

    public GameObject[] targetPool;
    public static BlockEffectOPManager instance;
    private void Awake()
    {
        instance = this;
        if (instance == null)
            Destroy(gameObject);

        redBlockEffectP = Resources.Load<GameObject>("BlockEffect/MatchRedEffect");
        yellowBlockEffectP = Resources.Load<GameObject>("BlockEffect/MatchYellowEffect");
        greenBlockEffectP = Resources.Load<GameObject>("BlockEffect/MatchGreenEffect");
        blueBlockEffectP = Resources.Load<GameObject>("BlockEffect/MatchBlueEffect");
        purpleBlockEffectP = Resources.Load<GameObject>("BlockEffect/MatchPurpleEffect");
        mudEffectP = Resources.Load<GameObject>("Effect/MudEffectPuzzle");
        eggDeadP = Resources.Load<GameObject>("Effect/EggDeadPuzzleFX");
        wormDeadP = Resources.Load<GameObject>("Effect/WormDeadPuzzleFX");
        EvolutionEffectP = Resources.Load<GameObject>("Effect/EvolutionEffect");
        wevvilDeadP = Resources.Load<GameObject>("Effect/ChestBugDeadPuzzleFX");
        squirrelEffectP = Resources.Load<GameObject>("BlockEffect/SquirrelEffect");
        monsterHitEffectP = Resources.Load<GameObject>("BlockEffect/HitOverPuzzleFX");
        beeSkillEffectP = Resources.Load<GameObject>("BlockEffect/BeeStunFX");
        beeStingSkillEffectP = Resources.Load<GameObject>("BlockEffect/Sting");
        rabbitSkillEffectP = Resources.Load<GameObject>("BlockEffect/RabbitSkill");
        rabbitKnifeEffectP = Resources.Load<GameObject>("BlockEffect/Shuriken");
        woodEffectP = Resources.Load<GameObject>("Effect/Rabbit_TeleportFX");
        shurikenWithWoodP = Resources.Load<GameObject>("BlockEffect/ShurikenWithWood");
        chestnutEffectP = Resources.Load<GameObject>("BlockEffect/ChestnutEffect");
        fertilizerEffectP = Resources.Load<GameObject>("BlockEffect/FertilizerEffect");
        eagleEffectP = Resources.Load<GameObject>("BlockEffect/BirdEffect");
        stickEffectP = Resources.Load<GameObject>("BlockEffect/SmokeEffect_Boundary");
        sprayEffectP = Resources.Load<GameObject>("BlockEffect/SprayShot");

        redBlockEffect = new GameObject[100];
        yellowBlockEffect = new GameObject[100];
        greenBlockEffect = new GameObject[100];
        blueBlockEffect = new GameObject[100];
        purpleBlockEffect = new GameObject[100];
        mudEffect = new GameObject[5];
        eggDead = new GameObject[5];
        wormDead = new GameObject[5];
        EvolutionEffect = new GameObject[5];
        wevvilDead = new GameObject[5];
        squirrelEffect = new GameObject[1];
        monsterHitEffect = new GameObject[10];
        beeSkillEffect = new GameObject[50];
        beeStingSkillEffect = new GameObject[50];
        rabbitSkillEffect = new GameObject[50];
        rabbitKnifeEffect = new GameObject[50];
        woodEffect = new GameObject[50];
        shurikenWithWood = new GameObject[50];
        chestnutEffect = new GameObject[100];
        fertilizerEffect = new GameObject[72];
        eagleEffect = new GameObject[10];
        stickEffect = new GameObject[10];
        sprayEffect = new GameObject[10];
    }
    private void Start()
    {
        GetObject();
        
    }
    void GetObject()
    {
        for (int i = 0; i < redBlockEffect.Length; i++)
        {
            redBlockEffect[i] = Instantiate<GameObject>(redBlockEffectP, transform.position, transform.rotation);
            redBlockEffect[i].SetActive(false);
            redBlockEffect[i].transform.SetParent(transform);
            redBlockEffect[i].transform.localScale = Vector3.one;
        }
        for (int i = 0; i < yellowBlockEffect.Length; i++)
        {
            yellowBlockEffect[i] = Instantiate<GameObject>(yellowBlockEffectP, transform.position, transform.rotation);
            yellowBlockEffect[i].SetActive(false);
            yellowBlockEffect[i].transform.SetParent(transform);
            yellowBlockEffect[i].transform.localScale = Vector3.one;
        }
        for (int i = 0; i < greenBlockEffect.Length; i++)
        {
            greenBlockEffect[i] = Instantiate<GameObject>(greenBlockEffectP, transform.position, transform.rotation);
            greenBlockEffect[i].SetActive(false);
            greenBlockEffect[i].transform.SetParent(transform);
            greenBlockEffect[i].transform.localScale = Vector3.one;
        }
        for (int i = 0; i < blueBlockEffect.Length; i++)
        {
            blueBlockEffect[i] = Instantiate<GameObject>(blueBlockEffectP, transform.position, transform.rotation);
            blueBlockEffect[i].SetActive(false);
            blueBlockEffect[i].transform.SetParent(transform);
            blueBlockEffect[i].transform.localScale = Vector3.one;
        }
        for (int i = 0; i < purpleBlockEffect.Length; i++)
        {
            purpleBlockEffect[i] = Instantiate<GameObject>(purpleBlockEffectP, transform.position, transform.rotation);
            purpleBlockEffect[i].SetActive(false);
            purpleBlockEffect[i].transform.SetParent(transform);
            purpleBlockEffect[i].transform.localScale = Vector3.one;
        }
        for (int i = 0; i < mudEffect.Length; i++)
        {
            mudEffect[i] = Instantiate<GameObject>(mudEffectP, transform.position, transform.rotation);
            mudEffect[i].SetActive(false);
            mudEffect[i].transform.SetParent(transform);
            mudEffect[i].transform.localScale = Vector3.one;
        }
        for (int i = 0; i < eggDead.Length; i++)
        {
            eggDead[i] = Instantiate<GameObject>(eggDeadP, transform.position, transform.rotation);
            eggDead[i].SetActive(false);
            eggDead[i].transform.SetParent(transform);
            eggDead[i].transform.localScale = Vector3.one;
        }
        for (int i = 0; i < wormDead.Length; i++)
        {
            wormDead[i] = Instantiate<GameObject>(wormDeadP, transform.position, transform.rotation);
            wormDead[i].SetActive(false);
            wormDead[i].transform.SetParent(transform);
            wormDead[i].transform.localScale = Vector3.one;
        }
        for (int i = 0; i < EvolutionEffect.Length; i++)
        {
            EvolutionEffect[i] = Instantiate<GameObject>(EvolutionEffectP, transform.position, transform.rotation);
            EvolutionEffect[i].SetActive(false);
            EvolutionEffect[i].transform.SetParent(transform);
            EvolutionEffect[i].transform.localScale = Vector3.one;
        }
        for (int i = 0; i < wevvilDead.Length; i++)
        {
            wevvilDead[i] = Instantiate<GameObject>(wevvilDeadP, transform.position, transform.rotation);
            wevvilDead[i].SetActive(false);
            wevvilDead[i].transform.SetParent(transform);
            wevvilDead[i].transform.localScale = Vector3.one;
        }
        for (int i = 0; i < squirrelEffect.Length; i++)
        {
            squirrelEffect[i] = Instantiate<GameObject>(squirrelEffectP, transform.position, transform.rotation);
            squirrelEffect[i].SetActive(false);
            squirrelEffect[i].transform.SetParent(GameObject.FindGameObjectWithTag("MiddleCanvas").transform);
            squirrelEffect[i].transform.localScale = Vector3.one;
        }
        for (int i = 0; i < monsterHitEffect.Length; i++)
        {
            monsterHitEffect[i] = Instantiate<GameObject>(monsterHitEffectP, transform.position, transform.rotation);
            monsterHitEffect[i].SetActive(false);
            monsterHitEffect[i].transform.SetParent(GameObject.FindGameObjectWithTag("MiddleCanvas").transform);
            monsterHitEffect[i].transform.localScale = new Vector3(50, 50, 50);
        }
        for (int i = 0; i < beeSkillEffect.Length; i++)
        {
            beeSkillEffect[i] = Instantiate<GameObject>(beeSkillEffectP, transform.position, transform.rotation);
            beeSkillEffect[i].SetActive(false);
            //beeSkillEffect[i].transform.localScale = Vector3.one;
            //beeSkillEffect[i].transform.rotation = Quaternion.Euler(new Vector3(90,0,0));
        }
        for (int i = 0; i < beeStingSkillEffect.Length; i++)
        {
            beeStingSkillEffect[i] = Instantiate<GameObject>(beeStingSkillEffectP, transform.position, transform.rotation);
            beeStingSkillEffect[i].SetActive(false);
            beeStingSkillEffect[i].transform.SetParent(GameObject.FindGameObjectWithTag("MiddleCanvas").transform);
            beeStingSkillEffect[i].transform.localScale = Vector3.one;
        }
        for (int i = 0; i < rabbitKnifeEffect.Length; i++)
        {
            rabbitKnifeEffect[i] = Instantiate<GameObject>(rabbitKnifeEffectP, transform.position, transform.rotation);
            rabbitKnifeEffect[i].SetActive(false);
            rabbitKnifeEffect[i].transform.SetParent(GameObject.FindGameObjectWithTag("MiddleCanvas").transform);
            rabbitKnifeEffect[i].transform.localScale = Vector3.one;
        }
        for (int i = 0; i < rabbitSkillEffect.Length; i++)
        {
            rabbitSkillEffect[i] = Instantiate<GameObject>(rabbitSkillEffectP, transform.position, transform.rotation);
            rabbitSkillEffect[i].SetActive(false);
            rabbitSkillEffect[i].transform.SetParent(GameObject.FindGameObjectWithTag("MiddleCanvas").transform);
            rabbitSkillEffect[i].transform.localScale = Vector3.one;
        }
        for (int i = 0; i < woodEffect.Length; i++)
        {
            woodEffect[i] = Instantiate<GameObject>(woodEffectP, transform.position, transform.rotation);
            woodEffect[i].SetActive(false);
            woodEffect[i].transform.SetParent(GameObject.FindGameObjectWithTag("MiddleCanvas").transform);
            woodEffect[i].transform.localScale = Vector3.one;
        }
        for (int i = 0; i < shurikenWithWood.Length; i++)
        {
            shurikenWithWood[i] = Instantiate<GameObject>(shurikenWithWoodP, transform.position, transform.rotation);
            shurikenWithWood[i].SetActive(false);
            shurikenWithWood[i].transform.SetParent(GameObject.FindGameObjectWithTag("MiddleCanvas").transform);
            shurikenWithWood[i].transform.localScale = Vector3.one;
        }
        for (int i = 0; i < chestnutEffect.Length; i++)
        {
            chestnutEffect[i] = Instantiate<GameObject>(chestnutEffectP, transform.position, transform.rotation);
            chestnutEffect[i].SetActive(false);
            chestnutEffect[i].transform.SetParent(GameObject.FindGameObjectWithTag("MiddleCanvas").transform);
            chestnutEffect[i].transform.localScale = new Vector3(50,50,50);
        }
        for (int i = 0; i < fertilizerEffect.Length; i++)
        {
            fertilizerEffect[i] = Instantiate<GameObject>(fertilizerEffectP, transform.position, transform.rotation);
            fertilizerEffect[i].SetActive(false);
            fertilizerEffect[i].transform.SetParent(GameObject.FindGameObjectWithTag("MiddleCanvas").transform);
            fertilizerEffect[i].transform.localScale = new Vector3(50, 50, 50);
        }
        for (int i = 0; i < eagleEffect.Length; i++)
        {
            eagleEffect[i] = Instantiate<GameObject>(eagleEffectP, transform.position, transform.rotation);
            eagleEffect[i].SetActive(false);
            eagleEffect[i].transform.SetParent(GameObject.FindGameObjectWithTag("MiddleCanvas").transform);
            eagleEffect[i].transform.localScale = Vector3.one;
        }
        for (int i = 0; i < stickEffect.Length; i++)
        {
            stickEffect[i] = Instantiate<GameObject>(stickEffectP, transform.position, transform.rotation);
            stickEffect[i].GetComponent<SmokeEffect>().smokeArea = TileManager.instance.stickSkillLevel;
            stickEffect[i].SetActive(false);
            stickEffect[i].transform.SetParent(GameObject.FindGameObjectWithTag("MiddleCanvas").transform);
            stickEffect[i].transform.localScale = Vector3.one;
        }
        for (int i = 0; i < sprayEffect.Length; i++)
        {
            sprayEffect[i] = Instantiate<GameObject>(sprayEffectP, transform.position, transform.rotation);
            sprayEffect[i].SetActive(false);
            sprayEffect[i].transform.SetParent(GameObject.FindGameObjectWithTag("MiddleCanvas").transform);
            sprayEffect[i].transform.localScale = new Vector3(100,100,100);
        }
    }
    /// <summary>
    /// 0. »¡°£»ö ¹ã 1. ³ë¶û 2. ÃÊ·Ï3. ÆÄ¶û 4. º¸¶ó 5. ÁøÈë 6 ¾Ë Á×À½ 7.¾Ö¹ú·¹ Á×À½ 8. ÁøÈ­ 9.¹Ù±¸¹Ì Á×À½ 10. ´Ù¶÷Áã ÀÌÆåÆ® 11. ¸ó½ºÅÍ È÷Æ® 12. ¹ú ½ºÅ³ 13.¹úÄ§ ½ºÅ³ 14. ¼ö¸®°Ë 15.Åä³¢ ³ª¹« ÀÌÆåÆ® 16. ³ª¹« »ç¶óÁö´Â ÀÌÆåÆ® 17 ³ª¹«À§ ¼ö¸®°Ë ÀÌÆåÆ® 18 ¹ã ÀÌÆåÆ® 19 ºñ·á ÀÌÆåÆ® 20 µ¶¼ö¸® ÀÌÆåÆ® 21.ÀáÀÚ¸®Ã¤ÀÌÆåÆ®22.½ºÇÁ·¹ÀÌÀÌÆåÆ®
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public GameObject SetObject(int type)
    {
        switch (type)
        {
            case 0:
                targetPool = redBlockEffect;
                break;
            case 1:
                targetPool = yellowBlockEffect;
                break;
            case 2:
                targetPool = greenBlockEffect;
                break;
            case 3:
                targetPool = blueBlockEffect;
                break;
            case 4:
                targetPool = purpleBlockEffect;
                break;
            case 5:
                targetPool = mudEffect;
                break;
            case 6:
                targetPool = eggDead;
                break;
            case 7:
                targetPool = wormDead;
                break;
            case 8:
                targetPool = EvolutionEffect;
                break;
            case 9:
                targetPool = wevvilDead;
                break;
            case 10:
                targetPool = squirrelEffect;
                break;
            case 11:
                targetPool = monsterHitEffect;
                break;
            case 12:
                targetPool = beeSkillEffect;
                break;
            case 13:
                targetPool = beeStingSkillEffect;
                break;
            case 14:
                targetPool = rabbitKnifeEffect;
                break;
            case 15:
                targetPool = rabbitSkillEffect;
                break;
            case 16:
                targetPool = woodEffect;
                break;
            case 17:
                targetPool = shurikenWithWood;
                break;
            case 18:
                targetPool = chestnutEffect;
                break;
            case 19:
                targetPool = fertilizerEffect;
                break; 
            case 20:
                targetPool = eagleEffect;
                break;
            case 21:
                targetPool = stickEffect;
                break;
            case 22:
                targetPool = sprayEffect;
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
