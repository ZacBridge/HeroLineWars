using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this script controls the enimes hp/speed/but also their value
// and what happens when they die.
public class Enemy : MonoBehaviour {

    public float startSpeed = 10f; // the default speed never modfied.

    [HideInInspector]
    public float speed;

    public float startHealth = 100f; // sets default health to 100 to begin with
    private float health; // controls the current health

    public int worth = 50;

    public GameObject deathEffect;

    [Header("Unity Stuff")]
    public Image healthBar;

    private bool isDead = false;
    
    void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    // when this method is called from another script it allows the enemy take damage by the amount  defined in the float
    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0 && !isDead)  // if target is below or at 0 hp, call the death method.
        {
            Die();
        }
    }

    public void Slow (float pct)
    {
        speed = startSpeed * (1f - pct); // sets speed to be equal startspeed * 1 and take away the pct slow, using start speed means the value wont constantyl decrease and stack ontop of each other if there is multiple lasers.
    }

    void Die() // if target is killed, called this method
    {
        isDead = true;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity); // play the death effect
        Destroy(effect, 5f); // destroy the effect after 5 seconds

        Destroy(gameObject); // kill the object
    }
}
