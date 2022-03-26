using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscapePhaseUIScript : MonoBehaviour
{
    public Text labelText;
    public int countDownTimer;
    public TimerScript timerScript;
    private bool escapePhaseInitFlag = false; //逃走フェーズ切り替え時の初期化処理用
    public GameObject[] childGameObjects;
    // Start is called before the first frame update
    void Start()
    {
        childGameObjects = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
//            Debug.Log("childEscape:" + transform.GetChild(i).gameObject);
            childGameObjects[i] = transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //逃走フェーズの処理
        if (CommonScript.phase == CommonScript.Phase.STARTESCAPE || CommonScript.phase == CommonScript.Phase.ESCAPEPHASE)
        {
            if (!escapePhaseInitFlag)
            {
                escapePhaseInitFlag = true;
                StartCoroutine(CountDown(countDownTimer));
            }
            SetActiveChildGameObjects(true);
        }
        else
        {
            escapePhaseInitFlag = false;
            SetActiveChildGameObjects(false);
        }
    }
    //逃走フェーズ開始までのカウントダウン表示
    IEnumerator CountDown(int num)
    {
        labelText.text = num.ToString();
        if (num > 0)
        {
            yield return new WaitForSeconds(1);
            StartCoroutine(CountDown(--num));
        } else
        {
            labelText.text = "Start";
            yield return new WaitForSeconds(1);
            labelText.text = "";
            CommonScript.phase = CommonScript.Phase.ESCAPEPHASE;
            timerScript.StartTimer();
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
