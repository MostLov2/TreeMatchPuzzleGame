using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSquirrelSpawn : MonoBehaviour
{
    [SerializeField]
    Transform Squirrel;
    int radomSpawnTime;
    [SerializeField]
    float waitTime = 0;
    void Awake()
    {
        radomSpawnTime = Random.Range(180, 300);
        Squirrel = transform.GetChild(0).transform;
    }

    
    void Update()
    {
        waitTime += Time.deltaTime;
        if(radomSpawnTime < waitTime)
        {
            Squirrel.gameObject.SetActive(true);
            radomSpawnTime = Random.Range(180, 300);
            waitTime = 0;
        }
    }
}
