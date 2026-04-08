using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    [SerializeField] string gameSceneName;
    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void StartScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
