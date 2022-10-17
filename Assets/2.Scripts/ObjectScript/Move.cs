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
        GetComponent<Rigidbody2D>().isKinematic = false;//물리를 다시 설정
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
    private void OnTriggerEnter2D(Collider2D collision)//몬스터와 충동시 함수 한번실행 
    {
        if (collision.CompareTag("ChestNutSpawnPoint"))//지정된 램덤 포인트 콜라이더 접촉시 함수 실행
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
    void RandomNumMaker(GameObject[] MovePoint)//몬스터 상태에 따라 랜덤값 생성
    {
        randomPointNum = Random.Range(0, MovePoint.Length);//0에서 적 이동 포인트.length사이에 수 램덤 생성
        if (saveNum == 0)
        {
            saveNum = randomPointNum;
        }
        else
        {
            if (saveNum == randomPointNum)//같은 숫자가 나왔을때 다시 초기화{
            {
                RandomNumMaker(MovePoint);
            }
            else if (saveNum != randomPointNum)//같은 숫자가 나왔을때 다시 초기화
            {
                saveNum = randomPointNum;
            }
        }
    }
    
}
