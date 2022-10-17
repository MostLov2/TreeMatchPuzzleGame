using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunEffect : MonoBehaviour
{
    public int SwapCount = 0;
    public Block block;
    private void OnEnable()
    {
        SwapCount = 0;
    }
}
