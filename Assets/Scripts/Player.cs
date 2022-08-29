using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health = 100;
    
    private int _currentHealth;
    private int _numberOfCoins;

    public event UnityAction<int> HealthChanged;
    public event UnityAction<int> NumberOfCoinsChanged;
    public event UnityAction IsDead;

    public void Start()
    {
        _currentHealth = _health;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth);

        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
            IsDead?.Invoke();
        }
    }

    public void TakeCoin()
    {
        _numberOfCoins++;
        NumberOfCoinsChanged?.Invoke(_numberOfCoins);        
    }
}