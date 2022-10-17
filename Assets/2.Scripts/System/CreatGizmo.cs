using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatGizmo : MonoBehaviour
{
    public float radiusSphere;
    public Color colorSphere;
    private void OnDrawGizmos()
    {
        Gizmos.color = colorSphere;
        Gizmos.DrawSphere(transform.position, radiusSphere); 
    }
}
