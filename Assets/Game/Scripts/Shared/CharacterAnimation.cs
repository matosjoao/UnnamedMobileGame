using UnityEngine;

public static class CharacterMovementAnimationKeys
{
    public const string HorizontalSpeed = "HorizontalSpeed";
    public const string VerticalSpeed = "VerticalSpeed";
    public const string IsGrounded = "IsGrounded";
    public const string Dead = "Dead";
}

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterMovement))]
public class CharacterAnimation : MonoBehaviour
{
    // Nas variaveis colocamos protected para ser acessível nos filhos
    // O mesmo nas funções para que possam ser override nos filhos colocamos protected virtual
    // E nos filhos chamamos protected override nomeFuncao, e se quisermos executar
    // o código do pai dessa função, chama-mos base.nomeFuncao();
    
    protected Animator _animator;
    protected CharacterMovement _characterMovement;
    protected IDamageable _damagable;

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterMovement = GetComponent<CharacterMovement>();
        _damagable = GetComponent<IDamageable>();
    }
    private void OnEnable()
    {
        if (_damagable != null)
        {
            _damagable.OnDeathEvent += OnDeath;
        }
    }

    private void OnDisable()
    {
        if (_damagable != null)
        {
            _damagable.OnDeathEvent -= OnDeath;
        }
    }

    protected virtual void Update()
    {
        _animator.SetFloat(CharacterMovementAnimationKeys.HorizontalSpeed, _characterMovement.CurrentVelocity.x / _characterMovement.MaxGroundSpeed);
    }

    protected virtual void OnDeath()
    {
        _animator.SetTrigger(CharacterMovementAnimationKeys.Dead);
    }

}
