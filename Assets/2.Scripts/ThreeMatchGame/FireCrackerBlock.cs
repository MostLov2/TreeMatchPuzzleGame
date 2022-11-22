using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FireCrackerBlock : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Image>().color = Color.white;
        StartCoroutine(FirecrackerEffectDelay());
    }
    IEnumerator FirecrackerEffectDelay()
    {
        yield return new WaitForSeconds(0.4f);
        if(GetComponent<Block>().blockColor == 15)
        {
            List<GameObject> block = new List<GameObject>();
            GetComponent<Image>().color = Color.clear;
            GameObject firecracker = BlockEffectOPManager.instance.SetObject(24);
            
            for (int i = 0; i < TileManager.instance.tiles.Length; i++)
            {
                if (TileManager.instance.tiles[i].GetComponent<Tile>().blockColor == 0)
                {
                    block.Add(TileManager.instance.tiles[i]);
                }
                if (TileManager.instance.tiles[i].GetComponent<Tile>().block == gameObject)
                {
                    TileManager.instance.tiles[i].GetComponent<Tile>().isEmpty = true;
                    firecracker.transform.position = TileManager.instance.tiles[i].transform.position;
                }
            }
            int randomBlock = Random.Range(0, block.Count);
            firecracker.GetComponent<TwoDLookAt>().target = block[randomBlock];
            yield return new WaitForSeconds(1f);

            TileManager.instance.isHaveDestroyBlock++;
        }
        else if (GetComponent<Block>().blockColor == 16)
        {
            List<GameObject> block = new List<GameObject>();
            GetComponent<Image>().color = Color.clear;
            GameObject firecracker = BlockEffectOPManager.instance.SetObject(25);
            for (int i = 0; i < TileManager.instance.tiles.Length; i++)
            {
                if (TileManager.instance.tiles[i].GetComponent<Tile>().blockColor == 1)
                {
                    block.Add(TileManager.instance.tiles[i]);
                }
                if (TileManager.instance.tiles[i].GetComponent<Tile>().block == gameObject)
                {
                    TileManager.instance.tiles[i].GetComponent<Tile>().isEmpty = true;
                    firecracker.transform.position = TileManager.instance.tiles[i].transform.position;
                }
            }
            int randomBlock = Random.Range(0, block.Count);
            firecracker.GetComponent<TwoDLookAt>().target = block[randomBlock];
            yield return new WaitForSeconds(1f);

            TileManager.instance.isHaveDestroyBlock++;
        }
    
        else if (GetComponent<Block>().blockColor == 17)
        {
            List<GameObject> block = new List<GameObject>();
            GetComponent<Image>().color = Color.clear;
            GameObject firecracker = BlockEffectOPManager.instance.SetObject(26);
            for (int i = 0; i < TileManager.instance.tiles.Length; i++)
            {
                if (TileManager.instance.tiles[i].GetComponent<Tile>().blockColor == 2)
                {
                    block.Add(TileManager.instance.tiles[i]);
                }
                if (TileManager.instance.tiles[i].GetComponent<Tile>().block == gameObject)
                {
                    TileManager.instance.tiles[i].GetComponent<Tile>().isEmpty = true;
                    firecracker.transform.position = TileManager.instance.tiles[i].transform.position;
                }
            }
            int randomBlock = Random.Range(0, block.Count);
            firecracker.GetComponent<TwoDLookAt>().target = block[randomBlock];
            yield return new WaitForSeconds(1f);

            TileManager.instance.isHaveDestroyBlock++;
        }
        else if (GetComponent<Block>().blockColor == 18)
        {
            List<GameObject> block = new List<GameObject>();
            GetComponent<Image>().color = Color.clear;
            GameObject firecracker = BlockEffectOPManager.instance.SetObject(27);
            for (int i = 0; i < TileManager.instance.tiles.Length; i++)
            {
                if (TileManager.instance.tiles[i].GetComponent<Tile>().blockColor == 3)
                {
                    block.Add(TileManager.instance.tiles[i]);
                }
                if (TileManager.instance.tiles[i].GetComponent<Tile>().block == gameObject)
                {
                    TileManager.instance.tiles[i].GetComponent<Tile>().isEmpty = true;
                    firecracker.transform.position = TileManager.instance.tiles[i].transform.position;
                }
            }
            int randomBlock = Random.Range(0, block.Count);
            firecracker.GetComponent<TwoDLookAt>().target = block[randomBlock];
            yield return new WaitForSeconds(1f);
 
            TileManager.instance.isHaveDestroyBlock++;
        }
        else if (GetComponent<Block>().blockColor == 19)
        {
            List<GameObject> block = new List<GameObject>();
            GetComponent<Image>().color = Color.clear;
            GameObject firecracker = BlockEffectOPManager.instance.SetObject(28);
            for (int i = 0; i < TileManager.instance.tiles.Length; i++)
            {
                if (TileManager.instance.tiles[i].GetComponent<Tile>().blockColor == 4)
                {
                    block.Add(TileManager.instance.tiles[i]);
                }
                if (TileManager.instance.tiles[i].GetComponent<Tile>().block == gameObject)
                {
                    TileManager.instance.tiles[i].GetComponent<Tile>().isEmpty = true;
                    firecracker.transform.position = TileManager.instance.tiles[i].transform.position;
                }
            }
            int randomBlock = Random.Range(0, block.Count);
            firecracker.GetComponent<TwoDLookAt>().target = block[randomBlock];
            yield return new WaitForSeconds(1f);

            TileManager.instance.isHaveDestroyBlock++;
        }
        gameObject.SetActive(false);
    }
}

