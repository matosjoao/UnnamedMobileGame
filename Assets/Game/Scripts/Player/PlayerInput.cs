using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private InputActions _inputActions;
    private float _movementDirection;
    private bool _jumpInput;
    private bool _jumpHeldInput;
    public bool _attackInput;
    public float MovementDirection { get { return _movementDirection; } }

    private void OnEnable()
    {
        if (_inputActions == null)
        {
            _inputActions = new InputActions();

            _inputActions.CharacterMovement.Movement.performed += OnMovement;

            _inputActions.CharacterMovement.Jump.performed += OnJump;
            _inputActions.CharacterMovement.Jump.canceled += OnJump;

            _inputActions.CharacterMovement.JumpHeld.performed += OnJumpHeld;

            _inputActions.CharacterActions.Attack.performed += OnAttack;
            _inputActions.CharacterActions.Attack.canceled += OnAttack;

        }

        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        _movementDirection = context.ReadValue<float>();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if(context.phase is InputActionPhase.Performed)
        {
            _jumpInput = true;
        }
        else if(context.phase is InputActionPhase.Canceled)
        {
            _jumpInput = false;
        }
    }

    private void OnJumpHeld(InputAction.CallbackContext context)
    {
        _jumpHeldInput = context.ReadValue<float>() > 0.4f;
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase is InputActionPhase.Performed)
        {
            _attackInput = true;
        }
        else if (context.phase is InputActionPhase.Canceled)
        {
            _attackInput = false;
        }
    }

    public Vector2 GetMovementInput()
    {
        // Input Teclado
        float horizontalInput = _movementDirection;

        // Input Joystick
        // (Mathf.Approximately(horizontalInput, 0.0f))
        //
            //Input Mobile
            //horizontalInput = CrossPlatformInputManager.GetAxisRaw(PlayerInputConstants.Horizontal);
        //

        return new Vector2(horizontalInput, 0);
    }

    public bool IsJumpButtonDown()
    {
        return _jumpInput;
        //bool isKeyboardButtonDown = Input.GetKeyDown(KeyCode.Space);
        //bool isMobileButtonDown = CrossPlatformInputManager.GetButtonDown(PlayerInputConstants.Jump);

        //return isKeyboardButtonDown || isMobileButtonDown;
    }

    public bool IsJumpButtonHeld()
    {
        return _jumpHeldInput;
        //bool isKeyboardButtonHeld = Input.GetKey(KeyCode.Space);
        //bool isMobileButtonHeld = CrossPlatformInputManager.GetButton(PlayerInputConstants.Jump);

        //return isKeyboardButtonHeld || isMobileButtonHeld;
    }

    public bool IsAttackButtonDown()
    {
        return _attackInput;
    }
}
