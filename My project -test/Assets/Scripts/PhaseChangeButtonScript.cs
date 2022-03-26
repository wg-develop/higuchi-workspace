using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseChangeButtonScript : MonoBehaviour
{
    public TimerScript timerScript;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        changePhase();
    }
    public void changePhase()
    {
        foreach (GameObject trapGameObject in CommonScript.trapGameObjects)
        {
            Destroy(trapGameObject);
        }
        CommonScript.trapGameObjects.Clear();
        timerScript.Timeup();
    }
}
