using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShootScript : MonoBehaviour
{

    //Definerer posisjonen til våpenet
    public Transform Gun;
    //Henter ut prefaben til kulen
    public GameObject Bullet;
    //Definerer farten til kulen
    public float bulletspeed;
    //definerer hvilken todimensjonal retning kulen skal ha
    private Vector2 direction;
    //Finner posisjonen til løpet
    public Transform shootPoint;
    //Finner Componenten som lager lyden
    public AudioSource Source;
    //finner camera så vi kan finne hvor musepekeren er i verden
    public Camera Cam;
    //en egen datatype for å finne hvilken type våpen det er
    public enum gunShootType
    {
        Pistol,
        Rifle,
        Shotgun
    }
    //gjør datatypen til en variabel
    public gunShootType gunType;
    //Definerer hvor lang tid mellom skudd
    public int shotGunPellets;
    public float fireRate;
    //en timestamp som blir brukt sammen med fireRate
    private float readyForNextShot;
    //Gir tilgang til animasjonen til pistolen, i.e rekyl
    public Animator gunAnimator;

    private float weaponSpread;

    // Start is called before the first frame update
    void Start()
    {
        //Finn lydkilden før spillet starter
        Source = GameObject.FindGameObjectWithTag("ShootSound").GetComponent<AudioSource>();
        gunType = gunShootType.Pistol;
    }





    // Update is called once per frame
    void Update()
    {
        //Har shootscriptet ingen kamera, så vil den lete etter gameobjectet med taggen "PlayerCamera" og finne camerakomponenten
        if (Cam == null)
        {
            Cam = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<Camera>();
        }

        //Posisjonen til muspekeren i spillverdenen til en Vector2
        Vector2 MousePos = Cam.ScreenToWorldPoint(Input.mousePosition);
        direction = MousePos - (Vector2)this.transform.position;



        //Hotkeys for å bytte våpen
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gunType = gunShootType.Pistol;
            Debug.Log("Pistol");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gunType = gunShootType.Rifle;
            Debug.Log("Rifle");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            gunType = gunShootType.Shotgun;
            Debug.Log("Shotgun");
        }





        switch (gunType)
        {
            case gunShootType.Pistol:
                fireRate = 1f;
                weaponSpread = 0.1f;
                break;
            case gunShootType.Rifle:
                fireRate = 3f;
                weaponSpread = 10f;
                break;
            case gunShootType.Shotgun:
                fireRate = 0.8f;
                weaponSpread = 500f;
                break;
        }


        if (Input.GetMouseButton(0))
        {
            if (Time.time > readyForNextShot)
            {
                readyForNextShot = Time.time + 1 / fireRate;
                shoot();
            }
        }
    }



    public void shoot()
    {

        if (gunType == gunShootType.Shotgun)
        {
            for (int i = 0; i < shotGunPellets; i++)
            {


                float spreadY = Random.Range(-weaponSpread, weaponSpread);

                Quaternion spread = Quaternion.Euler(0f, 0f, transform.eulerAngles.z + Random.Range(-spreadY, spreadY));

                Debug.Log("SpreadY= " + spreadY + " | Qspread= " + spread);

                GameObject bullet = Instantiate(Bullet, shootPoint.position, spread);

                Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);

                bullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * bulletspeed;
                Destroy(bullet, 2f);
            }
            gunAnimator.SetTrigger("Shoot");
            Source.Play();

        }
        else
        {
            float spreadY = Random.Range(-weaponSpread, weaponSpread);
            Quaternion spread = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + spreadY, 0);
            GameObject bullet = Instantiate(Bullet, shootPoint.position, spread);
            bullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * bulletspeed;
            Destroy(bullet, 2f);
            gunAnimator.SetTrigger("Shoot");
            Source.Play();
        }
    }
}
