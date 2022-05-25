using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    private ArrayList players;
    public int playerCount;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //find all players and push into arraylist
        players = new ArrayList();
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        playerCount = playerObjects.Length;
    }
}
