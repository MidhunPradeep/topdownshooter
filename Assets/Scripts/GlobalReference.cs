using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalReference : MonoBehaviour
{
    public bool playerIsAlive = true;

    public float playerHealth;
    public int rightAmmoClip;
    public int rightAmmoTotal;
    public int leftAmmoClip;
    public int leftAmmoTotal;
    public int killCount;
    public int enemyCount;
    public int hits;
    public int misses;
    public float accuracy;
    public float killsPerMinute;
    public string formattedTime;
    public float timeToNextDrop;

    private float timer;

    private PlayerBehaviour playerBehaviour;
    private EnemySpawner enemySpawner;
    private AmmoManagement ammoManagementRight;
    private AmmoManagement ammoManagementLeft;

    void Start()
    {
        playerBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        enemySpawner = GameObject.Find("Spawner").GetComponent<EnemySpawner>();
        ammoManagementRight = GameObject.Find("Firepoint1").GetComponent<AmmoManagement>();
        ammoManagementLeft = GameObject.Find("Firepoint2").GetComponent<AmmoManagement>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        playerHealth = playerBehaviour.health;

        rightAmmoClip = ammoManagementRight.magazineAmmo;
        rightAmmoTotal = ammoManagementRight.magazineCount * ammoManagementRight.magazineSize;

        leftAmmoClip = ammoManagementLeft.magazineAmmo;
        leftAmmoTotal = ammoManagementLeft.magazineCount * ammoManagementLeft.magazineSize;

        enemyCount = enemySpawner.enemyCount;

        if (hits + misses != 0)
        {
            accuracy = (((float)hits) / (hits + misses)) * 100;
        }

        killsPerMinute = (killCount / timer) * 60;

        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        formattedTime = $"{minutes:0}:{seconds:00}";
    }
}
