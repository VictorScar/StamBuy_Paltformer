using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform muzzle;

    [SerializeField] private float delay = 0.5f;

    private bool canShoot = true;

    public void Shoot()
    {
        if (canShoot)
        {
            PhotonNetwork.Instantiate(bulletPrefab.name, muzzle.position, muzzle.rotation);
            canShoot = false;
            StartCoroutine(ShootDelay());
        }
    }

    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(delay);
        canShoot = true;
    }
}
