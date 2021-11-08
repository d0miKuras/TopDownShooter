using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDamageComponent : MonoBehaviour
{
    public float damage = 25f;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<HealthComponent>().TakeDamage(damage);
        }
    }
}
