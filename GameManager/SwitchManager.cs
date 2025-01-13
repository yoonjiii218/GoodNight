using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Switch
{
    public string switchName;
    public bool bools;
}

public class SwitchManager : MonoBehaviour
{
    public static SwitchManager instance;
    public RawImage startAnim;
    public RawImage fightAnim;
    public float time;
    public RawImage light;

    public RawImage Blink1;
    public RawImage Blink2;

    Color tmp;

    Color halfA = new Color(1, 1, 1, 0.5f);
    Color fullA = new Color(1, 1, 1, 1);

    float a;

    [Header("스위치 등록")]
    [SerializeField]
    public Switch[] switches;

    void Start()
    {
        instance = this;   
    }

    
    void Update()
    {
        if(switches[0].bools == true)
        {
            StartCoroutine(alphablink(startAnim));
            if (Player.instance.isForest || PlayerPrefs.GetInt("save_Back") == 1 && !Player.instance.isTown)
            {
                PlayerPrefs.SetInt("savePoint", 3);
            } 
            else if (Player.instance.isTown || PlayerPrefs.GetInt("save_Back") == 2)
            {
                PlayerPrefs.SetInt("savePoint", 5);
            }
            else
            {
                PlayerPrefs.SetInt("savePoint", 1);
            }
        }
        if (switches[1].bools == true)
        {
            StartCoroutine(Blink());

            tmp = Blink1.color;
            tmp.a = 1;
            Blink1.color = tmp;

            tmp = Blink2.color;
            tmp.a = 1;
            Blink2.color = tmp;

            EffectManager.instance.effectSounds[8].source.Play();
            switches[1].bools = false;
        }
        if (switches[2].bools == true)
        {
            StartCoroutine(alphablink(fightAnim));
            if (Player.instance.isForest || PlayerPrefs.GetInt("save_Back") == 1 && !Player.instance.isTown)
            {
                PlayerPrefs.SetInt("savePoint", 4);
            }
            else if (Player.instance.isTown || PlayerPrefs.GetInt("save_Back") == 2)
            {
                PlayerPrefs.SetInt("savePoint", 6);
            }
            else
            {
                PlayerPrefs.SetInt("savePoint", 2);
            }
        }
        if(switches[3].bools == true)
        {
            SoundManager.instance.bgmPlayer.clip = SoundManager.instance.bgmSounds[4].clip;
            SoundManager.instance.bgmPlayer.Play();
            Player.instance.enabled = false;
            StartCoroutine(Ending());
            switches[3].bools = false;
        }
    }

    public void RealEnding()
    {
        SoundManager.instance.bgmPlayer.Stop();
        SceneManager.LoadScene("title");
    }

    IEnumerator Ending()
    {
        Blink1.GetComponent<Blink>().speed = 20;
        Blink2.GetComponent<Blink>().speed = 20;

        yield return new WaitForSeconds(0.1f);

        tmp = Blink1.color;
        tmp.a = 1;
        Blink1.color = tmp;

        tmp = Blink2.color;
        tmp.a = 1;
        Blink2.color = tmp;
    }


    IEnumerator Blink()
    {
        a = 1;
        while (a >= 0)
        {
            light.color = new Vector4(1, 1, 1, a);
            a -= 0.05f;
            yield return null;
        }
    }

    IEnumerator alphablink(RawImage img)
    {
        yield return new WaitForSeconds(0.4f);
        img.color = fullA;
        yield return new WaitForSeconds(0.1f);
        img.color = halfA;
        yield return new WaitForSeconds(0.1f);
        img.color = fullA;
        yield return new WaitForSeconds(0.1f);
        /*startAnim.color = halfA;
        yield return new WaitForSeconds(0.1f);*/
        img.color = new Color(255, 255, 255, 0);
        if(img == startAnim)
        {
            switches[0].bools = false;
        }
        if(img == fightAnim)
        {
            switches[2].bools = false;
        }
    }
}
