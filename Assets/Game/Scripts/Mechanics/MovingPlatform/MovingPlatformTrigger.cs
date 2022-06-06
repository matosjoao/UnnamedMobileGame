using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MovingPlatformTrigger : MonoBehaviour
{
    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponentInParent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CharacterMovement characterMovement))
        {
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.transform.parent = transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CharacterMovement characterMovement))
        {
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.transform.parent = null;
        }
    }
}
