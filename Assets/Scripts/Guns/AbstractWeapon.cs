using UnityEngine;

public abstract class AbstractWeapon : MonoBehaviour
{
    public string Name { get; protected set; }
    [SerializeField] protected float _distance;
    [SerializeField] protected float _damage;
    [SerializeField] protected int _countAmmoInMagazine;

    public virtual void SetupGun()
    {
        Name = "null";
        _distance = 0;
        _damage = 0;
        _countAmmoInMagazine = 0;
    }

    public void BaseShoot(Transform camera)
    {
        Shoot(camera);
    }

    protected virtual void Shoot(Transform camera) { }
}
