using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    [SerializeField] private WeaponBase[] weapons;
    [SerializeField] private GameObject weaponAttachmentObject;

    private int _inventorySize = 2;
    private int _activeWeaponIndex = 0;

    private void Start()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i != _activeWeaponIndex)
            {
                weapons[i].gameObject.SetActive(false);
            }
        }
    }

    public void SwitchWeapon(int index)
    {
        if (index >= weapons.Length || index == _activeWeaponIndex) return;

        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] && i != _activeWeaponIndex && i == index)
            {
                weapons[i].gameObject.SetActive(true);
            }
            else
            {
                weapons[i].gameObject.SetActive(false);
            }
        }
        
        _activeWeaponIndex = index;
    }

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
        weapon.gameObject.transform.parent = weaponAttachmentObject.transform;
        weapon.gameObject.transform.localPosition = Vector3.zero;
        weapon.gameObject.SetActive(false);
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

    void Drop()
    {
        WeaponBase equipedWeapon = GetEquipedWeapon();
        weapons[_activeWeaponIndex] = null;
        equipedWeapon.transform.parent = null;
        Vector3 right = transform.right;
        equipedWeapon.transform.position = transform.position + (right * 2);
    }

    void ReplaceEquipedWeapon(WeaponBase newWeapon)
    {
        Drop();
        weapons[_activeWeaponIndex] = newWeapon;
        WeaponBase equipedWeapon = GetEquipedWeapon();
        equipedWeapon.transform.parent = weaponAttachmentObject.transform;
        equipedWeapon.transform.localPosition = Vector3.zero;
    }

    public WeaponBase GetEquipedWeapon()
    {
        return weapons[_activeWeaponIndex];
    }
}