using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DamageType
{
    Normal, Fire, Ice 
}

public interface IDamageInterface
{
    void Damage(DamageType type, int damage);
}
