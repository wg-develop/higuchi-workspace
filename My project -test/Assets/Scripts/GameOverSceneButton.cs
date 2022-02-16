using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverSceneButton : MonoBehaviour
{
    /// <summary>
    /// Restartボタン押下時にゲーム画面へ戻る処理
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    /// <summary>
    /// Endボタン押下時にプログラムを終了する処理
    /// </summary>
    public void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;   // UnityEditorの実行を停止する処理
#else
        Application.Quit();                                // ゲームを終了する処理
#endif
    }

}
