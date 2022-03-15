using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject PlayerPrfb;
    public GameObject PlayerCam;

    public bool spawnPlayer;
    public bool spawnCam;
    public bool makeRRespawnPlayer;

    // Start is called before the first frame update
    void Start()
    {
        spawn();
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.R))
        {
            if(makeRRespawnPlayer)
            {
                spawn();
            }
        }
    }

    void spawn() {   
        if(spawnPlayer) {
        Instantiate(PlayerPrfb, new Vector3(0, 10, 0), Quaternion.identity);
        }
        if(spawnCam) {
        Instantiate(PlayerCam, new Vector3(0, 5, -10), Quaternion.identity);
    }
    }
}
