using UnityEngine;
using UnityEngine.SceneManagement;
public class GoToTheGame : MonoBehaviour
{
    public string sceneName;
    public void loadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
