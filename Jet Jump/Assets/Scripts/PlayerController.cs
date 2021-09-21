using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public float maxfuel = 10f;
    public float fuel = 10f;
    public float fuelregen = 0.02f;
    public float consumption = 0.2f;


    private float health;

    public float maxhealth = 10f;

    public float healthregen = 0.1f;

    public float startHealAfterTime;
    public float regenCoolDown;




    public Slider Slider;

    public Slider healthSlider;

    public ParticleSystem Flames;

    public Transform Nozzle;


    private void Awake()
    {
        //Bare setter noen variabler
        rb = GetComponent<Rigidbody2D>();
        fuel = maxfuel;
        health = maxhealth;
    }

    private void Update()
    {
        //alt medbevegelse og fuel & health-bar
        MoveInput = Input.GetAxis("Horizontal");
        isFlying = Input.GetKey(KeyCode.Space);
        Slider.value = fuel;
        healthSlider.value = health;
    }

    private void FixedUpdate()
    {
        //fysikk - Jetpack, fuel og bevegelse

    if(rb.velocity.y == 0) {isGrounded = true;} else {isGrounded = false;}

        if (isGrounded) {
            fuel = fuel + fuelregen;
        }

        fuel = fuel > maxfuel ? maxfuel : fuel;

        rb.velocity = new Vector2(MoveInput * moveSpeed, rb.velocity.y);


        if (fuel >= 0.1f)
        {
            if (isFlying)
            {
                rb.AddForce(Vector2.up * jetpower);
                fuel = fuel - consumption;
            }
        }
  
    
    
    health = health > maxhealth ? maxhealth : health;
    if(health > 0) {    
    if(Time.time > regenCoolDown) {
        health += healthregen;
    } else if(health <= 0) {
        Destroy(this.gameObject);
    }
    }

}

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag.Equals("Enemy")) {
            Damage();
        } else if(collision.gameObject.tag.Equals("EnemyBullet")) {
            Damage();
        }
    }
    
    void Damage() {
        health -= 3f;
        regenCoolDown = Time.time + startHealAfterTime;
    }
}