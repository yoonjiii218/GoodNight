using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    public float speed;
    public bool isMiddle;
    public Transform Pos_M;
    public RawImage img;
    public Transform pos;
    Color tmp;

    public static Blink instance;

    void Start()
    {
        instance = this;
    }

    
    void Update()
    {
        if(img.color.a == 1)
        {
            if (Player.instance.isEnding)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, Pos_M.localPosition, speed * Time.deltaTime);
                if (transform.localPosition == Pos_M.localPosition)
                {
                    SwitchManager.instance.Invoke("RealEnding", 2);
                }
            }
            else
            {
                if (!isMiddle)
                {
                    transform.localPosition = Vector3.MoveTowards(transform.localPosition, Pos_M.localPosition, speed * Time.deltaTime);
                    if (transform.localPosition == Pos_M.localPosition)
                    {
                        isMiddle = true;
                    }
                }
                else
                {
                    transform.localPosition = Vector3.MoveTowards(transform.localPosition, pos.localPosition, speed * Time.deltaTime);
                    if (transform.localPosition == pos.localPosition)
                    {
                        Debug.Log(transform.localPosition);
                        img.color = tmp;
                        tmp.a = 0;
                        img.color = tmp;
                        isMiddle = false;
                    }
                }
            }
        }
    }
}
