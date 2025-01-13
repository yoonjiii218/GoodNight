using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject[] Dialogue;
    int k = 0;

    public Text dialogueText;
    public GameObject nextText;
    public CanvasGroup dialoguegroup;
    public Queue<string> sentences;

    private string currentSentence;

    public float typingSpeed = 0.03f;
    public bool istyping;

    public bool isStart;
    public bool isFight;
    public bool RealEnding;

    public GameObject Boss_R1;
    public GameObject Boss_R2;
    public GameObject Boss_R3;

    public GameObject Start_R1;
    public GameObject Start_R2;
    public GameObject Start_R3;

    public static DialogueManager instance;

    public Image[] Dimage;
    public int[] img_index;
    public int i;

    public CanvasGroup Boss3_hpBar;

    public GameObject P;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        sentences = new Queue<string>(); //Sentences 초기화
    }

    public void Ondialogue(string[] lines) //OnDialogue가 호출될 때마다 큐에 대화를 넣고 대화창에 출력되도록
    {
        sentences.Clear();//큐에 있을지도 모르는 인자를 지워줍니다.
        i = -1;
        foreach(string line in lines) //전달받은 인자들을 큐에 차례로 넣어줍니다.
        {
            sentences.Enqueue(line);
        }
        dialoguegroup.alpha = 1;
        dialoguegroup.blocksRaycasts = true;

        NextSentence();
    }

    public void NextSentence() //큐의 데이터를 전부 사용할때까지 출력
    {
        if(sentences.Count != 0) //count를 사용하여 큐의 데이터 갯수를 파악
        {
            i++;
            if (i != 0)
            {
                Dimage[img_index[i - 1]].color = new Color(255, 255, 255, 0);
            }

            //큐에 존재하는 데이터중 가장 먼저 들어온 데이터를 반환하고 삭제하는
            currentSentence = sentences.Dequeue();
            
            //코루틴
            Dimage[img_index[i]].color = new Color(255, 255, 255, 255);
            nextText.SetActive(false);
            if (!istyping)
            {
                StartCoroutine(Typing(currentSentence));
            }
        }
        else
        {
            Dimage[img_index[i]].color = new Color(255, 255, 255, 0);
            dialoguegroup.alpha = 0;
            dialoguegroup.blocksRaycasts = false;
            if (isStart)
            {
                SwitchManager.instance.switches[0].bools = true;
                if (Player.instance.isForest || PlayerPrefs.GetInt("save_Back") == 1 && !Player.instance.isTown)
                {
                    SoundManager.instance.bgmPlayer.clip = SoundManager.instance.bgmSounds[1].clip;
                    SoundManager.instance.bgmPlayer.Play();
                }
                else if (Player.instance.isTown || PlayerPrefs.GetInt("save_Back") == 2) 
                {
                    SoundManager.instance.bgmPlayer.clip = SoundManager.instance.bgmSounds[2].clip;
                    SoundManager.instance.bgmPlayer.Play();
                }
                else
                {
                    SoundManager.instance.bgmPlayer.clip = SoundManager.instance.bgmSounds[0].clip;
                    SoundManager.instance.bgmPlayer.Play();
                }
                isStart = false;
            }
            else if (isFight)
            {
                SwitchManager.instance.switches[2].bools = true;
                if (Player.instance.isForest || PlayerPrefs.GetInt("save_Back") == 1 && !Player.instance.isTown)
                {
                    Boss2_2.instance.isStart = true;
                    SoundManager.instance.bgmPlayer.clip = SoundManager.instance.bgmSounds[1].clip;
                    SoundManager.instance.bgmPlayer.Play();
                }
                else if (Player.instance.isTown || PlayerPrefs.GetInt("save_Back") == 2)
                {
                    Boss3.instance.isStart = true;
                    Boss3_hpBar.alpha = 1;
                    SoundManager.instance.bgmPlayer.clip = SoundManager.instance.bgmSounds[2].clip;
                    SoundManager.instance.bgmPlayer.Play();
                }
                else
                {
                    Boss.instance.isStart = true;
                    SoundManager.instance.bgmPlayer.clip = SoundManager.instance.bgmSounds[0].clip;
                    SoundManager.instance.bgmPlayer.Play();
                }
                isFight = false;
            }
            else if (RealEnding)
            {
                SwitchManager.instance.switches[3].bools = true;
            }

            Player.instance.isActive = true;
            EffectManager.instance.effectSounds[11].source.Stop();
        }
    }

    IEnumerator Typing(string line)
    {
        istyping = true;
        EffectManager.instance.effectSounds[11].source.Play();

        dialogueText.text = "";
        foreach(char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        EffectManager.instance.effectSounds[11].source.Stop();
        istyping = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (!istyping)
                NextSentence();
        }
    }
}
