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
    public GameObject gameUIGameObject;
    private GameUIScript gameUIScript;
    public GameObject trapPhaseUIGameObject;
    public GameObject debugUIGameObject;

    // Start is called before the first frame update
    void Start()
    {
        buttonText = GetComponentInChildren<Text>();
        timerScript = timerGameObject.GetComponent<TimerScript>();
        gameUIScript = gameUIGameObject.GetComponent<GameUIScript>();
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

            changePhase();
        }
    }
    public void changePhase()
    {
        gameUIScript.initPosition();
        CommonScript.phaseFlag = true;
        trapPhaseUIGameObject.SetActive(true);
        debugUIGameObject.SetActive(false);
        foreach (GameObject trapGameObject in CommonScript.trapGameObjects)
        {
            Destroy(trapGameObject);
        }
        CommonScript.trapGameObjects.Clear();
    }
}
