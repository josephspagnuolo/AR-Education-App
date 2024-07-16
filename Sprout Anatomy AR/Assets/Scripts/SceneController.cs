using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public static string nextScene;

    public void SetNextScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("AROptionsScene");
    }

    public void LoadNextScene(string mode)
    {
        if (mode == "Free")
	{
	    SceneManager.LoadScene(nextScene + "Free");
	}
	else
	{
            SceneManager.LoadScene(nextScene);
	}
    }

    public void SwitchScenes(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
