using UnityEngine;

public class WeaponPistol : AbstractWeapon
{
    public override void SetupGun()
    {
        Name = "pistol";
        _distance = 100f;
        _damage = 10f;
        _countAmmoInMagazine = 12;
    }

    protected override void Shoot(Transform camera)
    {
        if (Physics.Raycast(camera.position, camera.forward, out RaycastHit hit, _distance))
        {
            Debug.Log(hit.transform.name);
        }
    }
}
