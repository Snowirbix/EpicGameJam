using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponBase[] weapons;
    public int current = 0;

    private void Awake()
    {
        if (weapons == null || weapons.Length == 0)
            throw new MissingComponentException("No Weapon");
    }

    public void Charge ()
    {
        weapons[current]?.Charge();
    }

    public void Fire ()
    {
        weapons[current]?.Fire();
    }
}
