using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparrowFly : MonoBehaviour
{
    [SerializeField]
    GameObject[]    right;//�����ʿ� �ִ� ����Ʈ 
    [SerializeField]
    GameObject[]    left;//���ʿ� �ִ� ����Ʈ��
    [SerializeField]
    int             randomNum;//���� �� ���� ����
    private void Awake()
    {
        right = GameObject.FindGameObjectsWithTag("SparrowPointRight");
        left = GameObject.FindGameObjectsWithTag("SparrowPointLeft");
    }
    private void OnEnable()
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
        if (SparrowItem.randomPointSpawnNum > 1)
        {
            randomNum = Random.Range(0, right.Length);//
        }
        if (SparrowItem.randomPointSpawnNum <= 1)
        {
            randomNum = Random.Range(0, left.Length);
        }
        StartCoroutine(Upcol());
        StartCoroutine(Gone());
    }
    private void OnDisable()
    {
        GetComponent<Rigidbody2D>().isKinematic = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SparrowPointRight")&& SparrowItem.randomPointSpawnNum > 1)
        {
            gameObject.SetActive(false);
        }
        if (collision.CompareTag("SparrowPointLeft")&& SparrowItem.randomPointSpawnNum <= 1)
        {
            gameObject.SetActive(false);
        }
    }
    IEnumerator Gone()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
    IEnumerator Upcol()
    {
        while (!SpawnManager.isGameover)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            Move();
        }
    }
    void Move()//������ �Լ�
    {
        if (SparrowItem.randomPointSpawnNum > 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, right[randomNum].transform.position, Time.deltaTime*(50 - (GameLogicManager.birdMovementSpeed*2)));
            if (right[randomNum].transform.localPosition.y - transform.localPosition.y >= 0)
            {
                GetComponent<Animator>().SetBool("IsFly", true);
            }
            if (right[randomNum].transform.localPosition.y - transform.localPosition.y < 0)
            {
                GetComponent<Animator>().SetBool("IsFly", false);
            }
        }
        if (SparrowItem.randomPointSpawnNum <= 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, left[randomNum].transform.position, Time.deltaTime * 50 - (GameLogicManager.birdMovementSpeed * 2));
            if (left[randomNum].transform.localPosition.y - transform.localPosition.y >= 0)
            {
                GetComponent<Animator>().SetBool("IsFly", true);
            }
            if (left[randomNum].transform.localPosition.y - transform.localPosition.y < 0)
            {
                GetComponent<Animator>().SetBool("IsFly", false);
            }
        }
    }
}
