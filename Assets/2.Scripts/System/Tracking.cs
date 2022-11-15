using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracking : MonoBehaviour
{
    public Transform target;
    public Transform resetPosition;
    private void OnEnable()
    {
        transform.position = resetPosition.position;
    }
    private void Update()
    {
        transform.position = target.position;
    }
}
