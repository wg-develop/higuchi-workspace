using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public float limit;
    public bool countFlag;
    private float timer;
    private Text text;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = limit;
        text = GetComponent<Text>();
        CountTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if(countFlag == true)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer = 0;
                Timeup();
            }
            CountTimer();
        }
//        Debug.Log("経過時間(秒)" + Time.time);
//        Debug.Log("delta(秒)" + Time.deltaTime);
    }
    private void CountTimer()
    {
        int minute = (int)Math.Floor(timer / 60.0f);
        float seconds = (int)Math.Floor(timer % 60.0f);
        text.text = minute.ToString("00") + ":" + seconds.ToString("00");
    }
    public float GetTimer()
    {
        return this.timer;
    }
    public void SetTimer(float timer)
    {
        this.timer = timer;
    }
    public void StartTimer()
    {
        this.countFlag = true;
    }
    public void StopTimer()
    {
        this.countFlag = false;
        Debug.Log("残り秒数：" + timer);
    }
    public void ResetTimer()
    {
        this.timer = limit;
        CountTimer();
    }
    public void Display()
    {
        this.text.enabled = true;
    }
    public void Hide()
    {
        this.text.enabled = false;
    }
    private void Timeup()
    {
        //00:00の処理を書く
        StopTimer();
        this.countFlag = false;
    }
}
