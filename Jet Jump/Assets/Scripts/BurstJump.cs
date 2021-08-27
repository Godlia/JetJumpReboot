using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstJump : MonoBehaviour
{
    public AudioSource Audio;
    public GameObject player;
    public Rigidbody2D rb;
    private KeyCode burstjumpkey;
    public float burstjumppower = 40f;
    public float burstrate;
    
    private float timestamp;


    void Awake()
    {
        rb = player.GetComponent<Rigidbody2D>();
        burstjumpkey = KeyCode.E;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKeyDown(burstjumpkey) && Time.time > timestamp) {
            rb.AddForce(Vector2.up * burstjumppower ,ForceMode2D.Impulse);
            timestamp = Time.time + burstrate;   
        }
    }
}
