using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    public GameObject Player;
    public Transform Gun;
    Vector2 direction;
    public GameObject Bullet;
    public float bulletspeed;
    public KeyCode shootKey;
    public Transform shootPoint;
    public AudioSource Source;


    public float fireRate;
    public float readyForNextShot;
    public Animator gunAnimator;
    public ParticleSystem muzzleFlash;

    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = MousePos - (Vector2)Gun.position;
        //FaceMouse();

        if (Input.GetMouseButton(0))
        {
            if (Time.time > readyForNextShot)
            {
                readyForNextShot = Time.time + 1 / fireRate;
                shoot();
            }
        }
    }



    void FaceMouse()
    {
        //Gun.transform.right = direction;
    }

    void shoot()
    {
        GameObject BulletIns = Instantiate(Bullet, shootPoint.position, shootPoint.rotation);
        BulletIns.GetComponent<Rigidbody2D>().AddForce(BulletIns.transform.right * bulletspeed);
        //Instantiate(muzzleFlash, shootPoint.position, shootPoint.rotation); Destroy(muzzleFlash, 1f);
        Destroy(BulletIns, 2);
        gunAnimator.SetTrigger("Shoot");
        Source.Play();
    } 
}
