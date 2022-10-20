using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayShot : MonoBehaviour
{
    [SerializeField] private GameObject beamStart;  // 빔 시작부분 (시작점은 여러번 플레이하지 않고 계속 재생)
    [SerializeField] private GameObject beamStreak; // 빔 중간부분
    [SerializeField] private GameObject beamEnd;    // 빔 끝부분 (터지는 효과)
    
    [Header("Beam Color")]
    private Renderer[] beamRenderers;   // 빔 재질 관리용
    public  Color beamAddColor = Color.black;

    [Header("Target Transform Array")]
    //public Transform[] targets;   // 빔이 향할 타겟들을 배열로 받음
    public List<Transform> targets = new List<Transform>();
    private ParticleSystem beamPs, beamEndPs;   // 빔 중간부분, 끝부분 컴포넌트
    private ParticleSystemRenderer beamPsr; // 빔 중간부분은 길이조절을 위해 렌더러도 같이 참조
    
    private void Awake()
    {
        beamPs = beamStreak.GetComponent<ParticleSystem>();
        beamPsr = beamStreak.GetComponent<ParticleSystemRenderer>();
        beamEndPs = beamEnd.GetComponent<ParticleSystem>();
        beamRenderers = GetComponentsInChildren<Renderer>();
    }

    void OnEnable() // 활성화되면 빔을 자동으로 발사한다.
    {
        if (CountDownInPuzzle.isGameStart)
        {

            beamStart.transform.position = transform.position;
            // 필요하면 쏘기전 광선 색을 변경할 수 있다. (beamColor = Color.green;)
            // Emission Color를 씌우는거라 색이 그대로 나오진 않음 (검은색으로 하면 기본 파란색이 나옴)
            foreach (var br in beamRenderers)
            {
                br.material.EnableKeyword("_EMISSION");
                br.material.SetColor("_EmissionColor", beamAddColor);
            }

            StartCoroutine(BeamSpraying()); // 배열의 목표들마다 연속해서 쏘도록 코루틴 실행
        }
    }

    IEnumerator BeamSpraying()
    {
        for(int i=0; i<targets.Count; i++)
        {
            var beamLength = Vector2.Distance(beamStart.transform.position, targets[i].position);
            beamPsr.lengthScale = beamLength * 0.4f;    // 빔 길이가 목표에 맞도록 조절
            beamStreak.transform.LookAt(targets[i]);    // 빔이 목표를 향하도록 회전
            beamPs.Play();  // 루프마다 한번씩 발사
        
            beamEnd.transform.position = targets[i].position;
            beamEndPs.Play();   // 루프마다 한번씩 실행

            // 타겟이 맞았을 때 처리하는곳
            targets[i].GetComponent<Tile>().block.GetComponent<Block>().ScoreUp(); // 맞은 타겟을 지운다.
            targets[i].GetComponent<Tile>().block.gameObject.SetActive(false); // 맞은 타겟을 지운다.
            targets[i].GetComponent<Tile>().block = null; // 맞은 타겟을 지운다.
            
            yield return new WaitForSeconds(0.1f);  // 다음 발사까지 시간
        }
        targets.Clear();
        gameObject.SetActive(false);
    }
    
}
