using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwappingBlock : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    Vector2 mousePos;
    RaycastHit2D hit;
    public AudioClip[] clip;
    public void OnPointerDown(PointerEventData eventData)
    {
        SwapTarget();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log(TileManager.instance.targetTile.GetComponent<Tile>().block.GetComponent<Block>().blocktype);
        if (TileManager.instance.targetTile.GetComponent<Tile>().block.GetComponent<Block>().blocktype ==Block.BlockType.CHESTNUTBLOCK|| TileManager.instance.targetTile.GetComponent<Tile>().block.GetComponent<Block>().blocktype == Block.BlockType.ITEM|| TileManager.instance.targetTile.GetComponent<Tile>().block != null)
        {
               // Debug.Log("Didn't0");
            if (!TileManager.instance.isSwapping)
            {
                mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                TileManager.instance.MousePos = mousePos;
                //Debug.Log("Didn't");
                TileManager.instance.SwappingBlock();
            }
        }
        
    }

    public void SwapTarget()
    {
        TileManager.instance.targetTile = this.gameObject;
    }
}
