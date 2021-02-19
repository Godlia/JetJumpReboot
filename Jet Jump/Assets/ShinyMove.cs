using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShinyMove : MonoBehaviour
{
    public GameObject player;
    public float movespeed = 7f;
    private Rigidbody2D rb;
    private float moveInput;


    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
        moveInput = Input.GetAxis("Horizontal");
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(moveInput * movespeed, rb.velocity.y);
    }
}