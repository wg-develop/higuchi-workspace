using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEscapeTime : MonoBehaviour
{
    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        //ì¶ëñéûä‘Çï\é¶Ç∑ÇÈ
        EscapeTime();
    }

    public void EscapeTime()
    {
        float escapeTime = 120.0f - EnemyMove.remainingTime;
        int minute = (int)Math.Floor(escapeTime / 60.0f);
        float seconds = (int)Math.Floor(escapeTime % 60.0f);
        text.text = "Escape time Åy " + minute.ToString("00") + ":" + seconds.ToString("00") + " Åz";
    }

}
