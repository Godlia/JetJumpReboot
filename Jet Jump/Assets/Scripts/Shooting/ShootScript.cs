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
    public AudioSource shootingSound;
    //finner camera så vi kan finne hvor musepekeren er i verden
    public Camera Cam;
    //en egen datatype for å finne hvilken type våpen det er

    [SerializeField]
    private SpriteRenderer gunRenderer;
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
    [SerializeField] private float fireRate;
    //en timestamp som blir brukt sammen med fireRate
    private float readyForNextShot;
    //Gir tilgang til animasjonen til pistolen, i.e rekyl
    public Animator gunAnimator;
    private float weaponSpread;

    [SerializeField]
    private Sprite[] gunSprites;
    public Upgrades upgrades;

    public AudioClip[] cockSounds;
    public AudioClip[] shootSounds;
    public AudioSource cockPlayer;

    // Start is called before the first frame update
    void Start()
    {
        //Finn lydkilden før spillet starter
        shootingSound = GameObject.Find("ShootSound").GetComponent<AudioSource>();
        gunType = gunShootType.Pistol;
        switchWeapon();
        //Dette vil ikke funke i multiplayer
        gunRenderer = GameObject.Find("Gun").GetComponent<SpriteRenderer>();

        cockPlayer = GameObject.Find("GunCockSound").GetComponent<AudioSource>();
        gunRenderer.sprite = gunSprites[0];
        upgrades = GameObject.Find("GameplayManager").GetComponent<Upgrades>();
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
            switchWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gunType = gunShootType.Rifle;
            switchWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            gunType = gunShootType.Shotgun;
            switchWeapon();
        }







        //handler firerate 
        if (Input.GetMouseButton(0))
        {
            if (Time.time > readyForNextShot)
            {
                readyForNextShot = Time.time + 1 / fireRate;
                shoot();
            }

        }
    }


    /*
    Gjør at du kan skyte som det våpenet du har (i.e. hagle skyter flere kuler)
    */
    private void shoot()
    {
        if (gunType == gunShootType.Shotgun) // Sjekk om våpenet er en hagle
        {
            //flere kuler for hagle
            for (int i = 0; i < shotGunPellets; i++) //Hvor mange kuler skal bli skutt, pga oppgraderinger
            {
                doShoot();
            }
        }
        else
        {
            //1 kule
            doShoot();
        }
    }


    private void doShoot()
    {
        //kanskje den mest kompliserte funksjonen i hele spillet
        //definerer en float som er en random verdi mellom våpenets spreadverdi
        float spreadY = Random.Range(-weaponSpread, weaponSpread);
        //quaternion er en 4-dimensjonal datatype for rotasjon, så vi lager en tilfeldig spread på z-aksen som kan påføres når kulen blir skapt
        Quaternion spread = Quaternion.Euler(0f, 0f, transform.eulerAngles.z + Random.Range(-spreadY, spreadY));
        //Vi skaper en kule fra prefaben, som skapes på posisjonen "shootPoint" som er løpet til våpenet,
        // og påfører rotasjonen på kulen, så alle kuler spawner med en tilfeldig rotasjon
        GameObject bullet = Instantiate(Bullet, shootPoint.position, spread);
        //ignorer kollisjon mellom kuler
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
        //Gi kulen en hastighet i den lokale vectoren, så den beveger seg på sin egen x-akse
        bullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(direction.x * bulletspeed, direction.y * bulletspeed));
        
        if (gunType == gunShootType.Shotgun)
        {
            Destroy(bullet, 0.5f);
        }
        else
        {
            Destroy(bullet, 2f);
        }
        gunAnimator.SetTrigger("Shoot");
        shootingSound.Play();
    }



    void switchWeapon()
    {

        //custom datatype for å holde styr på våpenene og det som gjør de forskjellige
        switch (gunType)
        {
            case gunShootType.Pistol:
                cockPlayer.clip = cockSounds[0]; //lag ritkig utrekkslyd
                cockPlayer.Play(); //spill lyden
                fireRate = 1f; //pistolen skyter hvert sekund
                weaponSpread = 0.1f; //pistolen har en spread på 0.1
                bulletspeed = 1f; //pistolen skyter med 1 enhet per sekund
                gunRenderer.sprite = gunSprites[0]; //sett pistolens sprite i gun, via spritearray
                gunRenderer.GetComponentInParent<Transform>().localScale = new Vector3(2, 2, 1); //gjør den 2x så stor
                shootingSound.clip = shootSounds[0]; //sett lyden til pistolens skudd
                break;
            case gunShootType.Rifle:
                cockPlayer.clip = cockSounds[1];
                cockPlayer.Play();
                fireRate = 3f;
                weaponSpread = 12.5f;
                bulletspeed = 1f;
                gunRenderer.sprite = gunSprites[1];
                gunRenderer.GetComponentInParent<Transform>().localScale = new Vector3(3, 3, 1);
                shootingSound.clip = shootSounds[1];
                break;
            case gunShootType.Shotgun:
                cockPlayer.clip = cockSounds[2];
                cockPlayer.Play();
                fireRate = 0.8f;
                weaponSpread = 20f;
                bulletspeed = 1f;
                gunRenderer.sprite = gunSprites[2];
                gunRenderer.GetComponentInParent<Transform>().localScale = new Vector3(3, 3, 1);
                shootingSound.clip = shootSounds[2];
                break;
        }
        readyForNextShot = Time.time + 1 / fireRate;
    }

    public void Upgrade()
    {
        fireRate *= upgrades.getMarkiplier("fireRate");
        bulletspeed *= upgrades.getMarkiplier("bulletSpeed");
        shotGunPellets += Mathf.RoundToInt(upgrades.getMarkiplier("bullets")) - 1;
    }
}