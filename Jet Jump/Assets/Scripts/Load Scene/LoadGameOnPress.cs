using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadGameOnPress : MonoBehaviour
{

    public void LoadSceneFromButton(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void doExitGame()
    {
        Application.Quit();
    }
}
