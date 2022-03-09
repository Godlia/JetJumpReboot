using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    /* 
    # more damage
    # More speed
    # projectile speed
    # Shotgun spread and bullets
    # more health
    # More fuel
    # Faster fuel regain    
    */

    [SerializeField] private float speedMultiplier = 1.0f;
    [SerializeField] private float damageMultiplier = 1.0f;
    [SerializeField] private float projectileSpeedMultiplier = 1.0f;
    [SerializeField] private float fireRateMultiplier = 1.0f;
    [SerializeField] private float spreadMultiplier = 1.0f;
    [SerializeField] private float bulletsMultiplier = 1.0f;
    [SerializeField] private float healthMultiplier = 1.0f;
    [SerializeField] private float fuelMultiplier = 1.0f;
    [SerializeField] private float fuelRegainMultiplier = 1.0f;


    public float getMarkiplier(string multiplierName) {
        switch(multiplierName) {
            case "speed":
                return speedMultiplier;
            case "damage":
                return damageMultiplier;
            case "projectileSpeed":
                return projectileSpeedMultiplier;
            case "fireRate":
                return fireRateMultiplier;
            case "spread":
                return spreadMultiplier;
            case "bullets":
                return bulletsMultiplier;
            case "health":
                return healthMultiplier;
            case "fuel":
                return fuelMultiplier;
            case "fuelRegain":
                return fuelRegainMultiplier;
            default:
                return 1.0f;
        }
    }
}
