using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Transform _camera;

    private float yRotation = 0f;

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    public void ProcessLook(Vector2 input, float deltaTime)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        yRotation -= (mouseY * deltaTime) * ySensitivity;
        yRotation = Mathf.Clamp(yRotation, -80f, 80f);

        _camera.localRotation = Quaternion.Euler(yRotation, 0, 0);

        transform.Rotate(Vector3.up * (mouseX * deltaTime) * xSensitivity);
    }
}
