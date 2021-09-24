using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviourPun
{
    public GameObject playerPrefab;
    public GameObject Camera;
    public GameObject myPlayer;

    private void Start()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0, 0, 0), Quaternion.identity);
        PhotonNetwork.Instantiate(Camera.name, new Vector3(0, 0, 0), Quaternion.identity);
        //Camera.GetComponent<CameraFollow>().target = GameObject.FindGameObjectsWithTag("Player");
    }
}
