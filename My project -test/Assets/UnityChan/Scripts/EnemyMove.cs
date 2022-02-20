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
    private string playerName = "SD_unitychan_humanoid"; //プレイヤーオブジェクト名
    private Vector3 startPosition = new Vector3(-10, -0.2f, 0); //敵スタート位置

    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        timerScript = timerGameObject.GetComponent<TimerScript>();
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);

        float timer = timerScript.GetTimer();
        bool isStartedTimer = timerScript.countFlag;
        if (timer == 120.0f)
        {
            // 停止→リセット時
            transform.position = startPosition;
        }

        if (isStartedTimer)
        {
            // タイマー起動中
            if (timer == 0.0f)
            {
                // 120秒逃げ切った場合：ゲームクリア画面に遷移？
                Moving(false, 0);
            }
            EnemyMoveControl();
        }
        else
        {
            // タイマー停止中
            Moving(false, 0);
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

        if (enemyPosition.x + 0.6f < playerPosition.x)
        {
            // プレイヤーが敵よりプラス位置にいたらプラス方向へ動く
            Moving(false, 0);
            this.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            Moving(true, 1);
        }
        else if (enemyPosition.x > playerPosition.x + 0.6f)
        {
            // プレイヤーが敵よりマイナス位置にいたらマイナス方向へ動く
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
    /// 当たり判定
    /// </summary>
    /// <param name="collision">衝突した相手の情報</param>
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit :" + collision.gameObject.name);
        if (collision.gameObject.name == playerName)
        {
            // 敵とプレイヤーがぶつかったらゲームオーバー画面へ遷移する
            //SceneManager.LoadScene("GameOverScene");
        }
    }

    //Unityちゃんの足音を再生する（「Running@loop」Animationから呼ばれる）
    public void PlayFootstepSE()
    {
        //エネミーにも足音をつけるとうるさそうなので保留
    }
}
