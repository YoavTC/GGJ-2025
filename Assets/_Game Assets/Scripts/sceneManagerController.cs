using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManagerController : MonoBehaviour
{
    public void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void loadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void loadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
}
