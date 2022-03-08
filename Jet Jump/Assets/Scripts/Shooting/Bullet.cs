using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
        void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag.Equals("Enemy")) {
            Destroy(this.gameObject);
        } else if (collision.gameObject.tag.Equals("Ground")) {
            Destroy(this.gameObject);
        } 
    }
}