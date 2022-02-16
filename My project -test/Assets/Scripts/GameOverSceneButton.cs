using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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
