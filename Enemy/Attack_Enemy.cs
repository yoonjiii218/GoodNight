using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Enemy : MonoBehaviour
{
    public float speed;
    public float distance;
    public LayerMask isLayer;

    public int time;
    public bool isMove;

    public bool isB;


    void Start()
    {
        Invoke("DestroyBullet", time);
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
                    ray.collider.GetComponent<Player>().Hurt();
                }

                DestroyBullet();
            }
        }
        if (isMove == true)
        {
            if(transform.rotation.y == 0)
            {
                transform.position += new Vector3(-1 * Time.deltaTime * speed, 0, 0); // 왼쪽
            }
            else
            {
                transform.position += new Vector3(Time.deltaTime * speed, 0, 0); // 오른쪽
            }
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
