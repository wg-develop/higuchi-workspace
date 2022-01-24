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
            CountTimer();
        }
//        Debug.Log("Œo‰ßŽžŠÔ(•b)" + Time.time);
//        Debug.Log("delta(•b)" + Time.deltaTime);
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
}
