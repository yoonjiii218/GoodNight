using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public string[] sentences;
    public static Dialogue dia;
    public bool isPlayer;

    public GameObject P;

    public bool isStart;
    public bool isFight;
    public bool RealEnding;

    public BoxCollider2D box;

    public int[] img;

    public bool St;

    void Start()
    {
        dia = this;
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && isPlayer == false)
        {
            Player.instance.isActive = false;
            Invoke("Stop", 0.1f);
            St = true;
            DialogueManager.instance.img_index = img;
            DialogueManager.instance.Ondialogue(sentences);
            DialogueManager.instance.isStart = isStart;
            DialogueManager.instance.isFight = isFight;
            DialogueManager.instance.RealEnding = RealEnding;
            isPlayer = true;
        }
    }

    public void Stop()
    {
        Player.instance.Front();
    }
}
