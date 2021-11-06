using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDamageComponent : MonoBehaviour
{
    public Rigidbody2D characterRb;
    public float damage = 25f;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<HealthComponent>().TakeDamage(damage);
            // characterRb.AddForce(transform.up * 2000f, ForceMode2D.Impulse);
        }
    }
}
