using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public AudioSource painSource;
    public Animator animator;

    public float maxHealth = 100;
    public float health;

    private GlobalReference globalReference;


    void Start()
    {
        globalReference = GameObject.Find("Global Reference").GetComponent<GlobalReference>();
        health = maxHealth;
    }


    void Update()
    {
        if (health <= 0)
        {
            globalReference.playerIsAlive = false;
            Destroy(gameObject);
        }
    }


    public void TakeDamage(float damage)
    {
        animator.SetTrigger("isHit");
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);
        painSource.Play();
    }

    public bool AddHealth(float amount)
    {
        if (health < maxHealth)
        {
            health += amount;
            health = Mathf.Clamp(health, 0, maxHealth);
            return true;
        }
        return false;
    }
}