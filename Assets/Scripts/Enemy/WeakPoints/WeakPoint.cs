using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour, IDamagable
{
    [SerializeField] private float _coeffDamage = 4f;
    [SerializeField] private float _healtPoint = 100f;
    [SerializeField] private Enemy _enemy;

    private float _damageForOneShoot = 34f;

    public void GetDamage(float damage)
    {
        _healtPoint -= _damageForOneShoot;
        _enemy.GetDamage(damage * _coeffDamage);
    }


    // Update is called once per frame
    void Update()
    {
        if (_healtPoint <= 0)
            gameObject.active = false;
    }
}
