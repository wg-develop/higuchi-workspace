using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPhaseButtonScript : MonoBehaviour
{
    public GameObject trapPhaseUIGameObject;
    public GameObject debugUIGameObject;
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
        CommonScript.phaseFlag = false;
        debugUIGameObject.SetActive(true);
        trapPhaseUIGameObject.SetActive(false);
    }
}
