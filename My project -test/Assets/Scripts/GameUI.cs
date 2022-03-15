using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public GameObject cameraGameObject;
    public GameObject unitychanGameObject;
    public GameObject enemyGameObject;
    public Vector3 cameraInitPosition;
    public Vector3 unitychanInitPosition;
    public Vector3 enemyInitPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cameraGameObject.transform.position = cameraInitPosition;
        unitychanGameObject.transform.position = unitychanInitPosition;
        enemyGameObject.transform.position = enemyInitPosition;
    }
}
