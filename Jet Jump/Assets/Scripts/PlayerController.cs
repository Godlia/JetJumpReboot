﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

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


    PhotonView view;

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
        view = GetComponent<PhotonView>();


    }

    private void Update()
    {
        //alt medbevegelse og fuel & health-bar
        MoveInput = Input.GetAxisRaw("Horizontal");
        isFlying = Input.GetKey(KeyCode.Space);
        Debug.Log(fuel);
    }

    void FixedUpdate()
    {
        //fysikk - Jetpack, fuel og bevegelse

        if (view.IsMine)
        {
            Slider.value = fuel;
            healthSlider.value = health;
            if (rb.velocity.y == 0) { isGrounded = true; } else { isGrounded = false; }

            if (isGrounded)
            {
                fuel += fuelregen;
            }

            fuel = fuel > maxfuel ? maxfuel : fuel;

            playerpos.transform.Translate(MoveInput * moveSpeed * Time.deltaTime, 0, 0);



            if (fuel >= 0.1f)
            {
                if (isFlying)
                {
                    rb.AddForce(Vector2.up * jetpower * Time.deltaTime);
                    fuel = fuel - consumption;
                }
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
                Debug.Log("yuh");
                Destroy(this.gameObject);
                SceneManager.LoadScene("Main Menu");
            }

        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (view.IsMine)
        {
            if (collision.gameObject.tag.Equals("Enemy"))
            {
                Damage();
            }
            else if (collision.gameObject.tag.Equals("EnemyBullet"))
            {
                Damage();
            }
        }
    }

    void Damage()
    {
        if (view.IsMine)
        {
            health -= 3f;
            regenCoolDown = Time.time + startHealAfterTime;
        }
    }
}