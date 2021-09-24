using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviourPun
{
    public GameObject playerPrefab;
    public GameObject Camera;

    private void Awake()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0, 0, 0), Quaternion.identity);
        
        PhotonNetwork.Instantiate(Camera.name, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
