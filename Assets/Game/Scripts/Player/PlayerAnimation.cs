using UnityEngine;

public class PlayerAnimation : CharacterAnimation
{
    protected override void Awake()
    {
        base.Awake();
        
    }

    protected override void Update()
    {
        base.Update();
        _animator.SetFloat(CharacterMovementAnimationKeys.VerticalSpeed, _characterMovement.CurrentVelocity.y / _characterMovement.JumpSpeed);
        _animator.SetBool(CharacterMovementAnimationKeys.IsGrounded, _characterMovement.IsGrounded);
    }
}
