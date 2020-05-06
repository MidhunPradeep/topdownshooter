using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public float damage;
    public GlobalReference globalReference;

    private void Start()
    {
        globalReference = GameObject.Find("Global Reference").GetComponent<GlobalReference>();
        var player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
       
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            globalReference.hits++;
            if (Random.Range(1, 5 + 1) == 1)
            {
                damage = damage * 2;
            }
            var enemyBehaviour = collision.GetComponent<EnemyBehaviour>();
            enemyBehaviour.TakeDamage(damage);

        }
        else
        {
            globalReference.misses++;
        }
        Destroy(gameObject);
    }
}
