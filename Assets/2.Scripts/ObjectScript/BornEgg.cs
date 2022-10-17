using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BornEgg : MonoBehaviour
{
    public IEnumerator IsBornEgg(GameObject parentChestnutBur, Vector3 point)
    {
        if (!MiniGameManager.isMiniGameStart && Egg.eggCount < 10)//�̴ϰ����� �ƴϰų� eggCount�� 10�� ������ ��� 
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
            if (!parentChestnutBur.GetComponent<ChestnutBur>().isBornEggHere)
            {
                GameObject egg = OPManager.instance.SetObject("Egg");//�˼�ȯ
                egg.transform.position = point;//��ġ�� ����� ������ ���� ��ġ
            }
            yield return new WaitForSeconds(3f);
            GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }
}
