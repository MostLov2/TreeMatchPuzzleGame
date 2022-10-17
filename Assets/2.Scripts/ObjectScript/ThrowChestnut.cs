using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowChestnut : MonoBehaviour
{
    public int speed;
    public void RabbitThrowChestnut()
    {
        transform.GetChild(0).GetComponent<Rigidbody2D>().AddForce(Vector2.right * Time.deltaTime * speed,ForceMode2D.Impulse);
    }
}
