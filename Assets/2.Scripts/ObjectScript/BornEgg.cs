using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BornEgg : MonoBehaviour
{
    public IEnumerator IsBornEgg(GameObject parentChestnutBur, Vector3 point)
    {
        if (!MiniGameManager.isMiniGameStart && Egg.eggCount < 10)//미니게임이 아니거나 eggCount가 10개 이하일 경우 
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
            if (!parentChestnutBur.GetComponent<ChestnutBur>().isBornEggHere)
            {
                GameObject egg = OPManager.instance.SetObject("Egg");//알소환
                egg.transform.position = point;//위치는 저장된 접촉한 밤의 위치
            }
            yield return new WaitForSeconds(3f);
            GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }
}
