using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    public float maxHealth = 100f;
    private float _health;

    private void Start()
    {
        _health = maxHealth;
    }

    public void TakeDamage(float dmg)
    {
        _health = Mathf.Clamp(_health - dmg, 0f, 100f);
        if (_health <= 0.0f)
            Die();
    }

    private void Die()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().EnemyDied(); // Decreases the amount of alive enemies in the game manager.
        Destroy(gameObject);
    }
}
