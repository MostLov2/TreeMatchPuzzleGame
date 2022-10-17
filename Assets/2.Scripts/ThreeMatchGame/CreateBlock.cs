using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CreateBlock : MonoBehaviour
{
    public GameObject[] createTile;
    public List<bool> line0 = new List<bool>();
    public List<bool> line1 = new List<bool>();
    public List<bool> line2 = new List<bool>();
    public List<bool> line3 = new List<bool>();
    public List<bool> line4 = new List<bool>();
    public List<bool> line5 = new List<bool>();
    public List<bool> line6 = new List<bool>();
    public List<bool> line7 = new List<bool>();
    public List<GameObject> line0G = new List<GameObject>();
    public List<GameObject> line1G = new List<GameObject>();
    public List<GameObject> line2G = new List<GameObject>();
    public List<GameObject> line3G = new List<GameObject>();
    public List<GameObject> line4G = new List<GameObject>();
    public List<GameObject> line5G = new List<GameObject>();
    public List<GameObject> line6G = new List<GameObject>();
    public List<GameObject> line7G = new List<GameObject>();
    public List<Transform> line0T = new List<Transform>();
    public List<Transform> line1T = new List<Transform>();
    public List<Transform> line2T = new List<Transform>();
    public List<Transform> line3T = new List<Transform>();
    public List<Transform> line4T = new List<Transform>();
    public List<Transform> line5T = new List<Transform>();
    public List<Transform> line6T = new List<Transform>();
    public List<Transform> line7T = new List<Transform>();
    public List<int> line0C = new List<int>();
    public List<int> line1C = new List<int>();
    public List<int> line2C = new List<int>();
    public List<int> line3C = new List<int>();
    public List<int> line4C = new List<int>();
    public List<int> line5C = new List<int>();
    public List<int> line6C = new List<int>();
    public List<int> line7C = new List<int>();

    public static CreateBlock instance;
    private void Awake()
    {
        instance = this;
        createTile = GameObject.FindGameObjectsWithTag("CreateTile");
        LineSetting();
        BlockToTile();
    }
    void LineSetting()
    {
        for (int i = 0; i < TileManager.instance.tileEmpty.Length; i++)
        {
            if (i % 8 == 0)
            {
                line7.Add(TileManager.instance.tileinScript[i].isEmpty);
                line7T.Add(TileManager.instance.tileinScript[i].transform);
                line7G.Add(TileManager.instance.tileinScript[i].block);
                line7C.Add(TileManager.instance.tileinScript[i].blockColor);
            }
            if (i % 8 == 1)
            {
                line6.Add(TileManager.instance.tileinScript[i].isEmpty);
                line6T.Add(TileManager.instance.tileinScript[i].transform);
                line6G.Add(TileManager.instance.tileinScript[i].block);
                line6C.Add(TileManager.instance.tileinScript[i].blockColor);
            }
            if (i % 8 == 2)
            {
                line5.Add(TileManager.instance.tileinScript[i].isEmpty);
                line5T.Add(TileManager.instance.tileinScript[i].transform);
                line5G.Add(TileManager.instance.tileinScript[i].block);
                line5C.Add(TileManager.instance.tileinScript[i].blockColor);
            }
            if (i % 8 == 3)
            {
                line4.Add(TileManager.instance.tileinScript[i].isEmpty);
                line4T.Add(TileManager.instance.tileinScript[i].transform);
                line4G.Add(TileManager.instance.tileinScript[i].block);
                line4C.Add(TileManager.instance.tileinScript[i].blockColor);
            }
            if (i % 8 == 4)
            {
                line3.Add(TileManager.instance.tileinScript[i].isEmpty);
                line3T.Add(TileManager.instance.tileinScript[i].transform);
                line3G.Add(TileManager.instance.tileinScript[i].block);
                line3C.Add(TileManager.instance.tileinScript[i].blockColor);
            }
            if (i % 8 == 5)
            {
                line2.Add(TileManager.instance.tileinScript[i].isEmpty);
                line2T.Add(TileManager.instance.tileinScript[i].transform);
                line2G.Add(TileManager.instance.tileinScript[i].block);
                line2C.Add(TileManager.instance.tileinScript[i].blockColor);
            }
            if (i % 8 == 6)
            {
                line1.Add(TileManager.instance.tileinScript[i].isEmpty);
                line1T.Add(TileManager.instance.tileinScript[i].transform);
                line1G.Add(TileManager.instance.tileinScript[i].block);
                line1C.Add(TileManager.instance.tileinScript[i].blockColor);
            }
            if (i % 8 == 7)
            {
                line0.Add(TileManager.instance.tileinScript[i].isEmpty);
                line0T.Add(TileManager.instance.tileinScript[i].transform);
                line0G.Add(TileManager.instance.tileinScript[i].block);
                line0C.Add(TileManager.instance.tileinScript[i].blockColor);
            }
        }
    }
    public void BlockToTile()
    {
        for (int i = 0; i < line0.Count; i++)
        {
            if (!line0[i])
            {
                int colorNum = RandomBlockSpwan();
                GameObject block = OPBlock.instance.SetObject(colorNum);
                line0C[i] = colorNum;
                line0G[i] = block;
                block.transform.position = createTile[0].transform.position;
                block.transform.DOMove(line0T[i].transform.position, 1f);
                line0[i] = true;
            }
        }
        for (int i = 0; i < line1.Count; i++)
        {
            if (!line1[i])
            {
                int colorNum = RandomBlockSpwan();
                GameObject block = OPBlock.instance.SetObject(colorNum);
                line1C[i] = colorNum;
                line1G[i] = block;
                block.transform.position = createTile[1].transform.position;
                block.transform.DOMove(line1T[i].transform.position, 1f);
                line1[i] = true;
            }
        }
        for (int i = 0; i < line2.Count; i++)
        {
            if (!line2[i])
            {
                int colorNum = RandomBlockSpwan();
                GameObject block = OPBlock.instance.SetObject(colorNum);
                line2C[i] = colorNum;
                line2G[i] = block;
                block.transform.position = createTile[2].transform.position;
                block.transform.DOMove(line2T[i].transform.position, 1f);
                line2[i] = true;
            }
        }
        for (int i = 0; i < line3.Count; i++)
        {
            if (!line3[i])
            {
                int colorNum = RandomBlockSpwan();
                GameObject block = OPBlock.instance.SetObject(colorNum);
                line3C[i] = colorNum;
                line3G[i] = block;
                block.transform.position = createTile[3].transform.position;
                block.transform.DOMove(line3T[i].transform.position, 1f);
                line3[i] = true;
            }
        }
        for (int i = 0; i < line4.Count; i++)
        {
            if (!line4[i])
            {
                int colorNum = RandomBlockSpwan();
                GameObject block = OPBlock.instance.SetObject(colorNum);
                line4C[i] = colorNum;
                line4G[i] = block;
                block.transform.position = createTile[4].transform.position;
                block.transform.DOMove(line4T[i].transform.position, 1f);
                line4[i] = true;
            }
        }
        for (int i = 0; i < line5.Count; i++)
        {
            if (!line5[i])
            {
                int colorNum = RandomBlockSpwan();
                GameObject block = OPBlock.instance.SetObject(colorNum);
                line5C[i] = colorNum;
                line5G[i] = block;
                block.transform.position = createTile[5].transform.position;
                block.transform.DOMove(line5T[i].transform.position, 1f);
                line5[i] = true;
            }
        }
        for (int i = 0; i < line6.Count; i++)
        {
            if (!line6[i])
            {
                int colorNum = RandomBlockSpwan();
                GameObject block = OPBlock.instance.SetObject(colorNum);
                line6C[i] = colorNum;
                line6G[i] = block;
                block.transform.position = createTile[6].transform.position;
                block.transform.DOMove(line6T[i].transform.position, 1f);
                line6[i] = true;
            }
        }
        for (int i = 0; i < line7.Count; i++)
        {
            if (!line7[i])
            {
                int colorNum = RandomBlockSpwan();
                GameObject block = OPBlock.instance.SetObject(colorNum);
                line6C[i] = colorNum;
                line7G[i] = block;
                block.transform.position = createTile[7].transform.position;
                block.transform.DOMove(line7T[i].transform.position, 1f);
                line7[i] = true;
            }
        }

    }
    int RandomBlockSpwan()
    {
        int randomBlockNum = Random.Range(0, 5);
        return randomBlockNum;
    }
}
