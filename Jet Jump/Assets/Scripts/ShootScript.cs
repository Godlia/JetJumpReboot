using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ShootScript : MonoBehaviourPun
{
    public GameObject Player;
    public Transform Gun;
    public GameObject Bullet;
    public float bulletspeed;
    public KeyCode shootKey;
    private Vector2 direction;
    public Transform shootPoint;
    public AudioSource Source;
    PhotonView view;
    public Camera Cam;


    public float fireRate;
    public float readyForNextShot;
    public Animator gunAnimator;
    public ParticleSystem muzzleFlash;

    // Start is called before the first frame update
    void Awake()
    {
        Source = GameObject.FindGameObjectWithTag("ShootSound").GetComponent<AudioSource>();
        view = GetComponentInParent<PhotonView>();

    }



    private void Start()
    {
        GameObject[] Cameras = GameObject.FindGameObjectsWithTag("PlayerCamera");
        foreach (GameObject camera in Cameras)
        {
            if (camera.GetComponent<PhotonView>().IsMine)
            {
                Cam = camera.GetComponent<Camera>();
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
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
    }



    void shoot()
    {
        GameObject BulletIns = PhotonNetwork.Instantiate(Bullet.name, shootPoint.position, shootPoint.rotation);
        BulletIns.GetComponent<Rigidbody2D>().AddForce(BulletIns.transform.right * bulletspeed);
        Destroy(BulletIns, 2);
        gunAnimator.SetTrigger("Shoot");
        Source.Play();
    } 
}
