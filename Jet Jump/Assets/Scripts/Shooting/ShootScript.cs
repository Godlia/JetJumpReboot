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
    public enum gunShootType {
        Pistol,
        Rifle,
        Shotgun
    }
    //gjør datatypen til en variabel
    public gunShootType gunType;
    //Definerer hvor lang tid mellom skudd
    public float fireRate;
    //en timestamp som blir brukt sammen med fireRate
    private float readyForNextShot;
    //Gir tilgang til animasjonen til pistolen, i.e rekyl
    public Animator gunAnimator;

    // Start is called before the first frame update
    void Start()
    {
        //Finn lydkilden før spillet starter
        Source = GameObject.FindGameObjectWithTag("ShootSound").GetComponent<AudioSource>();
    }





    // Update is called once per frame
    void Update()
    {
        //Har shootscriptet ingen kamera, så vil den lete etter gameobjectet med taggen "PlayerCamera" og finne camerakomponenten
        if(Cam == null) {
        Cam = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<Camera>();
        }

        //Posisjonen til muspekeren i spillverdenen til en Vector2
        Vector2 MousePos = Cam.ScreenToWorldPoint(Input.mousePosition);
        //Finner retningen som kulen skal fly
        direction = MousePos - (Vector2)Gun.position;
        
        //egen datatype for hvilket våpen det er
        switch (gunType)
        {
            case gunShootType.Pistol:
                fireRate = 1f;
                break;
            case gunShootType.Rifle:
                fireRate = 5f;
                break;
            case gunShootType.Shotgun:
                fireRate = 0.8f;
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



    void shoot()
    {
        //Lager en kopi av prefaben "Bullet", og setter retning og rotasjon til skytepunktet
        GameObject BulletIns = Instantiate(Bullet, shootPoint.position, shootPoint.rotation);
        //Kulen er blitt spawnet i skytepunktet, så den må fart
        BulletIns.GetComponent<Rigidbody2D>().AddForce(BulletIns.transform.right * bulletspeed);
        //Siden dette blir kjørt individuelt, kan vi bruke destroy() for å ødelegge kulen etter en tid
        Destroy(BulletIns, 2);
        //Animasjonen blir trigget
        gunAnimator.SetTrigger("Shoot");
        //spill lyden som ligger i lydkilden (skytelyd)
        Source.Play();
    }
}
