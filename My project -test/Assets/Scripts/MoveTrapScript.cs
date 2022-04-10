using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrapScript : MonoBehaviour
{
   private Vector3 initPosition;
   private Vector3 slideDirection = new Vector3(0, 0, 0);
   private System.Random rand = new System.Random();
   bool positiveDirectionFlag = true;
   public int num;

   float maxWidth;
   float minWidth;
   float maxHeight;
   float minHeight;

    // Start is called before the first frame update
    void Start()
    {
        num = rand.Next(0,5);
    //    Debug.Log(num);
    }

    // Update is called once per frame
    void Update()
    {
      if(CommonScript.phase == CommonScript.Phase.ESCAPEPHASE)
      {
       if(maxWidth == 0)setSlideRange(3,3);

       switch(num){
         case 0:
//           Debug.Log("－");
           slide(1,2,0);
           break;
         case 1:
//           Debug.Log("｜");
           slide(1,0,2);
           break;
         case 2:
//           Debug.Log("/");
           slide(1,2,1);
           break;
         case 3:
//           Debug.Log("\\");
           slide(1,2,-1);
           break;
         case 4:
//           Debug.Log("O");
           circle(0.5f,2);
           break;
        }
      }
    }

    void setSlideRange(int horizon,int vertical)
    {
      Vector3 initPosition = transform.position;
      maxWidth = initPosition.x + horizon;
      minWidth = initPosition.x - horizon;
      maxHeight = initPosition.y + vertical;
      minHeight = initPosition.y - vertical;
    }

    //移動
    void slide(int speed , int horizon , int vertical)
    {
      slideDirection.x = speed * horizon * Time.deltaTime;
      slideDirection.y = speed * vertical * Time.deltaTime;

      if(positiveDirectionFlag)
      {
        transform.position += slideDirection;
        if(transform.position.x > maxWidth)positiveDirectionFlag = false;
        else if(transform.position.y > maxHeight)positiveDirectionFlag = false;
      }
      else{
        transform.position -= slideDirection;
        if(transform.position.x < minWidth)positiveDirectionFlag = true;
        else if(transform.position.y < minHeight)positiveDirectionFlag = true;
      }
    }
    //円移動
    void circle(float speed , int radius)
    {
      slideDirection.x = radius * Mathf.Sin(Mathf.PI * Time.time * speed) * Time.deltaTime;
      slideDirection.y = radius * Mathf.Cos(Mathf.PI * Time.time * speed) * Time.deltaTime;
      transform.position += slideDirection;
    }
}
