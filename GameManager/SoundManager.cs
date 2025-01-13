using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string soundName;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("브금 플레이어")]
    [SerializeField]
    public AudioSource bgmPlayer;

    [Header("사운드 등록")]
    [SerializeField]
    public Sound[] bgmSounds;

    void Start()
    {
        instance = this;
        /*bgmPlayer.clip = bgmSounds[0].clip;
        bgmPlayer.Play();*/
    }


    void Update()
    {
        
    }
}
