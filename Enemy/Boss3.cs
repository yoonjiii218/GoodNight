using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss3 : MonoBehaviour
{
    public float speed;
    public int hp;
    int fullHp;
    public Animator animator;
    public float CoolTime;
    bool isCool;

    public GameObject Knife;
    public Transform Pos_K;
    public SpriteRenderer sprite;

    public GameObject blade;
    public Transform Pos_B;

    bool isBCool;

    public float distance;

    float Speed;

    public bool isStart;
    public static Boss3 instance;

    public Slider hpBar;

    Color halfA = new Color(1, 1, 1, 0.5f);
    Color fullA = new Color(1, 1, 1, 1);

    public GameObject gameOver;

    void Start()
    {
        fullHp = hp;
        Speed = speed;
        instance = this;
    }

    void Update()
    {
        if (isStart)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            distance = Player.instance.transform.position.x - transform.position.x;

            if (hp <= 0)
            {
                speed = 0;
                isCool = true;
                isBCool = true;
                animator.SetTrigger("Die");
                StartCoroutine(alphablink());
                Invoke("DestroyBoss", 2);
            }

            if (isCool == false)
            {
                StartCoroutine(Attack());
            }
            if (isBCool == false)
            {
                StartCoroutine(Attack_B());
            }
            if (distance > 10)
            {
                StartCoroutine(SpeedUp());
            }
            if(distance < -5)
            {
                Player.instance.Hurt();
            }
        }

        hpBar.value = (float)hp / fullHp;
    }

    IEnumerator SpeedUp()
    {
        speed += 1;
        yield return new WaitForSeconds(0.7f);
        speed = Speed;
    }

    IEnumerator Attack()
    {
        isCool = true;

        EffectManager.instance.effectSounds[7].source.Play();
        Instantiate(Knife, Pos_K.position, Quaternion.Euler(0, 180, 0));

        yield return new WaitForSeconds(CoolTime * 0.8f);
        isCool = false;
    }

    IEnumerator Attack_B()
    {
        isBCool = true;
        speed = 0;
        animator.SetBool("Attack", true);

        yield return new WaitForSeconds(0.7f);
        Instantiate(blade, Pos_B.position, Quaternion.Euler(0, 5, 0));
        yield return new WaitForSeconds(0.9f);
        animator.SetBool("Attack", false);
        speed = Speed;

        yield return new WaitForSeconds(CoolTime);
        isBCool = false;
    }

    public void Hurt()
    {
        if (hp > 0)
        {
            hp--;
            EffectManager.instance.effectSounds[10].source.Play();
            StartCoroutine(alphablink());
        }
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

    void DestroyBoss()
    {
        Player.instance.Boss3_hpBar.alpha = 0;
        SoundManager.instance.bgmPlayer.Stop();
        hpBar.value = 1;
        Destroy(gameObject);
        Destroy(gameOver);
    }
}
