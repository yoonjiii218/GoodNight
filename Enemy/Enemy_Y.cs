using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Y : MonoBehaviour
{
    public int hp;
    int fullHp;
    public int CoolTime;
    bool isCool;
    public Animator animator;

    public GameObject ball;
    public Transform Pos;

    public SpriteRenderer sprite;

    public static Enemy_Y instance;

    public BoxCollider2D col;

    Color halfA = new Color(1, 1, 1, 0.5f);
    Color fullA = new Color(1, 1, 1, 1);

    void Start()
    {
        fullHp = hp;
        instance = this;
    }

    
    void Update()
    {
        if(isCool == false)
        {
            StartCoroutine(Attack());
            animator.SetBool("isAttack", true);
        }
        else
        {
            animator.SetBool("isAttack", false);
        }

        if(hp <= 0)
        {
            Debug.Log("노랑이 처치");
            col.enabled = false;
            Invoke("Die", 1);

            Color tmp = sprite.color;
            tmp.a = 0.7f;
            sprite.color = tmp;

            Invoke("DestroyEnemy", 2);
            enabled = false;

        }
    }

    IEnumerator Attack()
    {
        isCool = true;
        animator.SetTrigger("E_Attack");

        Instantiate(ball, Pos.position, Quaternion.Euler(0, 0, 0));
        Instantiate(ball, Pos.position, Quaternion.Euler(0, 0, 45));
        Instantiate(ball, Pos.position, Quaternion.Euler(0, 0, -45));

        yield return new WaitForSeconds(CoolTime * 0.5f);
        isCool = false;
    }

    public void Hurt()
    {
        if (hp > 0)
        {
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

    void Die()
    {
        Color tmp = sprite.color;
        tmp.a = 1;
        sprite.color = tmp;

        animator.SetTrigger("Die");
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
