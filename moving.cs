using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving : MonoBehaviour
{
   private bool isGround;//着地判定用
   public Vector3 moveDirection = new Vector3(0,0,0);
   public Vector3 initPositon = new Vector3(0,1,0);
   private float x = 0.06f; //移動速度
   private float y = 3.0f; //ジャンプ力

    // Start is called before the first frame update
    void Start()
    {
      Debug.Log("start");
    }

    // Update is called once per frame
    void Update()
    {
      moveDirection.x = 0;
      moveDirection.y = 0;

      //ジャンプ
      if(isGround == false){
        if(Input.GetKey(KeyCode.W)){
          moveDirection.y =  y;
          transform.position += moveDirection ;
          isGround = true ;
        }
      }

      //落下
      if(isGround == true ){
        //重力計算
        //moveDirection.y -= 2 * Time.deltaTime;
        if(transform.position.y > 1){
          moveDirection.y -= y / 50 ;
        }
        else{
          moveDirection.y = initPositon.y - transform.position.y ;
          isGround = false;
        }
        transform.position += moveDirection ;
      }

      //右へ移動
      if(Input.GetKey(KeyCode.D)){
        moveDirection.x =  x;
        transform.position += moveDirection ;
      }
      //左へ移動
      if(Input.GetKey(KeyCode.A)){
        moveDirection.x =  -1 * x;
        transform.position += moveDirection ;
      }

      Debug.Log(transform.position);

    }
}
