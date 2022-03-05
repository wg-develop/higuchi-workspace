using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameOverSceneButton : MonoBehaviour
{

    /// <summary>
    /// Restart�{�^���������ɃQ�[����ʂ֖߂鏈��
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    /// <summary>
    /// ReturnTitle�{�^���������Ƀ^�C�g����ʂ֖߂鏈��
    /// </summary>
    public void ReturnTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

    /// <summary>
    /// End�{�^���������Ƀv���O�������I�����鏈��
    /// </summary>
    public void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;   // UnityEditor�̎��s���~���鏈��
#else
        Application.Quit();                                // �Q�[�����I�����鏈��
#endif
    }

}
