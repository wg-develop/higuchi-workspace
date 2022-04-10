using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{
    private System.Random rand = new System.Random();
    private MovingCommonScript movingCommonScript;
    private float enemySpeed = 3.0f; //移動速度
    private float[] enemyJumpPower = {7, 8, 9 }; //ジャンプ力
    private int enemyJumpLevel = 0;
    public GameObject target;

    public GameObject timerGameObject;
    private TimerScript timerScript;
    public static float remainingTime = 0;


    void Start()
    {
        timerScript = timerGameObject.GetComponent<TimerScript>();
        movingCommonScript = GetComponent<MovingCommonScript>();
    }

    void Update()
    {
        //逃走フェーズの処理
         if (CommonScript.phase == CommonScript.Phase.ESCAPEPHASE)
        {
            remainingTime = timerScript.GetTimer(); //残り時間
                // ジャンプパワーアップ
                if (remainingTime <= 60.0f) enemyJumpLevel = 2;
                else if (remainingTime <= 90.0f) enemyJumpLevel = 1;

                EnemyMoveControl();
        }
    }

    /// <summary>
    /// 敵とプレイヤーの位置関係によって動作制御します
    /// </summary>
    void EnemyMoveControl()
    {
      //敵の現在位置とプレイヤーの現在地を取得
      Vector3 enemyPosition = this.transform.position;
      Vector3 playerPosition = target.transform.position;

      int jumpFlg = rand.Next(0,5);
      // 気まぐれにジャンプする

      if(jumpFlg == 1){
        movingCommonScript.Jumping(true,enemyJumpPower[enemyJumpLevel],0);
      }

      if (enemyPosition.x + 0.6f < playerPosition.x)
      {
        // プレイヤーが敵よりプラス位置にいたらプラス方向へ動く
        movingCommonScript.Moving(false, 0 ,enemySpeed);
        this.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
        movingCommonScript.Moving(true, 1 ,enemySpeed);
      }
      else if (enemyPosition.x > playerPosition.x + 0.6f)
      {
        //プレイヤーが敵よりマイナス位置にいたらマイナス方向へ動く
        movingCommonScript.Moving(false, 0 ,enemySpeed);
        this.transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
        movingCommonScript.Moving(true, -1, enemySpeed);
      }
      else
      {
        movingCommonScript.Moving(false, 0 ,0);
        return;
      }

    }

    /// <summary>
    /// 当たり判定
    /// </summary>
    /// <param name="collision">衝突した相手の情報</param>
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == target)
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
