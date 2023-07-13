using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private float _damage = 3f;
    private void OnTriggerStay(Collider other)
    {
        //if (other.gameObject.TryGetComponent(out PlayerActions player))
        //{

        //}
    }
}
