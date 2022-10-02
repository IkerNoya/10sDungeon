using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour, IDamageInterface
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private Color hitColor = Color.red;
    [SerializeField] private Material spriteMaterial;
    [SerializeField] private Material damageMaterial;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    private int _currentHealth = 0;
    public int CurrentHealth => _currentHealth;

    public static event Action DeathEvent;

    private void Start()
    {
        _currentHealth = maxHealth;
    }

    public void Heal(int amount)
    {
        _currentHealth += amount;
        if (_currentHealth > maxHealth)
            _currentHealth = maxHealth;
    }

    void Death()
    {
        DeathEvent?.Invoke();
    }

    public void Damage(DamageType type, int damage)
    {
        StartCoroutine(DamageEffect());
        _currentHealth -= damage;
        if(CompareTag("Player"))
            CameraShake.Instance.Shake(.1f,.1f);
        if (_currentHealth <= 0)
            Death();
    }

    IEnumerator DamageEffect()
    {
        bool applyEffect = spriteRenderer;

        if (applyEffect)
        {
            spriteRenderer.material = damageMaterial;
            spriteRenderer.color = hitColor;
            
            yield return new WaitForSeconds(.1f);
            
            spriteRenderer.material = spriteMaterial;
            spriteRenderer.color = Color.white;
        }
        yield return null;
    }
}
