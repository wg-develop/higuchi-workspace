using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private MovingCommonScript movingCommonScript;
    public float speed; //移動速度
    public float jumpPower; //ジャンプ力
    private Animator playerAnimator;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        movingCommonScript = GetComponent<MovingCommonScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //逃走フェーズの処理
  　　  if (CommonScript.phase == CommonScript.Phase.ESCAPEPHASE)
        {
            BasicAction();
        }

    }

    //キー入力による基本動作
    void BasicAction()
    {
        //ジャンプ
        if (Input.GetKeyDown(KeyCode.W) && !playerAnimator.GetBool("Damaged"))
        {
           movingCommonScript.Jumping(true,jumpPower,1);
           //movingCommonScript.playSE(1, 1.0f, 1.0f);
       }
        //右へ移動
        if (Input.GetKey(KeyCode.D) && !playerAnimator.GetBool("Damaged"))
        {
            this.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            movingCommonScript.Moving(true, 1 ,speed);
        }
        //左へ移動
        else if (Input.GetKey(KeyCode.A) && !playerAnimator.GetBool("Damaged"))
        {
            transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
            movingCommonScript.Moving(true, -1, speed);
        } else
        {
            movingCommonScript.Moving(false, 0, 0);
        }
    }

    //Unityちゃんの足音を再生する（「Running@loop」Animationから呼ばれる）
    void PlayFootstepSE()
    {
        movingCommonScript.playSE(0, 0.4f, 1.0f);
    }

    void OnCollisionEnter(Collision collision){}
}
