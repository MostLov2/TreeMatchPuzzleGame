using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    //---------------AudioMixer--------------------
    [SerializeField] AudioMixer audioMixer;
    //---------------SingleTurn--------------------
    public static SoundManager instance;
    public Transform sound;
    private void Awake()
    {
        if (null == instance)
        {
            instance =          this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            //instance =          null;
        }
    }
    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Sound").transform != null)
        {
            sound = GameObject.FindGameObjectWithTag("Sound").transform;
        }
    }
    /// <summary>
    /// 사운드 발생 함수
    /// </summary>
    /// <param name="clip">Audioclip배열</param>
    /// <param name="clipCount">몇번째 배열인지</param>
    /// <param name="valumn">소리 크기</param>
    /// <param name="count">BGM이면 0 Effect면 1</param>
    public void PlaySFX(AudioClip[] clip,int clipCount,float valumn,int count)
    {
        if (GameObject.FindGameObjectWithTag("Sound") != null)
        {
            sound = GameObject.FindGameObjectWithTag("Sound").transform;
            switch (count)   
            {
                case 0:
                    GameObject bgm = new GameObject("BGM");
                    bgm.transform.SetParent(sound.transform.GetChild(0));
                    AudioSource sourceB = bgm.AddComponent<AudioSource>();
                    sourceB.outputAudioMixerGroup = audioMixer.FindMatchingGroups("BGM")[0];
                    sourceB.clip = clip[clipCount];
                    sourceB.loop = true;
                    sourceB.Play();
                    break;
                case 1:
                    GameObject effect = new GameObject("Effect"); 
                    effect.transform.SetParent(sound.transform.GetChild(1));
                    AudioSource sourceE = effect.AddComponent<AudioSource>();
                    sourceE.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Effect")[0];
                    sourceE.clip = clip[clipCount];
                    sourceE.Play();
                    Destroy(effect, clip[clipCount].length);
                    break;
            }
        }

    }
    /// <summary>
    /// 씬 넘어 갈때 소리 삭제
    /// </summary>
    public void DestroyClip()
    {
        if (GameObject.FindGameObjectWithTag("Sound").transform != null)
        {
            sound = GameObject.FindGameObjectWithTag("Sound").transform;
            if (sound.transform.GetChild(0).childCount >= 1)
            {
                for (int i = 0; i < sound.transform.GetChild(0).transform.childCount; i++)
                {
                    Destroy(sound.transform.GetChild(0).transform.GetChild(i).GetComponent<GameObject>());
                    sound.transform.GetChild(0).transform.GetChild(i).GetComponent<AudioSource>().clip = null;
                }
            }
            if (sound.transform.GetChild(1).childCount >= 1)
            {
                for (int i = 0; i < sound.transform.GetChild(1).transform.childCount; i++)
                {
                    Destroy(sound.transform.GetChild(1).transform.GetChild(i).GetComponent<GameObject>());
                    sound.transform.GetChild(1).transform.GetChild(0).GetComponent<AudioSource>().clip = null;
                }
            }
        }
        Resources.UnloadUnusedAssets();
    }
}

