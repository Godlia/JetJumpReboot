using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class LoadGameOnPress : NetworkBehaviour
{
    public String SceneName;
    public KeyCode Key;

    public bool loadWithNetcode;

    public void LoadSceneFromButton(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }


    public void Update()
    {
        if(Input.GetKeyUp(Key))
        {
            SceneManager.LoadScene(SceneName);
        }
    }

    public void doExitGame()
    {
        Application.Quit();
    }

    //network scenemanager loades sceneName
    public void LoadScene(string SceneName)
    {
        if (loadWithNetcode)
        {
            //NetworkSceneManager.LoadScene(SceneName);
        }
        else
        {
            SceneManager.LoadScene(SceneName);
        }
    }

    
    

}
