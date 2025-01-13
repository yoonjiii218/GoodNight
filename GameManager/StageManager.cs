using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<StageManager>();
                if(instance == null)
                {
                    var instanceContainer = new GameObject("StageManager");
                    instance = instanceContainer.AddComponent<StageManager>();
                }
            }
            return instance;
        }
    }
    private static StageManager instance;

    public Image FadeInOutImag;

    float a;
    public IEnumerator FadeIn()
    {
        Player.instance.fullHeart();
        SoundManager.instance.bgmPlayer.Stop();
        a = 1;
        FadeInOutImag.color = new Vector4(0, 0, 0, a);
        yield return new WaitForSeconds(0.3f);
    }

    public IEnumerator FadeOut()
    {
        if (Player.instance.isEnding)
        {
            SoundManager.instance.bgmPlayer.clip = SoundManager.instance.bgmSounds[3].clip;
            SoundManager.instance.bgmPlayer.Play();
        }
        while (a >= 0)
        {
            FadeInOutImag.color = new Vector4(0, 0, 0, a);
            a -= 0.02f;
            yield return null;
        }
    }

    public IEnumerator MoveNext(Collider2D collision, Vector3 destination, bool fadeInOut, bool SmoothMoving)
    {
        yield return null;
        if (fadeInOut)
        {
            EffectManager.instance.effectSounds[5].source.Play();
            yield return StartCoroutine(FadeIn());
        }
        Camera.Instance.cameraSmoothMoving = SmoothMoving;

        collision.transform.position = destination;

        if (fadeInOut)
        {
            yield return StartCoroutine(FadeOut());
        }

    }
}
