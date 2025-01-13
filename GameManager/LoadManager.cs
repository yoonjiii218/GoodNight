using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public Transform[] savePoint;
    public Player player;

    void Start()
    {
        if (!PlayerPrefs.HasKey("savePoint"))
        {
            Debug.Log("시작");
            player.transform.position = savePoint[0].position;
        }
        else if(PlayerPrefs.GetInt("continue") == 0)
        {
            player.transform.position = savePoint[0].position;
        }
        else
        {
            Vector3 position = savePoint[PlayerPrefs.GetInt("savePoint")].position;
            position.z = 0;
            player.transform.position = position;

            Debug.Log(PlayerPrefs.GetInt("save_Back"));
            Debug.Log(PlayerPrefs.GetInt("savePoint"));
        }

        Camera.Instance.transform.position = player.transform.position;
    }

    public void ToTitle()
    {
        SceneManager.LoadScene("title");
    }
}
