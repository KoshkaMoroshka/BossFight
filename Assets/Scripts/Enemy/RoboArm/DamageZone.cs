using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private float _damage = 0.5f;
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<PlayerHP>(out var playerHP))
        {
            playerHP.GetDamage(_damage);
        }
    }
}
