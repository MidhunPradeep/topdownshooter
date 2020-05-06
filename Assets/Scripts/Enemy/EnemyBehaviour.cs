using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject player;
    
    public float moveSpeed;
    public float health = 100f;
    public float damage = 10f;
    public float attackFrequency = 2.5f;

    private Rigidbody2D rb;
    private Vector3 lookDirection;
    private Vector2 movement;
    private float attackTimer;

    private Vector3 startPosition;
    private float positionTimer;
    private bool checkForPosition;

    private EnemySpawner enemySpawner;
    private PlayerBehaviour playerBehaviour;
    private AmmoManagement ammoManagementRight;
    private AmmoManagement ammoManagementLeft;
    private GlobalReference globalReference;

    void Start()
    {
        globalReference = GameObject.Find("Global Reference").GetComponent<GlobalReference>();

        startPosition = transform.position;
        checkForPosition = true;
        
        var multiplier = Random.Range(0.75f, 1.75f);
        moveSpeed /= multiplier;
        attackFrequency /= multiplier;
        health *= multiplier;
        damage *= multiplier;
        transform.localScale *= multiplier;

        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();

        enemySpawner = GameObject.Find("Spawner").GetComponent<EnemySpawner>();
        playerBehaviour = player.GetComponent<PlayerBehaviour>();

        ammoManagementRight = player.transform.GetChild(0).GetComponent<AmmoManagement>();
        ammoManagementLeft = player.transform.GetChild(1).GetComponent<AmmoManagement>();

    }


    void Update()
    {
        if (globalReference.playerIsAlive)
        {
            if (checkForPosition)
            {
                positionTimer += Time.deltaTime;
                if (Vector3.Distance(transform.position, startPosition) < 5f && positionTimer > 5 && checkForPosition)
                {
                    Decay();
                }
                else if (positionTimer > 5)
                {
                    checkForPosition = false;
                }
            }
            

            if (health <= 0)
            {
                Die();
            }

            lookDirection = player.transform.position - transform.position;
            attackTimer += Time.deltaTime;

            movement = Vector2.zero;
            if (Vector3.Distance(transform.position, player.transform.position) > 0.1f)
            {
                movement.x = Mathf.Clamp(lookDirection.x, -1, 1);
                movement.y = Mathf.Clamp(lookDirection.y, -1, 1);
            }
            //else if (attackTimer > attackFrequency)
            //{
            //    AttackPlayer();
            //    attackTimer = 0;
            //}
        }
        else
        {
            moveSpeed = 2f;
            attackTimer += Time.deltaTime;
            if (attackTimer > Random.Range(3f, 12f))
            {
                lookDirection = Random.insideUnitSphere;
                attackTimer = 0;
            }
            movement.x = Mathf.Clamp(lookDirection.x, -1, 1);
            movement.y = Mathf.Clamp(lookDirection.y, -1, 1);
        }
    }
    


    void FixedUpdate()
    {
        rb.velocity = Vector2.zero;

        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        if (movement != Vector2.zero)
        {
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && attackTimer > attackFrequency)
        {
            AttackPlayer();
            attackTimer = 0;
        }
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    public void AttackPlayer()
    {
        playerBehaviour.TakeDamage(damage);
    }

    public void Die()
    {
        if (Random.Range(1, 4 + 1) == 1)
        {
            if (!playerBehaviour.AddHealth(Random.Range(1f, 20f)))
            {
                if (Random.Range(1, 2 + 1) == 1)
                {
                    ammoManagementRight.AddMagazine();
                }
                else
                {
                    ammoManagementLeft.AddMagazine();
                }
            }
        }
        globalReference.killCount++;
        Decay();
    }

    public void Decay()
    {
        enemySpawner.AddToEnemyCount(-1);
        Destroy(gameObject);
    }
}
