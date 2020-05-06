using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManagement : MonoBehaviour
{
    public GameObject reloadText;
    public AudioSource reloadSource;
    public AudioSource outOfAmmoSource;

    public int reloadSlot;
    public float reloadTime;
    public int maxMagazineCount;
    public int magazineSize;

    public float supplyDropDelay;
    private float timer;

    public int magazineCount;
    public int magazineAmmo;

    private bool reloading;

    private GlobalReference globalReference;


    void Start()
    {
        globalReference = GameObject.Find("Global Reference").GetComponent<GlobalReference>();
        magazineCount = maxMagazineCount;
        Reload();
    }


    void Update()
    {
        timer += Time.deltaTime;
        globalReference.timeToNextDrop = supplyDropDelay - timer;
        if (timer > supplyDropDelay)
        {
            AddMagazine();
            timer = 0;
        }

        if (Input.GetButtonDown($"Reload{reloadSlot}"))
        {
            Reload();
        }
    }


    public void Reload()
    {
        if (!reloading)
        {
            reloading = true;
            StartCoroutine(ReloadCoroutine());
        }
    }

    IEnumerator ReloadCoroutine()
    {
        if (CanReload())
        {
            reloadText.SetActive(true);
            reloadSource.Play();
            magazineCount -= 1;
            yield return new WaitForSeconds(reloadTime);
            magazineAmmo = magazineSize;
            reloadText.SetActive(false);
            reloading = false;
        }
    }


    public bool UseBullet()
    {
        if (magazineAmmo > 0)
        {
            magazineAmmo -= 1;
            return true;
        }
        else if (CanReload())
        {
            Reload();
        }
        else
        {
            outOfAmmoSource.Play();
        }
        return false;
    }

    public bool AddMagazine()
    {
        if (magazineCount < maxMagazineCount)
        {
            magazineCount++;
            return true;
        }
        return false;
    }

    public bool CanReload()
    {
        if (magazineCount > 0)
        {
            return true;
        }
        return false;
    }
}
