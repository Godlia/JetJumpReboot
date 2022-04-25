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

    [SerializeField] private float visionRange;


    // Use this for initialization
    void Start()
    {
        ThisT = this.GetComponent<Transform>();
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerT = Player.GetComponent<Transform>();
        fireRate = 2f;
        nextFire = Time.time;
        if (Random.Range(0, 2) == 0)
        {
            ranged = true;
        }
        else
        {
            ranged = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //
        if (ranged)
        {
            //Lag en raycast fra fienden mot spilleren, om den treffer spilleren, så skal den skyte
            RaycastHit2D hit;
            hit = Physics2D.Raycast(ThisT.position, new Vector2(PlayerT.position.x - ThisT.position.x, PlayerT.position.y - ThisT.position.y).normalized, visionRange);
            Debug.Log(hit.collider.gameObject.tag);
            Debug.DrawRay(ThisT.position, new Vector2(PlayerT.position.x - ThisT.position.x, PlayerT.position.y - ThisT.position.y).normalized * visionRange, Color.red, 0.8f);
            if (hit.collider == null || hit.collider.gameObject.tag == "Player")
            {
                CheckIfTimeToFire();
            }
        }

        //Hvis den faller av kartet, så blir den fjernet
        if (ThisT.position.y < -40)
        {
            Destroy(this.gameObject);
        }

        //Hvis den dør, så blir den fjernet
        if (health <= 0)
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


    void Damage(float dmg)
    {
        health -= dmg;
    }

    //Funksjon som regner ut poeng for å drepe fienden
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