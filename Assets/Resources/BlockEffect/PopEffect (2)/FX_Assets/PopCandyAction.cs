using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PopCandyAction : MonoBehaviour
{
    [SerializeField] AnimationCurve showCurve;  // 봉투가 나타나는 움직임 커브
    [SerializeField] float effectTime = 5f; // 전체 효과는 몇 초동안 보일것인가
    Tweener floatTweener, textTweener;    // 봉투, 글자 움직임 관련 트위너
    Sequence showSequence, hideSequence;    // 가운데 등장, 하단 이동 시퀀스

    ParticleSystem[] particles;             // 파티클 제어를 위한 배열
    [Header("Particle System Control - for Performance")]
    [SerializeField] int particleFallNum = 10; // 파티클이 떨어지는 개수 (초당)
    [SerializeField] int particleRiseNum = 30;  // 파티클이 위로 날아가는 개수 (초당)

    [SerializeField] bool trailOnOff = true;
    [SerializeField] bool glowOnOff = true;
    [SerializeField] bool dustOnOff = true;
    [SerializeField] bool decoOnOff = true;

    [SerializeField] private Sprite[] sprCandyBag;
    private SpriteRenderer sprBagRenderer;

    [SerializeField] private GameObject goDispText;
    [SerializeField] private float beginTextShowTime = 1.2f;
    
    void Awake()
    {
        var trEffect = transform.GetChild(0);   // Particle Group
        var trPopTxt = transform.GetChild(1);   // Text sprite
        
        particles = new ParticleSystem[2];
        particles[0] = trEffect.GetChild(0).GetComponent<ParticleSystem>();
        particles[1] = trEffect.GetChild(1).GetComponent<ParticleSystem>();
        
        sprBagRenderer = trEffect.GetChild(2).GetComponent<SpriteRenderer>();

        // Particle System Control
        var particleFallTrail = particles[0].trails;
        particleFallTrail.enabled = trailOnOff;
        var particleRiseTrail = particles[1].trails;
        particleRiseTrail.enabled = trailOnOff;

        var trHalo = trEffect.GetChild(5);
        trHalo.GetChild(0).gameObject.SetActive(dustOnOff);
        trHalo.GetChild(1).gameObject.SetActive(dustOnOff);
        trPopTxt.GetChild(0).gameObject.SetActive(decoOnOff);
        
        var particleFallModule = particles[0].emission;
        particleFallModule.rateOverTime = particleFallNum;
        var particleRiseModule = particles[1].emission;
        particleRiseModule.rateOverTime = particleRiseNum;
        
        floatTweener = trEffect.DOLocalMoveY(.5f, 1.5f)
            .SetRelative().SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);

        textTweener = trPopTxt.DOLocalMoveY(30f, 50f)
            .SetRelative().SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);

        showSequence = DOTween.Sequence().SetAutoKill(false).Pause()
            .AppendCallback(() => floatTweener.Play())
            .AppendCallback(() => textTweener.Play())
            .Join(trEffect.DOScale(0, .5f).From().SetEase(showCurve));

        hideSequence = DOTween.Sequence().SetAutoKill(false).Pause()
            .Join(trEffect.DOMoveY(-4.8f, .5f).SetEase(Ease.InBack, 1.6f))
            .Join(trEffect.DOScale(1.6f, .5f).SetEase(Ease.InBack, 1.6f))
            .AppendCallback(() => floatTweener.Pause())
            .OnComplete(() =>
            {
                for (int i = 0; i < particles.Length; i++) particles[i].Play();
            });

        goDispText.SetActive(false);
        showSequence.Append(hideSequence);
    }
    
    void OnEnable()
    {
        StartCoroutine(PopAnim(sprCandyBag));
        StartCoroutine(PopText(beginTextShowTime));
        StartCoroutine(PopAction(effectTime));
    }
    
    // 캔디 봉투 등장시 부풀린다.
    IEnumerator PopAnim(Sprite[] arrSpr)
    {
        for(int i=0; i<arrSpr.Length; i++)
        {
            sprBagRenderer.sprite = arrSpr[i];
            yield return new WaitForSeconds(0.1f);
        }
    }

    // 글자가 뜨는 시간을 관리한다.
    private IEnumerator PopText(float t)
    {
        goDispText.SetActive(false);
        yield return new WaitForSeconds(t);
        goDispText.SetActive(true);
        yield return new WaitForSeconds(effectTime - t);
        goDispText.SetActive(false);
    }

    // Dotween 시퀀스를 실행한다.
    IEnumerator PopAction(float t)
    {
        showSequence.Restart();
        yield return new WaitForSeconds(t);
        gameObject.SetActive(false);
    }
}