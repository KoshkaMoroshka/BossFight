using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private List<AbstractWeapon> _playersWeapon;

    private AbstractWeapon currentWeaponInArms;

    public void ShootCurrentWeapon()
    {
        if (currentWeaponInArms == null)
            return;
        currentWeaponInArms.BaseShoot(_camera);
    }

    public void GetWeaponInArms(string nameInteractableObj)
    {
        foreach (var weapon in _playersWeapon)
        {
            if (weapon.Name == nameInteractableObj)
            {
                currentWeaponInArms = weapon;
                weapon.gameObject.SetActive(true);
                return;
            }                
        }
    }

    public void SetupPlayersWeapon()
    {
        foreach (var weapon in _playersWeapon)
            weapon.SetupGun();
    }
}
