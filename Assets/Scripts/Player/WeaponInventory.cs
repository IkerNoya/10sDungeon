using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    
    [SerializeField] private WeaponBase[] weapons;
    private int _inventorySize = 2;
    private int _activeWeaponIndex = 0;
    
    
    public bool TrAddWeapon(WeaponBase weapon)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] == null)
            {
                AddWeapon(weapon, i);
                return true;
            }
            else if (i == _activeWeaponIndex)
            {
                ReplaceEquipedWeapon(weapon);
                return true;
            }
        }
        return false;
    }

    void AddWeapon(WeaponBase weapon, int index)
    {
        if (weapon)
            weapons[index] = weapon;
    }

    void RemoveWeapon(WeaponBase weapon)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] == weapon)
            {
                weapons[i] = null;
            }
        }   
    }

    void ReplaceEquipedWeapon(WeaponBase NewWeapon)
    {
        weapons[_activeWeaponIndex] = NewWeapon;
    }

    public WeaponBase GetEquipedWeapon()
    {
        return weapons[_activeWeaponIndex];
    }
}
