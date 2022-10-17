using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Animals animals;
    public int randomPointNum = 0;
    int saveNum = 0;
    private void Awake()
    {
        animals = GetComponent<Animals>(); 
    }
    public void Movement(float speed, Animator animator, GameObject[] MovePoint)
    {
        animator.SetBool("IsMove", true);
        GetComponent<Rigidbody2D>().isKinematic = false;//������ �ٽ� ����
        if (MovePoint[randomPointNum] != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, MovePoint[randomPointNum].transform.position, Time.deltaTime * speed);

        }
        else
        {
            RandomNumMaker(MovePoint);
            transform.position = Vector3.MoveTowards(transform.position, MovePoint[randomPointNum].transform.position, Time.deltaTime * speed);
        }
        if (transform.localPosition.x > MovePoint[randomPointNum].transform.localPosition.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (transform.localPosition.x < MovePoint[randomPointNum].transform.localPosition.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)//���Ϳ� �浿�� �Լ� �ѹ����� 
    {
        if (collision.CompareTag("ChestNutSpawnPoint"))//������ ���� ����Ʈ �ݶ��̴� ���˽� �Լ� ����
        {
            if (GetComponent<Squirrel>() != null || GetComponent<ChestBugs>() != null)
            {
                if (!transform.GetChild(0).gameObject.activeSelf)
                {
                    if (collision.gameObject == animals.MovePoint[randomPointNum].gameObject)
                    {
                        RandomNumMaker(animals.MovePoint);
                        StartCoroutine(ColOnOff());
                    }
                }
            }
            else
            {
                if (collision.gameObject == animals.MovePoint[randomPointNum].gameObject)
                {
                    RandomNumMaker(animals.MovePoint);
                    StartCoroutine(ColOnOff());
                }
            }
        }
    }
    IEnumerator ColOnOff()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        GetComponent<CircleCollider2D>().enabled = true;
    }
    void RandomNumMaker(GameObject[] MovePoint)//���� ���¿� ���� ������ ����
    {
        randomPointNum = Random.Range(0, MovePoint.Length);//0���� �� �̵� ����Ʈ.length���̿� �� ���� ����
        if (saveNum == 0)
        {
            saveNum = randomPointNum;
        }
        else
        {
            if (saveNum == randomPointNum)//���� ���ڰ� �������� �ٽ� �ʱ�ȭ{
            {
                RandomNumMaker(MovePoint);
            }
            else if (saveNum != randomPointNum)//���� ���ڰ� �������� �ٽ� �ʱ�ȭ
            {
                saveNum = randomPointNum;
            }
        }
    }
    
}
