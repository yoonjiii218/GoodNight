using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_G : MonoBehaviour
{
    public int hp;
    int fullHp;
    public float speed;
    bool isLeft = true;
    public Animator animator;
    bool isHit = false;

    public bool isReverse;

    void Start()
    {
        fullHp = hp;
    }

    
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (hp < fullHp && isHit == false)
        {
            isHit = true;
            speed += 5;
            animator.SetTrigger("Angry");
        }
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
