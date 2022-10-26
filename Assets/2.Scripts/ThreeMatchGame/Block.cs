using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Block : MonoBehaviour
{
    int count = 0;
    public AudioClip[] clip;
    public enum BlockType
    {
        CHESTNUTBLOCK,
        ITEM,
        MONSTER
    }
    public BlockType blocktype;
    public int blockColor;
    public int hp;
    public int SwapCount = 0;
    public int beeMonsterCount = 0;
    public bool IsHit = false;
    Transform chestnutPoint;
    Transform fertilizerPoint;
    public void Awake()
    {
        chestnutPoint = GameObject.FindGameObjectWithTag("MiddleCanvas").transform.GetChild(2).GetComponent<Transform>();
        fertilizerPoint = GameObject.FindGameObjectWithTag("MiddleCanvas").transform.GetChild(3).GetComponent<Transform>();
    }
    private void OnEnable()
    {
        if (name == "RedBlock(Clone)")
        {
            blockColor = 0;
        }
        else if (name == "YellowBlock(Clone)")
        {
            blockColor = 1;
        }
        else if (name == "GreenBlock(Clone)")
        {
            blockColor = 2;
        }
        else if (name == "BlueBlock(Clone)")
        {
            blockColor = 3;
        }
        else if (name == "PurpleBlock(Clone)")
        {
            blockColor = 4;
        }
        IsHit = false;
        SwapCount = 0;
        hp = 100;
        if(blockColor == 5)
        {
            hp = 2;
            if(count !=0)
                SoundManager.instance.PlaySFX(clip, 1, 1, 1);
            TileManager.beeCount++;
        }
        else if (blockColor == 6)
        {
            hp = 2;
        }
        else if (blockColor == 7)
        {
            hp = 1;
        }
        else if (blockColor == 8)
        {
            hp = 2;
        }
        else if (blockColor == 9)
        {
            hp = 2;
        }
        else if (blockColor == 10)
        {
            hp = 2;
            TileManager.wevvilCount++;
        }
        else if (blockColor == 11)
        {
            hp = 1;
            TileManager.wormCount++;
        }
        if(gameObject.transform.childCount >= 2)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void OnDisable()
    {
        if (count > 0)
        {
            if(blockColor < 5 && CountDownInPuzzle.isGameStart)
            {
                
                GameObject effect = BlockEffectOPManager.instance.SetObject(blockColor);
                effect.transform.position = this.transform.position;
                TileManager.beeCount--;
            }
            SoundManager.instance.PlaySFX(clip, 0, 1, 1);
            if (blockColor == 7)
            {
                TileManager.eggCount--;
                GameObject eggDeadEffect = BlockEffectOPManager.instance.SetObject(6);
                eggDeadEffect.transform.position = this.transform.position;
            }
            if (blockColor == 6)
            {
                TileManager.beeHiveCount--;
            }
            if (blockColor == 8)
            {
                TileManager.rabbitCount--;
            }
            if (blockColor == 9)
            {
                TileManager.squirrelCount--;
            }
            if (blockColor == 10)
            {
                TileManager.wevvilCount--;
                if (hp <= 0)
                {
                    GameObject wevvilDeadEffect = BlockEffectOPManager.instance.SetObject(9);
                    wevvilDeadEffect.transform.position = this.transform.position;
                }
            }
            if (blockColor == 11)
            {
                TileManager.wormCount--;
                if (hp <= 0)
                {
                    GameObject wormDeadEffect = BlockEffectOPManager.instance.SetObject(7);
                    wormDeadEffect.transform.position = this.transform.position;
                }
            }
        }
        else
        {
            count++;
        }
        if (gameObject.transform.childCount >= 2)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
        }
        if (gameObject.transform.childCount >= 1)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    void ChestnutEffect()
    {
        if (count > 0)
        {
            if (blockColor < 5)
            {
                GameObject effect = BlockEffectOPManager.instance.SetObject(blockColor);
                effect.transform.position = this.transform.position;
                TileManager.beeCount--;
            }
            SoundManager.instance.PlaySFX(clip, 0, 1, 1);
            
        }
        else
        {
            count++;
        }
    }
    public void ScoreUp()
    {
        if (CountDownInPuzzle.isGameStart)
        {
            if (blocktype == BlockType.CHESTNUTBLOCK)
            {
                int i = 0;
                EagleGauge.eagleGauge++;
                TreeMatchGameScoreManager.Instance.ChestnutPointChangeInText(ChestnutPointInTreeMatchPuzzle());
                GameObject effect = BlockEffectOPManager.instance.SetObject(18);
                StartCoroutine(CreateDeadEffect(18, effect));
            }
            else if (blocktype == BlockType.MONSTER)
            {
                TreeMatchGameScoreManager.Instance.FertilizerPointChangeInText(FertilizerPointInTreeMatchPuzzle());
                for (int i = 0; i < 3; i++)
                {
                    GameObject effect = BlockEffectOPManager.instance.SetObject(19);
                    StartCoroutine(CreateDeadEffect(19,effect));
                }
            }
        }
        else
        {
            if (blocktype == BlockType.CHESTNUTBLOCK)
            {
                gameObject.SetActive(false);
            }

            else if (blocktype == BlockType.MONSTER && blockColor != 6)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
    /// <summary>
    /// 블록이 사라졌을 때 나오는 이펙트 함수
    /// </summary>
    /// <returns></returns>
    IEnumerator CreateDeadEffect(int effectNum,GameObject effect)
    {
        
        effect.transform.position = this.transform.position;
        if(effectNum == 18)
        {
            //effect.transform.DOMove(chestnutPoint.position, 0.5f);
            ChestnutEffect();
            int RandomNum = Random.Range(0, 5);
            if (RandomNum == 0)
            {
                effect.transform.DOMoveX(chestnutPoint.position.x, 1f).SetEase(Ease.OutQuad);
                effect.transform.DOMoveY(chestnutPoint.position.y, 1f).SetEase(Ease.InQuad);
            }
            else if (RandomNum == 1)
            {
                effect.transform.DOMoveX(chestnutPoint.position.x, 1f).SetEase(Ease.InQuad);
                effect.transform.DOMoveY(chestnutPoint.position.y, 1f).SetEase(Ease.OutQuad);
            }
            else if (RandomNum == 2)
            {
                effect.transform.DOMoveX(chestnutPoint.position.x, 1f).SetEase(Ease.InSine);
                effect.transform.DOMoveY(chestnutPoint.position.y, 1f).SetEase(Ease.OutSine);
            }
            else if (RandomNum == 3)
            {
                effect.transform.DOMoveX(chestnutPoint.position.x, 1f).SetEase(Ease.OutSine);
                effect.transform.DOMoveY(chestnutPoint.position.y, 1f).SetEase(Ease.InSine);
            }
            else
            {
                effect.transform.DOMove(chestnutPoint.position, 1f, false);
            }
            effect.transform.DOScale(60,0.8f);
            yield return new WaitForSeconds(1f);
            effect.transform.DOScale(50,0.2f);

        }
        else
        {
            //effect.transform.DOMove(fertilizerPoint.position, 0.5f);
            ChestnutEffect();
            int RandomNum = Random.Range(0, 5);
            if (RandomNum == 0)
            {
                effect.transform.DOMoveX(fertilizerPoint.position.x, 1f).SetEase(Ease.OutQuad);
                effect.transform.DOMoveY(fertilizerPoint.position.y, 1f).SetEase(Ease.InQuad);
            }
            else if (RandomNum == 1)
            {
                effect.transform.DOMoveX(fertilizerPoint.position.x, 1f).SetEase(Ease.InQuad);
                effect.transform.DOMoveY(fertilizerPoint.position.y, 1f).SetEase(Ease.OutQuad);
            }
            else if (RandomNum == 2)
            {
                effect.transform.DOMoveX(fertilizerPoint.position.x, 1f).SetEase(Ease.InSine);
                effect.transform.DOMoveY(fertilizerPoint.position.y, 1f).SetEase(Ease.OutSine);
            }
            else if (RandomNum == 3)
            {
                effect.transform.DOMoveX(fertilizerPoint.position.x, 1f).SetEase(Ease.OutSine);
                effect.transform.DOMoveY(fertilizerPoint.position.y, 1f).SetEase(Ease.InSine);
            }
            else
            {
                effect.transform.DOMove(fertilizerPoint.position, 1f, false);
            }
            effect.transform.DOScale(60, 1f);
            yield return new WaitForSeconds(1f);
            effect.transform.DOScale(50, 0.1f);
            yield return new WaitForSeconds(2f);
            effect.gameObject.SetActive(false);
        }
        
        
    }
    /// <summary>
    /// 밤블록이 사라졌을때 나무의 레벨값에 따라 밤의 획득 정도를 설정하는 함수
    /// </summary>
    /// <returns></returns>
    int ChestnutPointInTreeMatchPuzzle()
    {
        int MinChestPoint = 0;
        int MaxChestPoint = 0;
        if(TileManager.instance.treeLevel >= 50)
        {
            MinChestPoint = 3;
            MaxChestPoint = 6;
        }
        else if (TileManager.instance.treeLevel >= 40)
        {
            MinChestPoint = 2;
            MaxChestPoint = 5;
        }
        else if (TileManager.instance.treeLevel >= 30)
        {
            MinChestPoint = 2;
            MaxChestPoint = 4;
        }
        else if (TileManager.instance.treeLevel >= 20)
        {
            MinChestPoint = 1;
            MaxChestPoint = 4;
        }
        else if (TileManager.instance.treeLevel >= 30)
        {
            MinChestPoint = 1;
            MaxChestPoint = 3;
        }
        else
        {
            MinChestPoint = 1;  
            MaxChestPoint = 2;
        }
        int RandomChestPoint = Random.Range(MinChestPoint+TreeMatchGameGameManager.instance.chestPointMin,MaxChestPoint+ TreeMatchGameGameManager.instance.chestPointMax);
        return RandomChestPoint;
    }
    /// <summary>
    /// 몬스터 블록이 사라졌을때 나무의 레벨값에 따라 비료의 획득 정도를 설정하는 함수
    /// </summary>
    /// <returns></returns>
    int FertilizerPointInTreeMatchPuzzle()
    {
        int minFertilizer = 0;
        int maxFertilizer = 0;
        if(TileManager.instance.treeLevel >= 50)
        {
            minFertilizer = 30;
            maxFertilizer = 40;
        }
        else if (TileManager.instance.treeLevel >= 40)
        {
            minFertilizer = 25;
            maxFertilizer = 30;
        }
        else if (TileManager.instance.treeLevel >= 30)
        {
            minFertilizer = 20;
            maxFertilizer = 25;
        }
        else if (TileManager.instance.treeLevel >= 20)
        {
            minFertilizer = 15;
            maxFertilizer = 20;
        }
        else if (TileManager.instance.treeLevel >= 10)
        {
            minFertilizer = 15;
            maxFertilizer = 10;
        }
        else
        {
            minFertilizer = 5;
            maxFertilizer = 10;
        }
        int RandomFer = Random.Range(minFertilizer + TreeMatchGameGameManager.instance.fertilizerPointMin, maxFertilizer + TreeMatchGameGameManager.instance.fertilizerPointMax);
        return RandomFer;
    }
}
