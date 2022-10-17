using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveChestnut : MonoBehaviour
{
    int RadomRL;
    public void OnEnable()
    {
        RadomRL = Random.Range(0, 2);
    }
    public void IsHaveChestnut(Animator animator)
    { 
        ChsetNutOnOff(animator);
        StartCoroutine(MoveUpDown());
    }
    void ChsetNutOnOff(Animator animator)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        if (RadomRL == 0)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * Time.deltaTime * 10, ForceMode2D.Impulse);//밤을 주웠을때 오른쪽으로 도망나감 다람쥐만 해당
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (RadomRL == 1)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * Time.deltaTime * 10, ForceMode2D.Impulse);//밤을 주웠을때 오른쪽으로 도망나감 다람쥐만 해당
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (RadomRL == 0)
        {
            animator.SetBool("IsChestRightOn", true);
            animator.SetBool("IsChestLeftOn", false);
        }
        else if (RadomRL == 1)
        {
            animator.SetBool("IsChestLeftOn", true);
            animator.SetBool("IsChestRightOn", false);
        }
    }
    IEnumerator MoveUpDown()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * Time.deltaTime * 10, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.7f);
        GetComponent<Rigidbody2D>().AddForce(Vector2.down * Time.deltaTime * 30, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.7f);
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * Time.deltaTime * 50, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.7f);
        GetComponent<Rigidbody2D>().AddForce(Vector2.down * Time.deltaTime * 70, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.7f);
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * Time.deltaTime * 90, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.7f);
        GetComponent<Rigidbody2D>().AddForce(Vector2.down * Time.deltaTime * 110, ForceMode2D.Impulse);
    }
}
