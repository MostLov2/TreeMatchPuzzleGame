using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public void GotHit(int inithp, int hp, SpriteRenderer spriteRenderer, Collider2D collision,Animator animator)
    {
        animator.SetTrigger("IsHit");
        if (GetComponent<Squirrel>() != null || GetComponent<ChestBugs>() != null)
        {
            if(GetComponent<Squirrel>() != null)
            {
                Push(collision);

            }
            ColorChange(inithp, hp, spriteRenderer);

        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }
    void ColorChange(int inithp, int hp, SpriteRenderer spriteRenderer)
    {
        if (!transform.GetChild(0).gameObject.activeSelf)
        {
            if (hp <= (inithp / 5))
            {
                spriteRenderer.color = Color.red;
            }
            else if (hp < ((inithp / 5) * 2)) 
            {
                spriteRenderer.color = new Color(255 / 255f, 85 / 255f, 0 / 255f);
            }
            else if (hp < ((inithp / 5) * 3))
            {
                spriteRenderer.color = new Color(255 / 255f, 170 / 255f, 0 / 255f);
            }
            else if (hp < ((inithp / 5) * 4))
            {
                spriteRenderer.color = new Color(255 / 255f, 255 / 255f, 0 / 255f);
            }
            else if (hp <= inithp)
            {
                spriteRenderer.color = Color.white;
            }
        }
    }
    void Push(Collider2D collision)
    {
        float rigCalculatorX = 0;
        float rigCalculatorY = 0;
        rigCalculatorX = transform.position.x - collision.transform.position.x;
        rigCalculatorY = transform.position.y - collision.transform.position.y;
        int forcePower = 35;
        if (rigCalculatorX < 0)//왼쪽
        {
            Vector2 force = new Vector2(rigCalculatorX * forcePower, 0);
            GetComponent<Rigidbody2D>().AddForce(force * Time.deltaTime, ForceMode2D.Impulse);
        }
        else if (rigCalculatorX >= 0)//오른쪽
        {
            Vector2 force = new Vector2(rigCalculatorX * forcePower, 0);
            GetComponent<Rigidbody2D>().AddForce(force * Time.deltaTime, ForceMode2D.Impulse);
        }
        if (rigCalculatorY < 0)//아래
        {
            Vector2 force = new Vector2(0, rigCalculatorY * forcePower);
            GetComponent<Rigidbody2D>().AddForce(force * Time.deltaTime, ForceMode2D.Impulse);
        }
        else if (rigCalculatorY >= 0)//위
        {
            Vector2 force = new Vector2(0, rigCalculatorY * forcePower);
            GetComponent<Rigidbody2D>().AddForce(force * Time.deltaTime, ForceMode2D.Impulse);
        }
    }
}
