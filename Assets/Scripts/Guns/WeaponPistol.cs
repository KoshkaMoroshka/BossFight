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
            var obj = hit.collider.gameObject;
            if (obj.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.GetDamage(_damage/10);
            }
            if (obj.TryGetComponent<WeakPoint>(out var weakPoint))
            {
                weakPoint.GetDamage(_damage);
            }
        }
    }
}
