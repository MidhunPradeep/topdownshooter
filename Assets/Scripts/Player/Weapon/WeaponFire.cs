using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : MonoBehaviour
{
    public GameObject bulletPrefabRight;
    public GameObject bulletPrefabLeft;

    public Transform firePointRight;
    public float bulletRateRight = .5f;
    public float bulletLifeRight = 5f;
    public float bulletSpeedRight = 10;
    public AudioSource audioSourceRight;

    public Transform firePointLeft;
    public float bulletRateLeft = .5f;
    public float bulletLifeLeft = 5f;
    public float bulletSpeedLeft = 10;
    public AudioSource audioSourceLeft;

    private float timerRight = 0f;
    private float timerLeft = 0f;

    
    private PlayerMovement playerMovement;
    private AmmoManagement ammoManagementRight;
    private AmmoManagement ammoManagementLeft;


    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();

        ammoManagementRight = GameObject.Find("Firepoint1").GetComponent<AmmoManagement>();
        ammoManagementLeft = GameObject.Find("Firepoint2").GetComponent<AmmoManagement>();
    }


    void Update()
    {
        timerRight += Time.deltaTime;
        timerLeft += Time.deltaTime;

        if (Input.GetButton("Fire1") && timerRight > bulletRateRight && !playerMovement.sprinting)
        {
            Shoot(firePointRight, bulletPrefabRight, bulletSpeedRight, bulletLifeRight, ammoManagementRight, audioSourceRight);
            timerRight = 0;
        }
        if (Input.GetButton("Fire2") && timerLeft > bulletRateLeft && !playerMovement.sprinting)
        {
            Shoot(firePointLeft, bulletPrefabLeft, bulletSpeedLeft, bulletLifeLeft, ammoManagementLeft, audioSourceLeft);
            timerLeft = 0;
        }
    }

    void Shoot(Transform firePoint, GameObject bulletPrefab, float bulletSpeed, float bulletLife, AmmoManagement ammo, AudioSource audioSource)
    {
        if (ammo.UseBullet())
        {
            var lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            audioSource.Play();

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);

            Destroy(bullet, bulletLife);
        }
    }
}
