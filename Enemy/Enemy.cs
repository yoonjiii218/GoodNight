using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    int fullHp;
    public float speed;
    float Speed;
    bool isLeft = true;
    public Animator animator;
    public SpriteRenderer sprite;
    public bool isReverse;

    public BoxCollider2D col;

    public bool isB;
    public int Shootcooltime;
    bool isShootcool;
    public GameObject bandage;
    public Transform Pos_b;

    bool isShootcool_c;
    public bool isC;
    public Transform Pos_c;
    public GameObject thorn;

    public bool isG;

    Color halfA = new Color(1, 1, 1, 0.5f);
    Color fullA = new Color(1, 1, 1, 1);

    public static Enemy instance;

    void Start()
    {      
        fullHp = hp;
        Speed = speed;
        instance = this;
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (hp <= 0)
        {
            col.enabled = false;
            Die();
            Invoke("DestroyEnemy", 1);
            enabled = false;
        }

        if (hp > 0 && isShootcool == false && isB == true)
        {
            StartCoroutine(Attack());
        }

        if(hp > 0 && isC == true && isShootcool_c == false)
        {
            StartCoroutine(Attack_T());
        }
    }

    IEnumerator Attack_T()
    {
        isShootcool_c = true;

        Instantiate(thorn, Pos_c.position, Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(Shootcooltime * 0.3f);

        isShootcool_c = false;
    }

    IEnumerator Attack()
    {
        isShootcool = true;
        speed = 0;
        animator.SetBool("Attack", true);
        if (isLeft)
        {
            Instantiate(bandage, Pos_b.position, Quaternion.Euler(0, 0, 0));
        }
        else
        {
            Instantiate(bandage, Pos_b.position, Quaternion.Euler(0, 180, 0));
        }
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Attack", false);
        speed = Speed;
        yield return new WaitForSeconds(Shootcooltime * 0.5f);
        isShootcool = false;
    }


    void Die()
    {

        if(isC || isB || isG)
        {
            EffectManager.instance.effectSounds[2].source.Play();
        }
        else
        {
            EffectManager.instance.effectSounds[1].source.Play();
        }

        animator.SetTrigger("Die");
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    public void Hurt()
    {
        Debug.Log("깜빡깜빡");
        if(hp > 0)
        {
            EffectManager.instance.effectSounds[10].source.Play();
            StartCoroutine(alphablink());
        }
    }

    IEnumerator alphablink()
    {
        Debug.Log("아야");
        sprite.color = halfA;
        yield return new WaitForSeconds(0.1f);
        sprite.color = fullA;
        yield return new WaitForSeconds(0.1f);
        sprite.color = halfA;
        yield return new WaitForSeconds(0.1f);
        sprite.color = fullA;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "endPoint")
        {
            if (isReverse == true)
            {
                if (isLeft == true)
                {
                    transform.eulerAngles = new Vector3(180, 180, 0);
                    isLeft = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(180, 0, 0);
                    isLeft = true;
                }
            }
            else
            {
                if (isLeft == true)
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    isLeft = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    isLeft = true;
                }
            }
        }
    }
}
