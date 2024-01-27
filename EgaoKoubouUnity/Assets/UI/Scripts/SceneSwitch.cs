using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public void SwitchGameScene()	//ゲームシーンに切り替える
    {
        SceneManager.LoadScene("GameScene");
    }

    public void SwitchTitleScene()	//タイトルシーンに切り替える
    {
        SceneManager.LoadScene("Title");
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
