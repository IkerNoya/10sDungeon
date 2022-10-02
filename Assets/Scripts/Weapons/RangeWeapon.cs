using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : WeaponBase
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject instigator;
    [SerializeField] private float spawnOffset = 1f;
    private Vector3 _spawnOffset;
    public override void Attack(Vector3 direction)
    {
        if (!canAttack) return;
        _spawnOffset = transform.position + (direction.normalized * spawnOffset);
        _spawnOffset.z = 0;
        
        SpawnProjectile(direction);
        CameraShake.Instance.Shake(magnitude, duration);
        canAttack = false;
    }

    void SpawnProjectile(Vector3 moveDirection)
    {
        GameObject go = Instantiate(projectile,_spawnOffset, Quaternion.identity);
        if (go)
        {
            ProjectileBase projectileBase = go.GetComponent<ProjectileBase>();
            if(projectileBase)
                projectileBase.Initialize(moveDirection, instigator);
        }
    }
    
}
