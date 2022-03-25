using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTrapButtonScript : MonoBehaviour
{
    public GameObject[] buttonGameObjects;
    private Text buttonText;
    private bool buttonFlag = true;

    // Start is called before the first frame update
    void Start()
    {
        buttonText = GetComponentInChildren<Text>();
        SetActiveChildGameObjects(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        if (buttonFlag)
        {
            buttonText.text = "�߂�";
            buttonFlag = false;
            SetActiveChildGameObjects(true);
        }
        else
        {
            buttonText.text = "㩐ݒu";
            buttonFlag = true;
            SetActiveChildGameObjects(false);
        }
    }
    public void Init()
    {
        buttonText.text = "㩐ݒu";
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
