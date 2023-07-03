using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerActions playerInput;
    private PlayerActions.FootActions onFoot;

    private PlayerMovement movement;
    private PlayerLook look;
    // Start is called before the first frame update
    private void Awake()
    {
        playerInput = new PlayerActions();
        onFoot = playerInput.Foot;

        movement = GetComponent<PlayerMovement>();
        look = GetComponent<PlayerLook>();

        onFoot.Jump.performed += ctx => movement.Jump();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        movement.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
