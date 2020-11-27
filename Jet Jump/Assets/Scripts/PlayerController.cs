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

    private Rigidbody2D rb;
    private float MoveInput;

    private bool isFlying = false;

    public float jetpower = 20f;
    public float maxfuel = 10f;
    private float fuel = 10f;
    public float fuelregen = 0.02f;
    public float consumption = 0.2f;

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

    private void Awake()
    {
        //Bare setter noen variabler
        rb = GetComponent<Rigidbody2D>();
        fuel = maxfuel;
    }

    private void Update()
    {
        //alt medbevegelse og fuel-bar
        MoveInput = Input.GetAxis("Horizontal");
        isFlying = Input.GetKey(KeyCode.Space);
        Slider.value = fuel;
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

        rb.velocity = new Vector2(MoveInput * moveSpeed, rb.velocity.y);
        

        if (fuel >= 0.1f)
        {
            if (isFlying)
            {
                rb.AddForce(Vector2.up * jetpower);
                fuel = fuel - consumption;
            }
        }
    }

    private void OnDrawGizmos()
    {
        //Lager en boks som sjekker om jeg er i kontakt med et objekt med taggen "Ground"
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(groundPosition.position, new Vector2(boxLength, boxHeight));
    }
}