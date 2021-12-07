﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

    public Slider Slider;

    public Slider healthSlider;


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
    }

    private void Update()
    {
        if (healthSlider == null && Slider == null)
        {

            Slider = GameObject.FindGameObjectWithTag("FuelSlider").GetComponent<Slider>();
            healthSlider = GameObject.FindGameObjectWithTag("HealthSlider").GetComponent<Slider>();
        }


        //alt medbevegelse og fuel & health-bar
        MoveInput = Input.GetAxisRaw("Horizontal");
        isFlying = Input.GetKey(KeyCode.Space);
        Debug.Log(MoveInput);
    }

    void FixedUpdate()
    {
        //fysikk - Jetpack, fuel og bevegelse

        Slider.value = fuel;
        healthSlider.value = health;
        if (rb.velocity.y == 0) { isGrounded = true; } else { isGrounded = false; }

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
            SceneManager.LoadScene("Main Menu");
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
        health -= 3.34f;
        regenCoolDown = Time.time + startHealAfterTime;

    }
}