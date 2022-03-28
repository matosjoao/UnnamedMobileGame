using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    [Header("Movement")]
    [SerializeField] private float groundMaxSpeed = 7.0f;
    [SerializeField] private float groundAcceleration = 100.0f;

    [Header("Jump")]
    [SerializeField] private float maxJumpHeight = 4.0f;
    [SerializeField] private float jumpPeakTime = 0.4f;
    [SerializeField] private float jumpAbortDecceleration = 100.0f;

    [Header("Collision")]
    [SerializeField] LayerMask groundedLayerMask = default;
    [SerializeField] float groundedCheckRadius = 0.3f;
    [SerializeField] Transform groundedCheck;

    private bool isGrounded;
    private bool wasGroundedLastFrame;
    private Vector2 currentVelocity;

    // Manter sempre as variáveis privadas 
    // Para ter uma variável publica colocar sempre um método á parte
    // Para aceder ao valor e modificar
    public bool IsGrounded { get { return isGrounded == wasGroundedLastFrame && isGrounded; } }
    public bool IsJumping { get { return currentVelocity.y > 0; } }
    public float MaxGroundSpeed { get { return groundMaxSpeed; } set { groundMaxSpeed = value; } }
    public Vector2 CurrentVelocity { get { return currentVelocity; } 
        set { 
            currentVelocity.y = Mathf.Clamp(value.y, 0, 50); 
            currentVelocity.x = value.x; 
        }
    }
    private float Gravity { get { return maxJumpHeight * 2 / (jumpPeakTime * jumpPeakTime); } }
    public float JumpSpeed { get { return Gravity * jumpPeakTime; } }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.gravityScale = 0;
        _rigidbody2D.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        ApplyGravity();

        HandleMovement();

        CheckCapsuleCollisionsBottom();
    }
        
    public void ProcessMovementInput(Vector2 movementInput)
    {
        // Ajustar a velocidade
        float desiredHorizontalSpeed = movementInput.x * MaxGroundSpeed;
        currentVelocity.x = Mathf.MoveTowards(currentVelocity.x, desiredHorizontalSpeed, groundAcceleration * Time.deltaTime);
    }

    public void Teleport(Vector2 position)
    {

        _rigidbody2D.position = position;
    }

    protected bool CanJump()
    {
        return IsGrounded && !IsJumping;
    }

    public void Jump()
    {
        if (CanJump())
        {
             currentVelocity.y = JumpSpeed;
        }
    }

    public void UpdateJumpAbort()
    {
        if (IsJumping)
        {
            currentVelocity.y -= jumpAbortDecceleration * Time.deltaTime;
        }
    }

    private void HandleMovement()
    {
        // 1º Opção e a mais básica
        //_rigidbody2D.velocity = currentVelocity * groundAcceleration * Time.fixedDeltaTime;


        // Aqui conseguimos ter um movimento mais fluido, melhor para quando temos joystick
        // onde podemos andar mais rápido ou não
        // e conseguimos ter o click para saltar pouco ou o longo click para saltar mais
        Vector2 previousPosition = _rigidbody2D.position;
        Vector2 currentPosition = previousPosition + currentVelocity * Time.fixedDeltaTime;

        _rigidbody2D.MovePosition(currentPosition);
    }

    public void StopImmediately()
    {
        currentVelocity = Vector2.zero;
    }

    private void ApplyGravity()
    {
        currentVelocity.y -= Gravity * Time.fixedDeltaTime;
    }

    private void CheckCapsuleCollisionsBottom()
    {
        wasGroundedLastFrame = isGrounded;

        isGrounded = Physics2D.OverlapCircle(groundedCheck.position, groundedCheckRadius, groundedLayerMask);

        if (isGrounded && !IsJumping)
        {
            currentVelocity.y = 0;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundedCheck.position, groundedCheckRadius);
    }
}
