using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrfb;
    private Vector3 spawnPos;
    private int enemiesleft;
    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesleft = enemies.Length;
        if(enemiesleft == 0)
        {
            wave();
        }
    }

    void wave()
    {
        spawnPos = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
        Instantiate(EnemyPrfb, spawnPos, Quaternion.identity);
    }
}
