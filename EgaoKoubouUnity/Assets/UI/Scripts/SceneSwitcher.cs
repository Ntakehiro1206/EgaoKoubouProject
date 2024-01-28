using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void GoToMain()	//ゲームシーンに切り替える
    {
        SceneManager.LoadScene("EmotionTest");
    }

    public void GoToTitle()	//タイトルシーンに切り替える
    {
        SceneManager.LoadScene("Title");
    }

    public void GoToResult()	//リザルトシーンに切り替える
    {
        SceneManager.LoadScene("Result");
    }

    public void EndGame()     //ゲームを終了する
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;	//ゲームプレイ終了
#else
    Application.Quit();	//ゲームプレイ終了
#endif
    }
}
