using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{

    public float shootCooltime;
    bool Shootcooltime_UD;
    bool Shootcooltime_UD2;
    bool Shootcooltime_M;
    public GameObject fire;
    public GameObject fire2;

    bool Shootcooltime_C;
    public GameObject enemy;

    public Transform shootPos1;
    public Transform shootPos2;
    public Transform shootPos3;
    public Transform shootPos4;
    public Transform shootPos5;

    public Transform Pos_cry;

    public Animator animator;

    public int hp;
    int fullHp;

    public static Boss instance;

    public bool isHurt;
    public SpriteRenderer sprite;
    Color halfA = new Color(1, 1, 1, 0.5f);
    Color fullA = new Color(1, 1, 1, 1);

    public bool isStart;
    public Slider hpBar;

    public BoxCollider2D col;

    public GameObject CameraStop;

    public GameObject Block;


    void Start()
    {
        fullHp = hp;
        instance = this;
    }

    void Update()
    {
        if (hp <= 0)
        {
            Shootcooltime_UD = true;
            Shootcooltime_M = true;
            Shootcooltime_C = true;
            animator.SetTrigger("Die");
            Invoke("DestroyBoss", 2);
        }

        if (isStart)
        {
            Debug.Log("시작");
            if (Shootcooltime_UD == false)
            {
                StartCoroutine(Attack_Down());
            }
            if (Shootcooltime_M == false)
            {
                StartCoroutine(Attack_M());
            }
            if (Shootcooltime_C == false)
            {
                StartCoroutine(Cry());
            }
        }

        hpBar.value = (float)hp / fullHp;
    }

    IEnumerator Attack_Down()
    {
        Shootcooltime_UD = true;
        animator.SetBool("Cry", false);

        yield return new WaitForSeconds(0.1f);
        Instantiate(fire, shootPos2.position, Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(0.2f);
        Instantiate(fire, shootPos1.position, Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(0.2f);
        Instantiate(fire, shootPos3.position, Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(0.2f);
        Instantiate(fire, shootPos4.position, Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(0.5f);
        Instantiate(fire, shootPos3.position, Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(0.2f);
        Instantiate(fire, shootPos1.position, Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(0.2f);
        Instantiate(fire, shootPos2.position, Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(0.2f);
        Instantiate(fire, shootPos4.position, Quaternion.Euler(0, 0, 0));

        yield return new WaitForSeconds(shootCooltime * 0.2f);
        Shootcooltime_UD = false;
    }

    IEnumerator Attack_M()
    {
        Shootcooltime_M = true;
        yield return new WaitForSeconds(2);
        animator.SetBool("Cry", false);

        EffectManager.instance.effectSounds[9].source.Play();
        Instantiate(fire2, shootPos5.position, Quaternion.Euler(0, 1, -45));
        Instantiate(fire2, shootPos5.position, Quaternion.Euler(0, 1, 0));
        Instantiate(fire2, shootPos5.position, Quaternion.Euler(0, 1, 45));
        yield return new WaitForSeconds(shootCooltime * 0.1f);
        EffectManager.instance.effectSounds[9].source.Stop();

        yield return new WaitForSeconds(shootCooltime * 0.2f);
        Shootcooltime_M = false;
    }

    IEnumerator Cry()
    {
        Shootcooltime_C = true;
        animator.SetBool("Cry", true) ;

        Instantiate(enemy, Pos_cry.position, Quaternion.Euler(0, 0, 0));

        yield return new WaitForSeconds(shootCooltime * 0.7f);
        Shootcooltime_C = false;
    }

    public void Hurt()
    {
        Debug.Log("맞았다..!");
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
        isHurt = false;
    }

    void DestroyBoss()
    {
        EffectManager.instance.effectSounds[1].source.Play();
        SoundManager.instance.bgmPlayer.Stop();
        Camera.Instance.isStop = false;
        Player.instance.Boss_hpBar.alpha = 0;
        hpBar.value = 1;
        Destroy(gameObject);
        Destroy(CameraStop);
        Destroy(Block);
    }
}
