using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour,  IWeaponInterface
{
    [SerializeField] protected float timeBetweenAttacks = 0.5f;
    
    protected bool canAttack = true;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Attack()
    {
        Debug.Log("ATTACK!");
    }
}
