using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamegeTrapScript : MonoBehaviour
{
    private GameObject player;
    private Animator playerAnimator;
    private Rigidbody rigidBody;
    public float impulse;
    public float damageTime; //被ダメ時間
    private float countDamageTime = 0; //被ダメ時間計算用
    private bool momentumCancelFlag = false; //吹っ飛び緩和フラグ
    private GameObject childGameObject0; //点滅用？
    private GameObject childGameObject1; //点滅用？

    // Start is called before the first frame update
    void Start()
    {
      player = GameObject.Find("player_unitychan");
      playerAnimator = player.GetComponent<Animator>();
      rigidBody = player.GetComponent<Rigidbody>();
      childGameObject0 = player.transform.GetChild(0).gameObject;
      childGameObject1 = player.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
      //被ダメした場合の処理
      if (playerAnimator.GetBool("Damaged")) DamageMotion();
    }

    public void DamageMotion()
    {
        //被ダメ後の無敵時間
        if (playerAnimator.GetBool("Damaged"))
        {
            //Color color = renderer.material.color;
            //color.a = Mathf.Sin(Time.time) / 2 + 0.5f;
            countDamageTime++;
            childGameObject0.SetActive(!childGameObject0.activeSelf);
            childGameObject1.SetActive(!childGameObject1.activeSelf);
            if (countDamageTime > damageTime * 0.25 && !momentumCancelFlag)
            {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x * 0.1f, rigidBody.velocity.y * 0.1f, rigidBody.velocity.z);
                momentumCancelFlag = true;
            }
            if (countDamageTime > damageTime)
            {
                countDamageTime = 0;
                playerAnimator.SetBool("Damaged", false);
                momentumCancelFlag = false;
                //                color.a = 1.0f;
                childGameObject0.SetActive(true);
                childGameObject1.SetActive(true);
            }
            //            renderer.material.color = color;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
       if (collision.gameObject == player && !playerAnimator.GetBool("Damaged"))
        {
            if (player.transform.localEulerAngles.y == 90)
            {
                rigidBody.AddForce(new Vector3(-1.0f, 1.0f, 0.0f) * impulse, ForceMode.Force);
            }
            else if (player.transform.localEulerAngles.y == 270)
            {
                rigidBody.AddForce(new Vector3(1.0f, 1.0f, 0.0f) * impulse, ForceMode.Force);
            }
            playerAnimator.SetBool("Damaged", true);
        }
    }
}
