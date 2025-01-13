using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public GameObject wind;
    public GameObject Water;
    public Transform[] Pos;
    public float speed;
    public Transform[] ShootPos1;
    public Transform[] ShootPos2;
    public int ShootCoolTime;
    bool isShootCool;
    bool isShootCool1;
    bool isShootCool2;


    void Start()
    {
        
    }

    void Update()
    {
        transform.position = new Vector3(Player.instance.Pos_C.position.x, Player.instance.Pos_C.position.y, 0);

        if(isShootCool == false)
        {
            StartCoroutine(AttackManager());
        }
        if (isShootCool1 == false)
        {
            StartCoroutine(callWind());
        }

        if(Player.instance.isSky == false)
        {
            DestroyObject();
        }
    }

    IEnumerator AttackManager()
    {
        isShootCool = true;

        StartCoroutine(Attack1());
        yield return new WaitForSeconds(ShootCoolTime * 0.5f);
        StartCoroutine(Attack2());
        yield return new WaitForSeconds(ShootCoolTime * 0.5f);

        isShootCool = false;
    }

    IEnumerator Attack1()
    {
        Instantiate(Water, ShootPos1[0].position, Quaternion.Euler(0, 0, 0));
        Instantiate(Water, ShootPos1[1].position, Quaternion.Euler(0, 0, 0));
        Instantiate(Water, ShootPos1[2].position, Quaternion.Euler(0, 0, 0));
        Instantiate(Water, ShootPos1[3].position, Quaternion.Euler(0, 0, 0));

        yield return new WaitForSeconds(0);
    }

    IEnumerator Attack2()
    {
        Instantiate(Water, ShootPos2[0].position, Quaternion.Euler(0, 0, 0));
        Instantiate(Water, ShootPos2[1].position, Quaternion.Euler(0, 0, 0));
        Instantiate(Water, ShootPos2[2].position, Quaternion.Euler(0, 0, 0));
        Instantiate(Water, ShootPos2[3].position, Quaternion.Euler(0, 0, 0));

        yield return new WaitForSeconds(0);
    }

    IEnumerator callWind()
    {
        isShootCool1 = true;

        Instantiate(wind, Pos[0].position, Quaternion.Euler(0, 0, 0));
        Instantiate(wind, Pos[1].position, Quaternion.Euler(0, 0, 0));
        Instantiate(wind, Pos[2].position, Quaternion.Euler(0, 0, 0));


        yield return new WaitForSeconds(ShootCoolTime);
        isShootCool1 = false;
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
