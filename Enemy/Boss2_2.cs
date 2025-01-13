using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss2_2 : MonoBehaviour
{
    public int hp;
    int fullHp;
    bool isLeft;
    public SpriteRenderer sprite;
    public BoxCollider2D col;

    bool isShootCool;
    public int Shootcooltime;

    public static Boss2_2 instance;

    Color halfA = new Color(1, 1, 1, 0.5f);
    Color fullA = new Color(1, 1, 1, 1);

    public Animator animator;

    public GameObject sword;
    public Transform[] Pos;

    public bool isStart;

    public Slider hpBar;
    public GameObject CameraStop;

    public GameObject Block;


    void Start()
    {
        instance = this;
        fullHp = 12;
    }

    void Update()
    {
        if (isStart)
        {
            if (hp <= 0)
            {
                isShootCool = true;
                EffectManager.instance.effectSounds[2].source.Play();
                animator.SetTrigger("Die");
                Invoke("DestroyObject", 1);
            }

            if (isShootCool == false)
            {
                StartCoroutine(Attack());
            }
        }

        hpBar.value = (float)hp / fullHp;
    }

    public void Hurt()
    {
        StartCoroutine(alphablink());
    }

    IEnumerator alphablink()
    {
        sprite.color = halfA;
        yield return new WaitForSeconds(0.1f);
        sprite.color = fullA;
        yield return new WaitForSeconds(0.1f);
        sprite.color = halfA;
        yield return new WaitForSeconds(0.1f);
        sprite.color = fullA;
    }

    IEnumerator Attack()
    {
        isShootCool = true;

        yield return new WaitForSeconds(Shootcooltime * 0.1f);
        EffectManager.instance.effectSounds[7].source.Play();
        Instantiate(sword, Pos[0].position, Quaternion.Euler(0, 0, 0));
        Instantiate(sword, Pos[1].position, Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(Shootcooltime * 0.5f);
        EffectManager.instance.effectSounds[7].source.Play();
        Instantiate(sword, Pos[2].position, Quaternion.Euler(0, 0, 0));
        Instantiate(sword, Pos[3].position, Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(Shootcooltime*0.5f);
        EffectManager.instance.effectSounds[7].source.Play();
        Instantiate(sword, Pos[4].position, Quaternion.Euler(0, 0, 90));
        Instantiate(sword, Pos[5].position, Quaternion.Euler(0, 0, 90));
        Instantiate(sword, Pos[6].position, Quaternion.Euler(0, 0, 90));
        yield return new WaitForSeconds(Shootcooltime * 0.5f);

        isShootCool = false;
    }

    void DestroyObject()
    {
        Camera.Instance.isStop = false;
        Player.instance.Boss2_hpBar.alpha = 0;
        SoundManager.instance.bgmPlayer.Stop();
        hpBar.value = 1;
        Destroy(gameObject);
        Destroy(CameraStop);
        Destroy(Block);
    }
}
