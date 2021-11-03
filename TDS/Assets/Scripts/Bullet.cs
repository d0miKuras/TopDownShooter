using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explosionFX;
    public float damage = 25f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject fx = Instantiate(explosionFX, transform.position, Quaternion.identity);
        if (other.gameObject.tag == "Enemy") other.gameObject.GetComponent<HealthComponent>().TakeDamage(damage);
        Destroy(fx, 1f);
        Destroy(gameObject);
    }
}
