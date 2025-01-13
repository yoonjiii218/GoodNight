using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public float speed;
    public float power;
    bool isGround;
    public int JumpCnt;
    int jumpCnt;

    public Animator animator;

    public Rigidbody2D rigidbody2D;       

    public Transform foot;
    public LayerMask isLayer_g;
    public float checkRadius;

    public float shootCooltime;
    bool isShootCool;
    public GameObject bullet;
    public Transform Pos_R;
    public Transform Pos_L;

    public static Player instance;

    bool Left;
    bool Right;
    bool Down;
    bool Up;

    bool isHurt = false;
    public int hp;
    int fullHp;
    public SpriteRenderer sprite;
    Color halfA = new Color(1, 1, 1, 0.5f);
    Color fullA = new Color(1, 1, 1, 1);

    public Image fade;
    public Transform c_pos;

    public bool isReverse;

    public RawImage[] heart;

    public CanvasGroup dialoguegroup;
    public CanvasGroup Boss_hpBar;
    public CanvasGroup Boss2_hpBar;
    public CanvasGroup Boss3_hpBar;

    public bool isSky;
    public bool isForest;
    public bool isTown;
    public bool isEnding;
    public bool isMusicON;

    public int ran = 130613;
    public bool isWind;

    public GameObject cloud;
    public Transform Pos_C;
    public bool isCloud;

    bool isSwap;
    public float Speed;
    public float Power;
    

    public CanvasGroup M;
    public Button Home;
    public Button Continue;

    public bool isStage2;
    public bool isStage3;

    int back;

    public bool isActive;

    public BoxCollider2D Ending;

    public SpriteRenderer swap_img;

    public bool isM;
    public bool isEscape;

    public bool isNoHurt;

    void Start()
    {
        Speed = speed;
        Power = power;
        jumpCnt = JumpCnt;
        rigidbody2D = GetComponent<Rigidbody2D>();
        instance = this;
        fullHp = hp;
        isActive = true;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            StartCoroutine(GameOver());
        }

        if (isActive)
        {
            if (Input.GetKey(KeyCode.Z) && isShootCool == false)
            {
                StartCoroutine(Attack());
            }
        }

        if (isStage2)
        {
            PlayerPrefs.SetInt("save_Back", 1);
        }
        else if (isStage3)
        {
            PlayerPrefs.SetInt("save_Back", 2);
        }

        if (Input.GetKey(KeyCode.C))
        {
            DialogueManager.instance.sentences.Clear();
            DialogueManager.instance.NextSentence();
        }


        isGround = Physics2D.OverlapCircle(foot.position, checkRadius, isLayer_g);

        if (isActive)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (ran == -1) //플레이어의 이동키 랜덤하게 바꿈
                {
                    Move_L();
                }
                else if (ran == 0)
                {
                    Down = true;
                }
                else
                {
                    Move_R();
                }
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (ran == -1)
                {
                    Move_R();
                }
                else if (ran == 0)
                {
                    Up = true;
                }
                else
                {
                    Move_L();
                }
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (isSky)
                {
                    Move_U();
                }
                else if (isReverse == true || ran == -1)
                {
                    if (ran == 0)
                    {
                        Move_R();
                    }
                    else
                    {
                        Down = true;
                    }
                }
                else if (ran == 0)
                {
                    Move_R();
                }
                else
                {
                    Up = true;
                }
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                if (isSky)
                {
                    Move_D();
                }
                else if (isReverse == true || ran == -1)
                {
                    if (ran == 0)
                    {
                        Move_L();
                    }
                    else
                    {
                        Up = true;
                    }
                }
                else if (ran == 0)
                {
                    Move_L();
                }
                else
                {
                    Down = true;
                }
            }

            if (!Input.GetKey(KeyCode.RightArrow))
            {
                if (ran == -1)
                {
                    animator.SetBool("Run_L", false);
                    Left = false;
                }
                else if (ran == 0)
                {
                    Down = false;
                }
                else
                {
                    animator.SetBool("Run_R", false);
                    Right = false;
                }
            }
            if (!Input.GetKey(KeyCode.LeftArrow))
            {
                if (ran == -1)
                {
                    animator.SetBool("Run_R", false);
                    Right = false;
                }
                else if (ran == 0)
                {
                    Up = false;
                }
                else
                {
                    animator.SetBool("Run_L", false);
                    Left = false;
                }
            }
            if (!Input.GetKey(KeyCode.UpArrow))
            {
                if (isReverse == true || ran == -1)
                {
                    if (ran == 0)
                    {
                        animator.SetBool("Run_R", false);
                    }
                    else
                    {
                        Down = false;
                    }
                }
                else if (ran == 0)
                {
                    animator.SetBool("Run_R", false);
                    Right = false;
                }
                else
                {
                    Up = false;
                }
            }
            if (!Input.GetKey(KeyCode.DownArrow))
            {
                if (isReverse == true || ran == -1)
                {
                    if (ran == 0)
                    {
                        animator.SetBool("Run_L", false);
                    }
                    else
                    {
                        Up = false;
                    }
                }
                else if (ran == 0)
                {
                    animator.SetBool("Run_L", false);
                    Left = false;
                }
                else
                {
                    Down = false;
                }
            }

            if (isGround == true && jumpCnt > 0 && Input.GetKeyDown(KeyCode.Space) && !isSky)
            {
                if (isReverse)
                {
                    rigidbody2D.velocity = -1 * Vector2.up * power;
                }
                else
                {
                    rigidbody2D.velocity = Vector2.up * power;
                }
            }
            if (isGround == false && jumpCnt > 0 && Input.GetKeyDown(KeyCode.Space) && !isSky)
            {
                if (isReverse)
                {
                    rigidbody2D.velocity = -1 * Vector2.up * power;
                }
                else
                {
                    rigidbody2D.velocity = Vector2.up * power;
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                jumpCnt--;
            }
            if (isGround)
            {
                jumpCnt = JumpCnt;
            }
        }
        

        if (isReverse)
        {
            rigidbody2D.gravityScale = -5;
            transform.rotation = Quaternion.Euler(-180, 0, 0);
        }
        if (!isReverse)
        {
            rigidbody2D.gravityScale = 5;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (isSky)
        {
            transform.localScale = new Vector3(0.17f, 0.17f, 1);
            rigidbody2D.gravityScale = 0;
            if(isCloud == true)
            {
                Debug.Log("먹구름");
                Instantiate(cloud, Pos_C.position, Quaternion.Euler(0, 0, 0));
                isCloud = false;
            }
        }
        if (!isSky)
        {
            transform.localScale = new Vector3(0.12f, 0.12f, 1);
            animator.SetBool("Fly", false);
            isCloud = true;
            if (!isReverse)
            {
                rigidbody2D.gravityScale = 5;
            }
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            if (!isEscape)
            {
                StartCoroutine(Menu());
                Debug.Log("MENU");
            }
        }

        if (Input.GetKey(KeyCode.I))
        {
            StartCoroutine(NoHurt());
        }
    }

    IEnumerator NoHurt()
    {
        isNoHurt = true;
        isHurt = true;
        SoundManager.instance.bgmPlayer.volume = 0.1f;
        EffectManager.instance.effectSounds[14].source.Play();
        EffectManager.instance.effectSounds[14].source.volume += 3;
        yield return new WaitForSeconds(10);
        EffectManager.instance.effectSounds[14].source.volume -= 3;
        SoundManager.instance.bgmPlayer.volume = 1;
        yield return new WaitForSeconds(0.5f);
        isNoHurt = false;
        isHurt = false;
    }

    public void Front()
    {
        animator.SetBool("Run_L", false);
        animator.SetBool("Run_R", false);
        animator.SetTrigger("Dialogue");
    }

    IEnumerator Menu()
    {
        isEscape = true;
        isM = !isM;
        if (isM)
        {
            Debug.Log("MENU_Open");
            speed = 0;
            power = 0;
            isHurt = true;
            isNoHurt = true;
            isShootCool = true;
            M.alpha = 1;
            Home.interactable = true;
            Continue.interactable = true;
        }
        else
        {
            Debug.Log("MENU_Close");
            M.alpha = 0;
            Home.interactable = false;
            Continue.interactable = false;
            isHurt = false;
            isNoHurt = false;
            isShootCool = false;
            speed = Speed;
            power = Power;
        }

        yield return new WaitForSeconds(0.1f);
        isEscape = false;
    }

    public void CloseM()
    {
        M.alpha = 0;
        Home.interactable = false;
        Continue.interactable = false;
        isHurt = false;
        isShootCool = false;
        speed = Speed;
        power = Power;
    }


    public void Move_R()
    {
        if (isSky)
        {
            animator.SetBool("Fly", true); //날아다니는 animation
            if (isWind)
            {
                transform.position += new Vector3(2 * Time.deltaTime, 0, 0);
            }
            else
            {
                transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            }
        }
        else
        {
            animator.SetBool("Run_R", true);
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
        Right = true; //공격할 때 사용
    }

    public void Move_L()
    {
        if (isSky)
        {
            animator.SetBool("Fly", true); //날아다니는 animation
            if (isWind)
            {
                transform.position -= new Vector3(7 * Time.deltaTime, 0, 0);
            }
            else
            {
                transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            }
        }
        else
        {
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            animator.SetBool("Run_L", true);
        }
        Left = true; //공격할 때 사용
    }

    public void Move_U()
    {
        animator.SetBool("Fly", true);
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        Up = true;
    }

    public void Move_D()
    {
        animator.SetBool("Fly", true);
        transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        Down = true;
    }

    IEnumerator Attack()
    {
        isShootCool = true;

        if (Up || Down || Left || Right)
        {
            animator.SetTrigger("Shoot");

            if (Right == true)
            {
                if (Up == true)
                {
                    Instantiate(bullet, Pos_R.position, Quaternion.Euler(180, 0, 0));
                    Up = false;
                }
                else if (Down == true)
                {
                    Instantiate(bullet, Pos_R.position, Quaternion.Euler(0, 0, 180));
                    Down = false;
                }
                else
                {
                    Instantiate(bullet, Pos_R.position, Quaternion.Euler(0, 0, 0));
                    Right = false;
                }
            }
            else if (Left == true)
            {
                if (Up == true)
                {
                    Instantiate(bullet, Pos_L.position, Quaternion.Euler(180, 0, 0));
                    Up = false;
                }
                else if (Down == true)
                {
                    Instantiate(bullet, Pos_L.position, Quaternion.Euler(0, 0, 180));
                    Down = false;
                }
                else
                {
                    Instantiate(bullet, Pos_L.position, Quaternion.Euler(0, 180, 0));
                    Left = false;
                }
            }

            if (Right == false && Left == false)
            {
                if (Up == true)
                {
                    Instantiate(bullet, Pos_R.position, Quaternion.Euler(180, 0, 0));
                    Up = false;
                }
                else if (Down == true)
                {
                    Instantiate(bullet, Pos_R.position, Quaternion.Euler(0, 0, 180));
                    Down = false;
                }
            }

            EffectManager.instance.effectSounds[0].source.Play();
        }

        yield return new WaitForSeconds(shootCooltime * 0.3f);
        isShootCool = false;
    }
 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "GameOver")
        {
            Debug.Log("GameOver");
            StartCoroutine(GameOver());
        }

        if(collision.collider.tag == "Ending")
        {
            SoundManager.instance.bgmPlayer.Stop();
            EffectManager.instance.effectSounds[12].source.Play();

            Ending.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "enemy" || col.gameObject.tag == "enemy_g" || col.gameObject.tag == "enemy_Y" || col.gameObject.tag == "Boss2_1" 
            || col.gameObject.tag == "Boss2-3" || col.gameObject.tag == "Boss2" || col.gameObject.tag == "Wind" || col.gameObject.tag == "Boss")
        {
            Hurt();     
        }
        if(col.gameObject.tag == "Camera_stop")
        {
            Camera.Instance.Stop();
            if (isForest)
            {
                Boss2_hpBar.alpha = 1;
            }
            else if(!isEnding)
            {
                Boss_hpBar.alpha = 1;
            }
        }
        if(col.gameObject.tag == "Stop")
        {
            dialoguegroup.alpha = 0;
            EffectManager.instance.effectSounds[11].source.Stop();
            int k, j;
            k = DialogueManager.instance.i;
            j = DialogueManager.instance.img_index[k];
            DialogueManager.instance.Dimage[j].color = new Color(255, 255, 255, 0);
            if(Dialogue.dia.isStart || Dialogue.dia.isFight)
            {
                if(DialogueManager.instance.sentences.Count == 0 && Dialogue.dia.St == true)
                    Dialogue.dia.isPlayer = true;
            }
        }
        if(col.gameObject.tag == "People")
        {
            if(isSwap == false)
            {
                StartCoroutine(Swap());
            }
        }
        if(col.gameObject.tag == "Slow")
        {
            speed = 1;
        }
        if(col.gameObject.tag == "NotSlow")
        {
            speed = Speed;
        }
    }

    public void Hurt()
    {
        if (!isHurt && !isNoHurt)
        {
            EffectManager.instance.effectSounds[13].source.Play();
            isHurt = true;
            hp--;
            if(hp >= 0)
            {
                Color tmp = heart[hp].color;
                tmp.a = 0.3f;
                heart[hp].color = tmp;
            }

            if(hp <= 0)
            {
                Debug.Log("Game Over");
                isHurt = false;
                StartCoroutine(GameOver());
            }
            else
            {
                StartCoroutine(HurtRountine());
                StartCoroutine(alphablink());
            }
        }
    }

    public void fullHeart()
    {
        for (int i = 0; i < fullHp; i++)
        {
            Color tmp = heart[i].color;
            tmp.a = 1;
            heart[i].color = tmp;
        }
        hp = fullHp;
    }


    Color tmp;
    IEnumerator Swap()
    {
        isSwap = true;
        ran = Random.Range(-1, 1);
        tmp = swap_img.color;
        tmp.a = 255;
        swap_img.color = tmp;
        yield return new WaitForSeconds(7);
        tmp.a = 0;
        swap_img.color = tmp;
        ran = 130613;
        isSwap = false;
    }

    IEnumerator HurtRountine()
    {
        if (!isNoHurt)
        {
            yield return new WaitForSeconds(2);
            isHurt = false;
        }
    }

    IEnumerator alphablink()
    {
        while (isHurt && !isNoHurt)
        {
            yield return new WaitForSeconds(0.1f);
            sprite.color = halfA;
            yield return new WaitForSeconds(0.1f);
            sprite.color = fullA;
        }
    }

    float a;

    IEnumerator GameOver()
    {
        PlayerPrefs.SetInt("continue", 1);
        if (isStage2)
        {
            PlayerPrefs.SetInt("save_Back", 1);
        }
        else if (isStage3)
        {
            PlayerPrefs.SetInt("save_Back", 2);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        animator.SetTrigger("Dialogue");
        isActive = false;

        yield return new WaitForSeconds(0.5f);
        while (a >= 0)
        {
            fade.color = new Vector4(0, 0, 0, a);
            a -= 0.02f;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
    }
}
