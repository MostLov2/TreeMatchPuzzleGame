using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Windows.Forms;
using Unity.VisualScripting;
using System.Linq;
using System.Web.Razor.Parser.SyntaxTree;
using Cysharp.Threading.Tasks.Triggers;
using UnityEditor.Rendering;

[System.Serializable]
public class tileRowArrayX
{
    public Tile[] tilesScriptX = new Tile[8];
}
[System.Serializable]
public class tileRowArrayY
{
    public tileRowArrayX[] tilesY = new tileRowArrayX[9];
}
[System.Serializable]
public class tileColumnArrayY
{
    public List<Tile> tilesScriptY = new List<Tile>();
}
[System.Serializable]
public class tileColumnArrayX
{
    public tileColumnArrayY[] tilesX = new tileColumnArrayY[8];
}

public class TileManager : MonoBehaviour
{
    public int comboCount = 0;
    public GameObject targetTile;
    public Vector3 MousePos;
    public GameObject tempBlock;
    public int tempColor;
    public bool isSwapping = false;
    public AudioClip[] clip;
    public static int eggCount;
    public static int wormCount;
    public static int wevvilCount;
    public static int squirrelCount;
    public static int beeHiveCount;
    public static int rabbitCount;
    public static int beeCount;
    public Vector2 targetPos;
    public List<Tile> destroyTile;
    public List<Tile> destroyTempTile;
    public List<GameObject> mudEffects;
    public List<GameObject> squirrelSkills;
    public List<GameObject> stunEffect;
    public Vector2 skillPoint;
    public int treeLevel;
    public int stickSkillLevel;
    public int spraySkillLevel;
    public bool hpUp = false;
    public Transform StartGame;
    bool useItem = false;
    #region Block
    public tileRowArrayY arrayRow;
    public tileColumnArrayX arrayColumn;
    public static TileManager instance;
    public GameObject[] tiles;
    public GameObject[] tilesRow;
    public Tile[] tileinScript;
    public bool[] tileEmpty;
    public GameObject[] createTile;
    public Transform[] tile;
    public Transform createTiles;
    public Transform[] ctiles;
    public List<GameObject> line0 = new List<GameObject>();
    public List<GameObject> line1 = new List<GameObject>();
    public List<GameObject> line2 = new List<GameObject>();
    public List<GameObject> line3 = new List<GameObject>();
    public List<GameObject> line4 = new List<GameObject>();
    public List<GameObject> line5 = new List<GameObject>();
    public List<GameObject> line6 = new List<GameObject>();
    public List<GameObject> line7 = new List<GameObject>();
    public List<GameObject> destroyBlock = new List<GameObject>();
    public bool nopeSwap = false;
    List<int> destroyBlockC = new List<int>();
    List<bool> destroyBlockB = new List<bool>();
    public int isHaveDestroyBlock  = 0;
    Tile firstTile;
    Tile lastTile;
    public List<Tile> targetTiles = new List<Tile>();
    public List<GameObject> targetBlock = new List<GameObject>();
    public HashSet<int> notDouble = new HashSet<int>();
    #endregion
    private void Awake()
    {
        instance = this;
        eggCount = 0;
        wormCount = 0;
        wevvilCount = 0;
        treeLevel = GameLogicManager.treeLevel;
        stickSkillLevel = MySqlSystem.dragonflyStickLevelPoint;
        spraySkillLevel = MySqlSystem.sprayLevelPoint;
        treeLevel = 50;
        spraySkillLevel = 5;
        stickSkillLevel = 5;
        createTiles = GameObject.FindGameObjectWithTag("CreateTile").transform;
        ctiles = createTiles.GetComponentsInChildren<Transform>();
        createTile = new GameObject[8];
        for (int i = 1; i < ctiles.Length; i++)
        {
            createTile[i - 1] = ctiles[i].gameObject;

        }
        TileRowSet();
        TileColumnSet();
        StartCoroutine(SettingBlock());
    }
    /// <summary>
    /// 타일을 가로 배열에 넣기
    /// </summary>
    void TileRowSet()
    {
        tilesRow = new GameObject[72];
        tileinScript = new Tile[72];
        tiles = new GameObject[72];
        Transform gamePanel = GameObject.FindGameObjectWithTag("GamePanel").transform;
        tile = gamePanel.GetComponentsInChildren<Transform>();
        for (int i = 1; i < tile.Length; i++)
        {
            tiles[i-1] = tile[i].gameObject;
        }
        for (int i = 71; i >= 0; i--)
        {
            tilesRow[i] = tiles[71 - i];
        }
        for (int i = 0; i < tiles.Length; i++)
        {
            tileinScript[i] = tilesRow[i].GetComponent<Tile>();
        }
        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                arrayRow.tilesY[y].tilesScriptX[x] = tileinScript[y * 8 + x];
            }
        }
    }
    /// <summary>
    /// 타일을 세로 배열에 넣기
    /// </summary>
    void TileColumnSet()
    {
        for (int i = 0; i <72; i++)
        {
            if(i%8 == 0)
            {
                arrayColumn.tilesX[0].tilesScriptY.Add(tileinScript[i]);
            }
            else if(i%8 == 1)
            {
                arrayColumn.tilesX[1].tilesScriptY.Add(tileinScript[i]);

            }
            else if(i%8 == 2)
            {
                arrayColumn.tilesX[2].tilesScriptY.Add(tileinScript[i]);

            }
            else if(i%8 == 3)
            {
                arrayColumn.tilesX[3].tilesScriptY.Add(tileinScript[i]);

            }
            else if(i%8 == 4)
            {
                arrayColumn.tilesX[4].tilesScriptY.Add(tileinScript[i]);

            }
            else if(i%8 == 5)
            {
                arrayColumn.tilesX[5].tilesScriptY.Add(tileinScript[i]);

            }
            else if(i%8 == 6)
            {
                arrayColumn.tilesX[6].tilesScriptY.Add(tileinScript[i]);

            }
            else
            {
                arrayColumn.tilesX[7].tilesScriptY.Add(tileinScript[i]);

            }
        }
    }
    /// <summary>
    /// 빈블록을 찾아 블록을 채우는 
    /// </summary>
    /// <returns></returns>
    IEnumerator SettingBlock()
    {
        
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (arrayColumn.tilesX[j].tilesScriptY[i].isEmpty)
                {
                    int randomBlock = RandomBlock();
                    GameObject block = OPBlock.instance.SetObject(randomBlock);
                    block.transform.position = createTile[7 - j].transform.position;
                    if(block.GetComponent<Block>().blockColor == 7)
                    {
                        eggCount++;
                    }
                    if (block.GetComponent<Block>().blockColor == 9)
                    {
                        squirrelCount++;
                    }
                    if (block.GetComponent<Block>().blockColor == 8)
                    {
                        rabbitCount++;
                    }
                    if (block.GetComponent<Block>().blockColor == 6)
                    {
                        beeHiveCount++;
                    }
                    if (i == 0)
                    {
                        block.transform.DOMove(arrayColumn.tilesX[j].tilesScriptY[i].transform.position, 0.5f);
                    }
                    else if (i == 1)
                    {
                        block.transform.DOMove(arrayColumn.tilesX[j].tilesScriptY[i].transform.position, 0.6f);
                    }
                    else if (i == 2)
                    {
                        block.transform.DOMove(arrayColumn.tilesX[j].tilesScriptY[i].transform.position, 0.7f);
                    }
                    else if (i == 3)
                    {
                        block.transform.DOMove(arrayColumn.tilesX[j].tilesScriptY[i].transform.position, 0.8f);
                    }
                    else if (i == 4)
                    {
                        block.transform.DOMove(arrayColumn.tilesX[j].tilesScriptY[i].transform.position, 0.9f);
                    }
                    else if (i == 5)
                    {
                        block.transform.DOMove(arrayColumn.tilesX[j].tilesScriptY[i].transform.position, 1f);
                    }
                    else if (i == 6)
                    {
                        block.transform.DOMove(arrayColumn.tilesX[j].tilesScriptY[i].transform.position, 1.1f);
                    }
                    else if (i == 7)
                    {
                        block.transform.DOMove(arrayColumn.tilesX[j].tilesScriptY[i].transform.position, 1.2f);
                    }
                    else if (i == 8)
                    {
                        block.transform.DOMove(arrayColumn.tilesX[j].tilesScriptY[i].transform.position, 1.3f);
                    }
                    arrayColumn.tilesX[j].tilesScriptY[i].isEmpty = false;
                    arrayColumn.tilesX[j].tilesScriptY[i].blockColor = randomBlock;
                    arrayColumn.tilesX[j].tilesScriptY[i].block = block;
                }
            }
        }
        yield return new WaitForSeconds(1.3f);
        StartCoroutine(DestroyBlock());
    }
    /// <summary>
    /// 블록이 부서졌을때 리필해주는 함수
    /// </summary>
    /// <returns></returns>
    IEnumerator RefillBlock()
    {
        
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (arrayColumn.tilesX[j].tilesScriptY[i].isEmpty)
                {
                    int randomBlock = RandomBlock();
                    GameObject block = OPBlock.instance.SetObject(randomBlock);
                    block.transform.localScale = Vector3.one;
                    block.transform.position = createTile[7 - j].transform.position;
                    if (block.GetComponent<Block>().blockColor == 7)
                    {
                        eggCount++;
                    }
                    if(block.GetComponent<Block>().blockColor == 9)
                    {
                        squirrelCount++;
                    }
                    if (block.GetComponent<Block>().blockColor == 8)
                    {
                        rabbitCount++;
                    }
                    if (block.GetComponent<Block>().blockColor == 6)
                    {
                        beeHiveCount++;
                    }
                    if (i == 0)
                    {
                        block.transform.DOMove(arrayColumn.tilesX[j].tilesScriptY[i].transform.position, 0.3f).SetEase(Ease.OutCubic);
                    }
                    else if (i == 1)
                    {
                        block.transform.DOMove(arrayColumn.tilesX[j].tilesScriptY[i].transform.position, 0.3f).SetEase(Ease.OutCubic);
                    }
                    else if (i == 2)
                    {
                        block.transform.DOMove(arrayColumn.tilesX[j].tilesScriptY[i].transform.position, 0.3f).SetEase(Ease.OutCubic);
                    }
                    else if (i == 3)
                    {
                        block.transform.DOMove(arrayColumn.tilesX[j].tilesScriptY[i].transform.position, 0.3f).SetEase(Ease.OutCubic);
                    }
                    else if (i == 4)
                    {
                        block.transform.DOMove(arrayColumn.tilesX[j].tilesScriptY[i].transform.position, 0.3f).SetEase(Ease.OutCubic);
                    }
                    else if (i == 5)
                    {
                        block.transform.DOMove(arrayColumn.tilesX[j].tilesScriptY[i].transform.position, 0.3f).SetEase(Ease.OutCubic);
                    }
                    else if (i == 6)
                    {
                        block.transform.DOMove(arrayColumn.tilesX[j].tilesScriptY[i].transform.position, 0.3f).SetEase(Ease.OutCubic);
                    }
                    else if (i == 7)
                    {
                        block.transform.DOMove(arrayColumn.tilesX[j].tilesScriptY[i].transform.position, 0.3f).SetEase(Ease.OutCubic);
                    }
                    else if (i == 8)
                    {
                        block.transform.DOMove(arrayColumn.tilesX[j].tilesScriptY[i].transform.position, 0.3f).SetEase(Ease.OutCubic);
                    }
                    arrayColumn.tilesX[j].tilesScriptY[i].isEmpty = false;
                    arrayColumn.tilesX[j].tilesScriptY[i].blockColor = randomBlock;
                    arrayColumn.tilesX[j].tilesScriptY[i].block = block;
                }
            }
        }
        for (int i = 0; i < tiles.Length; i++)
        {
            if(tiles[i].GetComponent<Tile>().blockColor == 6&& tiles[i].GetComponent<Tile>().block.GetComponent<Block>().IsHit)
            {
                StartCoroutine(BeeHiveCreateBee(i));
                tiles[i].GetComponent<Tile>().block.GetComponent<Block>().IsHit = false;
            }

        }
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(DestroyBlock());
    }
    /// <summary>
    /// 3개이상 연속으로 이어져 있는 밤블록 제거 함수
    /// </summary>
    /// <returns></returns>
    IEnumerator DestroyBlock()
    {
        destroyBlock.Clear();
        if (CountDownInPuzzle.isGameStart)
        {
            
            StartCoroutine(CreateSpray());
            StartCoroutine(CreateStick());
            for (int i = 0; i < destroyBlock.Count; i++)
            {
                Debug.Log("Did");
                StartCoroutine(DestroyBlock(destroyBlock[i].gameObject));
            }
            if (destroyBlock.Count >= 1 && CountDownInPuzzle.isGameStart)
            {
                StartCoroutine(EffectSoundMaker());
            }
            for (int i = 0; i < destroyTile.Count; i++)
            {
                if (!destroyTempTile.Contains(destroyTile[i]))
                {
                    destroyTempTile.Add(destroyTile[i]);
                    destroyTile[i].GetComponent<Tile>().block = null;
                }
            }
            destroyTile.Clear();
        }
        destroyBlock.Clear();
        isHaveDestroyBlock = 0;
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < 6; i++)
        {
            if (arrayRow.tilesY[0].tilesScriptX[i].blockColor == arrayRow.tilesY[0].tilesScriptX[i + 1].blockColor && arrayRow.tilesY[0].tilesScriptX[i].blockColor == arrayRow.tilesY[0].tilesScriptX[i + 2].blockColor)
            {
                if (arrayRow.tilesY[0].tilesScriptX[i].blockColor < 5)
                {
                    isHaveDestroyBlock++;
                    arrayRow.tilesY[0].tilesScriptX[i].isEmpty = true;
                    arrayRow.tilesY[0].tilesScriptX[i + 1].isEmpty = true;
                    arrayRow.tilesY[0].tilesScriptX[i + 2].isEmpty = true;
                    destroyBlock.Add(arrayRow.tilesY[0].tilesScriptX[i].block.gameObject);
                    destroyBlock.Add(arrayRow.tilesY[0].tilesScriptX[i + 1].block.gameObject);
                    destroyBlock.Add(arrayRow.tilesY[0].tilesScriptX[i + 2].block.gameObject);
                    destroyTile.Add(arrayRow.tilesY[0].tilesScriptX[i]);
                    destroyTile.Add(arrayRow.tilesY[0].tilesScriptX[i + 1]);
                    destroyTile.Add(arrayRow.tilesY[0].tilesScriptX[i + 2]);
                }
            }
            if (arrayRow.tilesY[1].tilesScriptX[i].blockColor == arrayRow.tilesY[1].tilesScriptX[i + 1].blockColor && arrayRow.tilesY[1].tilesScriptX[i].blockColor == arrayRow.tilesY[1].tilesScriptX[i + 2].blockColor)
            {
                if (arrayRow.tilesY[1].tilesScriptX[i].blockColor < 5)
                {
                    isHaveDestroyBlock++;
                    arrayRow.tilesY[1].tilesScriptX[i].isEmpty = true;
                    arrayRow.tilesY[1].tilesScriptX[i + 1].isEmpty = true;
                    arrayRow.tilesY[1].tilesScriptX[i + 2].isEmpty = true;
                    destroyBlock.Add(arrayRow.tilesY[1].tilesScriptX[i].block.gameObject);
                    destroyBlock.Add(arrayRow.tilesY[1].tilesScriptX[i + 1].block.gameObject);
                    destroyBlock.Add(arrayRow.tilesY[1].tilesScriptX[i + 2].block.gameObject);
                    destroyTile.Add(arrayRow.tilesY[1].tilesScriptX[i]);
                    destroyTile.Add(arrayRow.tilesY[1].tilesScriptX[i + 1]);
                    destroyTile.Add(arrayRow.tilesY[1].tilesScriptX[i + 2]);
                }
            }
            if (arrayRow.tilesY[2].tilesScriptX[i].blockColor == arrayRow.tilesY[2].tilesScriptX[i + 1].blockColor && arrayRow.tilesY[2].tilesScriptX[i].blockColor == arrayRow.tilesY[2].tilesScriptX[i + 2].blockColor)
            {
                if (arrayRow.tilesY[2].tilesScriptX[i].blockColor < 5)
                {
                    isHaveDestroyBlock++;
                    arrayRow.tilesY[2].tilesScriptX[i].isEmpty = true;
                    arrayRow.tilesY[2].tilesScriptX[i + 1].isEmpty = true;
                    arrayRow.tilesY[2].tilesScriptX[i + 2].isEmpty = true;
                    destroyBlock.Add(arrayRow.tilesY[2].tilesScriptX[i].block.gameObject);
                    destroyBlock.Add(arrayRow.tilesY[2].tilesScriptX[i + 1].block.gameObject);
                    destroyBlock.Add(arrayRow.tilesY[2].tilesScriptX[i + 2].block.gameObject);
                    destroyTile.Add(arrayRow.tilesY[2].tilesScriptX[i]);
                    destroyTile.Add(arrayRow.tilesY[2].tilesScriptX[i + 1]);
                    destroyTile.Add(arrayRow.tilesY[2].tilesScriptX[i + 2]);
                }
            }
            if (arrayRow.tilesY[3].tilesScriptX[i].blockColor == arrayRow.tilesY[3].tilesScriptX[i + 1].blockColor && arrayRow.tilesY[3].tilesScriptX[i].blockColor == arrayRow.tilesY[3].tilesScriptX[i + 2].blockColor)
            {
                if (arrayRow.tilesY[3].tilesScriptX[i].blockColor < 5)
                {
                    isHaveDestroyBlock++;
                    arrayRow.tilesY[3].tilesScriptX[i].isEmpty = true;
                    arrayRow.tilesY[3].tilesScriptX[i + 1].isEmpty = true;
                    arrayRow.tilesY[3].tilesScriptX[i + 2].isEmpty = true;
                    destroyBlock.Add(arrayRow.tilesY[3].tilesScriptX[i].block.gameObject);
                    destroyBlock.Add(arrayRow.tilesY[3].tilesScriptX[i + 1].block.gameObject);
                    destroyBlock.Add(arrayRow.tilesY[3].tilesScriptX[i + 2].block.gameObject);
                    destroyTile.Add(arrayRow.tilesY[3].tilesScriptX[i]);
                    destroyTile.Add(arrayRow.tilesY[3].tilesScriptX[i + 1]);
                    destroyTile.Add(arrayRow.tilesY[3].tilesScriptX[i + 2]);
                }
            }
            if (arrayRow.tilesY[4].tilesScriptX[i].blockColor == arrayRow.tilesY[4].tilesScriptX[i + 1].blockColor && arrayRow.tilesY[4].tilesScriptX[i].blockColor == arrayRow.tilesY[4].tilesScriptX[i + 2].blockColor)
            {
                if (arrayRow.tilesY[4].tilesScriptX[i].blockColor < 5)
                {
                    isHaveDestroyBlock++;
                    arrayRow.tilesY[4].tilesScriptX[i].isEmpty = true;
                    arrayRow.tilesY[4].tilesScriptX[i + 1].isEmpty = true;
                    arrayRow.tilesY[4].tilesScriptX[i + 2].isEmpty = true;
                    destroyBlock.Add(arrayRow.tilesY[4].tilesScriptX[i].block.gameObject);
                    destroyBlock.Add(arrayRow.tilesY[4].tilesScriptX[i + 1].block.gameObject);
                    destroyBlock.Add(arrayRow.tilesY[4].tilesScriptX[i + 2].block.gameObject);
                    destroyTile.Add(arrayRow.tilesY[4].tilesScriptX[i]);
                    destroyTile.Add(arrayRow.tilesY[4].tilesScriptX[i + 1]);
                    destroyTile.Add(arrayRow.tilesY[4].tilesScriptX[i + 2]);
                }
            }
            if (arrayRow.tilesY[5].tilesScriptX[i].blockColor == arrayRow.tilesY[5].tilesScriptX[i + 1].blockColor && arrayRow.tilesY[5].tilesScriptX[i].blockColor == arrayRow.tilesY[5].tilesScriptX[i + 2].blockColor)
            {
                if (arrayRow.tilesY[5].tilesScriptX[i].blockColor < 5)
                {
                    isHaveDestroyBlock++;
                    arrayRow.tilesY[5].tilesScriptX[i].isEmpty = true;
                    arrayRow.tilesY[5].tilesScriptX[i + 1].isEmpty = true;
                    arrayRow.tilesY[5].tilesScriptX[i + 2].isEmpty = true;
                    destroyBlock.Add(arrayRow.tilesY[5].tilesScriptX[i].block.gameObject);
                    destroyBlock.Add(arrayRow.tilesY[5].tilesScriptX[i + 1].block.gameObject);
                    destroyBlock.Add(arrayRow.tilesY[5].tilesScriptX[i + 2].block.gameObject);
                    destroyTile.Add(arrayRow.tilesY[5].tilesScriptX[i]);
                    destroyTile.Add(arrayRow.tilesY[5].tilesScriptX[i + 1]);
                    destroyTile.Add(arrayRow.tilesY[5].tilesScriptX[i + 2]);
                }
            }
            if (arrayRow.tilesY[6].tilesScriptX[i].blockColor == arrayRow.tilesY[6].tilesScriptX[i + 1].blockColor && arrayRow.tilesY[6].tilesScriptX[i].blockColor == arrayRow.tilesY[6].tilesScriptX[i + 2].blockColor)
            {
                if (arrayRow.tilesY[6].tilesScriptX[i].blockColor < 5)
                {
                    isHaveDestroyBlock++;
                    arrayRow.tilesY[6].tilesScriptX[i].isEmpty = true;
                    arrayRow.tilesY[6].tilesScriptX[i + 1].isEmpty = true;
                    arrayRow.tilesY[6].tilesScriptX[i + 2].isEmpty = true;
                    destroyBlock.Add(arrayRow.tilesY[6].tilesScriptX[i].block.gameObject);
                    destroyBlock.Add(arrayRow.tilesY[6].tilesScriptX[i + 1].block.gameObject);
                    destroyBlock.Add(arrayRow.tilesY[6].tilesScriptX[i + 2].block.gameObject);
                    destroyTile.Add(arrayRow.tilesY[6].tilesScriptX[i]);
                    destroyTile.Add(arrayRow.tilesY[6].tilesScriptX[i + 1]);
                    destroyTile.Add(arrayRow.tilesY[6].tilesScriptX[i + 2]);
                }
            }
            if (arrayRow.tilesY[7].tilesScriptX[i].blockColor == arrayRow.tilesY[7].tilesScriptX[i + 1].blockColor && arrayRow.tilesY[7].tilesScriptX[i].blockColor == arrayRow.tilesY[7].tilesScriptX[i + 2].blockColor)
            {
                if (arrayRow.tilesY[7].tilesScriptX[i].blockColor < 5)
                {
                    isHaveDestroyBlock++;
                    arrayRow.tilesY[7].tilesScriptX[i].isEmpty = true;
                    arrayRow.tilesY[7].tilesScriptX[i + 1].isEmpty = true;
                    arrayRow.tilesY[7].tilesScriptX[i + 2].isEmpty = true;
                    destroyBlock.Add(arrayRow.tilesY[7].tilesScriptX[i].block.gameObject);
                    destroyBlock.Add(arrayRow.tilesY[7].tilesScriptX[i + 1].block.gameObject);
                    destroyBlock.Add(arrayRow.tilesY[7].tilesScriptX[i + 2].block.gameObject);
                    destroyTile.Add(arrayRow.tilesY[7].tilesScriptX[i]);
                    destroyTile.Add(arrayRow.tilesY[7].tilesScriptX[i + 1]);
                    destroyTile.Add(arrayRow.tilesY[7].tilesScriptX[i + 2]);
                }
            }
            if (arrayRow.tilesY[8].tilesScriptX[i].blockColor == arrayRow.tilesY[8].tilesScriptX[i + 1].blockColor && arrayRow.tilesY[8].tilesScriptX[i].blockColor == arrayRow.tilesY[8].tilesScriptX[i + 2].blockColor)
            {
                if (arrayRow.tilesY[8].tilesScriptX[i].blockColor < 5)
                {
                    isHaveDestroyBlock++;
                    arrayRow.tilesY[8].tilesScriptX[i].isEmpty = true;
                    arrayRow.tilesY[8].tilesScriptX[i + 1].isEmpty = true;
                    arrayRow.tilesY[8].tilesScriptX[i + 2].isEmpty = true;
                    destroyBlock.Add(arrayRow.tilesY[8].tilesScriptX[i].block.gameObject);
                    destroyBlock.Add(arrayRow.tilesY[8].tilesScriptX[i + 1].block.gameObject);
                    destroyBlock.Add(arrayRow.tilesY[8].tilesScriptX[i + 2].block.gameObject);
                    destroyTile.Add(arrayRow.tilesY[8].tilesScriptX[i]);
                    destroyTile.Add(arrayRow.tilesY[8].tilesScriptX[i + 1]);
                    destroyTile.Add(arrayRow.tilesY[8].tilesScriptX[i + 2]);
                }
            }
        }
        for (int i = 0; i < 7; i++)
        {
            if (arrayColumn.tilesX[0].tilesScriptY[i].blockColor == arrayColumn.tilesX[0].tilesScriptY[i + 1].blockColor && arrayColumn.tilesX[0].tilesScriptY[i].blockColor == arrayColumn.tilesX[0].tilesScriptY[i + 2].blockColor)
            {
                if (arrayColumn.tilesX[0].tilesScriptY[i].blockColor < 5)
                {
                    isHaveDestroyBlock++;
                    arrayColumn.tilesX[0].tilesScriptY[i].isEmpty = true;
                    arrayColumn.tilesX[0].tilesScriptY[i + 1].isEmpty = true;
                    arrayColumn.tilesX[0].tilesScriptY[i + 2].isEmpty = true;
                    destroyBlock.Add(arrayColumn.tilesX[0].tilesScriptY[i].block.gameObject);
                    destroyBlock.Add(arrayColumn.tilesX[0].tilesScriptY[i + 1].block.gameObject);
                    destroyBlock.Add(arrayColumn.tilesX[0].tilesScriptY[i + 2].block.gameObject);
                    destroyTile.Add(arrayColumn.tilesX[0].tilesScriptY[i]);
                    destroyTile.Add(arrayColumn.tilesX[0].tilesScriptY[i + 1]);
                    destroyTile.Add(arrayColumn.tilesX[0].tilesScriptY[i + 2]);
                }
            }
            if (arrayColumn.tilesX[1].tilesScriptY[i].blockColor == arrayColumn.tilesX[1].tilesScriptY[i + 1].blockColor && arrayColumn.tilesX[1].tilesScriptY[i].blockColor == arrayColumn.tilesX[1].tilesScriptY[i + 2].blockColor)
            {
                if (arrayColumn.tilesX[1].tilesScriptY[i].blockColor < 5)
                {
                    isHaveDestroyBlock++;
                    arrayColumn.tilesX[1].tilesScriptY[i].isEmpty = true;
                    arrayColumn.tilesX[1].tilesScriptY[i + 1].isEmpty = true;
                    arrayColumn.tilesX[1].tilesScriptY[i + 2].isEmpty = true;
                    destroyBlock.Add(arrayColumn.tilesX[1].tilesScriptY[i].block.gameObject);
                    destroyBlock.Add(arrayColumn.tilesX[1].tilesScriptY[i + 1].block.gameObject);
                    destroyBlock.Add(arrayColumn.tilesX[1].tilesScriptY[i + 2].block.gameObject);
                    destroyTile.Add(arrayColumn.tilesX[1].tilesScriptY[i]);
                    destroyTile.Add(arrayColumn.tilesX[1].tilesScriptY[i + 1]);
                    destroyTile.Add(arrayColumn.tilesX[1].tilesScriptY[i + 2]);
                }
            }
            if (arrayColumn.tilesX[2].tilesScriptY[i].blockColor == arrayColumn.tilesX[2].tilesScriptY[i + 1].blockColor && arrayColumn.tilesX[2].tilesScriptY[i].blockColor == arrayColumn.tilesX[2].tilesScriptY[i + 2].blockColor)
            {
                if (arrayColumn.tilesX[2].tilesScriptY[i].blockColor < 5)
                {
                    isHaveDestroyBlock++;
                    arrayColumn.tilesX[2].tilesScriptY[i].isEmpty = true;
                    arrayColumn.tilesX[2].tilesScriptY[i + 1].isEmpty = true;
                    arrayColumn.tilesX[2].tilesScriptY[i + 2].isEmpty = true;
                    destroyBlock.Add(arrayColumn.tilesX[2].tilesScriptY[i].block.gameObject);
                    destroyBlock.Add(arrayColumn.tilesX[2].tilesScriptY[i + 1].block.gameObject);
                    destroyBlock.Add(arrayColumn.tilesX[2].tilesScriptY[i + 2].block.gameObject);
                    destroyTile.Add(arrayColumn.tilesX[2].tilesScriptY[i]);
                    destroyTile.Add(arrayColumn.tilesX[2].tilesScriptY[i + 1]);
                    destroyTile.Add(arrayColumn.tilesX[2].tilesScriptY[i + 2]);
                }
            }
            if (arrayColumn.tilesX[3].tilesScriptY[i].blockColor == arrayColumn.tilesX[3].tilesScriptY[i + 1].blockColor && arrayColumn.tilesX[3].tilesScriptY[i].blockColor == arrayColumn.tilesX[3].tilesScriptY[i + 2].blockColor)
            {
                if (arrayColumn.tilesX[3].tilesScriptY[i].blockColor < 5)
                {
                    isHaveDestroyBlock++;
                    arrayColumn.tilesX[3].tilesScriptY[i].isEmpty = true;
                    arrayColumn.tilesX[3].tilesScriptY[i + 1].isEmpty = true;
                    arrayColumn.tilesX[3].tilesScriptY[i + 2].isEmpty = true;
                    destroyBlock.Add(arrayColumn.tilesX[3].tilesScriptY[i].block.gameObject);
                    destroyBlock.Add(arrayColumn.tilesX[3].tilesScriptY[i + 1].block.gameObject);
                    destroyBlock.Add(arrayColumn.tilesX[3].tilesScriptY[i + 2].block.gameObject);
                    destroyTile.Add(arrayColumn.tilesX[3].tilesScriptY[i]);
                    destroyTile.Add(arrayColumn.tilesX[3].tilesScriptY[i + 1]);
                    destroyTile.Add(arrayColumn.tilesX[3].tilesScriptY[i + 2]);
                }
            }
            if (arrayColumn.tilesX[4].tilesScriptY[i].blockColor == arrayColumn.tilesX[4].tilesScriptY[i + 1].blockColor && arrayColumn.tilesX[4].tilesScriptY[i].blockColor == arrayColumn.tilesX[4].tilesScriptY[i + 2].blockColor)
            {
                if (arrayColumn.tilesX[4].tilesScriptY[i].blockColor < 5)
                {
                    isHaveDestroyBlock++;
                    arrayColumn.tilesX[4].tilesScriptY[i].isEmpty = true;
                    arrayColumn.tilesX[4].tilesScriptY[i + 1].isEmpty = true;
                    arrayColumn.tilesX[4].tilesScriptY[i + 2].isEmpty = true;
                    destroyBlock.Add(arrayColumn.tilesX[4].tilesScriptY[i].block.gameObject);
                    destroyBlock.Add(arrayColumn.tilesX[4].tilesScriptY[i + 1].block.gameObject);
                    destroyBlock.Add(arrayColumn.tilesX[4].tilesScriptY[i + 2].block.gameObject);
                    destroyTile.Add(arrayColumn.tilesX[4].tilesScriptY[i]);
                    destroyTile.Add(arrayColumn.tilesX[4].tilesScriptY[i + 1]);
                    destroyTile.Add(arrayColumn.tilesX[4].tilesScriptY[i + 2]);
                }
            }
            if (arrayColumn.tilesX[5].tilesScriptY[i].blockColor == arrayColumn.tilesX[5].tilesScriptY[i + 1].blockColor && arrayColumn.tilesX[5].tilesScriptY[i].blockColor == arrayColumn.tilesX[5].tilesScriptY[i + 2].blockColor)
            {
                if (arrayColumn.tilesX[5].tilesScriptY[i].blockColor < 5)
                {
                    isHaveDestroyBlock++;
                    arrayColumn.tilesX[5].tilesScriptY[i].isEmpty = true;
                    arrayColumn.tilesX[5].tilesScriptY[i + 1].isEmpty = true;
                    arrayColumn.tilesX[5].tilesScriptY[i + 2].isEmpty = true;
                    destroyBlock.Add(arrayColumn.tilesX[5].tilesScriptY[i].block.gameObject);
                    destroyBlock.Add(arrayColumn.tilesX[5].tilesScriptY[i + 1].block.gameObject);
                    destroyBlock.Add(arrayColumn.tilesX[5].tilesScriptY[i + 2].block.gameObject);
                    destroyTile.Add(arrayColumn.tilesX[5].tilesScriptY[i]);
                    destroyTile.Add(arrayColumn.tilesX[5].tilesScriptY[i + 1]);
                    destroyTile.Add(arrayColumn.tilesX[5].tilesScriptY[i + 2]);
                }
            }
            if (arrayColumn.tilesX[6].tilesScriptY[i].blockColor == arrayColumn.tilesX[6].tilesScriptY[i + 1].blockColor && arrayColumn.tilesX[6].tilesScriptY[i].blockColor == arrayColumn.tilesX[6].tilesScriptY[i + 2].blockColor)
            {
                if (arrayColumn.tilesX[6].tilesScriptY[i].blockColor < 5)
                {
                    isHaveDestroyBlock++;
                    arrayColumn.tilesX[6].tilesScriptY[i].isEmpty = true;
                    arrayColumn.tilesX[6].tilesScriptY[i + 1].isEmpty = true;
                    arrayColumn.tilesX[6].tilesScriptY[i + 2].isEmpty = true;
                    destroyBlock.Add(arrayColumn.tilesX[6].tilesScriptY[i].block.gameObject);
                    destroyBlock.Add(arrayColumn.tilesX[6].tilesScriptY[i + 1].block.gameObject);
                    destroyBlock.Add(arrayColumn.tilesX[6].tilesScriptY[i + 2].block.gameObject);
                    destroyTile.Add(arrayColumn.tilesX[6].tilesScriptY[i]);
                    destroyTile.Add(arrayColumn.tilesX[6].tilesScriptY[i + 1]);
                    destroyTile.Add(arrayColumn.tilesX[6].tilesScriptY[i + 2]);
                }
            }
            if (arrayColumn.tilesX[7].tilesScriptY[i].blockColor == arrayColumn.tilesX[7].tilesScriptY[i + 1].blockColor && arrayColumn.tilesX[7].tilesScriptY[i].blockColor == arrayColumn.tilesX[7].tilesScriptY[i + 2].blockColor)
            {
                if (arrayColumn.tilesX[7].tilesScriptY[i].blockColor < 5)
                {
                    isHaveDestroyBlock++;
                    arrayColumn.tilesX[7].tilesScriptY[i].isEmpty = true;
                    arrayColumn.tilesX[7].tilesScriptY[i + 1].isEmpty = true;
                    arrayColumn.tilesX[7].tilesScriptY[i + 2].isEmpty = true;
                    destroyBlock.Add(arrayColumn.tilesX[7].tilesScriptY[i].block.gameObject);
                    destroyBlock.Add(arrayColumn.tilesX[7].tilesScriptY[i + 1].block.gameObject);
                    destroyBlock.Add(arrayColumn.tilesX[7].tilesScriptY[i + 2].block.gameObject);
                    destroyTile.Add(arrayColumn.tilesX[7].tilesScriptY[i]);
                    destroyTile.Add(arrayColumn.tilesX[7].tilesScriptY[i + 1]);
                    destroyTile.Add(arrayColumn.tilesX[7].tilesScriptY[i + 2]);
                }
            }
        }
        for (int i = 0; i < destroyBlock.Count; i++)
        {
            StartCoroutine(DestroyBlock(destroyBlock[i].gameObject));
        }
        if (destroyBlock.Count >= 1&&CountDownInPuzzle.isGameStart)
        {
            StartCoroutine(EffectSoundMaker());
        }
        for (int i = 0; i < destroyTile.Count; i++)
        {
            if (!destroyTempTile.Contains(destroyTile[i]))
            {
                destroyTempTile.Add(destroyTile[i]);
            }
        }
        destroyTile.Clear();
        if(isHaveDestroyBlock != 0&&CountDownInPuzzle.isGameStart)
        {
            comboCount++;
            Debug.Log(comboCount);
            if (comboCount >= 6)
            {
                ComboSystem.instance.ComboTextUp();
                TreeMatchGameGameManager.TimeCount += 3;
                Debug.Log("did");
            }
            else if (comboCount >= 4)
            {
                ComboSystem.instance.ComboTextUp();
                TreeMatchGameGameManager.TimeCount += 2;
                Debug.Log("did1");
            }
            else if (comboCount >= 2)
            {
                ComboSystem.instance.ComboTextUp();
                TreeMatchGameGameManager.TimeCount += 1;
                Debug.Log("did2");
            }
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(DamgeToBlock());
    }
    
    /// <summary>
    /// 사라지는 블록 연출 함수
    /// </summary>
    /// <param name="block"></param>
    /// <returns></returns>
    IEnumerator DestroyBlock(GameObject block)
    {
        block.transform.DOScale(Vector3.zero, 0.1f);
        yield return new WaitForSeconds(0.1f);
        block.GetComponent<Block>().ScoreUp();
    }
    /// <summary>
    /// 블록이 부서지고 블록들을 아래로 내리는 함수
    /// </summary>
    /// <returns></returns>
    IEnumerator PullDownBlock()
    {
        if(isHaveDestroyBlock == 0)
        {
            if (CountDownInPuzzle.isGameStart)
            {
                comboCount = 0;
                for (int i = 0; i < tiles.Length; i++)
                {
                    if (tiles[i].GetComponent<Tile>().block != null)
                    {
                        tiles[i].GetComponent<Tile>().block.GetComponent<Block>().SwapCount++;
                        hpUp = true;
                    }
                }
                if (!nopeSwap)
                { 
                    for (int i = 0; i < mudEffects.Count; i++)
                    {
                        mudEffects[i].GetComponent<MudEffect>().SwapCount++;
                    }
                    for (int i = 0; i < squirrelSkills.Count; i++)
                    {
                        squirrelSkills[i].GetComponent<SquirrelEffect>().SwapCount++;
                    }
                    for (int i = 0; i < stunEffect.Count; i++)
                    {
                        stunEffect[i].GetComponent<StunEffect>().SwapCount++;

                    }
                }
            }
        }
        line0.Clear();
        line1.Clear();
        line2.Clear();
        line3.Clear();
        line4.Clear();
        line5.Clear();
        line6.Clear();
        line7.Clear();
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < arrayColumn.tilesX[0].tilesScriptY.Count; i++)
        {
            if (!arrayColumn.tilesX[0].tilesScriptY[i].isEmpty)
            {
                line7.Add(arrayColumn.tilesX[0].tilesScriptY[i].block);
            }
            if (!arrayColumn.tilesX[1].tilesScriptY[i].isEmpty)
            {
                line6.Add(arrayColumn.tilesX[1].tilesScriptY[i].block);
            }
            if (!arrayColumn.tilesX[2].tilesScriptY[i].isEmpty)
            {
                line5.Add(arrayColumn.tilesX[2].tilesScriptY[i].block);
            }
            if (!arrayColumn.tilesX[3].tilesScriptY[i].isEmpty)
            {
                line4.Add(arrayColumn.tilesX[3].tilesScriptY[i].block);
            }
            if (!arrayColumn.tilesX[4].tilesScriptY[i].isEmpty)
            {
                line3.Add(arrayColumn.tilesX[4].tilesScriptY[i].block);
            }
            if (!arrayColumn.tilesX[5].tilesScriptY[i].isEmpty)
            {
                line2.Add(arrayColumn.tilesX[5].tilesScriptY[i].block);
            }
            if (!arrayColumn.tilesX[6].tilesScriptY[i].isEmpty)
            {
                line1.Add(arrayColumn.tilesX[6].tilesScriptY[i].block);
            }
            if (!arrayColumn.tilesX[7].tilesScriptY[i].isEmpty)
            {
                line0.Add(arrayColumn.tilesX[7].tilesScriptY[i].block);
            }
        }
        for (int y = 0; y < arrayColumn.tilesX.Length; y++)
        {
            for (int x = 0; x < arrayColumn.tilesX[y].tilesScriptY.Count; x++)
            {
                arrayColumn.tilesX[y].tilesScriptY[x].isEmpty = false;
            }
        }
        for (int i = 0; i < line0.Count; i++)
        {
            line0[i].transform.DOMove(arrayColumn.tilesX[7].tilesScriptY[i].transform.position, 0.3f).SetEase(Ease.OutCubic);
            arrayColumn.tilesX[7].tilesScriptY[i].block = line0[i];
        }
        for (int i = 0; i < line1.Count; i++)
        {
            line1[i].transform.DOMove(arrayColumn.tilesX[6].tilesScriptY[i].transform.position, 0.3f).SetEase(Ease.OutCubic);
            arrayColumn.tilesX[6].tilesScriptY[i].block = line1[i];
        }
        for (int i = 0; i < line2.Count; i++)
        {
            line2[i].transform.DOMove(arrayColumn.tilesX[5].tilesScriptY[i].transform.position, 0.3f).SetEase(Ease.OutCubic);
            arrayColumn.tilesX[5].tilesScriptY[i].block = line2[i];
        }
        for (int i = 0; i < line3.Count; i++)
        {
            line3[i].transform.DOMove(arrayColumn.tilesX[4].tilesScriptY[i].transform.position, 0.3f).SetEase(Ease.OutCubic);
            arrayColumn.tilesX[4].tilesScriptY[i].block = line3[i];
        }
        for (int i = 0; i < line4.Count; i++)
        {
            line4[i].transform.DOMove(arrayColumn.tilesX[3].tilesScriptY[i].transform.position, 0.3f).SetEase(Ease.OutCubic);
            arrayColumn.tilesX[3].tilesScriptY[i].block = line4[i];
        }
        for (int i = 0; i < line5.Count; i++)
        {
            line5[i].transform.DOMove(arrayColumn.tilesX[2].tilesScriptY[i].transform.position, 0.3f).SetEase(Ease.OutCubic);
            arrayColumn.tilesX[2].tilesScriptY[i].block = line5[i];
        }
        for (int i = 0; i < line6.Count; i++)
        {
            line6[i].transform.DOMove(arrayColumn.tilesX[1].tilesScriptY[i].transform.position, 0.3f).SetEase(Ease.OutCubic);
            arrayColumn.tilesX[1].tilesScriptY[i].block = line6[i];
        }
        for (int i = 0; i < line7.Count; i++)
        {
            line7[i].transform.DOMove(arrayColumn.tilesX[0].tilesScriptY[i].transform.position, 0.3f).SetEase(Ease.OutCubic);
            arrayColumn.tilesX[0].tilesScriptY[i].block = line7[i];
        }
        for (int i = 8; i >= line0.Count; i--)
        {
            arrayColumn.tilesX[7].tilesScriptY[i].isEmpty = true;
        }
        for (int i = 8; i >= line1.Count; i--)
        {
            arrayColumn.tilesX[6].tilesScriptY[i].isEmpty = true;
        }
        for (int i = 8; i >= line2.Count; i--)
        {
            arrayColumn.tilesX[5].tilesScriptY[i].isEmpty = true;
        }
        for (int i = 8; i >= line3.Count; i--)
        {
            arrayColumn.tilesX[4].tilesScriptY[i].isEmpty = true;
        }
        for (int i = 8; i >= line4.Count; i--)
        {
            arrayColumn.tilesX[3].tilesScriptY[i].isEmpty = true;
        }
        for (int i = 8; i >= line5.Count; i--)
        {
            arrayColumn.tilesX[2].tilesScriptY[i].isEmpty = true;
        }
        for (int i = 8; i >= line6.Count; i--)
        {
            arrayColumn.tilesX[1].tilesScriptY[i].isEmpty = true;
        }
        for (int i = 8; i >= line7.Count; i--)
        {
            arrayColumn.tilesX[0].tilesScriptY[i].isEmpty = true;
        }
        //라인에 색 바꾸기
        for (int i = 0; i < line0.Count; i++)
        {
            arrayColumn.tilesX[7].tilesScriptY[i].blockColor = line0[i].GetComponent<Block>().blockColor;
        }
        for (int i = 0; i < line1.Count; i++)
        {
            arrayColumn.tilesX[6].tilesScriptY[i].blockColor = line1[i].GetComponent<Block>().blockColor;
        }
        for (int i = 0; i < line2.Count; i++)
        {
            arrayColumn.tilesX[5].tilesScriptY[i].blockColor = line2[i].GetComponent<Block>().blockColor;
        }
        for (int i = 0; i < line3.Count; i++)
        {
            arrayColumn.tilesX[4].tilesScriptY[i].blockColor = line3[i].GetComponent<Block>().blockColor;
        }
        for (int i = 0; i < line4.Count; i++)
        {
            arrayColumn.tilesX[3].tilesScriptY[i].blockColor = line4[i].GetComponent<Block>().blockColor;
        }
        for (int i = 0; i < line5.Count; i++)
        {
            arrayColumn.tilesX[2].tilesScriptY[i].blockColor = line5[i].GetComponent<Block>().blockColor;
        }
        for (int i = 0; i < line6.Count; i++)
        {
            arrayColumn.tilesX[1].tilesScriptY[i].blockColor = line6[i].GetComponent<Block>().blockColor;
        }
        for (int i = 0; i < line7.Count; i++)
        {
            arrayColumn.tilesX[0].tilesScriptY[i].blockColor = line7[i].GetComponent<Block>().blockColor;
        }
        if (isHaveDestroyBlock > 0|| useItem)
        {
            StartCoroutine(RefillBlock());
            useItem = false;
        }
        
    }
    /// <summary>
    /// 다음 블록을 램덤으로 생성하는 함수 
    /// </summary>
    /// <returns></returns>
    int RandomBlock()
    {
        int randomBlockColor = 0;
        if (treeLevel >= 50)//토끼 2 , 벌집 1 , 바구미 2 , 다람쥐 1
        {
            randomBlockColor = Random.Range(0, 6);
            if (rabbitCount < 2 && randomBlockColor == 5)
            {
                randomBlockColor =  8;
            }
            else if(beeHiveCount< 1 && randomBlockColor == 5)
            {
                randomBlockColor = 6;
            }
            else if (eggCount+wormCount+wevvilCount < 2 && randomBlockColor == 5)
            {
                randomBlockColor = 7;
            }
            else if(squirrelCount < 1 && randomBlockColor == 5)
            {
                randomBlockColor = 9;
            }
            else
            {
                randomBlockColor = Random.Range(0, 5);
            }
            return randomBlockColor;
        }
        else if(treeLevel>= 40)//토끼 2 벌집 1 바구미 2 
        {
            randomBlockColor = Random.Range(0, 6);
            if (rabbitCount < 2 && randomBlockColor == 5)
            {
                randomBlockColor = 8;
            }
            else if (beeHiveCount < 1 && randomBlockColor == 5)
            {
                randomBlockColor = 6;
            }
            else if (eggCount + wormCount + wevvilCount < 2 && randomBlockColor == 5)
            {
                randomBlockColor = 7;
            }
            else
            {
                randomBlockColor = Random.Range(0, 5);
            }
            return randomBlockColor;
        }
        else if (treeLevel >= 30)//토끼 2 벌집 1
        {
            randomBlockColor = Random.Range(0, 6);
            if (rabbitCount < 2 && randomBlockColor == 5)
            {
                randomBlockColor = 8;
            }
            else if (beeHiveCount < 1 && randomBlockColor == 5)
            {
                randomBlockColor = 6;
            }
            else
            {
                randomBlockColor = Random.Range(0, 5);
            }
            return randomBlockColor;
        }
        else //바구미 4, 다람쥐 1
        {
            randomBlockColor = Random.Range(0, 6);
            if (squirrelCount < 1 && randomBlockColor == 5)
            {
                randomBlockColor = 9;
            }
            else if (eggCount + wormCount + wevvilCount < 4 && randomBlockColor == 5)
            {
                randomBlockColor = 7;
            }
            else
            {
                randomBlockColor = Random.Range(0, 5);
            }
            return randomBlockColor;
        }
        return randomBlockColor;
    }
    /// <summary>
    /// 몬스터 블록에 데미지를 주는 함수
    /// </summary>
    /// <returns></returns>
    IEnumerator DamgeToBlock()
    {
        for (int i = 0; i < destroyTempTile.Count; i++)
        {
            targetPos = destroyTempTile[i].GetComponent<Tile>().pos;
            for (int j = 0; j < tiles.Length; j++)
            {
                if(tiles[j].GetComponent<Tile>().pos.x <= 7&&CountDownInPuzzle.isGameStart)
                {
                    if(tiles[j].GetComponent<Tile>().pos == (targetPos + new Vector2(1, 0)))
                    {
                        tiles[j].GetComponent<Tile>().block.GetComponent<Block>().hp--;
                        if(tiles[j].GetComponent<Tile>().block.GetComponent<Animator>() != null)
                        {
                            if(tiles[j].GetComponent<Tile>().block.GetComponent<Block>().hp >= 1)
                            {
                                tiles[j].GetComponent<Tile>().block.GetComponent<Animator>().SetTrigger("IsHit");
                                GameObject effect = BlockEffectOPManager.instance.SetObject(11);
                                effect.transform.position = tiles[j].GetComponent<Tile>().block.transform.position;
                                tiles[j].GetComponent<Tile>().block.GetComponent<Block>().IsHit = true;
                            }
                        }
                        if (tiles[j].GetComponent<Tile>().block.transform.childCount >= 2)
                        {
                            tiles[j].GetComponent<Tile>().block.transform.GetChild(0).gameObject.SetActive(false);
                            tiles[j].GetComponent<Tile>().block.transform.GetChild(1).gameObject.SetActive(false);
                            tiles[j].GetComponent<Tile>().block.transform.GetChild(0).transform.SetParent(GameObject.FindGameObjectWithTag("MiddleCanvas").transform);
                            tiles[j].GetComponent<Tile>().block.transform.GetChild(0).transform.SetParent(GameObject.FindGameObjectWithTag("MiddleCanvas").transform);
                            GameObject effect = BlockEffectOPManager.instance.SetObject(16);
                            effect.transform.position = tiles[j].transform.position;
                            if (tiles[j].GetComponent<Tile>().block.name == "RedBlock(Clone)")
                            {
                                tiles[j].GetComponent<Tile>().block.GetComponent<Block>().blockColor = 0;
                                tiles[j].GetComponent<Tile>().blockColor = 0;
                            }
                            else if (tiles[j].GetComponent<Tile>().block.name == "YellowBlock(Clone)")
                            {
                                tiles[j].GetComponent<Tile>().block.GetComponent<Block>().blockColor = 1;
                                tiles[j].GetComponent<Tile>().blockColor = 1;
                            }
                            else if (tiles[j].GetComponent<Tile>().block.name == "GreenBlock(Clone)")
                            {
                                tiles[j].GetComponent<Tile>().block.GetComponent<Block>().blockColor = 2;
                                tiles[j].GetComponent<Tile>().blockColor = 2;
                            }
                            else if (tiles[j].GetComponent<Tile>().block.name == "BlueBlock(Clone)")
                            {
                                tiles[j].GetComponent<Tile>().block.GetComponent<Block>().blockColor = 3;
                                tiles[j].GetComponent<Tile>().blockColor = 3;
                            }
                            else if (tiles[j].GetComponent<Tile>().block.name == "PurpleBlock(Clone)")
                            {
                                tiles[j].GetComponent<Tile>().block.GetComponent<Block>().blockColor = 4;
                                tiles[j].GetComponent<Tile>().blockColor = 4;
                            }
                        }
                        //Debug.Log(targetPos + new Vector2(1, 0));
                    }
                }
                if (tiles[j].GetComponent<Tile>().pos.x >= 0 && CountDownInPuzzle.isGameStart)
                {
                    if (tiles[j].GetComponent<Tile>().pos == (targetPos + new Vector2(-1, 0)))
                    {
                        tiles[j].GetComponent<Tile>().block.GetComponent<Block>().hp--;
                        if (tiles[j].GetComponent<Tile>().block.GetComponent<Animator>() != null)
                        {
                            if (tiles[j].GetComponent<Tile>().block.GetComponent<Block>().hp >= 1)
                            {
                                tiles[j].GetComponent<Tile>().block.GetComponent<Animator>().SetTrigger("IsHit");
                                GameObject effect = BlockEffectOPManager.instance.SetObject(11);
                                effect.transform.position = tiles[j].GetComponent<Tile>().block.transform.position;
                                tiles[j].GetComponent<Tile>().block.GetComponent<Block>().IsHit = true;
                            }
                        }
                        if (tiles[j].GetComponent<Tile>().block.transform.childCount >= 2)
                        {
                            GameObject effect = BlockEffectOPManager.instance.SetObject(16);
                            effect.transform.position = tiles[j].transform.position;
                            tiles[j].GetComponent<Tile>().block.transform.GetChild(0).gameObject.SetActive(false);
                            tiles[j].GetComponent<Tile>().block.transform.GetChild(1).gameObject.SetActive(false);
                            tiles[j].GetComponent<Tile>().block.transform.GetChild(0).transform.SetParent(GameObject.FindGameObjectWithTag("MiddleCanvas").transform);
                            tiles[j].GetComponent<Tile>().block.transform.GetChild(0).transform.SetParent(GameObject.FindGameObjectWithTag("MiddleCanvas").transform);
                            if (tiles[j].GetComponent<Tile>().block.name == "RedBlock(Clone)")
                            {
                                tiles[j].GetComponent<Tile>().block.GetComponent<Block>().blockColor = 0;
                                tiles[j].GetComponent<Tile>().blockColor = 0;
                            }
                            else if (tiles[j].GetComponent<Tile>().block.name == "YellowBlock(Clone)")
                            {
                                tiles[j].GetComponent<Tile>().block.GetComponent<Block>().blockColor = 1;
                                tiles[j].GetComponent<Tile>().blockColor = 1;
                            }
                            else if (tiles[j].GetComponent<Tile>().block.name == "GreenBlock(Clone)")
                            {
                                tiles[j].GetComponent<Tile>().block.GetComponent<Block>().blockColor = 2;
                                tiles[j].GetComponent<Tile>().blockColor = 2;
                            }
                            else if (tiles[j].GetComponent<Tile>().block.name == "BlueBlock(Clone)")
                            {
                                tiles[j].GetComponent<Tile>().block.GetComponent<Block>().blockColor = 3;
                                tiles[j].GetComponent<Tile>().blockColor = 3;
                            }
                            else if (tiles[j].GetComponent<Tile>().block.name == "PurpleBlock(Clone)")
                            {
                                tiles[j].GetComponent<Tile>().block.GetComponent<Block>().blockColor = 4;
                                tiles[j].GetComponent<Tile>().blockColor = 4;
                            }
                        }
                        //Debug.Log(targetPos + new Vector2(-1, 0));
                    }
                }
                if (tiles[j].GetComponent<Tile>().pos.y <= 8 && CountDownInPuzzle.isGameStart)
                {
                    if (tiles[j].GetComponent<Tile>().pos == (targetPos + new Vector2(0, 1)))
                    {
                        tiles[j].GetComponent<Tile>().block.GetComponent<Block>().hp--;
                        if (tiles[j].GetComponent<Tile>().block.GetComponent<Animator>() != null)
                        {
                            if (tiles[j].GetComponent<Tile>().block.GetComponent<Block>().hp >= 1)
                            {
                                tiles[j].GetComponent<Tile>().block.GetComponent<Animator>().SetTrigger("IsHit");
                                GameObject effect = BlockEffectOPManager.instance.SetObject(11);
                                effect.transform.position = tiles[j].GetComponent<Tile>().block.transform.position;
                                tiles[j].GetComponent<Tile>().block.GetComponent<Block>().IsHit = true;
                            }
                        }
                        if (tiles[j].GetComponent<Tile>().block.transform.childCount >= 2)
                        {
                            GameObject effect = BlockEffectOPManager.instance.SetObject(16);
                            effect.transform.position = tiles[j].transform.position;
                            tiles[j].GetComponent<Tile>().block.transform.GetChild(0).gameObject.SetActive(false);
                            tiles[j].GetComponent<Tile>().block.transform.GetChild(1).gameObject.SetActive(false);
                            tiles[j].GetComponent<Tile>().block.transform.GetChild(0).transform.SetParent(GameObject.FindGameObjectWithTag("MiddleCanvas").transform);
                            tiles[j].GetComponent<Tile>().block.transform.GetChild(0).transform.SetParent(GameObject.FindGameObjectWithTag("MiddleCanvas").transform);
                            if (tiles[j].GetComponent<Tile>().block.name == "RedBlock(Clone)")
                            {
                                tiles[j].GetComponent<Tile>().block.GetComponent<Block>().blockColor = 0;
                                tiles[j].GetComponent<Tile>().blockColor = 0;
                            }
                            else if (tiles[j].GetComponent<Tile>().block.name == "YellowBlock(Clone)")
                            {
                                tiles[j].GetComponent<Tile>().block.GetComponent<Block>().blockColor = 1;
                                tiles[j].GetComponent<Tile>().blockColor = 1;
                            }
                            else if (tiles[j].GetComponent<Tile>().block.name == "GreenBlock(Clone)")
                            {
                                tiles[j].GetComponent<Tile>().block.GetComponent<Block>().blockColor = 2;
                                tiles[j].GetComponent<Tile>().blockColor = 2;
                            }
                            else if (tiles[j].GetComponent<Tile>().block.name == "BlueBlock(Clone)")
                            {
                                tiles[j].GetComponent<Tile>().block.GetComponent<Block>().blockColor = 3;
                                tiles[j].GetComponent<Tile>().blockColor = 3;
                            }
                            else if (tiles[j].GetComponent<Tile>().block.name == "PurpleBlock(Clone)")
                            {
                                tiles[j].GetComponent<Tile>().block.GetComponent<Block>().blockColor = 4;
                                tiles[j].GetComponent<Tile>().blockColor = 4;
                            }
                        }
                        //Debug.Log(targetPos + new Vector2(0, 1));
                    }
                }
                if (tiles[j].GetComponent<Tile>().pos.y >= 0 && CountDownInPuzzle.isGameStart)
                {
                    if (tiles[j].GetComponent<Tile>().pos == (targetPos + new Vector2(0, -1)))
                    {
                        tiles[j].GetComponent<Tile>().block.GetComponent<Block>().hp--;
                        if (tiles[j].GetComponent<Tile>().block.GetComponent<Animator>() != null)
                        {
                            if (tiles[j].GetComponent<Tile>().block.GetComponent<Block>().hp >= 1)
                            {
                                tiles[j].GetComponent<Tile>().block.GetComponent<Animator>().SetTrigger("IsHit");
                                GameObject effect = BlockEffectOPManager.instance.SetObject(11);
                                effect.transform.position = tiles[j].GetComponent<Tile>().block.transform.position;
                                tiles[j].GetComponent<Tile>().block.GetComponent<Block>().IsHit = true;
                            }
                        }
                        if (tiles[j].GetComponent<Tile>().block.transform.childCount >= 2)
                        {
                            GameObject effect = BlockEffectOPManager.instance.SetObject(16);
                            effect.transform.position = tiles[j].transform.position;
                            tiles[j].GetComponent<Tile>().block.transform.GetChild(0).gameObject.SetActive(false);
                            tiles[j].GetComponent<Tile>().block.transform.GetChild(1).gameObject.SetActive(false);
                            tiles[j].GetComponent<Tile>().block.transform.GetChild(0).transform.SetParent(GameObject.FindGameObjectWithTag("MiddleCanvas").transform);
                            tiles[j].GetComponent<Tile>().block.transform.GetChild(0).transform.SetParent(GameObject.FindGameObjectWithTag("MiddleCanvas").transform);
                            if (tiles[j].GetComponent<Tile>().block.name == "RedBlock(Clone)")
                            {
                                tiles[j].GetComponent<Tile>().block.GetComponent<Block>().blockColor = 0;
                                tiles[j].GetComponent<Tile>().blockColor = 0;
                            }
                            else if (tiles[j].GetComponent<Tile>().block.name == "YellowBlock(Clone)")
                            {
                                tiles[j].GetComponent<Tile>().block.GetComponent<Block>().blockColor = 1;
                                tiles[j].GetComponent<Tile>().blockColor = 1;
                            }
                            else if (tiles[j].GetComponent<Tile>().block.name == "GreenBlock(Clone)")
                            {
                                tiles[j].GetComponent<Tile>().block.GetComponent<Block>().blockColor = 2;
                                tiles[j].GetComponent<Tile>().blockColor = 2;
                            }
                            else if (tiles[j].GetComponent<Tile>().block.name == "BlueBlock(Clone)")
                            {
                                tiles[j].GetComponent<Tile>().block.GetComponent<Block>().blockColor = 3;
                                tiles[j].GetComponent<Tile>().blockColor = 3;
                            }
                            else if (tiles[j].GetComponent<Tile>().block.name == "PurpleBlock(Clone)")
                            {
                                tiles[j].GetComponent<Tile>().block.GetComponent<Block>().blockColor = 4;
                                tiles[j].GetComponent<Tile>().blockColor = 4;
                            }
                        }
                        //Debug.Log(targetPos + new Vector2(0, -1));
                    }
                }
            }
            
        }
        yield return new WaitForSeconds(0.25f);
        if (isHaveDestroyBlock == 0)
        {
            
            yield return new WaitForSeconds(0.5f);
            if (!CountDownInPuzzle.isGameStart && CountDownInPuzzle.StartCount == 0)
            {
                StartGame = GameObject.FindGameObjectWithTag("MiddleCanvas").transform.GetChild(1).GetChild(4).GetComponent<Transform>();
                StartGame.gameObject.SetActive(true);
            }
            EagleGauge.instance.EagleItem();
            BeeHiveHpUp();
            StunEffectOff();
            MudEffectOff();
            WevvilMakeEffect();
            SquirrelAttack();
            for (int i = 0; i < squirrelSkills.Count; i++)
            {
                if (squirrelSkills[0].GetComponent<SquirrelEffect>().SwapCount >= 2)
                {
                    StartCoroutine(SquirrelEffectOff());
                }
            }
            EggtoWorm();
            BeeBlockAttack();
            WormtoWevvil();
            StartCoroutine(RabbitAttack());
            isSwapping = false;
        }
        destroyTempTile.Clear();
        
        StartCoroutine(KillBlock());
        

    }
    /// <summary>
    /// 데미지를 받아 hp가 0이 되는 블록 제거하는 함수
    /// </summary>
    /// <returns></returns>
    IEnumerator KillBlock()
    {
        bool noting = false;
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].GetComponent<Tile>().block.GetComponent<Block>().hp <= 0 && tiles[i].GetComponent<Tile>().block != null)
            {
                if (tiles[i].GetComponent<Tile>().block.GetComponent<Animator>() != null)
                {
                    tiles[i].GetComponent<Tile>().block.GetComponent<Animator>().SetTrigger("IsDead");
                    tiles[i].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                    noting = true;
                }
            }
        }
        if (CountDownInPuzzle.isGameStart&&noting)
        {
            StartCoroutine(EffectSoundMaker());
        }
        noting = false;
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].GetComponent<Tile>().block != null)
            {
                if (tiles[i].GetComponent<Tile>().block.GetComponent<Block>().hp <= 0)
                {
                    tiles[i].GetComponent<Tile>().block.gameObject.SetActive(false);
                    tiles[i].GetComponent<Tile>().isEmpty = true;
                    tiles[i].GetComponent<Tile>().blockColor = 255;
                }
            }
        }

       

        for (int i = 0; i < destroyTempTile.Count; i++)
        {
            destroyTempTile[i].block = null;
        }
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(PullDownBlock());

    }
    /// <summary>
    /// 클릭 이후 블록을 스왑하는 함수
    /// </summary>
    public void SwappingBlock()

    {
        if (!isSwapping&&CountDownInPuzzle.isGameStart)
        {
            isSwapping = true;
            if (targetTile.transform.position.x - MousePos.x > 0.5f && targetTile.transform.position.x - MousePos.x > targetTile.transform.position.y - MousePos.y)
            {
                nopeSwap = false;
                for (int y = 0; y < 9; y++)
                {
                    for (int x = 0; x < 7; x++)
                    {
                        if (arrayRow.tilesY[y].tilesScriptX[x].pos == targetTile.GetComponent<Tile>().pos)
                        {
                            if (arrayRow.tilesY[y].tilesScriptX[x + 1].block.GetComponent<Block>().blocktype != Block.BlockType.MONSTER && arrayRow.tilesY[y].tilesScriptX[x].blockColor <= 14 && arrayRow.tilesY[y].tilesScriptX[x + 1].blockColor <= 14)
                            {
                                //Debug.Log("Didn't1");
                                SoundManager.instance.PlaySFX(clip, 1, 1, 1);
                                tempBlock = arrayRow.tilesY[y].tilesScriptX[x].block;
                                tempColor = arrayRow.tilesY[y].tilesScriptX[x].blockColor;
                                arrayRow.tilesY[y].tilesScriptX[x].block.transform.DOMove(arrayRow.tilesY[y].tilesScriptX[x + 1].transform.position, 0.5f);
                                arrayRow.tilesY[y].tilesScriptX[x + 1].block.transform.DOMove(arrayRow.tilesY[y].tilesScriptX[x].transform.position, 0.5f);
                                arrayRow.tilesY[y].tilesScriptX[x].block = arrayRow.tilesY[y].tilesScriptX[x + 1].block;
                                arrayRow.tilesY[y].tilesScriptX[x + 1].block = tempBlock;
                                arrayRow.tilesY[y].tilesScriptX[x].blockColor = arrayRow.tilesY[y].tilesScriptX[x + 1].blockColor;
                                arrayRow.tilesY[y].tilesScriptX[x + 1].blockColor = tempColor;
                                StartCoroutine(ReSwappingBlock(arrayRow.tilesY[y].tilesScriptX[x], arrayRow.tilesY[y].tilesScriptX[x + 1]));
                            }
                            else
                            {
                                isSwapping = false;
                            }
                        }
                    }
                }
            }
            else if (targetTile.transform.position.x - MousePos.x < -0.5f && targetTile.transform.position.x - MousePos.x < targetTile.transform.position.y - MousePos.y)
            {
                nopeSwap = false;
                for (int y = 0; y < 9; y++)
                {
                    for (int x = 1; x < 8; x++)
                    {
                        if (arrayRow.tilesY[y].tilesScriptX[x].pos == targetTile.GetComponent<Tile>().pos)
                        {
                            if (arrayRow.tilesY[y].tilesScriptX[x - 1].block.GetComponent<Block>().blocktype != Block.BlockType.MONSTER && arrayRow.tilesY[y].tilesScriptX[x].blockColor <= 14 && arrayRow.tilesY[y].tilesScriptX[x - 1].blockColor <= 14)
                            {
                                //Debug.Log("Didn't2");
                                SoundManager.instance.PlaySFX(clip, 1, 1, 1);
                                tempBlock = arrayRow.tilesY[y].tilesScriptX[x].block;
                                tempColor = arrayRow.tilesY[y].tilesScriptX[x].blockColor;
                                arrayRow.tilesY[y].tilesScriptX[x].block.transform.DOMove(arrayRow.tilesY[y].tilesScriptX[x - 1].transform.position, 0.5f);
                                arrayRow.tilesY[y].tilesScriptX[x - 1].block.transform.DOMove(arrayRow.tilesY[y].tilesScriptX[x].transform.position, 0.5f);
                                arrayRow.tilesY[y].tilesScriptX[x].block = arrayRow.tilesY[y].tilesScriptX[x - 1].block;
                                arrayRow.tilesY[y].tilesScriptX[x - 1].block = tempBlock;
                                arrayRow.tilesY[y].tilesScriptX[x].blockColor = arrayRow.tilesY[y].tilesScriptX[x - 1].blockColor;
                                arrayRow.tilesY[y].tilesScriptX[x - 1].blockColor = tempColor;
                                StartCoroutine(ReSwappingBlock(arrayRow.tilesY[y].tilesScriptX[x], arrayRow.tilesY[y].tilesScriptX[x - 1]));
                            }
                            else
                            {
                                isSwapping = false;
                            }
                        }
                    }
                }
            }
            else if (targetTile.transform.position.y - MousePos.y > 0.5f && targetTile.transform.position.x - MousePos.x < targetTile.transform.position.y - MousePos.y)
            {
                nopeSwap = false;
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 1; y < 9; y++)
                    {
                        if (arrayColumn.tilesX[x].tilesScriptY[y].pos == targetTile.GetComponent<Tile>().pos)
                        {
                            if (arrayColumn.tilesX[x].tilesScriptY[y - 1].block.GetComponent<Block>().blocktype != Block.BlockType.MONSTER && arrayColumn.tilesX[x].tilesScriptY[y].blockColor <= 14 && arrayColumn.tilesX[x].tilesScriptY[y - 1].blockColor <= 14)
                            {
                                SoundManager.instance.PlaySFX(clip, 1, 1, 1);
                                tempBlock = arrayColumn.tilesX[x].tilesScriptY[y].block;
                                tempColor = arrayColumn.tilesX[x].tilesScriptY[y].blockColor;
                                arrayColumn.tilesX[x].tilesScriptY[y].block.transform.DOMove(arrayColumn.tilesX[x].tilesScriptY[y - 1].transform.position, 0.5f);
                                arrayColumn.tilesX[x].tilesScriptY[y - 1].block.transform.DOMove(arrayColumn.tilesX[x].tilesScriptY[y].transform.position, 0.5f);
                                arrayColumn.tilesX[x].tilesScriptY[y].block = arrayColumn.tilesX[x].tilesScriptY[y - 1].block;
                                arrayColumn.tilesX[x].tilesScriptY[y].blockColor = arrayColumn.tilesX[x].tilesScriptY[y - 1].blockColor;
                                arrayColumn.tilesX[x].tilesScriptY[y - 1].block = tempBlock;
                                arrayColumn.tilesX[x].tilesScriptY[y - 1].blockColor = tempColor;
                                StartCoroutine(ReSwappingBlock(arrayColumn.tilesX[x].tilesScriptY[y], arrayColumn.tilesX[x].tilesScriptY[y - 1]));
                            }
                            else
                            {
                                isSwapping = false;
                            }
                        }
                    }
                }
            }
            else if (targetTile.transform.position.y - MousePos.y < -0.5f && targetTile.transform.position.x - MousePos.x > targetTile.transform.position.y - MousePos.y)
            {
                nopeSwap = false;
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        if (arrayColumn.tilesX[x].tilesScriptY[y].pos == targetTile.GetComponent<Tile>().pos)
                        {
                            if (arrayColumn.tilesX[x].tilesScriptY[y + 1].block.GetComponent<Block>().blocktype != Block.BlockType.MONSTER && arrayColumn.tilesX[x].tilesScriptY[y].blockColor <= 14 && arrayColumn.tilesX[x].tilesScriptY[y + 1].blockColor <= 14)
                            {
                                //Debug.Log("Didn't4");
                                SoundManager.instance.PlaySFX(clip, 1, 1, 1);
                                tempBlock = arrayColumn.tilesX[x].tilesScriptY[y].block;
                                tempColor = arrayColumn.tilesX[x].tilesScriptY[y].blockColor;
                                arrayColumn.tilesX[x].tilesScriptY[y].block.transform.DOMove(arrayColumn.tilesX[x].tilesScriptY[y + 1].transform.position, 0.5f);
                                arrayColumn.tilesX[x].tilesScriptY[y + 1].block.transform.DOMove(arrayColumn.tilesX[x].tilesScriptY[y].transform.position, 0.5f);
                                arrayColumn.tilesX[x].tilesScriptY[y].block = arrayColumn.tilesX[x].tilesScriptY[y + 1].block;
                                arrayColumn.tilesX[x].tilesScriptY[y].blockColor = arrayColumn.tilesX[x].tilesScriptY[y + 1].blockColor;
                                arrayColumn.tilesX[x].tilesScriptY[y + 1].block = tempBlock;
                                arrayColumn.tilesX[x].tilesScriptY[y + 1].blockColor = tempColor;
                                StartCoroutine(ReSwappingBlock(arrayColumn.tilesX[x].tilesScriptY[y], arrayColumn.tilesX[x].tilesScriptY[y + 1]));

                            }
                            else
                            {
                                isSwapping = false;
                            }
                        }
                    }
                }
            }
            else
            {
                nopeSwap = true;
                isSwapping = false;
                if (targetTile.GetComponent<Tile>().blockColor == 12)
                {
                    StartCoroutine(UseStickItem(targetTile.GetComponent<Tile>()));
                }
                else if (targetTile.GetComponent<Tile>().blockColor == 14)
                {
                    StartCoroutine(UseEagleItem(targetTile.GetComponent<Tile>()));
                }
                return;
            }
            
        }
    }
    /// <summary>
    /// SwapCount가 올라갈때 마다 BeeHive체력이 1 올라감
    /// </summary>
    public void BeeHiveHpUp()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (hpUp)
            {
                if (tiles[i].GetComponent<Tile>().block.GetComponent<Block>().SwapCount % 3 == 0 && tiles[i].GetComponent<Tile>().block.GetComponent<Block>().SwapCount != 0)
                {
                    if (tiles[i].GetComponent<Tile>().blockColor == 6)
                    {
                        if (tiles[i].GetComponent<Tile>().block.GetComponent<Block>().hp <= 4)
                        {
                            tiles[i].GetComponent<Tile>().block.GetComponent<Block>().hp++;
                            hpUp = false;
                        }
                    }
                }
            }
        }
    }
    /// <summary>
    /// 벌집 피격시 벌 한마리 소환
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public IEnumerator BeeHiveCreateBee(int i)
    {
        if (tiles[i].GetComponent<Tile>().blockColor == 6&&beeCount < 3)
        {
            int randomNum = randomBlockNumMake();
            GameObject bee = OPBlock.instance.SetObject(5);
            bee.transform.position = tiles[i].transform.position;
            bee.transform.DOMove(tiles[randomNum].transform.position, 0.5f);
            yield return new WaitForSeconds(0.4f);
            bee.GetComponent<Animator>().SetTrigger("IsAttack");
            tiles[randomNum].GetComponent<Tile>().block.SetActive(false);
            tiles[randomNum].GetComponent<Tile>().block = bee;
            tiles[randomNum].GetComponent<Tile>().blockColor = 5;
        }
    }
    /// <summary>
    /// 벌 소환시 벌의 위치를 램덤한 위치로 지정
    /// </summary>
    /// <returns></returns>
    public int randomBlockNumMake()
    {
        int returnNum = 0;
        int randomBlockChangeBeeNum = Random.Range(0, tiles.Length);
        if (tiles[randomBlockChangeBeeNum].GetComponent<Tile>().block.GetComponent<Block>().blocktype == Block.BlockType.CHESTNUTBLOCK && tiles[randomBlockChangeBeeNum].GetComponent<Tile>().block != null)
        {
            returnNum = randomBlockChangeBeeNum;
        }
        else
        {
            randomBlockNumMake();
        }
        return returnNum;
    }
    /// <summary>
    /// 알블록이 애벌레로 변하는 함수
    /// </summary>
    public void EggtoWorm()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].GetComponent<Tile>().block.GetComponent<Block>().SwapCount >= 1&& tiles[i].GetComponent<Tile>().blockColor ==7)
            {
                tiles[i].GetComponent<Tile>().block.gameObject.SetActive(false);
                GameObject worm = OPBlock.instance.SetObject(11);
                worm.transform.position = tiles[i].transform.position;
                tiles[i].GetComponent<Tile>().block = worm;
                tiles[i].GetComponent<Tile>().blockColor = 11;
            }
        }
    }
    /// <summary>
    /// 애벌레블록이 바구미로 변하는 함수
    /// </summary>
    public void WormtoWevvil()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].GetComponent<Tile>().block.GetComponent<Block>().SwapCount >= 1&& tiles[i].GetComponent<Tile>().blockColor == 11)
            {
                tiles[i].GetComponent<Tile>().block.gameObject.SetActive(false);
                GameObject wevvil = OPBlock.instance.SetObject(10);
                wevvil.transform.position = tiles[i].transform.position;
                GameObject evolutionEffect = BlockEffectOPManager.instance.SetObject(8);
                evolutionEffect.transform.position = tiles[i].transform.position;
                SoundManager.instance.PlaySFX(clip, 5, 1, 1);
                tiles[i].GetComponent<Tile>().block = wevvil;
                tiles[i].GetComponent<Tile>().blockColor = 10;
            }
        }
    }
    /// <summary>
    /// 바구미가 3턴이상 생존 했을때 스킬을 시전하는 함수
    /// </summary>
    public void WevvilMakeEffect()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].GetComponent<Tile>().block!= null&& tiles[i].GetComponent<Tile>().block.GetComponent<Block>().SwapCount>=3 && tiles[i].GetComponent<Tile>().blockColor == 10&& tiles[i].GetComponent<Tile>().block.GetComponent<Block>().SwapCount!=0)
            {
                GameObject mudEffect = BlockEffectOPManager.instance.SetObject(5);
                mudEffect.transform.position = tiles[i].transform.position;
                tiles[i].GetComponent<Tile>().block.GetComponent<Block>().SwapCount = 0;
                SoundManager.instance.PlaySFX(clip, 4, 1, 1);
                mudEffects.Add(mudEffect);
            }
        }
    }
    /// <summary>
    /// 다람쥐가 4턴이상 생존 했을때 사용하는 스킬 함수
    /// </summary>
    public void SquirrelAttack()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].GetComponent<Tile>().block != null && tiles[i].GetComponent<Tile>().block.GetComponent<Block>().SwapCount >= 4 && tiles[i].GetComponent<Tile>().blockColor == 9 && tiles[i].GetComponent<Tile>().block.GetComponent<Block>().SwapCount != 0)
            {
                GameObject squirrelSkill = BlockEffectOPManager.instance.SetObject(10);
                skillPoint = new Vector2(0, tiles[i].GetComponent<Tile>().transform.position.y);
                squirrelSkill.transform.position = skillPoint;
                tiles[i].GetComponent<Tile>().block.GetComponent<Block>().SwapCount = 0;
                squirrelSkills.Add(squirrelSkill);
                StartCoroutine(SquirrelMove(tiles[i].GetComponent<Tile>().block.gameObject, tiles[i].GetComponent<Tile>().pos,tiles[i].transform));
            }
        }
    }
    /// <summary>
    /// 스킬 시전 하는 함수 squirrelBlock = 다람쥐 블록, pos =  다람쥐 블록의 포스값xy,returnPos = 돌아갈 위치
    /// </summary>
    /// <param name="squirrelBlock"></param>
    /// <param name="pos"></param>
    /// <param name="returnPos"></param>
    /// <returns></returns>
    IEnumerator SquirrelMove(GameObject squirrelBlock, Vector2 pos,Transform returnPos)
    {
        Vector3 zeroBlockStartPos;
        Vector3 zeroBlockEndPos;
        for (int i = 0; i < tiles.Length; i++)
        {
            if(tiles[i].GetComponent<Tile>().pos == new Vector2(0, pos.y))
            {
                GameObject effect = BlockEffectOPManager.instance.SetObject(9);
                effect.transform.position = tiles[i].transform.position;
                zeroBlockStartPos = tiles[i].transform.position;
                SoundManager.instance.PlaySFX(squirrelBlock.GetComponent<Block>().clip, 1, 1, 1);
                squirrelBlock.transform.position = zeroBlockStartPos;
                squirrelBlock.GetComponent<Animator>().SetBool("IsRolling",true);
                squirrelBlock.transform.localScale = new Vector3(2, 2, 2);
            }
            
        }
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].GetComponent<Tile>().pos == new Vector2(7, pos.y))
            {
                zeroBlockEndPos = tiles[i].transform.position;
                squirrelBlock.transform.DOMove(zeroBlockEndPos, 0.5f);
            }
        }
        yield return new WaitForSeconds(0.5f);
        squirrelBlock.transform.localScale = Vector3.one;
        squirrelBlock.GetComponent<Animator>().SetBool("IsRolling", false);
        squirrelBlock.transform.position = returnPos.position;
    }
    /// <summary>
    /// 1턴이상 된 진흙 모션이 사라지는 함수 
    /// </summary>
    public void MudEffectOff()
    {
        for (int i = 0; i < mudEffects.Count; i++)
        {
            if (mudEffects[i].GetComponent<MudEffect>().SwapCount >= 1)
            {
                mudEffects[i].gameObject.SetActive(false);
                mudEffects[i].GetComponent<MudEffect>().SwapCount = 0;
                mudEffects.Remove(mudEffects[i].gameObject);
            }
        }

    }
    /// <summary>
    /// 벌이 공격시 스턴 이펙트 출력 및 블록 사용 불가능 하게 변경
    /// </summary>
    public void StunEffectOff()
    {
        for (int i = 0;i < tiles.Length; i++)
        {
            if(tiles[i].GetComponent<Tile>().block.GetComponent<Block>().blockColor ==255 && tiles[i].GetComponent<Tile>().block.GetComponent<Block>().transform.GetChild(0) != null&& tiles[i].GetComponent<Tile>().block.GetComponent<Block>().transform.GetChild(0).GetComponent<StunEffect>().SwapCount>=2)
            {
                if(tiles[i].GetComponent<Tile>().block.name == "RedBlock(Clone)")
                {
                    tiles[i].GetComponent<Tile>().block.GetComponent<Block>().blockColor = 0;
                    tiles[i].GetComponent<Tile>().blockColor = 0;
                }
                else if (tiles[i].GetComponent<Tile>().block.name == "YellowBlock(Clone)")
                {
                    tiles[i].GetComponent<Tile>().block.GetComponent<Block>().blockColor = 1;
                    tiles[i].GetComponent<Tile>().blockColor = 1;
                }
                else if (tiles[i].GetComponent<Tile>().block.name == "GreenBlock(Clone)")
                {
                    tiles[i].GetComponent<Tile>().block.GetComponent<Block>().blockColor = 2;
                    tiles[i].GetComponent<Tile>().blockColor = 2;
                }
                else if (tiles[i].GetComponent<Tile>().block.name == "BlueBlock(Clone)")
                {
                    tiles[i].GetComponent<Tile>().block.GetComponent<Block>().blockColor = 3;
                    tiles[i].GetComponent<Tile>().blockColor = 3;
                }
                else if (tiles[i].GetComponent<Tile>().block.name == "PurpleBlock(Clone)")
                {
                    tiles[i].GetComponent<Tile>().block.GetComponent<Block>().blockColor = 4;
                    tiles[i].GetComponent<Tile>().blockColor = 4;
                }
            }
        }
        for (int i = 0; i < stunEffect.Count; i++)
        {
            if (stunEffect[i].GetComponent<StunEffect>().SwapCount >=2)
            {
                stunEffect[i].gameObject.SetActive(false);
                stunEffect[i].GetComponent<StunEffect>().SwapCount = 0;
                stunEffect.Remove(stunEffect[i].gameObject);
            }
        }
        
    }
    /// <summary>
    /// 다람쥐가 사용한 스킬 2턴이후에 사라지게 하는 함수
    /// </summary>
    /// <returns></returns>
    /// 
    public IEnumerator SquirrelEffectOff()
    {
        for (int i = 0; i < squirrelSkills.Count; i++)
        {
            if (squirrelSkills[i].GetComponent<SquirrelEffect>().SwapCount >= 2)
            {
                squirrelSkills[i].GetComponent<Animator>().SetTrigger("FadeOut");

            }
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < squirrelSkills.Count; i++)
        {
            if (squirrelSkills[i].GetComponent<SquirrelEffect>().SwapCount >= 2)
            {
                squirrelSkills[i].gameObject.SetActive(false);
                squirrelSkills[i].GetComponent<SquirrelEffect>().SwapCount = 0;
                squirrelSkills.Clear();
            }
        }
    }
    /// <summary>
    /// 블록이 스왑 되었는데 부서지는 블록이 없을 경우 제자리로 돌아가고 안돌아갔을 경우 SwapCount가 올라가는 함수
    /// </summary>
    /// <param name="target"></param>
    /// <param name="SwappingTarget"></param>
    /// <returns></returns>
    IEnumerator ReSwappingBlock(Tile target, Tile SwappingTarget)
    {
        if(target.blockColor ==14||SwappingTarget.blockColor == 14)
        {
            comboCount++;
            StartCoroutine(UseEagleItem(target, SwappingTarget));
        }
        else if (target.blockColor == 13 || SwappingTarget.blockColor == 13)
        {
            comboCount++;
            StartCoroutine(UseSpray(target, SwappingTarget));
        }
        else if (target.blockColor == 12 || SwappingTarget.blockColor == 12)
        {
            StartCoroutine(UseStickItem(target, SwappingTarget));
        }
        else
        {
            StartCoroutine(DestroyBlock());
        }
        yield return new WaitForSeconds(0.4f);
        if (target.blockColor < 5 && SwappingTarget.blockColor < 5)
        {
            if (isHaveDestroyBlock <= 0&& !useItem)
            {
                target.block.transform.DOMove(SwappingTarget.block.transform.position, 0.1f);
                SwappingTarget.block.transform.DOMove(target.block.transform.position, 0.1f);
                tempBlock = target.block;
                target.block = SwappingTarget.block;
                SwappingTarget.block = tempBlock;
                tempColor = target.blockColor;
                target.blockColor = SwappingTarget.blockColor;
                SwappingTarget.blockColor = tempColor;
                SoundManager.instance.PlaySFX(clip, 0, 1, 1);
            }
            else
            {
                
            }
        }
    }
    /// <summary>
    /// 4턴이 지나면 벌이 공격하게 하는 함수
    /// </summary>
    void BeeBlockAttack()
    {
        bool createStun = true;
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].GetComponent<Tile>().block.GetComponent<Block>().SwapCount>= 4 && tiles[i].GetComponent<Tile>().blockColor == 5&& tiles[i].GetComponent<Tile>().block.GetComponent<Block>().SwapCount != 0)
            {
                isSwapping = true;
                StartCoroutine(StunBlock(tiles[i].transform,createStun,i));
                tiles[i].GetComponent<Tile>().block.GetComponent<Block>().SwapCount = 0;
            }
        }
    }
    /// <summary>
    /// 벌공격시 블록에 기절 이펙트와 벌침이 날아가는 이펙트 생성 함수
    /// </summary>
    /// <param name="beePos"></param>
    /// <param name="createStun"></param>
    /// <param name="tileNum"></param>
    /// <returns></returns>
    IEnumerator StunBlock(Transform beePos, bool createStun,int tileNum)
    {
        isSwapping = true;
        yield return new WaitForSeconds(0.35f);
        SoundManager.instance.PlaySFX(clip, 6, 1, 1);
        int randomNum = 0;
        for (int i = 0; i < 5; i++)
        {
            StunListSet();
            randomNum = randomBlockNumMake();
            tiles[tileNum].GetComponent<Tile>().block.GetComponent<Animator>().SetTrigger("IsAttack");
            if (tiles[randomNum].GetComponent<Tile>().blockColor < 5)
            {
                GameObject sting = BlockEffectOPManager.instance.SetObject(13);
                Vector3 dir = tiles[randomNum].transform.position - sting.transform.position;
                sting.transform.position = beePos.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                sting.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                sting.transform.DOMove(tiles[randomNum].transform.position, 0.5f);
                sting.gameObject.SetActive(false);
                GameObject effect = BlockEffectOPManager.instance.SetObject(12);
                effect.gameObject.SetActive(true);
                effect.transform.SetParent(tiles[randomNum].GetComponent<Tile>().block.transform);

                effect.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
                effect.transform.position = tiles[randomNum].transform.position;
                effect.GetComponent<StunEffect>().block = tiles[randomNum].GetComponent<Tile>().block.GetComponent<Block>();
                stunEffect.Add(effect);
                tiles[randomNum].GetComponent<Tile>().blockColor = 255;
                tiles[randomNum].GetComponent<Tile>().block.GetComponent<Block>().blockColor = 255;
                tiles[tileNum].GetComponent<Tile>().block.GetComponent<Block>().SwapCount = 0;
                createStun = false;
                isSwapping = false;
            }
        }
    }
    /// <summary>
    /// 기절한 블록의 리스트를 초기화
    /// </summary>
    void StunListSet()
    {
        for (int i = 0; i < stunEffect.Count; i++)
        {
            if (stunEffect[i].GetComponent<StunEffect>().SwapCount >= 2&& isHaveDestroyBlock <= 0)
            {
                stunEffect.RemoveAt(i);
            }
        }
    }
    /// <summary>
    /// 4턴이 지나면 토끼가 램덤한 블록을 방해하게 하는 함수
    /// </summary>
    /// <returns></returns>
    IEnumerator RabbitAttack()
    {
        int randomNum = 0;

        for (int i = 0; i < tiles.Length; i++)
        {
            if(tiles[i].GetComponent<Tile>().blockColor == 8 )
            {
                if(tiles[i].GetComponent<Tile>().block.GetComponent<Block>().SwapCount >=4)
                {
                    randomNum = RandomRabbitAttack();
                    tiles[randomNum].GetComponent<Tile>().blockColor = 250;
                    tiles[randomNum].GetComponent<Tile>().block.GetComponent<Block>().blockColor = 250;
                    GameObject woodEffect = BlockEffectOPManager.instance.SetObject(16);
                    woodEffect.transform.position = tiles[randomNum].transform.position;
                    GameObject knifeEffect = BlockEffectOPManager.instance.SetObject(14);
                    knifeEffect.transform.position = tiles[i].transform.position;
                    knifeEffect.transform.DOMove(tiles[randomNum].transform.position, 0.5f);
                    SoundManager.instance.PlaySFX(clip, 4, 1, 1);
                    tiles[i].GetComponent<Tile>().block.GetComponent<Block>().SwapCount = 0;
                    yield return new WaitForSeconds(0.5f);
                    GameObject wood = BlockEffectOPManager.instance.SetObject(15);
                    wood.transform.position = tiles[randomNum].transform.position;
                    wood.transform.SetParent(tiles[randomNum].GetComponent<Tile>().block.transform);
                    GameObject shurikenWithWood = BlockEffectOPManager.instance.SetObject(17);
                    shurikenWithWood.transform.position = wood.transform.position;
                    Vector3 dir = tiles[i].transform.position - shurikenWithWood.transform.position;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    Debug.Log(Quaternion.AngleAxis(angle, Vector3.forward));
                    shurikenWithWood.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    shurikenWithWood.transform.SetParent(tiles[randomNum].GetComponent<Tile>().block.transform);
                    knifeEffect.gameObject.SetActive(false);

                }
            }
        }
    }
    /// <summary>
    /// 랜덤한 블록을 지정해주는 함수
    /// </summary>
    /// <returns></returns>
    int RandomRabbitAttack()
    {
        int returnNum = 0;
        int randomNum = Random.Range(0,tiles.Length);
        if(tiles[randomNum].GetComponent<Tile>().block.transform.childCount < 1&&tiles[randomNum].GetComponent<Tile>().block.GetComponent<Block>().blocktype == Block.BlockType.CHESTNUTBLOCK)
        {
            returnNum = randomNum;
        }
        else if(tiles[randomNum].GetComponent<Tile>().block.transform.childCount >= 1)
        {
            RandomRabbitAttack();
        }
        return returnNum;
    }
    /// <summary>
    /// 이펙트 사운드를 발생하는 함수
    /// </summary>
    /// <returns></returns>
    IEnumerator EffectSoundMaker()
    {
        yield return new WaitForSeconds(1f);
        SoundManager.instance.PlaySFX(clip, 2, -30f, 1);
    }
    /// <summary>
    /// 잠자리채 아이템을 생성하는 함수
    /// </summary>
    /// <returns></returns>
    IEnumerator CreateStick()
    {
        yield return new WaitForSeconds(0.1f);
        for (int x = 0; x < 9; x++)
        {
            for (int i = 0; i < 4; i++)
            {
                if (arrayRow.tilesY[x].tilesScriptX[i].blockColor == arrayRow.tilesY[x].tilesScriptX[i + 1].blockColor && arrayRow.tilesY[x].tilesScriptX[i].blockColor == arrayRow.tilesY[x].tilesScriptX[i + 2].blockColor
                    && arrayRow.tilesY[x].tilesScriptX[i].blockColor == arrayRow.tilesY[x].tilesScriptX[i + 3].blockColor && arrayRow.tilesY[x].tilesScriptX[i].blockColor == arrayRow.tilesY[x].tilesScriptX[i + 4].blockColor)
                {
                    if (arrayRow.tilesY[x].tilesScriptX[i].blockColor < 5)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            if (j == 2)
                            {
                                isHaveDestroyBlock++;
                                GameObject stickItemBlock = OPBlock.instance.SetObject(12);
                                stickItemBlock.transform.position = arrayRow.tilesY[x].tilesScriptX[i+2].transform.position;
                                arrayRow.tilesY[x].tilesScriptX[i + j].block.gameObject.SetActive(false);
                                arrayRow.tilesY[x].tilesScriptX[i + j].block = stickItemBlock;
                                arrayRow.tilesY[x].tilesScriptX[i + j].blockColor = 12;
                                arrayRow.tilesY[x].tilesScriptX[i + j].isEmpty = false;
                                destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[i + j]);
                            }
                            else
                            {
                                arrayRow.tilesY[x].tilesScriptX[i + j].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[i + 2].transform.position, 0.3f);
                                arrayRow.tilesY[x].tilesScriptX[i + j].isEmpty = true;
                                destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[i + j].block.gameObject);
                                destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[i + j]);
                            }
                        }
                    }
                }
            }
        }
        for (int x = 0; x < 8; x++)
        {
            for (int i = 0; i < 5; i++)
            {
                if (arrayColumn.tilesX[x].tilesScriptY[i].blockColor == arrayColumn.tilesX[x].tilesScriptY[i+1].blockColor&& arrayColumn.tilesX[x].tilesScriptY[i].blockColor == arrayColumn.tilesX[x].tilesScriptY[i +2].blockColor
                    && arrayColumn.tilesX[x].tilesScriptY[i].blockColor == arrayColumn.tilesX[x].tilesScriptY[i + 3].blockColor&& arrayColumn.tilesX[x].tilesScriptY[i].blockColor == arrayColumn.tilesX[x].tilesScriptY[i + 4].blockColor)
                {
                    isHaveDestroyBlock++;
                    if (arrayColumn.tilesX[x].tilesScriptY[i].blockColor < 5)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            if (j == 2)
                            {
                                isHaveDestroyBlock++;
                                GameObject stickItemBlock = OPBlock.instance.SetObject(12);
                                stickItemBlock.transform.position = arrayColumn.tilesX[x].tilesScriptY[i+2].transform.position;
                                arrayColumn.tilesX[x].tilesScriptY[i + j].block.gameObject.SetActive(false);
                                arrayColumn.tilesX[x].tilesScriptY[i + j].block = stickItemBlock;
                                arrayColumn.tilesX[x].tilesScriptY[i + j].blockColor = 12;
                                arrayColumn.tilesX[x].tilesScriptY[i + j].isEmpty = false;
                                destroyTile.Add(arrayColumn.tilesX[x].tilesScriptY[i + j]);
                            }
                            else
                            {
                                isHaveDestroyBlock++;
                                arrayColumn.tilesX[x].tilesScriptY[i + j].block.transform.DOMove(arrayColumn.tilesX[x].tilesScriptY[i + 2].transform.position, 0.3f);
                                arrayColumn.tilesX[x].tilesScriptY[i + j].isEmpty = true;
                                destroyBlock.Add(arrayColumn.tilesX[x].tilesScriptY[i + j].block.gameObject);
                                destroyTile.Add(arrayColumn.tilesX[x].tilesScriptY[i + j]);
                            }
                        }
                    }
                }
            }
        }
        yield return new WaitForSeconds(1f);
    }
    /// <summary>
    /// 스프레이 아이템을 생성하는 함수
    /// </summary>
    /// <returns></returns>
    IEnumerator CreateSpray()
    {
        yield return new WaitForSeconds(0.1f);
        for (int x = 0; x < 9; x++)
        {
            for (int y = 0; y < 6; y++)
            {
                if (arrayRow.tilesY[x].tilesScriptX[y].blockColor == arrayRow.tilesY[x].tilesScriptX[y + 1].blockColor && arrayRow.tilesY[x].tilesScriptX[y].blockColor == arrayRow.tilesY[x].tilesScriptX[y + 2].blockColor)
                {
                    if (x == 0)
                    {
                        if (arrayRow.tilesY[x].tilesScriptX[y].blockColor == arrayRow.tilesY[x + 1].tilesScriptX[y].blockColor && arrayRow.tilesY[x].tilesScriptX[y].blockColor == arrayRow.tilesY[x + 2].tilesScriptX[y].blockColor)
                        {
                            for (int i = 0; i < 3; i++)//세로
                            {
                                if (i == 0)//겹치는 블록
                                {
                                    isHaveDestroyBlock++;
                                    GameObject sprayItem = OPBlock.instance.SetObject(13);
                                    sprayItem.transform.position = arrayRow.tilesY[x + i].tilesScriptX[y].transform.position;
                                    arrayRow.tilesY[x + i].tilesScriptX[y].block.SetActive(false);
                                    arrayRow.tilesY[x + i].tilesScriptX[y].block = sprayItem;
                                    arrayRow.tilesY[x + i].tilesScriptX[y].blockColor = 13;
                                    arrayRow.tilesY[x + i].tilesScriptX[y].isEmpty = false;
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y]);
                                }
                                else//나머지 블록 제거 및 이펙트 효과
                                {
                                    arrayRow.tilesY[x + i].tilesScriptX[y].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y].transform.position, 0.4f);
                                    arrayRow.tilesY[x + i].tilesScriptX[y].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x + i].tilesScriptX[y].block);
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y]);
                                }
                            }
                            for (int i = 1; i < 3; i++)//가로 
                            {
                                arrayRow.tilesY[x].tilesScriptX[y + i].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y].transform.position, 0.4f);
                                arrayRow.tilesY[x].tilesScriptX[y + i].isEmpty = true;
                                destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + i].block.gameObject);
                                destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + i]);
                            }
                        }

                        else if (arrayRow.tilesY[x].tilesScriptX[y + 1].blockColor == arrayRow.tilesY[x + 1].tilesScriptX[y + 1].blockColor && arrayRow.tilesY[x].tilesScriptX[y + 1].blockColor == arrayRow.tilesY[x + 2].tilesScriptX[y + 1].blockColor)
                        {
                            for (int i = 0; i < 3; i++)//세로
                            {
                                if (i == 0)//겹치는 블록
                                {
                                    isHaveDestroyBlock++;
                                    GameObject sprayItem = OPBlock.instance.SetObject(13);
                                    sprayItem.transform.position = arrayRow.tilesY[x + i].tilesScriptX[y + 1].transform.position;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].block.SetActive(false);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].block = sprayItem;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].blockColor = 13;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].isEmpty = false;
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 1]);
                                }
                                else//나머지 블록 제거 및 이펙트 효과
                                {
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 1].transform.position, 0.4f);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 1].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 1]);
                                }
                            }
                            for (int i = 1; i < 3; i++)//가로 
                            {
                                if (i == 1)
                                {
                                    arrayRow.tilesY[x].tilesScriptX[y + 0].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 1].transform.position, 0.4f);
                                    arrayRow.tilesY[x].tilesScriptX[y + 0].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + 0].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + 0]);
                                }
                                else
                                {
                                    arrayRow.tilesY[x].tilesScriptX[y + i].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 1].transform.position, 0.4f);
                                    arrayRow.tilesY[x].tilesScriptX[y + i].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + i].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + i]);
                                }
                            }
                        }

                        else if (arrayRow.tilesY[x].tilesScriptX[y + 2].blockColor == arrayRow.tilesY[x + 1].tilesScriptX[y + 2].blockColor && arrayRow.tilesY[x].tilesScriptX[y + 2].blockColor == arrayRow.tilesY[x + 2].tilesScriptX[y + 2].blockColor)
                        {
                            for (int i = 0; i < 3; i++)//세로
                            {
                                if (i == 0)//겹치는 블록
                                {
                                    isHaveDestroyBlock++;
                                    GameObject sprayItem = OPBlock.instance.SetObject(13);
                                    sprayItem.transform.position = arrayRow.tilesY[x + i].tilesScriptX[y + 2].transform.position;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].block.SetActive(false);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].block = sprayItem;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].blockColor = 13;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].isEmpty = false;
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 2]);
                                }
                                else//나머지 블록 제거 및 이펙트 효과
                                {
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 2].transform.position, 0.4f);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 2].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 2]);
                                }
                            }
                            for (int i = 0; i < 2; i++)//가로 
                            {
                                arrayRow.tilesY[x].tilesScriptX[y + i].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 2].transform.position, 0.4f);
                                arrayRow.tilesY[x].tilesScriptX[y + i].isEmpty = true;
                                destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + i].block.gameObject);
                                destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + i]);
                            }
                        }
                    }
                    else if (x == 1)
                    {
                        if (arrayRow.tilesY[x].tilesScriptX[y].blockColor == arrayRow.tilesY[x + 1].tilesScriptX[y].blockColor && arrayRow.tilesY[x].tilesScriptX[y].blockColor == arrayRow.tilesY[x + 2].tilesScriptX[y].blockColor)
                        {
                            for (int i = 0; i < 3; i++)//세로
                            {
                                if (i == 0)//겹치는 블록
                                {
                                    isHaveDestroyBlock++;
                                    GameObject sprayItem = OPBlock.instance.SetObject(13);
                                    sprayItem.transform.position = arrayRow.tilesY[x + i].tilesScriptX[y].transform.position;
                                    arrayRow.tilesY[x + i].tilesScriptX[y].block.SetActive(false);
                                    arrayRow.tilesY[x + i].tilesScriptX[y].block = sprayItem;
                                    arrayRow.tilesY[x + i].tilesScriptX[y].blockColor = 13;
                                    arrayRow.tilesY[x + i].tilesScriptX[y].isEmpty = false;
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y]);
                                }
                                else//나머지 블록 제거 및 이펙트 효과
                                {
                                    arrayRow.tilesY[x + i].tilesScriptX[y].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y].transform.position, 0.4f);
                                    arrayRow.tilesY[x + i].tilesScriptX[y].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x + i].tilesScriptX[y].block);
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y]);
                                }
                            }
                            for (int i = 1; i < 3; i++)//가로 
                            {
                                arrayRow.tilesY[x].tilesScriptX[y + i].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y].transform.position, 0.4f);
                                arrayRow.tilesY[x].tilesScriptX[y + i].isEmpty = true;
                                destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + i].block.gameObject);
                                destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + i]);
                            }
                        }
                        else if (arrayRow.tilesY[x].tilesScriptX[y].blockColor == arrayRow.tilesY[x + 1].tilesScriptX[y].blockColor && arrayRow.tilesY[x].tilesScriptX[y].blockColor == arrayRow.tilesY[x - 1].tilesScriptX[y].blockColor)
                        {
                            for (int i = -1; i < 2; i++)//세로
                            {
                                if (i == 0)//겹치는 블록
                                {
                                    isHaveDestroyBlock++;
                                    GameObject sprayItem = OPBlock.instance.SetObject(13);
                                    sprayItem.transform.position = arrayRow.tilesY[x + i].tilesScriptX[y].transform.position;
                                    arrayRow.tilesY[x + i].tilesScriptX[y].block.SetActive(false);
                                    arrayRow.tilesY[x + i].tilesScriptX[y].block = sprayItem;
                                    arrayRow.tilesY[x + i].tilesScriptX[y].blockColor = 13;
                                    arrayRow.tilesY[x + i].tilesScriptX[y].isEmpty = false;
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y]);
                                }
                                else//나머지 블록 제거 및 이펙트 효과
                                {
                                    arrayRow.tilesY[x + i].tilesScriptX[y].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y].transform.position, 0.4f);
                                    arrayRow.tilesY[x + i].tilesScriptX[y].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x + i].tilesScriptX[y].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y]);
                                }
                            }
                            for (int i = 1; i < 3; i++)//가로 
                            {
                                arrayRow.tilesY[x].tilesScriptX[y + i].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y].transform.position, 0.4f);
                                arrayRow.tilesY[x].tilesScriptX[y + i].isEmpty = true;
                                destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + i].block.gameObject);
                                destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + i]);
                            }
                        }

                        else if (arrayRow.tilesY[x].tilesScriptX[y + 1].blockColor == arrayRow.tilesY[x + 1].tilesScriptX[y + 1].blockColor && arrayRow.tilesY[x].tilesScriptX[y + 1].blockColor == arrayRow.tilesY[x + 2].tilesScriptX[y + 1].blockColor)
                        {
                            for (int i = 0; i < 3; i++)//세로
                            {
                                if (i == 0)//겹치는 블록
                                {
                                    isHaveDestroyBlock++;
                                    GameObject sprayItem = OPBlock.instance.SetObject(13);
                                    sprayItem.transform.position = arrayRow.tilesY[x + i].tilesScriptX[y + 1].transform.position;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].block.SetActive(false);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].block = sprayItem;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].blockColor = 13;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].isEmpty = false;
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 1]);
                                }
                                else//나머지 블록 제거 및 이펙트 효과
                                {
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 1].transform.position, 0.4f);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 1].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 1]);
                                }
                            }
                            for (int i = 1; i < 3; i++)//가로 
                            {
                                if (i == 1)
                                {
                                    arrayRow.tilesY[x].tilesScriptX[y + 0].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 1].transform.position, 0.4f);
                                    arrayRow.tilesY[x].tilesScriptX[y + 0].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + 0].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + 0]);
                                }
                                else
                                {
                                    arrayRow.tilesY[x].tilesScriptX[y + i].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 1].transform.position, 0.4f);
                                    arrayRow.tilesY[x].tilesScriptX[y + i].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + i].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + i]);
                                }
                            }
                        }
                        else if (arrayRow.tilesY[x].tilesScriptX[y + 1].blockColor == arrayRow.tilesY[x + 1].tilesScriptX[y + 1].blockColor && arrayRow.tilesY[x].tilesScriptX[y + 1].blockColor == arrayRow.tilesY[x - 1].tilesScriptX[y + 1].blockColor)
                        {
                            for (int i = -1; i < 2; i++)//세로
                            {
                                if (i == 0)//겹치는 블록
                                {
                                    isHaveDestroyBlock++;
                                    GameObject sprayItem = OPBlock.instance.SetObject(13);
                                    sprayItem.transform.position = arrayRow.tilesY[x + i].tilesScriptX[y + 1].transform.position;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].block.SetActive(false);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].block = sprayItem;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].blockColor = 13;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].isEmpty = false;
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 1]);
                                }
                                else//나머지 블록 제거 및 이펙트 효과
                                {
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 1].transform.position, 0.4f);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 1].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 1]);
                                }
                            }
                            for (int i = 1; i < 3; i++)//가로 
                            {
                                if (i == 1)
                                {
                                    arrayRow.tilesY[x].tilesScriptX[y + 0].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 1].transform.position, 0.4f);
                                    arrayRow.tilesY[x].tilesScriptX[y + 0].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + 0].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + 0]);
                                }
                                else
                                {
                                    arrayRow.tilesY[x].tilesScriptX[y + i].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 1].transform.position, 0.4f);
                                    arrayRow.tilesY[x].tilesScriptX[y + i].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + i].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + i]);
                                }
                            }
                        }

                        else if (arrayRow.tilesY[x].tilesScriptX[y + 2].blockColor == arrayRow.tilesY[x + 1].tilesScriptX[y + 2].blockColor && arrayRow.tilesY[x].tilesScriptX[y + 2].blockColor == arrayRow.tilesY[x + 2].tilesScriptX[y + 2].blockColor)
                        {
                            for (int i = 0; i < 3; i++)//세로
                            {
                                if (i == 0)//겹치는 블록
                                {
                                    isHaveDestroyBlock++;
                                    GameObject sprayItem = OPBlock.instance.SetObject(13);
                                    sprayItem.transform.position = arrayRow.tilesY[x + i].tilesScriptX[y + 2].transform.position;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].block.SetActive(false);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].block = sprayItem;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].blockColor = 13;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].isEmpty = false;
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 2]);
                                }
                                else//나머지 블록 제거 및 이펙트 효과
                                {
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 2].transform.position, 0.4f);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 2].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 2]);
                                }
                            }
                            for (int i = 0; i < 2; i++)//가로 
                            {
                                arrayRow.tilesY[x].tilesScriptX[y + i].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 2].transform.position, 0.4f);
                                arrayRow.tilesY[x].tilesScriptX[y + i].isEmpty = true;
                                destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + i].block.gameObject);
                                destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + i]);
                            }
                        }
                        else if (arrayRow.tilesY[x].tilesScriptX[y + 2].blockColor == arrayRow.tilesY[x + 1].tilesScriptX[y + 2].blockColor && arrayRow.tilesY[x].tilesScriptX[y + 2].blockColor == arrayRow.tilesY[x - 1].tilesScriptX[y + 2].blockColor)
                        {
                            for (int i = -1; i < 2; i++)//세로
                            {
                                if (i == 0)//겹치는 블록
                                {
                                    isHaveDestroyBlock++;
                                    GameObject sprayItem = OPBlock.instance.SetObject(13);
                                    sprayItem.transform.position = arrayRow.tilesY[x + i].tilesScriptX[y + 2].transform.position;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].block.SetActive(false);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].block = sprayItem;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].blockColor = 13;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].isEmpty = false;
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 2]);
                                }
                                else//나머지 블록 제거 및 이펙트 효과
                                {
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 2].transform.position, 0.4f);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 2].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 2]);
                                }
                            }
                            for (int i = 0; i < 2; i++)//가로 
                            {
                                arrayRow.tilesY[x].tilesScriptX[y + i].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 2].transform.position, 0.4f);
                                arrayRow.tilesY[x].tilesScriptX[y + i].isEmpty = true;
                                destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + i].block.gameObject);
                                destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + i]);
                            }
                        }
                    }
                    else if (x == 7)
                    {
                        if (arrayRow.tilesY[x].tilesScriptX[y].blockColor == arrayRow.tilesY[x + 1].tilesScriptX[y].blockColor && arrayRow.tilesY[x].tilesScriptX[y].blockColor == arrayRow.tilesY[x - 1].tilesScriptX[y].blockColor)
                        {
                            for (int i = -1; i < 2; i++)//세로
                            {
                                if (i == 0)//겹치는 블록
                                {
                                    isHaveDestroyBlock++;
                                    GameObject sprayItem = OPBlock.instance.SetObject(13);
                                    sprayItem.transform.position = arrayRow.tilesY[x + i].tilesScriptX[y].transform.position;
                                    arrayRow.tilesY[x + i].tilesScriptX[y].block.SetActive(false);
                                    arrayRow.tilesY[x + i].tilesScriptX[y].block = sprayItem;
                                    arrayRow.tilesY[x + i].tilesScriptX[y].blockColor = 13;
                                    arrayRow.tilesY[x + i].tilesScriptX[y].isEmpty = false;
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y]);
                                }
                                else//나머지 블록 제거 및 이펙트 효과
                                {
                                    arrayRow.tilesY[x + i].tilesScriptX[y].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y].transform.position, 0.4f);
                                    arrayRow.tilesY[x + i].tilesScriptX[y].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x + i].tilesScriptX[y].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y]);
                                }
                            }
                            for (int i = 1; i < 3; i++)//가로 
                            {
                                arrayRow.tilesY[x].tilesScriptX[y + i].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y].transform.position, 0.4f);
                                arrayRow.tilesY[x].tilesScriptX[y + i].isEmpty = true;
                                destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + i].block.gameObject);
                                destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + i]);
                            }
                        }
                        else if (arrayRow.tilesY[x].tilesScriptX[y].blockColor == arrayRow.tilesY[x - 1].tilesScriptX[y].blockColor && arrayRow.tilesY[x].tilesScriptX[y].blockColor == arrayRow.tilesY[x - 2].tilesScriptX[y].blockColor)
                        {
                            for (int i = -2; i < 1; i++)//세로
                            {
                                if (i == 0)//겹치는 블록
                                {
                                    isHaveDestroyBlock++;
                                    GameObject sprayItem = OPBlock.instance.SetObject(13);
                                    sprayItem.transform.position = arrayRow.tilesY[x + i].tilesScriptX[y].transform.position;
                                    arrayRow.tilesY[x + i].tilesScriptX[y].block.SetActive(false);
                                    arrayRow.tilesY[x + i].tilesScriptX[y].block = sprayItem;
                                    arrayRow.tilesY[x + i].tilesScriptX[y].blockColor = 13;
                                    arrayRow.tilesY[x + i].tilesScriptX[y].isEmpty = false;
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y]);
                                }
                                else//나머지 블록 제거 및 이펙트 효과
                                {
                                    arrayRow.tilesY[x + i].tilesScriptX[y].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y].transform.position, 0.4f);
                                    arrayRow.tilesY[x + i].tilesScriptX[y].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x + i].tilesScriptX[y].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y]);
                                }
                            }
                            for (int i = 1; i < 3; i++)//가로 
                            {
                                arrayRow.tilesY[x].tilesScriptX[y + i].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y].transform.position, 0.4f);
                                arrayRow.tilesY[x].tilesScriptX[y + i].isEmpty = true;
                                destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + i].block.gameObject);
                                destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + i]);
                            }
                        }

                        else if (arrayRow.tilesY[x].tilesScriptX[y + 1].blockColor == arrayRow.tilesY[x + 1].tilesScriptX[y + 1].blockColor && arrayRow.tilesY[x].tilesScriptX[y + 1].blockColor == arrayRow.tilesY[x - 1].tilesScriptX[y + 1].blockColor)
                        {
                            for (int i = -1; i < 2; i++)//세로
                            {
                                if (i == 0)//겹치는 블록
                                {
                                    isHaveDestroyBlock++;
                                    GameObject sprayItem = OPBlock.instance.SetObject(13);
                                    sprayItem.transform.position = arrayRow.tilesY[x + i].tilesScriptX[y + 1].transform.position;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].block.SetActive(false);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].block = sprayItem;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].blockColor = 13;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].isEmpty = false;
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 1]);
                                }
                                else//나머지 블록 제거 및 이펙트 효과
                                {
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 1].transform.position, 0.4f);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 1].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 1]);
                                }
                            }
                            for (int i = 1; i < 3; i++)//가로 
                            {
                                if (i == 1)
                                {
                                    arrayRow.tilesY[x].tilesScriptX[y + 0].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 1].transform.position, 0.4f);
                                    arrayRow.tilesY[x].tilesScriptX[y + 0].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + 0].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + 0]);
                                }
                                else
                                {
                                    arrayRow.tilesY[x].tilesScriptX[y + i].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 1].transform.position, 0.4f);
                                    arrayRow.tilesY[x].tilesScriptX[y + i].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + i].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + i]);
                                }
                            }
                        }
                        else if (arrayRow.tilesY[x].tilesScriptX[y + 1].blockColor == arrayRow.tilesY[x - 1].tilesScriptX[y + 1].blockColor && arrayRow.tilesY[x].tilesScriptX[y + 1].blockColor == arrayRow.tilesY[x - 2].tilesScriptX[y + 1].blockColor)
                        {
                            for (int i = -2; i < 1; i++)//세로
                            {
                                if (i == 0)//겹치는 블록
                                {
                                    isHaveDestroyBlock++;
                                    GameObject sprayItem = OPBlock.instance.SetObject(13);
                                    sprayItem.transform.position = arrayRow.tilesY[x + i].tilesScriptX[y + 1].transform.position;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].block.SetActive(false);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].block = sprayItem;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].blockColor = 13;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].isEmpty = false;
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 1]);
                                }
                                else//나머지 블록 제거 및 이펙트 효과
                                {
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 1].transform.position, 0.4f);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 1].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 1]);
                                }
                            }
                            for (int i = 1; i < 3; i++)//가로 
                            {
                                if (i == 1)
                                {
                                    arrayRow.tilesY[x].tilesScriptX[y + 0].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 1].transform.position, 0.4f);
                                    arrayRow.tilesY[x].tilesScriptX[y + 0].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + 0].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + 0]);
                                }
                                else
                                {
                                    arrayRow.tilesY[x].tilesScriptX[y + i].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 1].transform.position, 0.4f);
                                    arrayRow.tilesY[x].tilesScriptX[y + i].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + i].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + i]);
                                }
                            }
                        }

                        else if (arrayRow.tilesY[x].tilesScriptX[y + 2].blockColor == arrayRow.tilesY[x + 1].tilesScriptX[y + 2].blockColor && arrayRow.tilesY[x].tilesScriptX[y + 2].blockColor == arrayRow.tilesY[x - 1].tilesScriptX[y + 2].blockColor)
                        {
                            for (int i = -1; i < 2; i++)//세로
                            {
                                if (i == 0)//겹치는 블록
                                {
                                    isHaveDestroyBlock++;
                                    GameObject sprayItem = OPBlock.instance.SetObject(13);
                                    sprayItem.transform.position = arrayRow.tilesY[x + i].tilesScriptX[y + 2].transform.position;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].block.SetActive(false);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].block = sprayItem;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].blockColor = 13;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].isEmpty = false;
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 2]);
                                }
                                else//나머지 블록 제거 및 이펙트 효과
                                {
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 2].transform.position, 0.4f);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 2].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 2]);
                                }
                            }
                            for (int i = 0; i < 2; i++)//가로 
                            {
                                arrayRow.tilesY[x].tilesScriptX[y + i].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 2].transform.position, 0.4f);
                                arrayRow.tilesY[x].tilesScriptX[y + i].isEmpty = true;
                                destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + i].block.gameObject);
                                destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + i]);
                            }
                        }
                        else if (arrayRow.tilesY[x].tilesScriptX[y + 2].blockColor == arrayRow.tilesY[x - 1].tilesScriptX[y + 2].blockColor && arrayRow.tilesY[x].tilesScriptX[y + 2].blockColor == arrayRow.tilesY[x - 2].tilesScriptX[y + 2].blockColor)
                        {
                            for (int i = -2; i < 1; i++)//세로
                            {
                                if (i == 0)//겹치는 블록
                                {
                                    isHaveDestroyBlock++;
                                    GameObject sprayItem = OPBlock.instance.SetObject(13);
                                    sprayItem.transform.position = arrayRow.tilesY[x + i].tilesScriptX[y + 2].transform.position;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].block.SetActive(false);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].block = sprayItem;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].blockColor = 13;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].isEmpty = false;
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 2]);
                                }
                                else//나머지 블록 제거 및 이펙트 효과
                                {
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 2].transform.position, 0.4f);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 2].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 2]);
                                }
                            }
                            for (int i = 0; i < 2; i++)//가로 
                            {
                                arrayRow.tilesY[x].tilesScriptX[y + i].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 2].transform.position, 0.4f);
                                arrayRow.tilesY[x].tilesScriptX[y + i].isEmpty = true;
                                destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + i].block.gameObject);
                                destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + i]);
                            }
                        }
                    }
                    else if (x == 8)
                    {
                        if (arrayRow.tilesY[x].tilesScriptX[y].blockColor == arrayRow.tilesY[x - 1].tilesScriptX[y].blockColor && arrayRow.tilesY[x].tilesScriptX[y].blockColor == arrayRow.tilesY[x - 2].tilesScriptX[y].blockColor)
                        {
                            for (int i = -2; i < 1; i++)//세로
                            {
                                if (i == 0)//겹치는 블록
                                {
                                    isHaveDestroyBlock++;
                                    GameObject sprayItem = OPBlock.instance.SetObject(13);
                                    sprayItem.transform.position = arrayRow.tilesY[x + i].tilesScriptX[y].transform.position;
                                    arrayRow.tilesY[x + i].tilesScriptX[y].block.SetActive(false);
                                    arrayRow.tilesY[x + i].tilesScriptX[y].block = sprayItem;
                                    arrayRow.tilesY[x + i].tilesScriptX[y].blockColor = 13;
                                    arrayRow.tilesY[x + i].tilesScriptX[y].isEmpty = false;
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y]);
                                }
                                else//나머지 블록 제거 및 이펙트 효과
                                {
                                    arrayRow.tilesY[x + i].tilesScriptX[y].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y].transform.position, 0.4f);
                                    arrayRow.tilesY[x + i].tilesScriptX[y].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x + i].tilesScriptX[y].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y]);
                                }
                            }
                            for (int i = 1; i < 3; i++)//가로 
                            {
                                arrayRow.tilesY[x].tilesScriptX[y + i].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y].transform.position, 0.4f);
                                arrayRow.tilesY[x].tilesScriptX[y + i].isEmpty = true;
                                destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + i].block.gameObject);
                                destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + i]);
                            }
                        }

                        else if (arrayRow.tilesY[x].tilesScriptX[y + 1].blockColor == arrayRow.tilesY[x - 1].tilesScriptX[y + 1].blockColor && arrayRow.tilesY[x].tilesScriptX[y + 1].blockColor == arrayRow.tilesY[x - 2].tilesScriptX[y + 1].blockColor)
                        {
                            for (int i = -2; i < 1; i++)//세로
                            {
                                if (i == 0)//겹치는 블록
                                {
                                    isHaveDestroyBlock++;
                                    GameObject sprayItem = OPBlock.instance.SetObject(13);
                                    sprayItem.transform.position = arrayRow.tilesY[x + i].tilesScriptX[y + 1].transform.position;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].block.SetActive(false);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].block = sprayItem;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].blockColor = 13;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].isEmpty = false;
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 1]);
                                }
                                else//나머지 블록 제거 및 이펙트 효과
                                {
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 1].transform.position, 0.4f);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 1].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 1]);
                                }
                            }
                            for (int i = 1; i < 3; i++)//가로 
                            {
                                if (i == 1)
                                {
                                    arrayRow.tilesY[x].tilesScriptX[y + 0].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 1].transform.position, 0.4f);
                                    arrayRow.tilesY[x].tilesScriptX[y + 0].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + 0].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + 0]);
                                }
                                else
                                {
                                    arrayRow.tilesY[x].tilesScriptX[y + i].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 1].transform.position, 0.4f);
                                    arrayRow.tilesY[x].tilesScriptX[y + i].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + i].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + i]);
                                }
                            }
                        }

                        else if (arrayRow.tilesY[x].tilesScriptX[y + 2].blockColor == arrayRow.tilesY[x - 1].tilesScriptX[y + 2].blockColor && arrayRow.tilesY[x].tilesScriptX[y + 2].blockColor == arrayRow.tilesY[x - 2].tilesScriptX[y + 2].blockColor)
                        {
                            for (int i = -2; i < 1; i++)//세로
                            {
                                if (i == 0)//겹치는 블록
                                {
                                    isHaveDestroyBlock++;
                                    GameObject sprayItem = OPBlock.instance.SetObject(13);
                                    sprayItem.transform.position = arrayRow.tilesY[x + i].tilesScriptX[y + 2].transform.position;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].block.SetActive(false);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].block = sprayItem;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].blockColor = 13;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].isEmpty = false;
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 2]);
                                }
                                else//나머지 블록 제거 및 이펙트 효과
                                {
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 2].transform.position, 0.4f);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 2].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 2]);
                                }
                            }
                            for (int i = 0; i < 2; i++)//가로 
                            {
                                arrayRow.tilesY[x].tilesScriptX[y + i].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 2].transform.position, 0.4f);
                                arrayRow.tilesY[x].tilesScriptX[y + i].isEmpty = true;
                                destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + i].block.gameObject);
                                destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + i]);
                            }
                        }
                    }
                    else
                    {
                        if (arrayRow.tilesY[x].tilesScriptX[y].blockColor == arrayRow.tilesY[x + 1].tilesScriptX[y].blockColor && arrayRow.tilesY[x].tilesScriptX[y].blockColor == arrayRow.tilesY[x + 2].tilesScriptX[y].blockColor)
                        {
                            for (int i = 0; i < 3; i++)//세로
                            {
                                if (i == 0)//겹치는 블록
                                {
                                    isHaveDestroyBlock++;
                                    GameObject sprayItem = OPBlock.instance.SetObject(13);
                                    sprayItem.transform.position = arrayRow.tilesY[x + i].tilesScriptX[y].transform.position;
                                    arrayRow.tilesY[x + i].tilesScriptX[y].block.SetActive(false);
                                    arrayRow.tilesY[x + i].tilesScriptX[y].block = sprayItem;
                                    arrayRow.tilesY[x + i].tilesScriptX[y].blockColor = 13;
                                    arrayRow.tilesY[x + i].tilesScriptX[y].isEmpty = false;
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y]);
                                }
                                else//나머지 블록 제거 및 이펙트 효과
                                {
                                    arrayRow.tilesY[x + i].tilesScriptX[y].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y].transform.position, 0.4f);
                                    arrayRow.tilesY[x + i].tilesScriptX[y].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x + i].tilesScriptX[y].block);
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y]);
                                }
                            }
                            for (int i = 1; i < 3; i++)//가로 
                            {
                                arrayRow.tilesY[x].tilesScriptX[y + i].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y].transform.position, 0.4f);
                                arrayRow.tilesY[x].tilesScriptX[y + i].isEmpty = true;
                                destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + i].block.gameObject);
                                destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + i]);
                            }
                        }
                        else if (arrayRow.tilesY[x].tilesScriptX[y].blockColor == arrayRow.tilesY[x + 1].tilesScriptX[y].blockColor && arrayRow.tilesY[x].tilesScriptX[y].blockColor == arrayRow.tilesY[x - 1].tilesScriptX[y].blockColor)
                        {
                            for (int i = -1; i < 2; i++)//세로
                            {
                                if (i == 0)//겹치는 블록
                                {
                                    isHaveDestroyBlock++;
                                    GameObject sprayItem = OPBlock.instance.SetObject(13);
                                    sprayItem.transform.position = arrayRow.tilesY[x + i].tilesScriptX[y].transform.position;
                                    arrayRow.tilesY[x + i].tilesScriptX[y].block.SetActive(false);
                                    arrayRow.tilesY[x + i].tilesScriptX[y].block = sprayItem;
                                    arrayRow.tilesY[x + i].tilesScriptX[y].blockColor = 13;
                                    arrayRow.tilesY[x + i].tilesScriptX[y].isEmpty = false;
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y]);
                                }
                                else//나머지 블록 제거 및 이펙트 효과
                                {
                                    arrayRow.tilesY[x + i].tilesScriptX[y].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y].transform.position, 0.4f);
                                    arrayRow.tilesY[x + i].tilesScriptX[y].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x + i].tilesScriptX[y].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y]);
                                }
                            }
                            for (int i = 1; i < 3; i++)//가로 
                            {
                                arrayRow.tilesY[x].tilesScriptX[y + i].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y].transform.position, 0.4f);
                                arrayRow.tilesY[x].tilesScriptX[y + i].isEmpty = true;
                                destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + i].block.gameObject);
                                destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + i]);
                            }
                        }
                        else if (arrayRow.tilesY[x].tilesScriptX[y].blockColor == arrayRow.tilesY[x - 1].tilesScriptX[y].blockColor && arrayRow.tilesY[x].tilesScriptX[y].blockColor == arrayRow.tilesY[x - 2].tilesScriptX[y].blockColor)
                        {
                            for (int i = -2; i < 1; i++)//세로
                            {
                                if (i == 0)//겹치는 블록
                                {
                                    isHaveDestroyBlock++;
                                    GameObject sprayItem = OPBlock.instance.SetObject(13);
                                    sprayItem.transform.position = arrayRow.tilesY[x + i].tilesScriptX[y].transform.position;
                                    arrayRow.tilesY[x + i].tilesScriptX[y].block.SetActive(false);
                                    arrayRow.tilesY[x + i].tilesScriptX[y].block = sprayItem;
                                    arrayRow.tilesY[x + i].tilesScriptX[y].blockColor = 13;
                                    arrayRow.tilesY[x + i].tilesScriptX[y].isEmpty = false;
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y]);
                                }
                                else//나머지 블록 제거 및 이펙트 효과
                                {
                                    arrayRow.tilesY[x + i].tilesScriptX[y].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y].transform.position, 0.4f);
                                    arrayRow.tilesY[x + i].tilesScriptX[y].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x + i].tilesScriptX[y].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y]);
                                }
                            }
                            for (int i = 1; i < 3; i++)//가로 
                            {
                                arrayRow.tilesY[x].tilesScriptX[y + i].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y].transform.position, 0.4f);
                                arrayRow.tilesY[x].tilesScriptX[y + i].isEmpty = true;
                                destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + i].block.gameObject);
                                destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + i]);
                            }
                        }

                        else if (arrayRow.tilesY[x].tilesScriptX[y + 1].blockColor == arrayRow.tilesY[x + 1].tilesScriptX[y + 1].blockColor && arrayRow.tilesY[x].tilesScriptX[y + 1].blockColor == arrayRow.tilesY[x + 2].tilesScriptX[y + 1].blockColor)
                        {
                            for (int i = 0; i < 3; i++)//세로
                            {
                                if (i == 0)//겹치는 블록
                                {
                                    isHaveDestroyBlock++;
                                    GameObject sprayItem = OPBlock.instance.SetObject(13);
                                    sprayItem.transform.position = arrayRow.tilesY[x + i].tilesScriptX[y + 1].transform.position;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].block.SetActive(false);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].block = sprayItem;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].blockColor = 13;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].isEmpty = false;
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 1]);
                                }
                                else//나머지 블록 제거 및 이펙트 효과
                                {
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 1].transform.position, 0.4f);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 1].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 1]);
                                }
                            }
                            for (int i = 1; i < 3; i++)//가로 
                            {
                                if (i == 1)
                                {
                                    arrayRow.tilesY[x].tilesScriptX[y + 0].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 1].transform.position, 0.4f);
                                    arrayRow.tilesY[x].tilesScriptX[y + 0].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + 0].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + 0]);
                                }
                                else
                                { 
                                    arrayRow.tilesY[x].tilesScriptX[y + i].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 1].transform.position, 0.4f);
                                    arrayRow.tilesY[x].tilesScriptX[y + i].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + i].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + i]);
                                }
                            }
                        }
                        else if (arrayRow.tilesY[x].tilesScriptX[y + 1].blockColor == arrayRow.tilesY[x + 1].tilesScriptX[y + 1].blockColor && arrayRow.tilesY[x].tilesScriptX[y + 1].blockColor == arrayRow.tilesY[x - 1].tilesScriptX[y + 1].blockColor)
                        {
                            for (int i = -1; i < 2; i++)//세로
                            {
                                if (i == 0)//겹치는 블록
                                {
                                    isHaveDestroyBlock++;
                                    GameObject sprayItem = OPBlock.instance.SetObject(13);
                                    sprayItem.transform.position = arrayRow.tilesY[x + i].tilesScriptX[y + 1].transform.position;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].block.SetActive(false);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].block = sprayItem;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].blockColor = 13;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].isEmpty = false;
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 1]);
                                }
                                else//나머지 블록 제거 및 이펙트 효과
                                {
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 1].transform.position, 0.4f);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 1].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 1]);
                                }
                            }
                            for (int i = 1; i < 3; i++)//가로 
                            {
                                if (i == 1)
                                {
                                    arrayRow.tilesY[x].tilesScriptX[y + 0].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 1].transform.position, 0.4f);
                                    arrayRow.tilesY[x].tilesScriptX[y + 0].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + 0].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + 0]);
                                }
                                else
                                {
                                    arrayRow.tilesY[x].tilesScriptX[y + i].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 1].transform.position, 0.4f);
                                    arrayRow.tilesY[x].tilesScriptX[y + i].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + i].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + i]);
                                }
                            }
                        }
                        else if (arrayRow.tilesY[x].tilesScriptX[y + 1].blockColor == arrayRow.tilesY[x - 1].tilesScriptX[y + 1].blockColor && arrayRow.tilesY[x].tilesScriptX[y + 1].blockColor == arrayRow.tilesY[x - 2].tilesScriptX[y + 1].blockColor)
                        {
                            for (int i = -2; i < 1; i++)//세로
                            {
                                if (i == 0)//겹치는 블록
                                {
                                    isHaveDestroyBlock++;
                                    GameObject sprayItem = OPBlock.instance.SetObject(13);
                                    sprayItem.transform.position = arrayRow.tilesY[x + i].tilesScriptX[y + 1].transform.position;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].block.SetActive(false);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].block = sprayItem;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].blockColor = 13;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].isEmpty = false;
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 1]);
                                }
                                else//나머지 블록 제거 및 이펙트 효과
                                {
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 1].transform.position, 0.4f);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 1].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 1].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 1]);
                                }
                            }
                            for (int i = 1; i < 3; i++)//가로 
                            {
                                if (i == 1)
                                {
                                    arrayRow.tilesY[x].tilesScriptX[y + 0].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 1].transform.position, 0.4f);
                                    arrayRow.tilesY[x].tilesScriptX[y + 0].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + 0].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + 0]);
                                }
                                else
                                {
                                    arrayRow.tilesY[x].tilesScriptX[y + i].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 1].transform.position, 0.4f);
                                    arrayRow.tilesY[x].tilesScriptX[y + i].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + i].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + i]);
                                }
                            }
                        }

                        else if (arrayRow.tilesY[x].tilesScriptX[y + 2].blockColor == arrayRow.tilesY[x + 1].tilesScriptX[y + 2].blockColor && arrayRow.tilesY[x].tilesScriptX[y + 2].blockColor == arrayRow.tilesY[x + 2].tilesScriptX[y + 2].blockColor)
                        {
                            for (int i = 0; i < 3; i++)//세로
                            {
                                if (i == 0)//겹치는 블록
                                {
                                    isHaveDestroyBlock++;
                                    GameObject sprayItem = OPBlock.instance.SetObject(13);
                                    sprayItem.transform.position = arrayRow.tilesY[x + i].tilesScriptX[y + 2].transform.position;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].block.SetActive(false);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].block = sprayItem;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].blockColor = 13;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].isEmpty = false;
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 2]);
                                }
                                else//나머지 블록 제거 및 이펙트 효과
                                {
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 2].transform.position, 0.4f);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 2].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 2]);
                                }
                            }
                            for (int i = 0; i < 2; i++)//가로 
                            {
                                arrayRow.tilesY[x].tilesScriptX[y + i].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 2].transform.position, 0.4f);
                                arrayRow.tilesY[x].tilesScriptX[y + i].isEmpty = true;
                                destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + i].block.gameObject);
                                destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + i]);
                            }
                        }
                        else if (arrayRow.tilesY[x].tilesScriptX[y + 2].blockColor == arrayRow.tilesY[x + 1].tilesScriptX[y + 2].blockColor && arrayRow.tilesY[x].tilesScriptX[y + 2].blockColor == arrayRow.tilesY[x - 1].tilesScriptX[y + 2].blockColor)
                        {
                            for (int i = -1; i < 2; i++)//세로
                            {
                                if (i == 0)//겹치는 블록
                                {
                                    isHaveDestroyBlock++;
                                    GameObject sprayItem = OPBlock.instance.SetObject(13);
                                    sprayItem.transform.position = arrayRow.tilesY[x + i].tilesScriptX[y + 2].transform.position;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].block.SetActive(false);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].block = sprayItem;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].blockColor = 13;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].isEmpty = false;
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 2]);
                                }
                                else//나머지 블록 제거 및 이펙트 효과
                                {
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 2].transform.position, 0.4f);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 2].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 2]);
                                }
                            }
                            for (int i = 0; i < 2; i++)//가로 
                            {
                                arrayRow.tilesY[x].tilesScriptX[y + i].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 2].transform.position, 0.4f);
                                arrayRow.tilesY[x].tilesScriptX[y + i].isEmpty = true;
                                destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + i].block.gameObject);
                                destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + i]);
                            }
                        }
                        else if (arrayRow.tilesY[x].tilesScriptX[y + 2].blockColor == arrayRow.tilesY[x - 1].tilesScriptX[y + 2].blockColor && arrayRow.tilesY[x].tilesScriptX[y + 2].blockColor == arrayRow.tilesY[x - 2].tilesScriptX[y + 2].blockColor)
                        {
                            for (int i = -2; i < 1; i++)//세로
                            {
                                if (i == 0)//겹치는 블록
                                {
                                    isHaveDestroyBlock++;
                                    GameObject sprayItem = OPBlock.instance.SetObject(13);
                                    sprayItem.transform.position = arrayRow.tilesY[x + i].tilesScriptX[y + 2].transform.position;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].block.SetActive(false);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].block = sprayItem;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].blockColor = 13;
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].isEmpty = false;
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 2]);
                                }
                                else//나머지 블록 제거 및 이펙트 효과
                                {
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 2].transform.position, 0.4f);
                                    arrayRow.tilesY[x + i].tilesScriptX[y + 2].isEmpty = true;
                                    destroyBlock.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 2].block.gameObject);
                                    destroyTile.Add(arrayRow.tilesY[x + i].tilesScriptX[y + 2]);
                                }
                            }
                            for (int i = 0; i < 2; i++)//가로 
                            {
                                arrayRow.tilesY[x].tilesScriptX[y + i].block.transform.DOMove(arrayRow.tilesY[x].tilesScriptX[y + 2].transform.position, 0.4f);
                                arrayRow.tilesY[x].tilesScriptX[y + i].isEmpty = true;
                                destroyBlock.Add(arrayRow.tilesY[x].tilesScriptX[y + i].block.gameObject);
                                destroyTile.Add(arrayRow.tilesY[x].tilesScriptX[y + i]);
                            }
                        }
                    }
                    
                }        
            }
        }
        yield return new WaitForSeconds(1f);

    }
    /// <summary>
    /// 호루라기 아이템을 사용할 때 나오는 독수리 이펙트 생성
    /// </summary>
    /// <param name="tile"></param>
    /// <param name="XorY"></param>
    /// <returns></returns>
    IEnumerator CreateEagleEffect(Tile tile,bool XorY)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if(tiles[i].GetComponent<Tile>().pos.x == tile.pos.x && XorY)
            {
                if(tiles[i].GetComponent<Tile>().pos.y == 0)
                {
                    firstTile = tiles[i].GetComponent<Tile>();
                }
                if(tiles[i].GetComponent<Tile>().pos.y == 8)
                {
                    lastTile = tiles[i].GetComponent<Tile>();
                }
            }
            else if (tiles[i].GetComponent<Tile>().pos.y == tile.pos.y && !XorY)
            {
                if (tiles[i].GetComponent<Tile>().pos.x == 0)
                {
                    firstTile = tiles[i].GetComponent<Tile>();
                }
                if (tiles[i].GetComponent<Tile>().pos.x == 7)
                {
                    lastTile = tiles[i].GetComponent<Tile>();
                }
            }
        }
        GameObject eagleEffect = BlockEffectOPManager.instance.SetObject(20);
        if (XorY)
        {
            eagleEffect.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        }
        else
        {
            eagleEffect.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        eagleEffect.transform.position = firstTile.transform.position;
        eagleEffect.transform.localScale = new Vector3(20,20,20);
        eagleEffect.transform.DOMove(lastTile.transform.position, 0.25f);
        yield return new WaitForSeconds(0.25f);
        eagleEffect.gameObject.SetActive(false);
    }
    /// <summary>
    /// 독수리 아이템을 사용했을 때 아이템 효과
    /// </summary>
    /// <param name="target"></param>
    /// <param name="SwappingTarget"></param>
    /// <returns></returns>
    IEnumerator UseEagleItem(Tile target, Tile SwappingTarget)
    {
        yield return new WaitForSeconds(0.40f);
        useItem = true;
        if (target.blockColor == 14 && SwappingTarget.blockColor == 14)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                if (!tiles[i].GetComponent<Tile>().isEmpty && tiles[i].GetComponent<Tile>().pos.y == target.pos.y)
                {
                    if (tiles[i].GetComponent<Tile>().block.GetComponent<Block>().blocktype == Block.BlockType.ITEM && tiles[i].GetComponent<Tile>().pos != SwappingTarget.pos)
                    {
                        for (int j = tiles.Length - 1; j >= 0; j--)
                        {
                            if (tiles[i].GetComponent<Tile>().pos.x == tiles[j].GetComponent<Tile>().pos.x && tiles[j].GetComponent<Tile>().block != null && (tiles[i].GetComponent<Tile>().pos.y != tiles[j].GetComponent<Tile>().pos.y))
                            {
                                yield return new WaitForSeconds(0.01f);
                                if(tiles[j].GetComponent<Tile>().blockColor == 12)
                                {
                                    StartCoroutine(BoomItem(tiles[j].GetComponent<Tile>().pos));
                                }
                                else
                                {
                                    tiles[j].GetComponent<Tile>().isEmpty = true;
                                    tiles[j].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                                    tiles[j].GetComponent<Tile>().block.gameObject.SetActive(false);
                                    tiles[j].GetComponent<Tile>().block = null;
                                    if (tiles[j].GetComponent<Tile>().pos.y == 0)
                                    {
                                        isSwapping = true;
                                        StartCoroutine(CreateEagleEffect(tiles[j].GetComponent<Tile>(), true));
                                    }
                                }
                            }
                        }
                    }
                    yield return new WaitForSeconds(0.01f);
                    if (tiles[i].GetComponent<Tile>().blockColor == 12)
                    {
                        StartCoroutine(BoomItem(tiles[i].GetComponent<Tile>().pos));
                    }
                    else
                    {
                        tiles[i].GetComponent<Tile>().isEmpty = true;
                        tiles[i].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                        tiles[i].GetComponent<Tile>().block.gameObject.SetActive(false);
                        tiles[i].GetComponent<Tile>().block = null;
                        if (tiles[i].GetComponent<Tile>().pos.x == 0)
                        {
                            isSwapping = true;
                            StartCoroutine(CreateEagleEffect(tiles[i].GetComponent<Tile>(), false));
                        }
                    }
                }
                if (!tiles[i].GetComponent<Tile>().isEmpty && tiles[i].GetComponent<Tile>().pos.y == SwappingTarget.pos.y)
                {
                    if (tiles[i].GetComponent<Tile>().block.GetComponent<Block>().blocktype == Block.BlockType.ITEM && tiles[i].GetComponent<Tile>().pos != target.pos)
                    {
                        for (int j = tiles.Length - 1; j >= 0; j--)
                        {
                            if (tiles[i].GetComponent<Tile>().pos.x == tiles[j].GetComponent<Tile>().pos.x && tiles[j].GetComponent<Tile>().block != null && (tiles[i].GetComponent<Tile>().pos.y != tiles[j].GetComponent<Tile>().pos.y))
                            {
                                yield return new WaitForSeconds(0.01f);
                                if (tiles[j].GetComponent<Tile>().blockColor == 12)
                                {
                                    StartCoroutine(BoomItem(tiles[j].GetComponent<Tile>().pos));
                                }
                                else
                                {
                                    tiles[j].GetComponent<Tile>().isEmpty = true;
                                    tiles[j].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                                    tiles[j].GetComponent<Tile>().block.gameObject.SetActive(false);
                                    tiles[j].GetComponent<Tile>().block = null;
                                    if (tiles[j].GetComponent<Tile>().pos.y == 0)
                                    {
                                        isSwapping = true;
                                        StartCoroutine(CreateEagleEffect(tiles[j].GetComponent<Tile>(), true));
                                    }
                                }
                            }
                        }
                    }
                    yield return new WaitForSeconds(0.01f);
                    if (tiles[i].GetComponent<Tile>().blockColor == 12)
                    {
                        StartCoroutine(BoomItem(tiles[i].GetComponent<Tile>().pos));
                    }
                    else
                    {
                        tiles[i].GetComponent<Tile>().isEmpty = true;
                        tiles[i].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                        tiles[i].GetComponent<Tile>().block.gameObject.SetActive(false);
                        tiles[i].GetComponent<Tile>().block = null;
                        if (tiles[i].GetComponent<Tile>().pos.x == 0)
                        {
                            isSwapping = true;
                            StartCoroutine(CreateEagleEffect(tiles[i].GetComponent<Tile>(), false));
                        }
                    }
                }
            }
        }
        else if (target.blockColor != 14 && SwappingTarget.blockColor == 14)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                if (!tiles[i].GetComponent<Tile>().isEmpty && tiles[i].GetComponent<Tile>().pos.y == SwappingTarget.pos.y)
                {
                    if (tiles[i].GetComponent<Tile>().block.GetComponent<Block>().blocktype == Block.BlockType.ITEM && tiles[i].GetComponent<Tile>().pos != SwappingTarget.pos)
                    {
                        for (int j = tiles.Length-1; j >=0; j--)
                        {
                            if (tiles[i].GetComponent<Tile>().pos.x == tiles[j].GetComponent<Tile>().pos.x && tiles[j].GetComponent<Tile>().block !=null&& (tiles[i].GetComponent<Tile>().pos.y != tiles[j].GetComponent<Tile>().pos.y))
                            {
                                yield return new WaitForSeconds(0.01f);
                                if (tiles[j].GetComponent<Tile>().blockColor == 12)
                                {
                                    StartCoroutine(BoomItem(tiles[j].GetComponent<Tile>().pos));
                                }
                                else
                                {
                                    tiles[j].GetComponent<Tile>().isEmpty = true;
                                    tiles[j].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                                    tiles[j].GetComponent<Tile>().block.gameObject.SetActive(false);
                                    tiles[j].GetComponent<Tile>().block = null;
                                    if (tiles[j].GetComponent<Tile>().pos.y == 0)
                                    {
                                        isSwapping = true;
                                        StartCoroutine(CreateEagleEffect(tiles[j].GetComponent<Tile>(), true));
                                    }
                                }
                            }
                        }
                    }
                    yield return new WaitForSeconds(0.01f);
                    if (tiles[i].GetComponent<Tile>().blockColor == 12)
                    {
                        StartCoroutine(BoomItem(tiles[i].GetComponent<Tile>().pos));
                    }
                    else
                    {
                        tiles[i].GetComponent<Tile>().isEmpty = true;
                        tiles[i].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                        tiles[i].GetComponent<Tile>().block.gameObject.SetActive(false);
                        tiles[i].GetComponent<Tile>().block = null;
                        if (tiles[i].GetComponent<Tile>().pos.x == 0)
                        {
                            isSwapping = true;
                            StartCoroutine(CreateEagleEffect(tiles[i].GetComponent<Tile>(), false));
                        }
                    }
                }
            }
        }
        else if (target.blockColor == 14 && SwappingTarget.blockColor != 14)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                if (!tiles[i].GetComponent<Tile>().isEmpty && tiles[i].GetComponent<Tile>().pos.y == target.pos.y)
                {
                    if (tiles[i].GetComponent<Tile>().block.GetComponent<Block>().blocktype == Block.BlockType.CHESTNUTBLOCK)
                    {
                        TreeMatchGameScoreManager.chestnutPoint++;
                    }
                    else if (tiles[i].GetComponent<Tile>().block.GetComponent<Block>().blocktype == Block.BlockType.MONSTER && tiles[i].GetComponent<Tile>().blockColor != 6)
                    {
                        tiles[i].GetComponent<Tile>().block.GetComponent<Block>().hp = 0;
                        TreeMatchGameScoreManager.fertilizerPoint += 10;
                    }
                    if (tiles[i].GetComponent<Tile>().block.GetComponent<Block>().blocktype == Block.BlockType.ITEM && tiles[i].GetComponent<Tile>().pos != target.pos)
                    {
                        for (int j = tiles.Length - 1; j >= 0; j--)
                        {
                            if (tiles[i].GetComponent<Tile>().pos.x == tiles[j].GetComponent<Tile>().pos.x && tiles[j].GetComponent<Tile>().block != null && (tiles[i].GetComponent<Tile>().pos.y != tiles[j].GetComponent<Tile>().pos.y))
                            {
                                yield return new WaitForSeconds(0.01f);
                                if (tiles[j].GetComponent<Tile>().blockColor == 12)
                                {
                                    StartCoroutine(BoomItem(tiles[j].GetComponent<Tile>().pos));
                                }
                                else
                                {
                                    tiles[j].GetComponent<Tile>().isEmpty = true;
                                    tiles[j].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                                    tiles[j].GetComponent<Tile>().block.gameObject.SetActive(false);
                                    tiles[j].GetComponent<Tile>().block = null;
                                    if (tiles[j].GetComponent<Tile>().pos.y == 0)
                                    {
                                        isSwapping = true;
                                        StartCoroutine(CreateEagleEffect(tiles[j].GetComponent<Tile>(), true));
                                    }
                                }
                            }
                        }
                    }
                    yield return new WaitForSeconds(0.01f);
                    if (tiles[i].GetComponent<Tile>().blockColor == 12)
                    {
                        StartCoroutine(BoomItem(tiles[i].GetComponent<Tile>().pos));
                    }
                    else
                    {
                        tiles[i].GetComponent<Tile>().isEmpty = true;
                        tiles[i].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                        tiles[i].GetComponent<Tile>().block.gameObject.SetActive(false);
                        tiles[i].GetComponent<Tile>().block = null;
                        if (tiles[i].GetComponent<Tile>().pos.x == 0)
                        {
                            isSwapping = true;
                            StartCoroutine(CreateEagleEffect(tiles[i].GetComponent<Tile>(), false));
                        }
                    }
                }
            }
        }
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(PullDownBlock());
    }
    IEnumerator UseEagleItem(Tile target)
    {
        
        yield return new WaitForSeconds(0.40f);
        useItem = true;
        for (int y = 0; y < tiles.Length; y++)
        {
            if (tiles[y].GetComponent<Tile>().pos.y == target.pos.y)
            {
                if (tiles[y].GetComponent<Tile>().blockColor == 14)
                {
                    if (tiles[y].GetComponent<Tile>().pos != target.pos)
                    {
                        for (int x = 0; x < tiles.Length; x++)
                        {
                            if (tiles[x].GetComponent<Tile>().pos.x == tiles[y].GetComponent<Tile>().pos.x)
                            {
                                if (tiles[x].GetComponent<Tile>().blockColor == 14)
                                {
                                    tiles[x].GetComponent<Tile>().isEmpty = true;
                                    tiles[x].GetComponent<Tile>().block.gameObject.SetActive(false);
                                    tiles[x].GetComponent<Tile>().block = null;
                                    if (tiles[x].GetComponent<Tile>().pos.y == 0)
                                    {
                                        isSwapping = true;
                                        StartCoroutine(CreateEagleEffect(tiles[y].GetComponent<Tile>(), false));
                                    }
                                }
                                else
                                {
                                    tiles[x].GetComponent<Tile>().isEmpty = true;
                                    tiles[x].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                                    tiles[x].GetComponent<Tile>().block.gameObject.SetActive(false);
                                    tiles[x].GetComponent<Tile>().block = null;
                                    if (tiles[x].GetComponent<Tile>().pos.y == 0)
                                    {
                                        isSwapping = true;
                                        StartCoroutine(CreateEagleEffect(tiles[y].GetComponent<Tile>(), false));
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        tiles[y].GetComponent<Tile>().isEmpty = true;
                        tiles[y].GetComponent<Tile>().block.gameObject.SetActive(false);
                        tiles[y].GetComponent<Tile>().block = null;
                        if (tiles[y].GetComponent<Tile>().pos.x == 0)
                        {
                            isSwapping = true;
                            StartCoroutine(CreateEagleEffect(tiles[y].GetComponent<Tile>(), true));
                        }
                    }
                }
                else
                {
                    tiles[y].GetComponent<Tile>().isEmpty = true;
                    tiles[y].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                    tiles[y].GetComponent<Tile>().block.gameObject.SetActive(false);
                    tiles[y].GetComponent<Tile>().block = null;
                    if (tiles[y].GetComponent<Tile>().pos.x == 0)
                    {
                        isSwapping = true;
                        StartCoroutine(CreateEagleEffect(tiles[y].GetComponent<Tile>(), false));
                    }
                }
            }
        }
        
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(PullDownBlock());
    }
    /// <summary>
    /// 폭탄 아이템 사용효과
    /// </summary>
    /// <param name="vector2"></param>
    /// <returns></returns>
    IEnumerator BoomItem(Vector2 vector2)
    {
        Debug.Log(vector2);
        isSwapping = true;
        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < tiles.Length; i++)
        {   
            if (tiles[i].GetComponent<Tile>().pos == vector2)
            {
                GameObject effect = BlockEffectOPManager.instance.SetObject(21);
                effect.transform.position = tiles[i].transform.position;
                tiles[i].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                tiles[i].GetComponent<Tile>().block.gameObject.SetActive(false);
                tiles[i].GetComponent<Tile>().blockColor = 255;
                tiles[i].GetComponent<Tile>().block = null;
                tiles[i].GetComponent<Tile>().isEmpty = true;
            }
            //UPRIGHT
            if (tiles[i].GetComponent<Tile>().pos == vector2 + new Vector2(1, 1) && stickSkillLevel >= 2)
            {
                tiles[i].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                tiles[i].GetComponent<Tile>().block.gameObject.SetActive(false);
                tiles[i].GetComponent<Tile>().blockColor = 255;
                tiles[i].GetComponent<Tile>().block = null;
                tiles[i].GetComponent<Tile>().isEmpty = true;
            }
            if (tiles[i].GetComponent<Tile>().pos == vector2 + new Vector2(1, 2) && stickSkillLevel >= 4)
            {
                tiles[i].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                tiles[i].GetComponent<Tile>().block.gameObject.SetActive(false);
                tiles[i].GetComponent<Tile>().blockColor = 255;
                tiles[i].GetComponent<Tile>().block = null;
                tiles[i].GetComponent<Tile>().isEmpty = true;
            }
            if (tiles[i].GetComponent<Tile>().pos == vector2 + new Vector2(2, 1) && stickSkillLevel >= 5)
            {
                tiles[i].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                tiles[i].GetComponent<Tile>().block.gameObject.SetActive(false);
                tiles[i].GetComponent<Tile>().blockColor = 255;
                tiles[i].GetComponent<Tile>().block = null;
                tiles[i].GetComponent<Tile>().isEmpty = true;
            }
            if (tiles[i].GetComponent<Tile>().pos == vector2 + new Vector2(2, 2) && stickSkillLevel >= 5)
            {
                tiles[i].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                tiles[i].GetComponent<Tile>().block.gameObject.SetActive(false);
                tiles[i].GetComponent<Tile>().blockColor = 255;
                tiles[i].GetComponent<Tile>().block = null;
                tiles[i].GetComponent<Tile>().isEmpty = true;
            }
            //UPLEFT
            if (tiles[i].GetComponent<Tile>().pos == vector2 + new Vector2(-1, 1) && stickSkillLevel >= 2)
            {
                tiles[i].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                tiles[i].GetComponent<Tile>().block.gameObject.SetActive(false);
                tiles[i].GetComponent<Tile>().blockColor = 255;
                tiles[i].GetComponent<Tile>().block = null;
                tiles[i].GetComponent<Tile>().isEmpty = true;
            }
            if (tiles[i].GetComponent<Tile>().pos == vector2 + new Vector2(-1, 2) && stickSkillLevel >= 4)
            {
                tiles[i].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                tiles[i].GetComponent<Tile>().block.gameObject.SetActive(false);
                tiles[i].GetComponent<Tile>().blockColor = 255;
                tiles[i].GetComponent<Tile>().block = null;
                tiles[i].GetComponent<Tile>().isEmpty = true;
            }
            if (tiles[i].GetComponent<Tile>().pos == vector2 + new Vector2(-2, 1) && stickSkillLevel >= 5)
            {
                tiles[i].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                tiles[i].GetComponent<Tile>().block.gameObject.SetActive(false);
                tiles[i].GetComponent<Tile>().blockColor = 255;
                tiles[i].GetComponent<Tile>().block = null;
                tiles[i].GetComponent<Tile>().isEmpty = true;
            }
            if (tiles[i].GetComponent<Tile>().pos == vector2 + new Vector2(-2, 2) && stickSkillLevel >= 5)
            {
                tiles[i].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                tiles[i].GetComponent<Tile>().block.gameObject.SetActive(false);
                tiles[i].GetComponent<Tile>().blockColor = 255;
                tiles[i].GetComponent<Tile>().block = null;
                tiles[i].GetComponent<Tile>().isEmpty = true;
            }
            //UP
            if (tiles[i].GetComponent<Tile>().pos == vector2 + new Vector2(0, 1) && stickSkillLevel >= 1)
            {
                tiles[i].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                tiles[i].GetComponent<Tile>().block.gameObject.SetActive(false);
                tiles[i].GetComponent<Tile>().blockColor = 255;
                tiles[i].GetComponent<Tile>().block = null;
                tiles[i].GetComponent<Tile>().isEmpty = true;
            }
            if (tiles[i].GetComponent<Tile>().pos == vector2 + new Vector2(0, 2) && stickSkillLevel >= 3)
            {
                tiles[i].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                tiles[i].GetComponent<Tile>().block.gameObject.SetActive(false);
                tiles[i].GetComponent<Tile>().blockColor = 255;
                tiles[i].GetComponent<Tile>().block = null;
                tiles[i].GetComponent<Tile>().isEmpty = true;
            }
            //DOWNRIGHT
            if (tiles[i].GetComponent<Tile>().pos == vector2 + new Vector2(1, -1) && stickSkillLevel >= 2)
            {
                tiles[i].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                tiles[i].GetComponent<Tile>().block.gameObject.SetActive(false);
                tiles[i].GetComponent<Tile>().blockColor = 255;
                tiles[i].GetComponent<Tile>().block = null;
                tiles[i].GetComponent<Tile>().isEmpty = true;
            }
            if (tiles[i].GetComponent<Tile>().pos == vector2 + new Vector2(1, -2) && stickSkillLevel >= 4)
            {
                tiles[i].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                tiles[i].GetComponent<Tile>().block.gameObject.SetActive(false);
                tiles[i].GetComponent<Tile>().blockColor = 255;
                tiles[i].GetComponent<Tile>().block = null;
                tiles[i].GetComponent<Tile>().isEmpty = true;
            }
            if (tiles[i].GetComponent<Tile>().pos == vector2 + new Vector2(2, -2) && stickSkillLevel >= 5)
            {
                tiles[i].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                tiles[i].GetComponent<Tile>().block.gameObject.SetActive(false);
                tiles[i].GetComponent<Tile>().blockColor = 255;
                tiles[i].GetComponent<Tile>().block = null;
                tiles[i].GetComponent<Tile>().isEmpty = true;
            }
            if (tiles[i].GetComponent<Tile>().pos == vector2 + new Vector2(2, -1) && stickSkillLevel >= 5)
            {
                tiles[i].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                tiles[i].GetComponent<Tile>().block.gameObject.SetActive(false);
                tiles[i].GetComponent<Tile>().blockColor = 255;
                tiles[i].GetComponent<Tile>().block = null;
                tiles[i].GetComponent<Tile>().isEmpty = true;
            }
            //DOWMLEFT
            if (tiles[i].GetComponent<Tile>().pos == vector2 + new Vector2(-1, -1) && stickSkillLevel >= 2)
            {
                tiles[i].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                tiles[i].GetComponent<Tile>().block.gameObject.SetActive(false);
                tiles[i].GetComponent<Tile>().blockColor = 255;
                tiles[i].GetComponent<Tile>().block = null;
                tiles[i].GetComponent<Tile>().isEmpty = true;
            }
            if (tiles[i].GetComponent<Tile>().pos == vector2 + new Vector2(-1, -2) && stickSkillLevel >= 4)
            {
                tiles[i].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                tiles[i].GetComponent<Tile>().block.gameObject.SetActive(false);
                tiles[i].GetComponent<Tile>().blockColor = 255;
                tiles[i].GetComponent<Tile>().block = null;
                tiles[i].GetComponent<Tile>().isEmpty = true;
            }
            if (tiles[i].GetComponent<Tile>().pos == vector2 + new Vector2(-2, -1) && stickSkillLevel >= 5)
            {
                tiles[i].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                tiles[i].GetComponent<Tile>().block.gameObject.SetActive(false);
                tiles[i].GetComponent<Tile>().blockColor = 255;
                tiles[i].GetComponent<Tile>().block = null;
                tiles[i].GetComponent<Tile>().isEmpty = true;
            }
            if (tiles[i].GetComponent<Tile>().pos == vector2 + new Vector2(-2, -2) && stickSkillLevel >= 5)
            {
                tiles[i].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                tiles[i].GetComponent<Tile>().block.gameObject.SetActive(false);
                tiles[i].GetComponent<Tile>().blockColor = 255;
                tiles[i].GetComponent<Tile>().block = null;
                tiles[i].GetComponent<Tile>().isEmpty = true;
            }
            //DOWN
            if (tiles[i].GetComponent<Tile>().pos == vector2 + new Vector2(0, -1) && stickSkillLevel >= 1)
            {
                tiles[i].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                tiles[i].GetComponent<Tile>().block.gameObject.SetActive(false);
                tiles[i].GetComponent<Tile>().blockColor = 255;
                tiles[i].GetComponent<Tile>().block = null;
                tiles[i].GetComponent<Tile>().isEmpty = true;
            }
            if (tiles[i].GetComponent<Tile>().pos == vector2 + new Vector2(0, -2) && stickSkillLevel >= 3)
            {
                tiles[i].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                tiles[i].GetComponent<Tile>().block.gameObject.SetActive(false);
                tiles[i].GetComponent<Tile>().blockColor = 255;
                tiles[i].GetComponent<Tile>().block = null;
                tiles[i].GetComponent<Tile>().isEmpty = true;
            }
            //RIGHT
            if (tiles[i].GetComponent<Tile>().pos == vector2 + new Vector2(1, 0) && stickSkillLevel >= 1)
            {
                tiles[i].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                tiles[i].GetComponent<Tile>().block.gameObject.SetActive(false);
                tiles[i].GetComponent<Tile>().blockColor = 255;
                tiles[i].GetComponent<Tile>().block = null;
                tiles[i].GetComponent<Tile>().isEmpty = true;
            }
            if (tiles[i].GetComponent<Tile>().pos == vector2 + new Vector2(2, 0) && stickSkillLevel >= 3)
            {
                tiles[i].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                tiles[i].GetComponent<Tile>().block.gameObject.SetActive(false);
                tiles[i].GetComponent<Tile>().blockColor = 255;
                tiles[i].GetComponent<Tile>().block = null;
                tiles[i].GetComponent<Tile>().isEmpty = true;
            }
            //LEFT
            if (tiles[i].GetComponent<Tile>().pos == vector2 + new Vector2(-1, 0) && stickSkillLevel >= 1)
            {
                tiles[i].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                tiles[i].GetComponent<Tile>().block.gameObject.SetActive(false);
                tiles[i].GetComponent<Tile>().blockColor = 255;
                tiles[i].GetComponent<Tile>().block = null;
                tiles[i].GetComponent<Tile>().isEmpty = true;
            }
            if (tiles[i].GetComponent<Tile>().pos == vector2 + new Vector2(-2, 0) && stickSkillLevel >= 3)
            {
                tiles[i].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
                tiles[i].GetComponent<Tile>().block.gameObject.SetActive(false);
                tiles[i].GetComponent<Tile>().blockColor = 255;
                tiles[i].GetComponent<Tile>().block = null;
                tiles[i].GetComponent<Tile>().isEmpty = true;
            }

        }
    }
    /// <summary>
    /// 잠자리채 아이템 사용효과
    /// </summary>
    /// <param name="target"></param>
    /// <param name="SwappingTarget"></param>
    /// <returns></returns>
    IEnumerator UseStickItem(Tile target, Tile SwappingTarget)
    {
        yield return new WaitForSeconds(0.40f);
        useItem = true;
        if (target.blockColor == 12&& SwappingTarget.blockColor == 12)
        {
            StartCoroutine(BoomItem(target.pos));
            StartCoroutine(BoomItem(SwappingTarget.pos));
        }
        else if (target.blockColor == 12 && SwappingTarget.block.GetComponent<Block>().blocktype == Block.BlockType.ITEM)
        {
           // ClearAllBlock();
        }
        else if (target.blockColor != 12 && SwappingTarget.blockColor == 12)
        {
            StartCoroutine(BoomItem(SwappingTarget.pos));
        }
        else if (target.blockColor == 12 && SwappingTarget.blockColor != 12)
        {
            StartCoroutine(BoomItem(target.pos));
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(PullDownBlock());
    }
    IEnumerator UseStickItem(Tile target)
    {
        yield return new WaitForSeconds(0.40f);
        useItem = true;
        StartCoroutine(BoomItem(target.pos));
        yield return new WaitForSeconds(1f);
        StartCoroutine(PullDownBlock());
    }
    /// <summary>
    /// 스프레이 아이템 사용 효과
    /// </summary>
    /// <param name="target"></param>
    /// <param name="SwappingTarget"></param>
    /// <returns></returns>
    IEnumerator UseSpray(Tile target,Tile SwappingTarget)
    {
        yield return new WaitForSeconds(0.40f);
        useItem = true;
        if (target.blockColor == 13 && SwappingTarget.block.GetComponent<Block>().blocktype == Block.BlockType.ITEM)
        {
           // ClearAllBlock();
        }
        else if (target.blockColor == 13 && SwappingTarget.blockColor != 13)
        {
            SprayItem(target, SwappingTarget);
        }
        else if (target.blockColor != 13 && SwappingTarget.blockColor == 13)
        {
            SprayItem(SwappingTarget, target);
        }
        yield return new WaitForSeconds(notDouble.Count*0.1f+0.3f);
        StartCoroutine(PullDownBlock());
    }
    void SprayItem(Tile target, Tile swaptarget)
    {
        targetTiles.Clear();
        targetBlock.Clear();
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].GetComponent<Tile>().blockColor == swaptarget.blockColor)
            {

                targetTiles.Add(tiles[i].GetComponent<Tile>());
                targetBlock.Add(tiles[i].GetComponent<Tile>().block);
            }
        }
        GameObject sprayEffect = BlockEffectOPManager.instance.SetObject(22);
        sprayEffect.gameObject.SetActive(false);
        sprayEffect.transform.position = swaptarget.transform.position;
        target.block.SetActive(false);
        swaptarget.block.SetActive(false);
        target.isEmpty = true;
        swaptarget.isEmpty = true;
        swaptarget.block.GetComponent<Block>().ScoreUp();
        #region 이펙트 색상 변경
        if (swaptarget.GetComponent<Tile>().blockColor == 0)
        {
            sprayEffect.GetComponent<SprayShot>().beamAddColor = Color.red;
        }
        else if (swaptarget.GetComponent<Tile>().blockColor == 1)
        {
            sprayEffect.GetComponent<SprayShot>().beamAddColor = Color.yellow;
        }
        else if (swaptarget.GetComponent<Tile>().blockColor == 2)
        {
            sprayEffect.GetComponent<SprayShot>().beamAddColor = Color.green;
        }
        else if (swaptarget.GetComponent<Tile>().blockColor == 3)
        {
            sprayEffect.GetComponent<SprayShot>().beamAddColor = Color.blue;
        }
        else if (swaptarget.GetComponent<Tile>().blockColor == 4)
        {
            sprayEffect.GetComponent<SprayShot>().beamAddColor = Color.magenta;
        }
        #endregion
        SprayDestroyBlock(spraySkillLevel, sprayEffect);
    }
    /// <summary>
    /// 스프레이 사용시 블록이 없어지는 함수
    /// </summary>
    /// <param name="spraySkillLevel"></param>
    /// <param name="sprayEffect"></param>
    void SprayDestroyBlock(int spraySkillLevel, GameObject sprayEffect)
    {
        if (spraySkillLevel <= 4)
        {
            if (targetTiles.Count >= spraySkillLevel+1)
            {
                RandomNumNotDouble(spraySkillLevel + 1);
            }
            else if(targetTiles.Count < spraySkillLevel + 1)
            {
                RandomNumNotDouble(targetTiles.Count);
            }
            foreach (var num in notDouble)
            {
                sprayEffect.GetComponent<SprayShot>().targets.Add(targetTiles[num].GetComponent<Tile>().transform);
                sprayEffect.SetActive(true);
                targetTiles[num].GetComponent<Tile>().isEmpty = true;
            }
        }
        else
        {
            
            if (targetTiles.Count >= 8)
            {
                RandomNumNotDouble(8);
            }
            else if (targetTiles.Count < 8)
            {
                RandomNumNotDouble(targetTiles.Count);
            }
            foreach (var num in notDouble)
            {
                sprayEffect.GetComponent<SprayShot>().targets.Add(targetTiles[num].GetComponent<Tile>().transform);
                sprayEffect.SetActive(true);
                targetTiles[num].GetComponent<Tile>().isEmpty = true;
            }
        }

        
    }
    /// <summary>
    /// 겹치는 숫자가 없이 숫자를 배치해주는 함수
    /// </summary>
    /// <param name="max"></param>
    void RandomNumNotDouble(int max)
    {
        notDouble.Clear();
        while (notDouble.Count < max)
        {
            int randomNum = Random.Range(0, targetTiles.Count);
            notDouble.Add(randomNum);
        }
    }
    /// <summary>
    /// 모든 블록을 없애는 함수
    /// </summary>
    void ClearAllBlock()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].GetComponent<Tile>().block.SetActive(false);
            tiles[i].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
            tiles[i].GetComponent<Tile>().isEmpty = true;
        }
    }
}
