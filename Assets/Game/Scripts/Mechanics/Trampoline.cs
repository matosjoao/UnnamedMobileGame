using UnityEngine;

public static class TrampolineAnimationKeys
{
    public const string IsStepped = "IsStepped";
}

public class Trampoline : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] private Vector2 jumpVelocity;

    private bool _onTop;
    private CharacterMovement _characterMovement;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out _characterMovement))
        {
            _animator.SetBool(TrampolineAnimationKeys.IsStepped, true);
            _onTop = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _animator.SetBool(TrampolineAnimationKeys.IsStepped, false);
        _onTop = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out _characterMovement))
        {
            _animator.SetBool(TrampolineAnimationKeys.IsStepped, true);
            _onTop = true;
        }
    }

    private void Jump()
    {
        if (!_onTop) return;
        _characterMovement.CurrentVelocity = jumpVelocity;
    }
}
