using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
    public Transform weaponHolder;
    private int currentWeaponIndex = 0;

    void Start()
    {
        ActivateWeapon(currentWeaponIndex);
    }

    void Update()
    {
        // Klawisze 1, 2, 3 do wyboru broni
        if (Input.GetKeyDown(KeyCode.Alpha1)) SetWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SetWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SetWeapon(2);
    }

    void SetWeapon(int index)
    {
        if (index == currentWeaponIndex) return;

        currentWeaponIndex = index;
        ActivateWeapon(currentWeaponIndex);
    }

    void ActivateWeapon(int index)
    {
        for (int i = 0; i < weaponHolder.childCount; i++)
        {
            weaponHolder.GetChild(i).gameObject.SetActive(i == index);
        }
    }
}
