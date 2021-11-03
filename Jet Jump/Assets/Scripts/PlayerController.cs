using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
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
    public float fuel = 10f;
    public float fuelregen = 0.02f;
    public float consumption = 0.2f;


    public float health;

    public float maxhealth = 10f;

    private float healthregen = 0.01f;

    public float startHealAfterTime;
    public float regenCoolDown;




    public Slider Slider;

    public Slider healthSlider;

    public ParticleSystem Flames;

    public Transform Nozzle;



    void Awake()
    {

    }

    private void Start()
    {
        //Bare setter noen variabler
        rb = GetComponent<Rigidbody2D>();
        fuel = maxfuel;
        health = maxhealth;
        Slider = GameObject.FindGameObjectWithTag("FuelSlider").GetComponent<Slider>();
        healthSlider = GameObject.FindGameObjectWithTag("HealthSlider").GetComponent<Slider>();
        SpriteRender = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();


    }

    private void Update()
    {
        //alt medbevegelse og fuel & health-bar
        MoveInput = Input.GetAxisRaw("Horizontal");
        isFlying = Input.GetKey(KeyCode.Space);
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


            if(playerpos.transform.position.y <= -40)
            {
                playerpos.transform.position = new Vector3(0, 0, 0);
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