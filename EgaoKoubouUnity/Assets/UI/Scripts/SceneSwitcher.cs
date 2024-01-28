using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void GoToMain()	//ゲームシーンに切り替える
    {
        SceneManager.LoadScene("Main");
    }

    public void GoToTitle()	//タイトルシーンに切り替える
    {
        SceneManager.LoadScene("Title");
    }

    public void GoToResult()	//リザルトシーンに切り替える
    {
        SceneManager.LoadScene("Result");
    }
}
