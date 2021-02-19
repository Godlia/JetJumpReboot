using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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





    public float jetpower = 20f;
    public float maxfuel = 10f;
    private float fuel = 10f;
    public float fuelregen = 0.02f;
    public float consumption = 0.2f;


    private float health;

    public float maxhealth = 10f;

    public float healthregen = 0.5f;



    private Collider2D[] isGrounded = new Collider2D[1];
    public GameObject myPrefab;

    [SerializeField]
    private float boxLength;
    [SerializeField]
    private float boxHeight;
    [SerializeField]
    private Transform groundPosition;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private GameObject playertag;

    public Slider Slider;

    public Slider healthSlider;

    public ParticleSystem Flames;

    public Transform Nozzle;

    private ParticleSystem.MainModule pMain;

    private void Awake()
    {
        //Bare setter noen variabler
        rb = GetComponent<Rigidbody2D>();
        fuel = maxfuel;
        pMain = Flames.main;
        health = maxhealth;
    }

    private void Update()
    {
        //alt medbevegelse og fuel-bar
        MoveInput = Input.GetAxis("Horizontal");
        isFlying = Input.GetKey(KeyCode.Space);
        Slider.value = fuel;
        healthSlider.value = health;
    }

    private void FixedUpdate()
    {
        //fysikk - Jetpack, fuel og bevegelse

        isGrounded[0] = null;
        Physics2D.OverlapBoxNonAlloc(groundPosition.position, new Vector2(boxLength, boxHeight), 0, isGrounded, groundLayer);

        if (isGrounded[0]) {
            fuel = fuel + fuelregen;
        }

        fuel = fuel > maxfuel ? maxfuel : fuel;
        health = health > maxhealth ? maxhealth : health;

        rb.velocity = new Vector2(MoveInput * moveSpeed, rb.velocity.y);
        health += healthregen;


        if (fuel >= 0.1f)
        {
            if (isFlying)
            {
                rb.AddForce(Vector2.up * jetpower);
                fuel = fuel - consumption;
            }
        }

        if(isFlying) {
            pMain.startSize = 0.02f;
        } else if (isFlying == false) {
            pMain.startSize = 0f;
        }
    }

    private void OnDrawGizmos()
    {
        //Lager en boks som sjekker om jeg er i kontakt med et objekt med taggen "Ground"
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(groundPosition.position, new Vector2(boxLength, boxHeight));
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag.Equals("Enemy")) {
            health -= 3f;
        }
    }
}