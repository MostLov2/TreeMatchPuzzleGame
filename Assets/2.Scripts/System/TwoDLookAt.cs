using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDLookAt : MonoBehaviour
{
    public GameObject target;
    [SerializeField] GameObject explosion;
    [SerializeField] float speed = 2f, rotspeed = 2f;
    Quaternion rotTarget;
    Vector3 dir;
    Rigidbody2D rb;
    public float delayTime = 5;
    public AudioClip[] clip;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        if (CountDownInPuzzle.isGameStart)
        {
            SoundManager.instance.PlaySFX(clip, 0, 1, 1);
            transform.rotation = Quaternion.Euler(Vector3.zero);
            delayTime = 5;
        }
    }
    void Update()
    {
        if (target != null)
        {
            delayTime -= Time.deltaTime;
            if (delayTime >= 4)
            {
                FireFirecracker();
            }

            else if (delayTime >= 1)
            {
                GuideTarget();
            }
            else if(delayTime >= 0)
            {
                delayTime = 5;
            }
        }
    }
    void FireFirecracker()
    {
        rb.isKinematic = true;
        rb.isKinematic = false;
        float randomAngle = Random.Range(0, 1);
        rb.AddForce(new Vector2(randomAngle,1)*5,ForceMode2D.Force);
        
    }
    public void GuideTarget()
    {
        rb.isKinematic = true;
        rb.isKinematic = false;
        dir = (target.GetComponent<Tile>().block.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.x, dir.y)*Mathf.Rad2Deg;
        rotTarget = Quaternion.AngleAxis(angle, Vector3.back);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotTarget, Time.deltaTime * rotspeed);
        rb.velocity = new Vector2(dir.x*speed, dir.y*speed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Tile")&& target.GetComponent<Tile>().pos == collision.GetComponent<Tile>().pos)
        {
            gameObject.SetActive(false);
            SoundManager.instance.PlaySFX(clip, 1, 1, 1);
            collision.GetComponent<Tile>().block.GetComponent<Block>().ScoreUp();
            collision.GetComponent<Tile>().block.SetActive(false);
            collision.GetComponent<Tile>().isEmpty = true;
           
        }
    }
}
