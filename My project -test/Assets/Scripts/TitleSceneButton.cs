using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleSceneButton : MonoBehaviour
{

    /// <summary>
    /// Startボタン押下時処理
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    /// <summary>
    /// Howtoボタン押下時処理
    /// </summary>
    public void HowToGame()
    {
        //遊び方を提示
        //新シーンへ遷移かポップアップ等にするか要検討
    }

}
