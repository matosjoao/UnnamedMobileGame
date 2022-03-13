using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class ChainPush : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    [SerializeField] private Vector2 pushVelocity;

    private void Awake()
    {
        _rigidbody2D = transform.parent.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out CharacterMovement _characterMovement))
        {
            Vector2 moveDirection = _rigidbody2D.velocity.normalized * -1;

            _characterMovement.CurrentVelocity = moveDirection * pushVelocity;
        }
        
        //if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
    }
}
