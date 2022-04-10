using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingCommonScript : MonoBehaviour
{
    private Vector3 initPosition; //初期位置
    private Vector3 moveDirection = new Vector3(0, 0, 0);
    private Animator animator;
//    private System.Random rand = new System.Random();

    private bool is_ground;// ジャンプ用フラグ
    private float jumpPower;
    private bool hitBlockObject = false; //移動時壁抜け防止用
    private bool saveHitPositionFlag = false; //ジャンプ時の壁抜け防止用
    private float hitObjectRotation ;
    private float hitObjectPosition ;

    public float volume; //音量
    [SerializeField] AudioClip[] clips; //オーディオ
    protected AudioSource source; //オーディオ

  void Start()
  {
    initPosition = transform.position;
    source = GetComponents<AudioSource>()[0];
    animator = GetComponent<Animator>();
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
      BasicProcess();
    }
  }
  //基本処理
   void BasicProcess()
  {
    //   Debug.Log("velocity:" + rigidBody.velocity);
    moveDirection.x = 0;
    moveDirection.y = 0;
//    transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
    customJumpAnimation();
  }

  //移動共通処理
  /// <summary>
  /// 走らせます
  /// </summary>
  /// <param name="moveFlg">動作フラグ</param>
  /// <param name="val">進む方向</param>
  public void Moving(bool moveFlg, int val ,float speed)
  {
    if (moveFlg)
    {
      moveDirection.x = speed * val * Time.deltaTime;
      moveDirection.y = 0;
      if(hitBlockObject){
        moveDirection.x = 0;
      }
      transform.position += moveDirection;
      animator.SetBool("is_running", true);
    }
    else
    {
      animator.SetBool("is_running", false);
    }
  }

  //ジャンプ共通処理
  /// <summary>
  ///  ジャンプさせます
  /// </summary>
  /// <param name="jumpFlg">ジャンプフラグ</param>
  public void Jumping(bool jumpFlg,float jumpPower,int SE)
  {
    if(is_ground){
      is_ground = false;
      saveHitPositionFlag = true;
      playSE(1, SE, 1.0f);
    }
    if(jumpFlg && !is_ground)
    {
      animator.SetBool("is_jump", true);
    }
    this.jumpPower = jumpPower;
  }

  void customJumpAnimation()
  {
    //ジャンプアニメーションの調整
    if (animator.GetCurrentAnimatorStateInfo(0).IsName("JumpToTop"))
    {
      moveDirection.y = this.jumpPower * Time.deltaTime;
      if(hitObjectPosition != 0 ){
        moveDirection.y = 0;
      }
      transform.position += moveDirection;
    }
    if (animator.GetCurrentAnimatorStateInfo(0).IsName("fall"))
    {
      if (moveDirection.y < 0.7)
      {
        hitObjectPosition = 0;
        animator.SetBool("is_jump", false);
      }
    }
  }

  //効果音再生
  public void playSE(int clipNum, float volume, float pitch)
  {
    source.volume = volume;
    source.pitch = pitch;
    source.PlayOneShot(clips[clipNum]);
  }

  void OnCollisionEnter(Collision collision)
  {
    //オブジェクト衝突時の状態保存
    if(collision.gameObject.tag == "block-object")
    {
      hitBlockObject　= true;
      //x軸
      if(hitBlockObject){
        hitObjectRotation = transform.localEulerAngles.y;
      }
      //y軸
      if(saveHitPositionFlag){
        hitObjectPosition = transform.position.y;
      }
    }
    //壁ぬけしたときの初期化用
    if(collision.gameObject.tag == "end-line"){
      transform.position = initPosition;
    }
  }

  void OnCollisionStay(Collision collision)
  {
    //floorにいるときだけジャンプできる
    if (collision.gameObject.tag == "floor")
    {
      is_ground = true;
      saveHitPositionFlag = false;
    }
    //壁抜け防止用　
    if(collision.gameObject.tag == "block-object")
    {
      //x軸制御
      if(hitObjectRotation != transform.localEulerAngles.y && hitObjectRotation != 0 ){
        hitObjectRotation = 0;
        hitObjectPosition = 0;
        hitBlockObject = false;
      }
    }
  }

  void OnCollisionExit(Collision collision)
  {
    hitBlockObject = false;
  }
}
