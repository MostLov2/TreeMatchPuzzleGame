using System.Collections;
using UnityEngine;

public class BirdFly : MonoBehaviour
{
    Transform trBird, trGoal, trBurst, trTrail; // 새, 목표지점, 효과
    Vector2 hStartPos, hEndPos; // 시작위치, 끝위치 (좌우 방향)
    Vector2 vStartPos, vEndPos; // 시작위치, 끝위치 (상하 방향)
    
    [SerializeField] float lerpTime = 0.1f; // 효과 지속시간 (길면 느리게 날아감)
    // 실행중에는 시간에 100이 곱해지기 때문에 켜놓고 조절하려면 그에 맞게 값을 넣어주면서 하면됨
    public float LerpTime { get => lerpTime; set => lerpTime = value * 100; }
    float currentTime = 0;
    [SerializeField] AnimationCurve lerpCurve;  // 이동 속도 커브
    [HideInInspector ]
    public bool isVerticalDirection = false;  // 체크하면 위로 날아간다.
    [SerializeField] int tileNum;   // 숫자가 올라가면 한칸씩 아래(또는 오른쪽)에서 시작한다.

    void Awake()
    {
        trBird = transform.GetChild(0);
        trTrail = trBird.GetChild(0);
        trGoal = transform.GetChild(1);
        hStartPos = transform.GetChild(2).position;
        hEndPos = transform.GetChild(3).position;
        vStartPos = transform.GetChild(4).position;
        vEndPos = transform.GetChild(5).position;
        trBurst = transform.GetChild(6);
        
        LerpTime = lerpTime;
    }
    
    void OnEnable()
    {
       /* currentTime = 0;

        if (isVerticalDirection)
        {
            trBird.position = (Vector2) vStartPos + Vector2.right * tileNum * 120f / 192f;
            trGoal.position = (Vector2) vEndPos + Vector2.right * tileNum * 120f / 192f;
            trBurst.position = (Vector2)trBird.position + Vector2.up * 1.4f;
            trTrail.rotation = Quaternion.Euler(0, 0, 90f);
        }
        else
        {
            trBird.position = (Vector2) hStartPos + Vector2.down * tileNum * 120f / 192f;
            trGoal.position = (Vector2) hEndPos + Vector2.down * tileNum * 120f / 192f;
            trBurst.position = (Vector2)trBird.position + Vector2.right * 1.6f;
            trTrail.rotation = Quaternion.identity;
        }
        StartCoroutine(BirdOut());*/
    }
    
    /*void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= lerpTime) currentTime = lerpTime;
        trBird.position = Vector3.Lerp(trBird.position, trGoal.position, lerpCurve.Evaluate(currentTime / lerpTime));
    }
    
    private IEnumerator BirdOut()
    {
        yield return new WaitForSeconds(LerpTime * 0.01f + 0.3f);
        gameObject.SetActive(false);
    }*/
}