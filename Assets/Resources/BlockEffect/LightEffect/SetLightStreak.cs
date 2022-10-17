using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VLB;

public class SetLightStreak : MonoBehaviour
{
    public AnimationCurve lightDiffusion;
    public Color lightColor = Color.white;
    public Transform lightPos;

    ParticleSystem particleFX;
    VolumetricLightBeam[] vlbs;

    void Awake()
    {
        particleFX = transform.GetChild(0).GetComponent<ParticleSystem>();
        vlbs = GetComponentsInChildren<VolumetricLightBeam>();
    }

    void OnEnable() => LightFX();

    async UniTaskVoid LightFX()
    {
        if(lightPos != null) transform.position = lightPos.position;
        var vlList = vlbs.ToList();
        particleFX.startColor = lightColor;
        particleFX.gameObject.SetActive(true);
        vlList.ForEach(vlb => vlb.enabled = true);
        vlList.ForEach(vlb => vlb.color = lightColor);
        for(int i=0; i<20; i++)
        {
            var lightSize = lightDiffusion.Evaluate(i * 0.05f);
            vlbs[0].transform.localScale = new Vector3(1, lightSize, 1);
            vlbs[1].transform.localScale = new Vector3(1, lightSize, 1);
            vlbs[2].transform.localScale = new Vector3(lightSize, 1, 1);
            vlbs[3].transform.localScale = new Vector3(lightSize, 1, 1);
            await UniTask.Delay(10);
        }
        vlList.ForEach(vlb => vlb.enabled = false);
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
