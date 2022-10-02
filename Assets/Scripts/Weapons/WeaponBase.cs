using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponBase : MonoBehaviour,  IWeaponInterface
{
    [Header("Settings")]
    [SerializeField] protected float timeBetweenAttacks = 0.5f;
    [SerializeField] protected ParticleSystem attackParticle;
    [SerializeField] protected AudioClip sound;

    [Header("ScreenShake")] 
    [SerializeField] protected float magnitude = .1f;
    [SerializeField] protected float duration = .1f;
    
    
    protected bool canAttack = true;
    public virtual void Attack()
    {
        Debug.Log("ATTACK!");
    }
}
