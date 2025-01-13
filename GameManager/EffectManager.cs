using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Effect
{
    public string soundName;
    public AudioClip clip;
    public AudioSource source;
}

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;

    [Header("효과음 등록")]
    [SerializeField]
    public Effect[] effectSounds;

    void Start()
    {
        instance = this;

        for (int i = 0; i < effectSounds.Length; i++)
        {
            effectSounds[i].source = gameObject.AddComponent<AudioSource>();
            effectSounds[i].source.clip = effectSounds[i].clip;
            effectSounds[i].source.loop = false;
        }
    }


    void Update()
    {
        
    }
}
