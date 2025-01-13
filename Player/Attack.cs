using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    float speed = 20;
    public float distance;
    public LayerMask isLayer;

    void Start()
    {
        Invoke("DestroyBullet", 0.5f);
    }

    void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, distance, isLayer);
        if (ray.collider != null)
        {
            if (ray.collider.tag == "enemy" || ray.collider.tag == "People_A")
            {
                if (ray.collider.GetComponent<Enemy>().hp > 0)
                {
                    ray.collider.GetComponent<Enemy>().hp--;
                }

                if(ray.collider != null)
                {
                    ray.collider.GetComponent<Enemy>().Hurt();
                    DestroyBullet();
                }
            }
            if (ray.collider.tag == "enemy_g")
            {
                if (ray.collider.GetComponent<Enemy_G>().hp > 0)
                {
                    EffectManager.instance.effectSounds[10].source.Play();
                    ray.collider.GetComponent<Enemy_G>().hp--;
                }
                DestroyBullet();
            }
            if (ray.collider.tag == "enemy_Y")
            {
                if (ray.collider.GetComponent<Enemy_Y>().hp > 0)
                {
                    EffectManager.instance.effectSounds[10].source.Play();
                    ray.collider.GetComponent<Enemy_Y>().hp--;
                }

                ray.collider.GetComponent<Enemy_Y>().Hurt();
                DestroyBullet();
            }
            if (ray.collider.tag == "Boss" && Boss.instance.isStart == true)
            {
                if (ray.collider.GetComponent<Boss>().hp > 0)
                {
                    EffectManager.instance.effectSounds[10].source.Play();
                    ray.collider.GetComponent<Boss>().hp--;
                }

                ray.collider.GetComponent<Boss>().Hurt();

                DestroyBullet();
            }
            if (ray.collider.tag == "Boss2")
            {

                if (ray.collider.GetComponent<Boss2_2>().hp > 0 && Boss2_2.instance.isStart == true)
                {
                    EffectManager.instance.effectSounds[10].source.Play();
                    ray.collider.GetComponent<Boss2_2>().hp--;
                }

                Boss2_2.instance.Hurt();
                DestroyBullet();
            }
            if (ray.collider.tag == "Boss2_1")
            {
                if (Boss2_2.instance.hp > 8)
                {
                    EffectManager.instance.effectSounds[10].source.Play();
                    Boss2_2.instance.hp--;
                    Boss2_2.instance.Hurt();
                    Boss2_3.instance.Hurt();
                }
                else
                {
                    if (ray.collider.GetComponent<Boss2_1>().hp > 0)
                    {
                        EffectManager.instance.effectSounds[10].source.Play();
                        ray.collider.GetComponent<Boss2_1>().hp--;
                    }
                }
                Boss2_1.instance.Hurt();
                DestroyBullet();
            }
            if (ray.collider.tag == "Boss2-3")
            {
                if (Boss2_2.instance.hp > 4)
                {
                    EffectManager.instance.effectSounds[10].source.Play();
                    Boss2_2.instance.hp--;
                    if(Boss2_1.instance.hp > 0)
                    {
                        Boss2_1.instance.Hurt();
                    }
                    Boss2_2.instance.Hurt();
                }
                else
                {
                    if (ray.collider.GetComponent<Boss2_3>().hp > 0)
                    {
                        EffectManager.instance.effectSounds[10].source.Play();
                        ray.collider.GetComponent<Boss2_3>().hp--;
                    }
                }
                Boss2_3.instance.Hurt();
                DestroyBullet();
            }
            if (ray.collider.tag == "Boss3" && Boss3.instance.isStart == true)
            {
                if(Boss3.instance.hp > 0)
                {
                    Boss3.instance.Hurt();
                }
                DestroyBullet();
            }
            if(ray.collider.tag == "Wall")
            {
                DestroyBullet();
            }
        }

        if (transform.rotation.y == 0)
        {
            if (transform.rotation.x != 0)
            {
                transform.Translate(transform.up * speed * Time.deltaTime);
            }
            else
            {
                if (transform.rotation.z != 0)
                {
                    transform.Translate(-1 * transform.up * speed * Time.deltaTime);
                }
                else
                {
                    transform.Translate(transform.right * speed * Time.deltaTime);
                }
            }
        }
        else
        {
            transform.Translate(-1 * transform.right * speed * Time.deltaTime);
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
