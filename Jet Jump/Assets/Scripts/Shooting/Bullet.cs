using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulldmg = 3;
        void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag.Equals("Enemy")) {
            Destroy(this.gameObject);
        } else if (collision.gameObject.tag.Equals("Ground")) {
            Destroy(this.gameObject);
        } 
    }

    void FixedUpdate() {
        Debug.Log(Mathf.Abs(this.GetComponent<Rigidbody2D>().velocity.normalized.x));
        if((Mathf.Abs(this.GetComponent<Rigidbody2D>().velocity.normalized.x) > 0.5f)) { // this don't work
            //Destroy(this.gameObject);
        }
    }
}