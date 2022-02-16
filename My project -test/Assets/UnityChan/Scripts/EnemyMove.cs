using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMove : MonoBehaviour
{
    public Transform target;
    private Animator EnemyAnimator;
    private Vector3 EnemyMoveDirection = new Vector3(0, 0, 0);
    public GameObject timerGameObject;
    private TimerScript timerScript;
    private float EnemySpeed = 0.03f; //移動速度
    private string playerName = "SD_unitychan_humanoid"; //プレイヤーオブジェクト名

    void Start()
    {
        EnemyAnimator = GetComponent<Animator>();
        timerScript = timerGameObject.GetComponent<TimerScript>();
    }

    void Update()
    {
        EnemyMoveControl();
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
            moving(false, 0);
            this.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            moving(true, 1);
        }
        else if (enemyPosition.x > playerPosition.x + 0.6f)
        {
            // プレイヤーが敵よりマイナス位置にいたらマイナス方向へ動く
            moving(false, 0);
            this.transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
            moving(true, -1);
        }
        else
        {
            moving(false, 0);
            return;
        }
    }

    /// <summary>
    /// 敵を走らせます
    /// </summary>
    /// <param name="is_move">動作フラグ</param>
    /// <param name="val">進む方向</param>
    void moving(bool is_move, int val)
    {
        if (is_move)
        {
            EnemyMoveDirection.x = EnemySpeed * val;
            transform.position += EnemyMoveDirection;
            EnemyAnimator.SetBool("is_running", true);
        }
        else
        {
            EnemyAnimator.SetBool("is_running", false);
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
