using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DamageType
{
    Normal, Fire, Ice , Electric
}

public interface IDamageInterface
{
    void Damage(DamageType type, int damage);
}
