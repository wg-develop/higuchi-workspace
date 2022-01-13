using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitychanMove : MonoBehaviour
{
  private Animator animator;
    // Start is called before the first frame update
   public Vector3 moveDirection = new Vector3(0,0,0);
   private float x = 0.06f; //移動速度
   private float y = 3.0f; //ジャンプ力

  // キャラクターコントローラの参照
  private CapsuleCollider col;
  private Rigidbody rb;


  private AnimatorStateInfo currentState;
  //アニメーター各ステートの参照
/*
  static int standState = Animator.StringToHash("Base Layer.Standing@loop");
  static int runState = Animator.StringToHash("Base Layer.Running@loop");
  static int jumpState = Animator.StringToHash("Base Layer.jumping@loop");
*/

    void Start()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
    //    state = UnityChanState;
        Debug.Log("start");
    }

    // Update is called once per frame
    void Update()
    {
      moveDirection.x = 0;
      moveDirection.y = 0;

      //ジャンプ
      currentState = animator.GetCurrentAnimatorStateInfo(0);
      if(Input.GetKey(KeyCode.W)){
        jumping(true,currentState.fullPathHash);
      }
/*      if(state.fullPathHash == jumpState){
        animator.SetBool("Jump",false);
      }
      if(Input.GetKey(KeyCode.W)){
        jumping(true);
      }
*/
      //右へ移動
      else if(Input.GetKey(KeyCode.D)){
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
        jumping(false,currentState.fullPathHash);
      }
/*
      //右へ移動
      if(Input.GetKey(KeyCode.D)){
        moveDirection.x =  x;
        transform.position += moveDirection ;
        animator.SetBool("is_running", true);
      }
      //左へ移動
      else if(Input.GetKey(KeyCode.A)){
        moveDirection.x =  -1 * x;
        transform.position += moveDirection ;
        animator.SetBool("is_running", true);
      }
      else{
        animator.SetBool("is_running", false);
      }
*/

    }
    void moving(bool is_move , int val){
      if(is_move){
        moveDirection.x =  x * val;
        transform.position += moveDirection ;
        animator.SetBool("is_running", true);
      }
      else{
        animator.SetBool("is_running", false);
      }
    }

   //floorにいるときだけジャンプできる
   void OnCollisionStay(Collision collision){
     Debug.Log("接触中");
     Debug.Log("キューブタグ: " + collision.gameObject.tag);
     if(collision.gameObject.tag == "floor"){
       Debug.Log("床にいる");
       if(Input.GetKey(KeyCode.W)){
          moveDirection.y =  y;
          transform.position += moveDirection ;
          animator.SetBool("is_jump",true);
        }
     }
   }
   void OnCollisionExit(){}

    void jumping(bool is_jump,int stateVal){
/*    AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
//      Debug.Log(stateVal);
//      if(state.fullPathHash == standState || state.fullPathHash == runState){
      if(is_jump){
       if(stateVal == standState || stateVal == runState){
          animator.SetBool("is_jump",true);
        }
      }else{
         animator.SetBool("is_jump",false);
      }
*/
    }

  }
