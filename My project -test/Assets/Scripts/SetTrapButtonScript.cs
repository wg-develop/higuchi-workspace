using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTrapButtonScript : MonoBehaviour
{
    public GameObject[] buttonGameObjects;
    private Text buttonText;
    private bool buttonFlag = true;
    private bool trapPhaseInitFlag = false; //罠設置フェーズ切り替え時の初期化処理用

    // Start is called before the first frame update
    void Start()
    {
        buttonText = GetComponentInChildren<Text>();
        SetActiveChildGameObjects(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (CommonScript.phase == CommonScript.Phase.TRAPPHASE)
        {
            if (!trapPhaseInitFlag)
            {
                Init();
                trapPhaseInitFlag = true;
            }
        }
        else
        {
            trapPhaseInitFlag = false;
        }
    }

    public void OnClick()
    {
        if (buttonFlag)
        {
            buttonText.text = "戻る";
            buttonFlag = false;
            SetActiveChildGameObjects(true);
        }
        else
        {
            buttonText.text = "罠設置";
            buttonFlag = true;
            SetActiveChildGameObjects(false);
        }
    }
    public void Init()
    {
        buttonText.text = "罠設置";
        buttonFlag = true;
        SetActiveChildGameObjects(false);
    }

    public void SetActiveChildGameObjects(bool flag)
    {
        foreach (GameObject buttonGameObject in buttonGameObjects)
        {
            buttonGameObject.SetActive(flag);
        }
    }
}
