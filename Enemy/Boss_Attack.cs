using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Attack : MonoBehaviour
{
    public bool isStage2;
    public float speed;
    void Start()
    {
        Invoke("DestroyAttack", 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (isStage2)
        {
            if(transform.rotation.z == 0)
            {
                transform.Translate(transform.right * speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(-1 * transform.up * speed * Time.deltaTime);
            }
        }
        else
        {
            if (transform.rotation.y == 0)
            {
                transform.Translate(-1 * transform.up * speed * Time.deltaTime);
            }
            else
            {
                if (transform.rotation.z == 0)
                {
                    transform.Translate(-1 * transform.right * speed * Time.deltaTime);
                }
                else if (transform.rotation.z > 0)
                {

                    transform.position -= new Vector3(speed * Time.deltaTime, speed * Time.deltaTime, 0);
                }
                else
                {

                    transform.position += new Vector3(-1 * speed * Time.deltaTime, speed * Time.deltaTime, 0);
                }
            }
        }
    }

    void DestroyAttack()
    {
        Destroy(gameObject);
    }
}
