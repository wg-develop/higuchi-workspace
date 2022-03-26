using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPhaseUIScript : MonoBehaviour
{
    private GameObject[] childGameObjects;
    // Start is called before the first frame update
    void Start()
    {
        childGameObjects = new GameObject[transform.childCount];
        for (int i=0; i< transform.childCount; i++)
        {
//            Debug.Log("childTrap:" + transform.GetChild(i).gameObject);
            childGameObjects[i] = transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //罠設置フェーズの処理
        if (CommonScript.phase == CommonScript.Phase.TRAPPHASE)
        {
            SetActiveChildGameObjects(true);
        }
        else
        {
            SetActiveChildGameObjects(false);
        }
    }
    public void SetActiveChildGameObjects(bool flag)
    {
        foreach (GameObject childGameObject in childGameObjects)
        {
            childGameObject.SetActive(flag);
        }
    }
}
