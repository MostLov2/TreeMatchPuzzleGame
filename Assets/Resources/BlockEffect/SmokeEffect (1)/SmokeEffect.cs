using Cysharp.Threading.Tasks;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SmokeEffect : MonoBehaviour
{
    [SerializeField] float fxTime = 1f; // 연기 효과 지속시간
    [HideInInspector]
    public int smokeArea = 1; // 5라면 5x5타일 범위
    ParticleSystem particleStart;
    ParticleSystem particleMain;
    RectTransform smoke;

    void Awake()
    {
        var group = transform.GetChild(0);
        particleStart= group.GetChild(0).GetComponent<ParticleSystem>();
        particleMain = group.GetChild(1).GetComponent<ParticleSystem>();
        
        smoke = group.GetChild(1).GetComponent<RectTransform>();
    }
    
    void OnEnable() => SmokeFX(fxTime);
    
    async UniTaskVoid SmokeFX(float time)
    {
        smoke.localScale = Vector3.one * smokeArea * 0.2f;
        
        particleStart.Play();
        await UniTask.Delay(200);
        particleMain.Play();
        await UniTask.Delay((int)(time * 1000) - 200);
        gameObject.SetActive(false);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(SmokeEffect))]
public class customEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("여기서 효과 길이와 크기를 설정할 수 있지만,\n" +
                                "효과 발생 지점 변경시는 자식 오브젝트를 움직이길 권장합니다.\n" +
                                "여기에는 효과가 위아래로 넘치지 않도록 충돌체가 같이 있습니다.",
                                MessageType.Info);
        base.OnInspectorGUI();
    }
}
#endif