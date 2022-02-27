using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraScript : MonoBehaviour
{
    private GameObject player; //SDUnityChan 除法格納用
    private Vector3 offset; //相対距離取得用

    // Start is called before the first frame update
    void Start()
    {
      //SDUnityChan
      this.player = GameObject.Find("SD_unitychan_humanoid");
      //MaincameraとSDUnityChanの相対距離
      offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
      //Debug.Log("SDUnityChan＠カメラ"+player.transform.position);
      //Maincameraを追従（SDUnitychanにあわせて移動）
      transform.position = player.transform.position + offset;
    }
}
