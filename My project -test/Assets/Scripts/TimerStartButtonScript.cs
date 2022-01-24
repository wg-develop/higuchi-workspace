using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerStartButtonScript : MonoBehaviour
{
    public GameObject timerGameObject;
    private TimerScript timerScript;
    private Text buttonText;
    private bool buttonFlag = true;

    // Start is called before the first frame update
    void Start()
    {
        buttonText = GetComponentInChildren<Text>();
        timerScript = timerGameObject.GetComponent<TimerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if (buttonFlag)
        {
            buttonText.text = "Stop";
            timerScript.StartTimer();
            buttonFlag = false;
        }
        else
        {
            buttonText.text = "Start";
            timerScript.StopTimer();
            buttonFlag = true;
            Debug.Log("écÇËïbêîÅF" + timerScript.GetTimer());
        }
    }
}
