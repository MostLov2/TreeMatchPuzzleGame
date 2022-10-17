using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : MonoBehaviour
{
    public void IsIdle(Animator animator)
    {
        animator.SetBool("IsMove", false);
    }
}
