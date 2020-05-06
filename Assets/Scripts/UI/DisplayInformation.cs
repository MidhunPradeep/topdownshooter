using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInformation : MonoBehaviour
{
    private GlobalReference globalReference;
    private Text health, rightAmmo, leftAmmo, killCount, enemyCount, accuracy;


    void Start()
    {
        globalReference = GameObject.Find("Global Reference").GetComponent<GlobalReference>();

        rightAmmo = transform.GetChild(0).GetComponent<Text>();
        leftAmmo = transform.GetChild(1).GetComponent<Text>();
        health = transform.GetChild(2).GetComponent<Text>();
        killCount = transform.GetChild(3).GetComponent<Text>(); 
        enemyCount = transform.GetChild(4).GetComponent<Text>();
        accuracy = transform.GetChild(5).GetComponent<Text>();


    }


    void Update()
    {
        health.text = $"Health: {globalReference.playerHealth:0}";

        rightAmmo.text = $"SMG: {globalReference.rightAmmoClip}/{globalReference.rightAmmoTotal}";
        leftAmmo.text = $"Shotgun: {globalReference.leftAmmoClip}/{globalReference.leftAmmoTotal}";

        killCount.text = $"Kills: {globalReference.killCount}";
        enemyCount.text = $"Hostile Count: {globalReference.enemyCount}";
        accuracy.text = $"Accuracy: {globalReference.accuracy:F2}%";
    }
}
