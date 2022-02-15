using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitychanMove : MonoBehaviour
{
  private Animator animator;
    // Start is called before the first frame update
   private Vector3 moveDirection = new Vector3(0,0,0);
   public float x; //移動速度
   public float y; //ジャンプ力
   public float volume; //音量
    [SerializeField] AudioClip[] clips; //オーディオ
    protected AudioSource source; //オーディオ

    // キャラクターコントローラの参照
    private CapsuleCollider col;
  private Rigidbody rb;

 // ジャンプ用フラグ
  private bool is_ground;

  private AnimatorStateInfo currentState;

    void Start()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        source = GetComponents<AudioSource>()[0];
        Debug.Log("start");
    }

    // Update is called once per frame
    void Update()
    {
      //Debug.Log("フレーム間の経過秒数：" + Time.deltaTime);
      moveDirection.x = 0;
      moveDirection.y = 0;
      //ジャンプ
      if(Input.GetKeyDown(KeyCode.W)){
        if(is_ground){
          is_ground = false;

          jumpping(true);
        }
      }

     //ジャンプアニメーションの調整
     if(animator.GetCurrentAnimatorStateInfo(0).IsName("JumpToTop")){
       moveDirection.y =  y * Time.deltaTime ;
       transform.position += moveDirection ;
     }
     if(animator.GetCurrentAnimatorStateInfo(0).IsName("fall")){
       if(moveDirection.y  < 0.7){
         animator.SetBool("is_jump",false);
      }
     }

      //右へ移動
      if(Input.GetKey(KeyCode.D)){
        this.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
        moving(true,1);
      }
      //左へ移動
      else if(Input.GetKey(KeyCode.A)){
        transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
        moving(true,-1);
      }
      else{
        moving(false,0);
      }
    }

    void moving(bool is_move , int val){
      if(is_move){
        moveDirection.x =  x * val * Time.deltaTime;
        transform.position += moveDirection ;
        animator.SetBool("is_running", true);
      }
      else{
        animator.SetBool("is_running", false);
      }
    }

    void jumpping(bool is_jump){
      if(is_jump){
        animator.SetBool("is_jump",true);

        //ジャンプ音
        source.volume = volume;
        source.PlayOneShot(clips[1]);
      }
    }

   //floorにいるときだけジャンプできる
   void OnCollisionStay(Collision collision){
     if(collision.gameObject.tag == "floor"){
       is_ground=true;
       jumpping(false);
     }
   }
   void OnCollisionExit(){}

    //Unityちゃんの足音を再生する（「Running@loop」Animationから呼ばれる）
    public void PlayFootstepSE(){
        source.volume = volume * 0.4f;
        source.PlayOneShot(clips[0]);
    }
}
