using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform muzzlePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public float timeBetwenShots = 0.3f;
    private float _lastTimeShot;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (Time.time - _lastTimeShot > timeBetwenShots)
                Shoot();
        }
    }

    private void Shoot()
    {
        _lastTimeShot = Time.time;
        GameObject bullet = Instantiate(bulletPrefab, muzzlePoint.position, muzzlePoint.rotation);
        bullet.GetComponent<Bullet>().owner = gameObject;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(muzzlePoint.up * bulletForce, ForceMode2D.Impulse);
        Destroy(bullet, 5f);
    }
}
