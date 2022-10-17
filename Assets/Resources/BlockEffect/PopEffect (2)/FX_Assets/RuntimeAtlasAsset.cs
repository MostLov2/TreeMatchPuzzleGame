using UnityEngine;

public class RuntimeAtlasAsset : MonoBehaviour
{
    // 스프라이트로 쓸 텍스처 파일이 들어갈 배열
    [SerializeField] Texture2D[] atlasTextures;
    // 각 스프라이트별 아틀라스 내 공간좌표가 들어갈 배열
    [SerializeField] Rect[] rects;
    // 스프라이트를 파티클에 적용하기 위한 파티클 시스템 참조
    [SerializeField] private ParticleSystem psFall;
    [SerializeField] private ParticleSystem psRise;

    void Awake()
    {
        atlasTextures = new Texture2D[5];
        atlasTextures[0] = Resources.Load<Texture2D>("BlockImage/BlueBlock");
        atlasTextures[1] = Resources.Load<Texture2D>("BlockImage/GreenBlock");
        atlasTextures[2] = Resources.Load<Texture2D>("BlockImage/PurpleBlock");
        atlasTextures[3] = Resources.Load<Texture2D>("BlockImage/RedBlock");
        atlasTextures[4] = Resources.Load<Texture2D>("BlockImage/YellowBlock");

        psFall = transform.parent.GetChild(0).GetComponent<ParticleSystem>();
        psRise = transform.parent.GetChild(1).GetComponent<ParticleSystem>();
    }
    
    void Start()
    {
        // 각각의 텍스처를 가능한 작은 공간에 정리하고 2픽셀만큼 떨어뜨려준다.
        Texture2D atlas = new Texture2D(2048, 2048);
        rects = atlas.PackTextures(atlasTextures, 2, 8192);

        // ps.textureSheetAnimation속성은 TextureSheetAnimationModule로 받아서 설정
        var tsam1 = psFall.textureSheetAnimation;
        tsam1.enabled = true;
        tsam1.mode = ParticleSystemAnimationMode.Sprites;
        
        var tsam2 = psRise.textureSheetAnimation;
        tsam2.enabled = true;
        tsam2.mode = ParticleSystemAnimationMode.Sprites;
        
        // 시트에서 각 스프라이트의 좌표를 가져와 스프라이트를 생성하고 파티클 속성 목록에 추가한다.
        for(int i=0; i<rects.Length; i++)
        {
            var sprRect = new Rect(atlas.width * rects[i].x, atlas.height * rects[i].y, 
                atlas.width * rects[i].width, atlas.height * rects[i].height);
            var sprite = Sprite.Create(atlas, sprRect, new Vector2(0.5f, 0.5f), 140);
            tsam1.AddSprite(sprite);
            tsam2.AddSprite(sprite);
        }
    }
}
