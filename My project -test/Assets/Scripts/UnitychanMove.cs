using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitychanMove : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    private Vector3 moveDirection = new Vector3(0, 0, 0);
    public float speed; //移動速度
    public float jumpPower; //ジャンプ力
    public float volume; //音量
    private Vector3 initPosition; //初期位置
    private bool setTrapPhaseInitFlag = false; //罠設置フェーズ切り替え時の初期化処理用
    [SerializeField] AudioClip[] clips; //オーディオ
    protected AudioSource source; //オーディオ
    private Rigidbody rigidBody;
    public float impulse;
    public float damageTime; //被ダメ時間
    private float countDamageTime = 0; //被ダメ時間計算用
    private bool momentumCancelFlag = false; //吹っ飛び緩和フラグ
    private GameObject childGameObject0;
    private GameObject childGameObject1;

    // ジャンプ用フラグ
    private bool is_ground;

    private AnimatorStateInfo currentState;

    void Start()
    {
        initPosition = transform.position;
        childGameObject0 = transform.GetChild(0).gameObject;
        childGameObject1 = transform.GetChild(1).gameObject;

        animator = GetComponent<Animator>();
        source = GetComponents<AudioSource>()[0];
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //罠設置フェーズの処理
        if (CommonScript.phase == CommonScript.Phase.TRAPPHASE)
        {
            if (!setTrapPhaseInitFlag)
            {
                transform.position = initPosition;
                setTrapPhaseInitFlag = true;
            }
            Moving(false, 0);
            BasicProcess();
            //            BasicAction();
        }
        //逃走フェーズの処理
        else if (CommonScript.phase == CommonScript.Phase.ESCAPEPHASE)
        {
            setTrapPhaseInitFlag = false;
            BasicProcess();
            BasicAction();
        }
    }

    //基本処理
    void BasicProcess()
    {
        //   Debug.Log("velocity:" + rigidBody.velocity);
        moveDirection.x = 0;
        moveDirection.y = 0;

        transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
        //被ダメした場合の処理
        if (animator.GetBool("Damaged")) DamageMotion();

        //ジャンプアニメーションの調整
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("JumpToTop"))
        {
            moveDirection.y = jumpPower * Time.deltaTime;
            transform.position += moveDirection;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("fall"))
        {
            if (moveDirection.y < 0.7)
            {
                animator.SetBool("is_jump", false);
            }
        }
    }
    //キー入力による基本動作
    void BasicAction()
    {
        //ジャンプ
        if (Input.GetKeyDown(KeyCode.W) && !animator.GetBool("Damaged"))
        {
            if (is_ground)
            {
                is_ground = false;

                Jumpping(true);
            }
        }
        //右へ移動
        if (Input.GetKey(KeyCode.D) && !animator.GetBool("Damaged"))
        {
            this.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            Moving(true, 1);
        }
        //左へ移動
        else if (Input.GetKey(KeyCode.A) && !animator.GetBool("Damaged"))
        {
            transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
            Moving(true, -1);
        } else
        {
            Moving(false, 0);
        }
    }

    void Moving(bool is_move, int val)
    {
        if (is_move)
        {
            moveDirection.x = speed * val * Time.deltaTime;
            moveDirection.y = 0;
            transform.position += moveDirection;
            animator.SetBool("is_running", true);
        }
        else
        {
            animator.SetBool("is_running", false);
        }
    }

    void Jumpping(bool is_jump)
    {
        if (is_jump)
        {
            animator.SetBool("is_jump", true);

            //ジャンプ音
            playSE(1, 1.0f, 1.0f);
        }
    }

    //Unityちゃんの足音を再生する（「Running@loop」Animationから呼ばれる）
    void PlayFootstepSE()
    {
        playSE(0, 0.4f, 1.0f);
    }

    void DamageMotion()
    {
        //被ダメ後の無敵時間
        if (animator.GetBool("Damaged"))
        {
            //            Color color = renderer.material.color;
            //            color.a = Mathf.Sin(Time.time) / 2 + 0.5f;
            countDamageTime++;

            childGameObject0.SetActive(!childGameObject0.activeSelf);
            childGameObject1.SetActive(!childGameObject1.activeSelf);
            if (countDamageTime > damageTime * 0.25 && !momentumCancelFlag)
            {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x * 0.1f, rigidBody.velocity.y * 0.1f, rigidBody.velocity.z);
                momentumCancelFlag = true;
            }
            if (countDamageTime > damageTime)
            {
                countDamageTime = 0;
                animator.SetBool("Damaged", false);
                momentumCancelFlag = false;
                //                color.a = 1.0f;
                childGameObject0.SetActive(true);
                childGameObject1.SetActive(true);
            }
            //            renderer.material.color = color;
        }
    }
    //効果音再生
    void playSE(int clipNum, float volume, float pitch)
    {
        source.volume = volume;
        source.pitch = pitch;
        source.PlayOneShot(clips[clipNum]);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "damagetrap" && !animator.GetBool("Damaged"))
        {
            if (transform.localEulerAngles.y == 90)
            {
                rigidBody.AddForce(new Vector3(-1.0f, 1.0f, 0.0f) * impulse, ForceMode.Force);
            }
            else if (transform.localEulerAngles.y == 270)
            {
                rigidBody.AddForce(new Vector3(1.0f, 1.0f, 0.0f) * impulse, ForceMode.Force);
            }
            animator.SetBool("Damaged", true);

            //ダメージ音
            playSE(2, 1.0f, 1.2f);
        }
    }
    //floorにいるときだけジャンプできる
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            is_ground = true;
            Jumpping(false);
        }
    }
    void OnCollisionExit(Collision collision) { }
}
