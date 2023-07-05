using UnityEngine;

public abstract class AbstractGun : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private float _damage;
    [SerializeField] private int _countAmmoinMagazine;

    public void BaseShoot(Transform camera)
    {
        Shoot(camera);
    }
    
    protected virtual void Shoot(Transform camera) { }
}
