using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wind1 : MonoBehaviour
{
    void Start()
    {
        Player.instance.isWind = true;
        Invoke("DestroyWind", 2);
    }

    void Update()
    {
        
    }

    void DestroyWind()
    {
        Destroy(gameObject);
        Player.instance.isWind = false;
    }
}
