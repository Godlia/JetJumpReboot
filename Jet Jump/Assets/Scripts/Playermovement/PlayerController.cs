using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    // Dette er bare masse variable man kan endre i spillet
    public float moveSpeed = 3f;
    public Transform playerpos;



    public SpriteRenderer SpriteRender;
    private Rigidbody2D rb;
    private float MoveInput;

    private bool isFlying = false;
    private bool isGrounded = false;





    public float jetpower = 20f;
    private float maxfuel = 10f;
    private float fuel = 10f;
    public float fuelregen = 0.02f;
    public float consumption = 0.2f;


    private float health;

    public float maxhealth = 10f;

    private float healthregen = 0.01f;

    public float startHealAfterTime;
    private float regenCoolDown;

    public Slider fuelSlider;

    public Slider healthSlider;

    public Upgrades upgrades;

    private Vector2 oldNetPosisition;

    [SerializeField]
    private NetworkVariable<Vector2> networkPosition;

    [SerializeField]
    private Component netObj;



    void Start()
    {
        //Bare setter noen variabler
        rb = GetComponent<Rigidbody2D>();
        fuel = maxfuel;
        health = maxhealth;
        SpriteRender = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        netObj = GetComponent<NetworkObject>();
        upgrades = GameObject.Find("GameplayManager").GetComponent<Upgrades>();
        fuelSlider = GameObject.FindGameObjectWithTag("FuelSlider").GetComponent<Slider>();
        healthSlider = GameObject.FindGameObjectWithTag("HealthSlider").GetComponent<Slider>();
    }

    private void Update()
    {
        //alt medbevegelse og fuel & health-bar
        MoveInput = Input.GetAxisRaw("Horizontal");
        isFlying = Input.GetKey(KeyCode.Space);
        fuelSlider.maxValue = maxfuel;

    }



    void FixedUpdate()
    {
        //fysikk - Jetpack, fuel og bevegelse

        fuelSlider.value = fuel;
        healthSlider.value = health;

        //ternary if
        isGrounded = rb.velocity.y == 0 ? true : false;


        if (isGrounded)
        {
            fuel += fuelregen;
        }


        if (playerpos.transform.position.y <= -40)
        {
            playerpos.transform.position = new Vector3(0, 3, 0);
            rb.velocity = new Vector3(0, 0, 0);
        }

        fuel = fuel > maxfuel ? maxfuel : fuel;

        playerpos.transform.Translate(MoveInput * moveSpeed * Time.deltaTime, 0, 0);


        if (isFlying && fuel >= 0.2f)
        {
            rb.AddForce(Vector2.up * jetpower * Time.deltaTime);
            fuel = fuel - consumption;
        }



        health = health > maxhealth ? maxhealth : health;

        if (health > 0)
        {
            if (Time.time > regenCoolDown)
            {
                health += healthregen;
            }
        }
        else
        {   
            Destroy(this.gameObject);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy") || collision.gameObject.tag.Equals("Bullet") || collision.gameObject.tag.Equals("EnemyBullet"))
        {
            Damage();
        }

    }

    void Damage()
    {
        health -= Random.Range(3f, 3.5f);
        regenCoolDown = Time.time + startHealAfterTime;

    }

    public void Upgrade()
    {
        moveSpeed *= upgrades.getMarkiplier("speed");
        maxhealth *= upgrades.getMarkiplier("health");
        maxfuel *= upgrades.getMarkiplier("fuel");

    }

}
