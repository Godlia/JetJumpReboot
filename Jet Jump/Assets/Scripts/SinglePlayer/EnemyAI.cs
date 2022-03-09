using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] private GameObject bullet;

    float fireRate;
    float nextFire;
    public float health = 10f;
    public GameObject Player;
    public Transform PlayerT;
    public Transform ThisT;

    [SerializeField]
    private int scoreAward;
    public bool ranged;


    // Use this for initialization
    void Start()
    {
        ThisT = this.GetComponent<Transform>();
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerT = Player.GetComponent<Transform>();
        fireRate = 2f;
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (ranged)
        {
            CheckIfTimeToFire();
        }
        if (ThisT.position.y < -40) { Destroy(this.gameObject); }

        if(health <= 0)
        {
            calculatePoints();
            Destroy(this.gameObject);
        }
    }

    void CheckIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Damage(collision.gameObject.GetComponent<Bullet>().bulldmg);
        }

    }


    void Damage(float dmg) {
        health -= dmg;
    }

    void calculatePoints()
    {
        float points = 0;
        points += scoreAward;
        Score score = GameObject.FindGameObjectWithTag("ScoreCounter").GetComponent<Score>();

        float dist = Vector3.Distance(PlayerT.position, ThisT.position);
        Debug.Log("Distance: " + dist);
        if (dist > 10)
        {
            points *= 1.25f;
            Debug.Log("Longshot!");

        }
        score.setScore(score.getScore() + Mathf.RoundToInt(points));
    }
}