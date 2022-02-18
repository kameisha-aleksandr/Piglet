using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Creature creature;
    [SerializeField] private Slider health;

    private void OnEnable()
    {
        creature.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        creature.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int curHealth)
    {
        health.value = curHealth;
    }
}
