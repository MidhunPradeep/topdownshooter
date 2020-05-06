using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnTimer;
    public int maxEnemyCount;
    public int enemyCount;

    public Transform playerTransform;
    public GameObject enemyPrefab;

    public float minDistanceFromPlayer = 3f;
    public float maxDistanceFromPlayer = 7f;

    public Vector2 minBoundary;
    public Vector2 maxBoundary;

    private float timer;
    private float difficultyTimer;
    private Vector3 randomPosition;

    private GlobalReference globalReference;


    void Start()
    {
        globalReference = GameObject.Find("Global Reference").GetComponent<GlobalReference>();
    }


    void Update()
    {
        if (!globalReference.playerIsAlive)
        {
            gameObject.SetActive(false);
        }

        timer += Time.deltaTime;
        difficultyTimer += Time.deltaTime;
        if (timer > spawnTimer && enemyCount < maxEnemyCount)
        {
            randomPosition.x = Random.Range(-maxDistanceFromPlayer, maxDistanceFromPlayer+1);
            randomPosition.y = Random.Range(-maxDistanceFromPlayer, maxDistanceFromPlayer+1);

            randomPosition.x = Mathf.Clamp(randomPosition.x, minDistanceFromPlayer, maxDistanceFromPlayer);
            randomPosition.y = Mathf.Clamp(randomPosition.y, minDistanceFromPlayer, maxDistanceFromPlayer);

            randomPosition = playerTransform.position + randomPosition;

            randomPosition.x = Mathf.Clamp(randomPosition.x, minBoundary.x, maxBoundary.x);
            randomPosition.y = Mathf.Clamp(randomPosition.y, minBoundary.y, maxBoundary.y);

            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
            AddToEnemyCount(1);
            timer = 0;
        }
        if (difficultyTimer > 2.5f)
        {
            maxEnemyCount++;
            difficultyTimer = 0f;
        }
    }

    public void AddToEnemyCount(int amount)
    {
        enemyCount += amount;
    }
}
