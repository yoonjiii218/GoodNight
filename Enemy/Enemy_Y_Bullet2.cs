using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Y_Bullet2 : MonoBehaviour
{
    public float speed;
    public int distance;
    public LayerMask isLayer;

    public static Enemy_Y_Bullet2 instance;

    void Start()
    {
        Invoke("DestroyBullet", 3);
        instance = this;
    }

    void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, distance, isLayer);

        if (ray.collider != null)
        {
            if (ray.collider.tag == "Player")
            {
                if (ray.collider.GetComponent<Player>().hp > 0)
                {
                    Debug.Log("빵야");
                    ray.collider.GetComponent<Player>().Hurt();
                }

                DestroyBullet();
            }
        }

        if (transform.rotation.z == 0)
        {
            transform.position += new Vector3(0, -1 * speed * Time.deltaTime, 0);
        }
        else if (transform.rotation.z > 0)
        {
            transform.position += new Vector3(-1 * speed * Time.deltaTime, -1 * speed * Time.deltaTime, 0);
        }
        else
        {
            transform.position += new Vector3(speed * Time.deltaTime, -1 * speed * Time.deltaTime, 0);
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
