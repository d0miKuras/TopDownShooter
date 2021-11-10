using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Melee,
    Ranged,
    Boss
};
public class AIDamageComponent : MonoBehaviour
{
    public float damage = 25f;

    public EnemyType attackType;
    public GameObject projectilePrefab;
    public Transform muzzlePoint;
    public float projectileSpeed = 5.0f;
    public Transform playerTransform;
    public float timeBetwenShots = 0.3f;
    private float _lastTimeShot;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<HealthComponent>().TakeDamage(damage);
        }
    }

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);
        Physics2D.IgnoreLayerCollision(9, 9, true);
    }
    private void Shoot()
    {
        _lastTimeShot = Time.time;
        GameObject bullet = Instantiate(projectilePrefab, transform.position, transform.rotation);
        bullet.GetComponent<Bullet>().owner = gameObject;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce((playerTransform.position - transform.position).normalized * projectileSpeed, ForceMode2D.Impulse);
        Destroy(bullet, 5f);
    }
    private void Update()
    {
        if (attackType == EnemyType.Ranged || attackType == EnemyType.Boss)
        {
            if (Time.time - _lastTimeShot > timeBetwenShots)
            {
                Shoot();
            }
        }
    }

}
