using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponBase : MonoBehaviour, IWeaponInterface, IInteractionInterface
{
    [Header("Settings")] 
    [SerializeField] protected float timeBetweenAttacks = 0.5f;
    [SerializeField] protected ParticleSystem attackParticle;
    [SerializeField] protected AudioClip sound;

    [Header("ScreenShake")] 
    [SerializeField] protected float magnitude = .1f;
    [SerializeField] protected float duration = .1f;

    private float _attackTimer = 0;

    public static event Action<WeaponBase> OnPickup;
    
    private void Update()
    {
        if (canAttack) return;

        if (_attackTimer >= timeBetweenAttacks)
        {
            _attackTimer = 0;
            canAttack = true;
        }

        _attackTimer += Time.deltaTime;
    }

    protected bool canAttack = true;

    public virtual void Attack(Vector3 direction)
    {
        Debug.Log("ATTACK!");
    }

    public void HandleInteraction()
    {
        gameObject.layer = 11;
        OnPickup?.Invoke(this);
    }
}