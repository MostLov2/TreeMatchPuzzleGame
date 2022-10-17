using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySquirrel : MonoBehaviour
{
    [SerializeField]Transform[] MoveLPoint;
    [SerializeField]Transform[] MoveRPoint;
    [SerializeField]List<Transform> MoveTPoint = new List<Transform>();
    [SerializeField]int RandomSpawnPoint;
    [SerializeField]
    int i = 0;
    [SerializeField]
    int RandomDropItem;
    private void Awake()
    {
        MoveLPoint = GameObject.FindGameObjectWithTag("MoveLPoint").GetComponentsInChildren<Transform>();
        MoveRPoint = GameObject.FindGameObjectWithTag("MoveRPoint").GetComponentsInChildren<Transform>();
    }
    private void OnEnable()
    {
        i = 0;
        MoveTPoint.Clear();
        RandomSpawnPoint = Random.Range(0, 2);
        RandomDropItem = Random.Range(0, 4);
        if(RandomSpawnPoint == 1||RandomSpawnPoint == 2)
        {
            for (int x = 1; x < 4; x += 2)
            {
                MoveTPoint.Add(MoveLPoint[x]);
                MoveTPoint.Add(MoveRPoint[x]);
                MoveTPoint.Add(MoveRPoint[x + 1]);
                MoveTPoint.Add(MoveLPoint[x + 1]);
            }
            transform.position = MoveLPoint[1].position;
        }
        else
        {
            for (int x = 4; x > 0; x -= 2)
            {
                MoveTPoint.Add(MoveRPoint[x]);
                MoveTPoint.Add(MoveLPoint[x]);
                MoveTPoint.Add(MoveLPoint[x - 1]);
                MoveTPoint.Add(MoveRPoint[x - 1]);
            }
            transform.position = MoveRPoint[4].position;
        }
    }
    private void Update()
    {
        if (RandomSpawnPoint == 1 || RandomSpawnPoint == 2)
        {
            transform.position = Vector3.MoveTowards(transform.position, MoveTPoint[i].position, Time.deltaTime * 1);
            if (transform.position.x> MoveTPoint[i].position.x)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, MoveTPoint[i].position, Time.deltaTime * 1);
            float dist = Vector3.Distance(transform.position, MoveTPoint[i].position);
            if (transform.position.x > MoveTPoint[i].position.x)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Point"))
        {
            ChangePoint();
        }
    }
    void ChangePoint()
    {
        i++;
        if (i > 7)
        {
            i = 1;
            gameObject.SetActive(false);
        }
    }
    public void CatchSquirrel()
    {
        if (RandomDropItem == 0)
        {
            MySqlSystem.fertilizerPoint += 1;
            StartCoroutine(MySqlSystem.instance.SetFertilizer(MySqlSystem.fertilizerPoint));
            gameObject.SetActive(false);
        }
        else if (RandomDropItem == 1)
        {
            MySqlSystem.chestnutPoint += 1;
            StartCoroutine(MySqlSystem.instance.SetFertilizer(MySqlSystem.chestnutPoint));
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
        }
        GetComponent<Animator>().SetTrigger("IsDead");
    }
}
