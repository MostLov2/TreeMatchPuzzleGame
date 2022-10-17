using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chestnut : MonoBehaviour
{
    public static int   chestnutCount = 0;//���� ��ü���� üũ�ϴ� ����
    [SerializeField]float               randomNumX;//X�࿡ �ִ� ���� ��
    [SerializeField] float               randomNumY;//Y�࿡ �ִ� ���� ��
    [SerializeField]float               randomNumZ;//Y�࿡ �ִ� ���� ��
    int randomSpin = 0;
    private void Awake()
    {
        chestnutCount++;
        StartCoroutine(ChangeGravity());
        RandomPowerInChestnut();
        ForceChestnut();
    }
    private void OnEnable()
    {
        chestnutCount++;
        StartCoroutine(ChangeGravity());
        RandomPowerInChestnut();
        ForceChestnut();
        Destroy();
        StartCoroutine(Upcol());
        transform.localScale = Vector3.one;
        randomSpin = Random.Range(0, 2);
    }
    IEnumerator Upcol()
    {
        while (true)
        {
            if (randomSpin == 0)
            {
                transform.Rotate(Vector3.forward*Time.deltaTime*200);
            }
            else
            {
                transform.Rotate(Vector3.back*Time.deltaTime*200);
            }
            yield return new WaitForSeconds(Time.deltaTime);
            if (MiniGameManager.isMiniGameStart)
            {
                UpScale();
            }
        }
    }
    void UpScale()
    {
        transform.localScale += new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
    }
    private void OnDisable()
    {
        chestnutCount--;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }
        if (collision.CompareTag("CatchPoint"))
        {
            gameObject.SetActive(false);
        }
    }
    void RandomPowerInChestnut()//�� ������ ���� Ƣ�� ������ ȿ��
    {
        if (MiniGameManager.isMiniGameStart)
        {
            randomNumX = Random.Range(-5000, 5000);
            randomNumY = Random.Range(5000, 10000);
        }
        else
        {
            randomNumX = Random.Range(-500, 500);
            randomNumY = Random.Range(500, 1000);
        }
        randomNumZ = Random.Range(0, 1000);
    }
    /// <summary>
    /// ���� ä�� ������ ���� Ƣ����� �� ����
    /// </summary>
    void ForceChestnut()
    {
        Vector2 RandomPower = new Vector2(randomNumX, randomNumY);
        GetComponent<Rigidbody2D>().AddForce(RandomPower * Time.deltaTime, ForceMode2D.Impulse);
    }
    IEnumerator ChangeGravity()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0;
        yield return new WaitForSeconds(0.5f);
        GetComponent<Rigidbody2D>().gravityScale = 3;
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);

    }
}
