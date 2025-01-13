using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2_1 : MonoBehaviour
{
    public int speed;
    int Speed;
    public int hp;
    public SpriteRenderer sprite;
    public BoxCollider2D col;
    bool isLeft = true;
    public Animator animator;

    public static Boss2_1 instance;

    Color halfA = new Color(1, 1, 1, 0.5f);
    Color fullA = new Color(1, 1, 1, 1);

    bool isMove;

    void Start()
    {
        instance = this;
        Speed = speed;
        speed = 0;
    }

    
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if(Boss2_2.instance.hp <= 8 && isMove == false)
        {
            Debug.Log("움직인다.");
            isMove = true;
            speed = Speed;
        }

        if(hp <= 0)
        {
            speed = 0;
            EffectManager.instance.effectSounds[2].source.Play();
            animator.SetTrigger("Die");
            Invoke("DestroyObject", 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "endPoint")
        {
            if (isLeft)
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

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
