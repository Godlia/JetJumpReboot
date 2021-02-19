using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstJump : MonoBehaviour
{
    public AudioSource Audio;
    public GameObject player;
    public Rigidbody2D rb;
    private bool burstjumpcheck = true;
    private KeyCode burstjumpkey;
    public float burstjumppower = 40f;
    public float burstrate;
    
    private float timestamp;

    private bool ready;

    void Awake()
    {
        rb = player.GetComponent<Rigidbody2D>();
        burstjumpkey = KeyCode.E;
    }

    // Update is called once per frame
    void Update()
    {
        Burst();
        if(timestamp <= burstrate) {
            Audio.Play();
            ready = true;
        }
    }

    void Burst() {
        if(Input.GetKeyDown(burstjumpkey) && timestamp <= burstrate) {
            rb.AddForce(Vector2.up * burstjumppower ,ForceMode2D.Impulse);
            timestamp = Time.time + burstrate;
            ready = false;
            
        }
    }
}
