using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BurstJump : MonoBehaviour
{
    public AudioSource Audio;
    public GameObject player;
    public Rigidbody2D rb;
    private KeyCode burstjumpkey;
    public float burstjumppower = 30f * 100;
    public float burstrate;


    [SerializeField]
    private Image imageCooldown;
    [SerializeField]
    private Text textCooldown;

    private bool isCooldown = false;

    private float coolDownTime;

    private float coolDownTimer = 0.0f;


    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
        burstjumpkey = KeyCode.E;
        coolDownTime = burstrate;
        Audio = GameObject.FindGameObjectWithTag("BurstPing").GetComponent<AudioSource>();
        imageCooldown = GameObject.FindGameObjectWithTag("CooldownFill").GetComponent<Image>();
        textCooldown = GameObject.FindGameObjectWithTag("CooldownText").GetComponent<Text>();
    }


    void Update()
    {
        if (Input.GetKeyDown(burstjumpkey))
        {
            useJump();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
            Audio.Play();
            //Når CoolDown er ferdig
            isCooldown = false;
            textCooldown.gameObject.SetActive(false);
            imageCooldown.fillAmount = 1.0f;
        }
        else
        {
            //Cooldown er ikke ferdig
            textCooldown.text = Mathf.RoundToInt(coolDownTimer).ToString();
            imageCooldown.fillAmount = 1 - coolDownTimer / coolDownTime;
        }
    }

    public void useJump()
    {
        if (isCooldown)
        {

        }
        else
        {
            rb.AddForce(Vector2.up * burstjumppower * Time.deltaTime, ForceMode2D.Impulse);
            isCooldown = true;
            textCooldown.gameObject.SetActive(true);
            coolDownTimer = coolDownTime;
        }
    }
}
