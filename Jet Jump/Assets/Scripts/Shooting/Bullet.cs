using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulldmg = 3;
    void Start() {
        Upgrades upgrades = GameObject.Find("GameplayManager").GetComponent<Upgrades>();
        bulldmg *= upgrades.getMarkiplier("damage");
    }
        void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag.Equals("Enemy")) {
            Destroy(this.gameObject);
        } else if (collision.gameObject.tag.Equals("Ground")) {
            Destroy(this.gameObject);
        } 
    }

    
}