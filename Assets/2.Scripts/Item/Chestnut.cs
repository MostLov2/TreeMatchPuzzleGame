using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chestnut : MonoBehaviour
{
    public static int   chestnutCount = 0;//밤의 전체량을 체크하는 변수
    [SerializeField]float               randomNumX;//X축에 주는 힘의 값
    [SerializeField] float               randomNumY;//Y축에 주는 힘의 값
    [SerializeField]float               randomNumZ;//Y축에 주는 힘의 값
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
    void RandomPowerInChestnut()//밤 생성시 위로 튀어 오르는 효과
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
    /// 밤이 채취 했을때 밤이 튀어나오는 갑 설정
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
