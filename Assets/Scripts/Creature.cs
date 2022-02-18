using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Creature : MonoBehaviour
{
    public event UnityAction<int> HealthChanged;

    [SerializeField] protected int health = 100;
    [SerializeField] protected float speed = 5;
    [SerializeField] protected ExplosionTile explTile;

    protected int currentHealth;

    public void ChangeCurrentHealth(int hp)
    {
        currentHealth += hp;
        HealthChanged?.Invoke(currentHealth);
        if (currentHealth <= 0)
            Die();
    }

    public abstract void Init();
    public abstract void Move();
    public abstract void Die();
}
