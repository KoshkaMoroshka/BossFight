using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour, IDamagable
{
    [SerializeField] private float _healtPoint = 100f;
    [SerializeField] private Enemy _enemy;

    private float coeffDamage = 2f;

    private float _damageForOneShoot = 34f;

    public void GetDamage(float damage)
    {
        _healtPoint -= _damageForOneShoot;
        _enemy.GetDamage(damage * coeffDamage);
    }

    public void SetupMissle(Enemy enemy)
    {
        _enemy = enemy;
    }
    private void Update()
    {
        if (_healtPoint <= 0)
            gameObject.active = false;
    }
}
