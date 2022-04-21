using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShootScript : MonoBehaviour
{

    //Definerer posisjonen til våpenet
    public Transform Gun;
    public GameObject Bullet;
    public float bulletspeed;
    private Vector2 direction;
    public Transform shootPoint;
    public AudioSource shootingSound;
    public Camera Cam;
    [SerializeField] private SpriteRenderer gunRenderer;
    //Egen datatype for våpen
    public enum gunShootType
    {
        Pistol,
        Rifle,
        Shotgun
    }
    public gunShootType gunType;
    public int shotGunPellets;
    [SerializeField] private float fireRate;
    private float readyForNextShot;
    public Animator gunAnimator;
    private float weaponSpread;

    [SerializeField] private Sprite[] gunSprites;
    public Upgrades upgrades;

    public AudioClip[] cockSounds;
    public AudioClip[] shootSounds;
    public AudioSource cockPlayer;


    //Upgrade variabler
    private float effFireRate;
    private float effBulletSpeed;
    private float effPellets;

    // Start is called before the first frame update
    void Start()
    {
        //Finn gameObjectetene og componentene som blir brukt
        shootingSound = GameObject.Find("ShootSound").GetComponent<AudioSource>();
        gunType = gunShootType.Pistol;
        gunRenderer = GameObject.Find("Gun").GetComponent<SpriteRenderer>();
        cockPlayer = GameObject.Find("GunCockSound").GetComponent<AudioSource>();
        gunRenderer.sprite = gunSprites[0];
        upgrades = GameObject.Find("GameplayManager").GetComponent<Upgrades>();

        switchWeapon();
        Upgrade();
    }





    // Update is called once per frame
    void Update()
    {
        //Har shootscriptet ingen kamera, så vil den lete etter gameobjectet med taggen "PlayerCamera" og finne camerakomponenten
        if (Cam == null)
        {
            Cam = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<Camera>();
        }

        //Posisjonen til muspekeren i spillverdenen til en normalisert Vector2
        Vector2 MousePos = Cam.ScreenToWorldPoint(Input.mousePosition);
        direction = MousePos - (Vector2)this.transform.position;
        direction.Normalize();
        Upgrade();



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







        //Håndterer firerate
        if (Input.GetMouseButton(0))
        {
            if (Time.time > readyForNextShot)
            {
                readyForNextShot = Time.time + 1 / effFireRate;
                preShoot();
            }

        }
    }


    /*
    Gjør at du kan skyte som det våpenet du har (i.e. hagle skyter flere kuler)
    */
    private void preShoot()
    {
        if (gunType == gunShootType.Shotgun) // Sjekk om våpenet er en hagle
        {
            //flere kuler for hagle
            for (int i = 0; i < effPellets; i++) //Hvor mange kuler skal bli skutt, pga oppgraderinger
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
        //instansier ny kule med tilfeldig z-rotasjon for spredning
        float spreadY = Random.Range(-weaponSpread, weaponSpread);
        Quaternion spread = Quaternion.Euler(0f, 0f, transform.eulerAngles.z + Random.Range(-spreadY, spreadY));
        GameObject bullet = Instantiate(Bullet, shootPoint.position, spread);
        bullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(direction.x * effBulletSpeed, direction.y * bulletspeed));

        
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

        //custom datatype for å holde styr på våpenene og det som gjør de forskjellige og lett å implementere nye våpen
        //Kan bruke OOP, men tør ikke endre systemet
        //Index [0] = Pistol, [1] = Rifle, [2] = Shotgun
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
        readyForNextShot = Time.time + 1 / effFireRate;
    }

    public void Upgrade()
    {
        effFireRate = fireRate * upgrades.getMarkiplier("fireRate");
        effBulletSpeed = bulletspeed * upgrades.getMarkiplier("bulletSpeed");
        effPellets = shotGunPellets + Mathf.RoundToInt(upgrades.getMarkiplier("bullets")) - 1;
    }
}