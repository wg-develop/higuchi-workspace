using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerResetButtonScript : MonoBehaviour
{
    public GameObject timerGameObject;
    private TimerScript timerScript;

    // Start is called before the first frame update
    void Start()
    {
        timerScript = timerGameObject.GetComponent<TimerScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        timerScript.ResetTimer();
    }
}
