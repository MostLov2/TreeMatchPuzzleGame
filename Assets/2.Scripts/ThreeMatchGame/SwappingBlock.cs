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
    /// <summary>
    /// 포인터 클릭시 타켓 블록과 위치값을 저장하는 함수 
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        if (TileManager.instance.targetTile.GetComponent<Tile>().block.GetComponent<Block>().blocktype ==Block.BlockType.CHESTNUTBLOCK|| TileManager.instance.targetTile.GetComponent<Tile>().block.GetComponent<Block>().blocktype == Block.BlockType.ITEM|| TileManager.instance.targetTile.GetComponent<Tile>().block != null)
        {
            if (TileManager.instance.targetTile.GetComponent<Tile>().block.GetComponent<Block>().blocktype != Block.BlockType.MONSTER)
            {
                if (!TileManager.instance.isSwapping)
                {
                    mousePos = Input.mousePosition;
                    mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                    TileManager.instance.MousePos = mousePos;
                    TileManager.instance.SwappingBlock();
                }
            }
        }
        
    }

    public void SwapTarget()
    {
        TileManager.instance.targetTile = this.gameObject;
    }
}
