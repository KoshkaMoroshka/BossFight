using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour, IDamagable
{
    [SerializeField] private float _playerHP = 100f;
    public void GetDamage(float damage)
    {
        _playerHP -= damage;
    }
}
