using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : WeaponBase
{
    public override void Attack()
    {
        CameraShake.Instance.Shake(magnitude, duration);
    }
}
