using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public int spin = 0;
    public float speed;
    void Update()
    {
        if(spin == 0)
        {
            transform.Rotate(Vector3.up*Time.deltaTime*speed);
        }
        else
        {
            transform.Rotate(Vector3.down*Time.deltaTime*speed);
        }
    }
}
