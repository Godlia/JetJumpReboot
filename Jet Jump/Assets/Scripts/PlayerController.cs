using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Player;
    public float speed = 10f;
    public Rigidbody2D rb;

    public float fuelregen = 0.08f;
    public float consumption = -0.1f;
    public bool IsGrounded = true;
    public float jetpackforce = 20f;
    public float jetpackfuel = 1f;
    public float maxfuel = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = Player.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        {
            if(IsGrounded == true);
            jetpackfuel = jetpackfuel + fuelregen;
            if (jetpackfuel >= maxfuel)
                jetpackfuel = maxfuel;

            Debug.Log(jetpackfuel);
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        IsGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        IsGrounded = false;
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow))
            rb.AddForce(Vector2.right * speed);
    
        if (Input.GetKey(KeyCode.LeftArrow))
            rb.AddForce(Vector2.left * speed);

        if(jetpackfuel > 0.2)
        {
            if (Input.GetKey(KeyCode.UpArrow))
                rb.AddForce(Vector2.up * jetpackforce);
            jetpackfuel = jetpackfuel + consumption;

        }
    }





}