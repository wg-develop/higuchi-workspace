using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMove : MonoBehaviour
{
    public Transform target;
    private Animator enemyAnimator;
    private Vector3 enemyMoveDirection = new Vector3(0, 0, 0);
    public GameObject timerGameObject;
    private TimerScript timerScript;
    private float enemySpeed = 0.03f; //移動速度
    private float[] enemyJumpPower = {7, 8, 9 }; //ジャンプ力
    private int enemyJumpLevel = 0;
    private string playerName = "SD_unitychan_humanoid"; //プレイヤーオブジェクト名
    private Vector3 startPosition = new Vector3(-10, -0.2f, 0); //敵スタート位置
    public static float remainingTime = 0;
    private Vector3 initPosition; //初期位置
    private System.Random rand = new System.Random();

    void Start()
    {
        initPosition = transform.position;
        enemyAnimator = GetComponent<Animator>();
        timerScript = timerGameObject.GetComponent<TimerScript>();
    }

    void Update()
    {
        //罠設置フェーズの処理
        if (CommonScript.phase == CommonScript.Phase.TRAPPHASE)
        {
            transform.position = initPosition;

        }
        //逃走フェーズの処理
        else if (CommonScript.phase == CommonScript.Phase.ESCAPEPHASE)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);

            remainingTime = timerScript.GetTimer(); //残り時間
            bool isStartedTimer = timerScript.countFlag;
            if (remainingTime == 120.0f)
            {
                // 停止→リセット時
                transform.position = startPosition;
            }

            if (isStartedTimer)
            {
                // タイマー起動中
                if (remainingTime == 0.0f)
                {
                    // 120秒逃げ切った場合：タイムアップ処理
                    Moving(false, 0);
                }

                // ジャンプパワーアップ
                if (remainingTime <= 60.0f) enemyJumpLevel = 2;
                else if (remainingTime <= 90.0f) enemyJumpLevel = 1;

                EnemyMoveControl();
            }
            else
            {
                // タイマー停止中
                Moving(false, 0);
            }
        }
    }

    /// <summary>
    /// 敵とプレイヤーの位置関係によって動作制御します
    /// </summary>
    void EnemyMoveControl()
    {
        //敵の現在位置とプレイヤーの現在地を取得
        Vector3 enemyPosition = this.transform.position;
        Vector3 playerPosition = target.position;

        int jumpFlg = rand.Next(0,5);
        // 気まぐれにジャンプする
        Jumping(jumpFlg);

        if (enemyPosition.x + 0.6f < playerPosition.x)
        {
            // プレイヤーが敵よりプラス位置にいたらプラス方向へ動く
            Moving(false, 0);
            this.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            Moving(true, 1);
        }
        else if (enemyPosition.x > playerPosition.x + 0.6f)
        {
            //プレイヤーが敵よりマイナス位置にいたらマイナス方向へ動く
            Moving(false, 0);
            this.transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
            Moving(true, -1);
        }
        else
        {
            Moving(false, 0);
            return;
        }
    }

    /// <summary>
    /// 敵を走らせます
    /// </summary>
    /// <param name="is_move">動作フラグ</param>
    /// <param name="val">進む方向</param>
    void Moving(bool is_move, int val)
    {
        if (is_move)
        {
            enemyMoveDirection.x = enemySpeed * val;
            transform.position += enemyMoveDirection;
            enemyAnimator.SetBool("is_running", true);
        }
        else
        {
            enemyAnimator.SetBool("is_running", false);
        }
    }

    /// <summary>
    ///  敵をジャンプさせます
    /// </summary>
    /// <param name="jumpFlg">ジャンプフラグ(ランダム)</param>
    void Jumping(int jumpFlg)
    {
        if (jumpFlg == 1)
        {
            enemyAnimator.SetBool("is_jump", true);
            //ジャンプアニメーションの調整
            if (enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("JumpToTop"))
            {
                enemyMoveDirection.y = enemyJumpPower[enemyJumpLevel] * Time.deltaTime;
                transform.position += enemyMoveDirection;
            }
            if (enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("fall"))
            {
                if (enemyMoveDirection.y < 0.7)
                {
                    enemyAnimator.SetBool("is_jump", false);
                }
            }
        }
    }

    /// <summary>
    /// 当たり判定
    /// </summary>
    /// <param name="collision">衝突した相手の情報</param>
    void OnCollisionEnter(Collision collision)
    {
//        Debug.Log("Hit :" + collision.gameObject.name);
        if (collision.gameObject.name == playerName)
        {
            // 敵とプレイヤーがぶつかったらゲームオーバー画面へ遷移する
            SceneManager.LoadScene("GameOverScene");
        }
    }

    //Unityちゃんの足音を再生する（「Running@loop」Animationから呼ばれる
    public void PlayFootstepSE()
    {
        //ネミーにも足音をつけるとうるさそうなので保留
    }
}
