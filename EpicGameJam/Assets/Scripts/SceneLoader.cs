using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneName;

    public bool onStart = false;

    private void Start ()
    {
        if (onStart)
        {
            LoadScene();
        }
    }

    public void LoadScene ()
    {
        SceneManager.LoadScene(sceneName);
    }
    
    public void LoadScene (string sceneName)
    {
        this.sceneName = sceneName;
        LoadScene();
    }
}
