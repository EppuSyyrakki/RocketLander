using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterUI : MonoBehaviour
{
    Scene scene;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    public void NextScene()
    {
        SceneManager.LoadScene(scene.buildIndex + 1);
    }

    public void RetryCurrent()
    {
        SceneManager.LoadScene(scene.buildIndex);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
