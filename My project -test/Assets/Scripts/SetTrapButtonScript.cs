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
        setActiveChildGameObjects(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        if (buttonFlag)
        {
            buttonText.text = "ñﬂÇÈ";
            buttonFlag = false;
            setActiveChildGameObjects(true);
        }
        else
        {
            buttonText.text = "„©ê›íu";
            buttonFlag = true;
            setActiveChildGameObjects(false);
        }
    }

    public void setActiveChildGameObjects(bool flag)
    {
        for (int i = 0; i < buttonGameObjects.Length; i++)
        {
            buttonGameObjects[i].SetActive(flag);
        }
    }
}
