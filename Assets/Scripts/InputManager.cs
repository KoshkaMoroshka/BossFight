using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerActions PlayerInput { get; private set; }
    private PlayerActions.FootActions onFoot;

    private PlayerMovement movement;
    private PlayerLook look;
    private PlayerInteractable interact;
    // Start is called before the first frame update
    private void Awake()
    {
        PlayerInput = new PlayerActions();
        onFoot = PlayerInput.Foot;

        movement = GetComponent<PlayerMovement>();
        look = GetComponent<PlayerLook>();
        interact = GetComponent<PlayerInteractable>();

        onFoot.Jump.performed += ctx => movement.Jump();
        onFoot.Interact.performed += ctx => interact.Interact();
    }

    // Update is called once per frame
    private void Update()
    {
        movement.ProcessMove(onFoot.Movement.ReadValue<Vector2>(), Time.deltaTime);
    }

    private void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>(), Time.deltaTime);
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
