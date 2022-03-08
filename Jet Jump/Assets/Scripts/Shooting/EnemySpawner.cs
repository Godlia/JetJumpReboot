using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Finn enemy prefaben
    public GameObject EnemyPrfb;
    //Finn spawner posisjonen
    private Vector3 spawnPos;
    [SerializeField]
    //Hvor mange enemies er igjen til en hver tid
    private int enemiesleft;

    // Update is called once per frame
    void Update()
    {
        //finn alle enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesleft = enemies.Length;
        //Hvis alle enemies er borte, spawner vi en ny bølge med 2 (parameter) enemies
        if(enemiesleft == 0)
        {
            wave(2);
        }

    }

    //Spawner en bølge med antall enemies som parameter
    void wave(int size)
    {
        for (int i = 0; i < size; i++)
        {
            spawnPos = new Vector3(Random.Range(-10, 10), 3, 0);
            Instantiate(EnemyPrfb, spawnPos, Quaternion.identity);
        }
    }
}
