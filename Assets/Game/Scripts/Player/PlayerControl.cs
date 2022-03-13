using UnityEngine.Pool;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(CharacterFacing))]
[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(IDamageable))]
public class PlayerControl : MonoBehaviour
{
    private PlayerInput _playerInput;
    private CharacterFacing _characterFacing;
    private CharacterMovement _characterMovement;
    private IDamageable _damagable;
    private ObjectPool<Snowball> _poolSnowball;

    [SerializeField]
    private Snowball fireBall;
    [SerializeField]
    private Transform firePosition;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _characterFacing = GetComponent<CharacterFacing>();
        _characterMovement = GetComponent<CharacterMovement>();
        _damagable = GetComponent<IDamageable>();
        _poolSnowball = new ObjectPool<Snowball>(CreateSnowBall, OnTakeSnowBallFromPoll, OnReturnSnowBallToPoll);
    }

    private void OnEnable()
    {
        _damagable.DeathEvent += OnDeath;
    }

    private void OnDisable()
    {
        if(_damagable != null)
        {
            _damagable.DeathEvent -= OnDeath;
        }
    }

    private void Update()
    {
        // Quando _playerInput.MovementDirection:
        // -1 => vai mover-se para a esquerda
        // 1  => vai mover-se para a direita
        // Obter user input em um vector2
        Vector2 movementInput = _playerInput.GetMovementInput();
        // Processar movimento com o vector2
        _characterMovement.ProcessMovementInput(movementInput);

        // Dar flip no sprite para esquerda ou direita, conforme a movimentação
        _characterFacing.UpdateFacing(movementInput);

        // Saltar
        if (_playerInput.IsJumpButtonDown())
        {
            _characterMovement.Jump();
        }

        if (!_playerInput.IsJumpButtonHeld())
        {
            _characterMovement.UpdateJumpAbort();
        }

        //Attack
        if (_playerInput.IsAttackButtonDown())
        {
            _playerInput._attackInput = false;
            Attack();
        }
    }

    // Primeiramente quando acontece o trigger OnTriggerEnter2D no enimigo ele vai ver se
    // o que colideu é IDamageable, se for vai chamar o função takeDamage e 
    // invocar o evento "DamageEvent" que estamos a subscrever aqui
    private void OnDeath()
    {
        _characterMovement.StopImmediately();
        enabled = false;
    }

    private void Attack()
    {
        // Get SnowBall from pool
        var snowBall = _poolSnowball.Get();
        snowBall.transform.position = firePosition.position;
        snowBall.transform.rotation = firePosition.rotation;
        float moveX = _characterFacing.IsFacingRight() ? 1 : -1;
        snowBall.MoveDirection = new Vector2(moveX, 0);
        snowBall.LifeTime = 5.0f;
    }

    #region SnowBall Pool
    private Snowball CreateSnowBall()
    {
        var snowBall = Instantiate(fireBall);
        snowBall.SetPool(_poolSnowball);
        return snowBall;
    }

    private void OnTakeSnowBallFromPoll(Snowball snowBall)
    {
        snowBall.gameObject.SetActive(true);
    }

    private void OnReturnSnowBallToPoll(Snowball snowBall)
    {
        snowBall.gameObject.SetActive(false);
    }
    #endregion
}
