using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShootScript : MonoBehaviour
{
    public GameObject Player;
    public Transform Gun;
    public GameObject Bullet;
    public float bulletspeed;
    public KeyCode shootKey;
    private Vector2 direction;
    public Transform shootPoint;
    public AudioSource Source;
    public Camera Cam;


    public float fireRate;
    public float readyForNextShot;
    public Animator gunAnimator;
    public ParticleSystem muzzleFlash;

    // Start is called before the first frame update
    void Start()
    {
        Source = GameObject.FindGameObjectWithTag("ShootSound").GetComponent<AudioSource>();
        Cam = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<Camera>();
    }





    // Update is called once per frame
    void Update()
    {
            Vector2 MousePos = Cam.ScreenToWorldPoint(Input.mousePosition);
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



    void shoot()
    {
        GameObject BulletIns = Instantiate(Bullet, shootPoint.position, shootPoint.rotation);
        BulletIns.GetComponent<Rigidbody2D>().AddForce(BulletIns.transform.right * bulletspeed);
        Destroy(BulletIns, 2);
        gunAnimator.SetTrigger("Shoot");
        Source.Play();
    } 
}
