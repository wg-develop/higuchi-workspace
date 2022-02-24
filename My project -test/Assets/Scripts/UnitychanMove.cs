using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitychanMove : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    private Vector3 moveDirection = new Vector3(0, 0, 0);
    public float x; //移動速度
    public float y; //ジャンプ力
    public float volume; //音量
    [SerializeField] AudioClip[] clips; //オーディオ
    protected AudioSource source; //オーディオ
    private Rigidbody rigidBody;
    public float impulse;
    public float damageTime; //被ダメ時間
    private float countDamageTime = 0; //被ダメ時間計算用
    private bool momentumCancelFlag = false; //吹っ飛び緩和フラグ

    // ジャンプ用フラグ
    private bool is_ground;

    private AnimatorStateInfo currentState;

    void Start()
    {
        animator = GetComponent<Animator>();
        source = GetComponents<AudioSource>()[0];
        rigidBody = GetComponent<Rigidbody>();
        Debug.Log("start");
        Debug.Log("Render:" + GetComponent<Renderer>());
    }

    // Update is called once per frame
    void Update()
    {
        //   Debug.Log("velocity:" + rigidBody.velocity);

        moveDirection.x = 0;
        moveDirection.y = 0;

        transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
        //被ダメした場合の処理
        if (animator.GetBool("Damaged")) damageMotion();

        //ジャンプ
        if (Input.GetKeyDown(KeyCode.W) && !animator.GetBool("Damaged"))
        {
            if (is_ground)
            {
                is_ground = false;

                jumpping(true);
            }
        }
        //ジャンプアニメーションの調整
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("JumpToTop"))
        {
            moveDirection.y = y * Time.deltaTime;
            transform.position += moveDirection;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("fall"))
        {
            if (moveDirection.y < 0.7)
            {
                animator.SetBool("is_jump", false);
            }
        }
        //右へ移動
        if (Input.GetKey(KeyCode.D) && !animator.GetBool("Damaged"))
        {
            this.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            moving(true, 1);
        }
        //左へ移動
        else if (Input.GetKey(KeyCode.A) && !animator.GetBool("Damaged"))
        {
            transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
            moving(true, -1);
        }
        else
        {
            moving(false, 0);
        }
    }

    void moving(bool is_move, int val)
    {
        if (is_move)
        {
            moveDirection.x = x * val * Time.deltaTime;
            transform.position += moveDirection;
            animator.SetBool("is_running", true);
        }
        else
        {
            animator.SetBool("is_running", false);
        }
    }

    void jumpping(bool is_jump)
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

    void damageMotion()
    {
        //被ダメ後の無敵時間
        if (animator.GetBool("Damaged"))
        {
            //            Color color = renderer.material.color;
            //            color.a = Mathf.Sin(Time.time) / 2 + 0.5f;
            countDamageTime++;
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
        if (collision.gameObject.tag == "trap" && !animator.GetBool("Damaged"))
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
            jumpping(false);
        }
    }
    void OnCollisionExit(Collision collision) { }
}
