using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class LoadGameOnPress : MonoBehaviour
{
    public String SceneName;
    public KeyCode Key;

    public void LoadSceneFromButton(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }


    public void Update()
    {
        if(Input.GetKeyUp(Key))
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene(SceneName);
        }
    }

    public void doExitGame()
    {
        Application.Quit();
    }
}
