using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Networking : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject HUD;
    public GameObject stateCam;
    private void Start()
    {
        //Important for linux server (It will scale infinitly if not set)
        Application.targetFrameRate = 30;
        HUD = GameObject.FindGameObjectWithTag("HUD");
        HUD.SetActive(false);
        stateCam = GameObject.FindGameObjectWithTag("StateCam");
    }
    void OnGUI()
    {
        //The graphical buttons you see on the screen are handled here.
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            StartButtons();
        }
        else
        {
            StatusLabels();
            Destroy(stateCam);
            HUD.SetActive(true);

        }

        GUILayout.EndArea();
    }

    static void StartButtons()
    {
        //Static start button and what they start upon being clicked.
        if (GUILayout.Button("Host")) NetworkManager.Singleton.StartHost();
        if (GUILayout.Button("Client")) NetworkManager.Singleton.StartClient();
        if (GUILayout.Button("Server")) NetworkManager.Singleton.StartServer();
    }

    static void StatusLabels()
    {
        //Change the labels of the buttons if "Host"/"Client"/"Server" buttons are pressed.
        var mode = NetworkManager.Singleton.IsHost ?
            "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

        GUILayout.Label("Transport: " +
            NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
        GUILayout.Label("Mode: " + mode);
    }
}
