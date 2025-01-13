using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    //public Transform follow;
    public SpriteRenderer background_sunny;
    public SpriteRenderer background_sunset;
    public SpriteRenderer background_night;
    public Transform[] Pos;
    public bool isStop;

    public Transform skyPos;

    public static Camera Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<Camera>();
                if(instance == null)
                {
                    var instanceContainer = new GameObject("Camera");
                    instance = instanceContainer.AddComponent<Camera>();
                }
            }
            return instance;
        }
    }

    private static Camera instance;

    public GameObject player;

    public float offsetY = 1f;
    public float offsetZ = -10f;
    public float smooth = 5f;

    Vector3 target;


    public bool cameraSmoothMoving;

    private void LateUpdate()
    {
        if (!isStop && !Player.instance.isSky)
        {
            target = new Vector3(player.transform.position.x, player.transform.position.y + offsetY, player.transform.position.z + offsetZ);

            if (cameraSmoothMoving)
            {
                transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * smooth);
            }
            else
            {
                transform.position = target;
                cameraSmoothMoving = true;
            }
        }

        if (Player.instance.isForest || PlayerPrefs.GetInt("save_Back") == 1 && !Player.instance.isTown && !Player.instance.isSky && !Player.instance.isReverse)
        {
            background_night.transform.position = new Vector3(transform.position.x, transform.position.y, 1);
        }
        else if (Player.instance.isTown || PlayerPrefs.GetInt("save_Back") == 2 && !Player.instance.isReverse)
        {
            background_sunny.transform.position = new Vector3(transform.position.x, transform.position.y, 1);
        }
        else
        {
            background_sunset.transform.position = new Vector3(transform.position.x, transform.position.y, 1);
        }

        if (Player.instance.isSky)
        {
            Debug.Log("하늘");
            transform.position = new Vector3(skyPos.position.x, skyPos.position.y, -10);
            background_sunny.transform.position = new Vector3(transform.position.x, transform.position.y, 1);
        }
    }

    public void Stop()
    {
        if(Player.instance.isForest == true || PlayerPrefs.GetInt("save_Back") == 1 && !Player.instance.isEnding)
        {
            transform.position = new Vector3(Pos[1].position.x, Pos[1].position.y, -10);
        }
        else if (Player.instance.isEnding)
        {
            transform.position = new Vector3(Pos[2].position.x, Pos[2].position.y, -10);
        }
        else
        {
            transform.position = new Vector3(Pos[0].position.x, Pos[0].position.y, -10);
        }
        isStop = true;
    }
}
