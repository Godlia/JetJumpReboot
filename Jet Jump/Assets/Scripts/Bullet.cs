using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject BulletPrfb;

    // Start is called before the first frame update
    void Start()    {
    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            Destroy(BulletPrfb);
        }
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            Destroy(BulletPrfb);
        }

    }
}
