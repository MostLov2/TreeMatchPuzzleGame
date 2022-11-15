using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningY : MonoBehaviour
{
    public float rotSpeed = 100f;
    private void Update()
    {
        transform.Rotate(new Vector3(0, rotSpeed * Time.deltaTime, 0));
    }
}
