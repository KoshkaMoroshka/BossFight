using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractable : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private float distance = 3f;
    [SerializeField] private LayerMask mask;

    // Update is called once per frame
    private void Update()
    {
        Ray ray = new Ray(_camera.position, _camera.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, distance, mask))
        {
            if (hitInfo.collider.TryGetComponent<Interactable>(out Interactable obj))
            {
               obj.BaseInteract();
            }
        }
    }
}
