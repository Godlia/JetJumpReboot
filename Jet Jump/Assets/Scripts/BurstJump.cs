﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BurstJump : MonoBehaviour
{
    public AudioSource Audio;
    public GameObject player;
    public Rigidbody2D rb;
    private KeyCode burstjumpkey;
    public float burstjumppower = 40f;
    public float burstrate;
    
    private float timestamp;

    [SerializeField]
    private Image imageCooldown;
    [SerializeField]
    private Text textCooldown;

    private bool isCooldown = false;
    [SerializeField]
    private float coolDownTime;

    private float coolDownTimer = 0.0f;


    void Awake()
    {
        rb = player.GetComponent<Rigidbody2D>();
        burstjumpkey = KeyCode.E;
        coolDownTime = burstrate;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKeyDown(burstjumpkey) && Time.time > timestamp) {
            rb.AddForce(Vector2.up * burstjumppower ,ForceMode2D.Impulse);
            timestamp = Time.time + burstrate;
            useSpell();
        }


        if (isCooldown)
        {
            ApplyCoolDown();
        }
    }


    void ApplyCoolDown()
    {
        coolDownTimer -= Time.deltaTime;

        if (coolDownTimer < 0.0f)
        {
            isCooldown = false;
            textCooldown.gameObject.SetActive(false);
            imageCooldown.fillAmount = 1.0f;
        }
        else
        {
            textCooldown.text = Mathf.RoundToInt(coolDownTimer).ToString();
            imageCooldown.fillAmount = 1 - coolDownTimer / coolDownTime;
        }
    }

    public void useSpell()
    {
        if (isCooldown)
        {
            //still cooling
        }
        else
        {
            isCooldown = true;
            textCooldown.gameObject.SetActive(true);
            coolDownTimer = coolDownTime;
        }
    }
}
