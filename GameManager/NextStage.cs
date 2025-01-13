using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStage : MonoBehaviour
{
   public enum NextPositionType
    {
        InitPosiotion,
        SomePosition,
    };
    public NextPositionType nextPositionType;

    public Transform DestinationPoint;

    public static NextStage instance;

    public bool fadeInOut;
    public bool SmoothMoving;
    public bool isReverse;
    public bool isForest;
    public bool isSky;
    public bool isTown;
    public bool isEnding;

    public bool isStage2;
    public bool isStage3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if(nextPositionType == NextPositionType.InitPosiotion)
            {
                StartCoroutine(StageManager.Instance.MoveNext(collision, Vector3.zero, fadeInOut, SmoothMoving));
            }
            else if(nextPositionType == NextPositionType.SomePosition)
            {
                //collision.transform.position = DestinationPoint.position;
                if(fadeInOut == false)
                {
                    SwitchManager.instance.switches[1].bools = true;
                }
                Player.instance.isForest = isForest;
                Player.instance.isSky = isSky;
                Player.instance.isTown = isTown;
                Player.instance.isReverse = isReverse;
                Player.instance.isEnding = isEnding;
                if (isStage2)
                {
                    Player.instance.isStage2 = true;
                }
                else if (isStage3)
                {
                    Player.instance.isStage3 = true;
                }
                StartCoroutine(StageManager.Instance.MoveNext(collision, DestinationPoint.position, fadeInOut, SmoothMoving));
            }
        }
    }
    void Start()
    {
        instance = this;
    }

    
    void Update()
    {
        
    }
}
