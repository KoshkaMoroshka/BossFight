using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractable : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private float _distance = 3f;
    [SerializeField] private LayerMask _mask;

    private Interactable selectedObj;

    private void Update()
    {
        Ray ray = new Ray(_camera.position, _camera.forward);
        Debug.DrawRay(ray.origin, ray.direction * _distance);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, _distance, _mask))
            selectedObj = hitInfo.collider.GetComponent<Interactable>();
        else
            selectedObj = null;
    }

    public void Interact(GameObject obj)
    {
       if (selectedObj != null)
            selectedObj.BaseInteract(obj);
    }
}
