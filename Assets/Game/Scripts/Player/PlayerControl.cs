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
    private AudioSource _audioSource;
    private IDamageable _damagable;
    private ObjectPool<Snowball> _poolSnowball;

    [SerializeField] private Snowball fireBall;
    [SerializeField] private Transform firePosition;

    [Header("Sounds")]
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip jumpSound;

    [Header("Velocities")]
    [SerializeField] private Vector2 hurtVelocity;

    private int _coins;
    private int _lifes;
    public int Coins { get { return _coins; } }
    public int Lifes { get { return _lifes; } set { _lifes = value; } }

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _characterFacing = GetComponent<CharacterFacing>();
        _characterMovement = GetComponent<CharacterMovement>();
        _audioSource = GetComponent<AudioSource>();
        _damagable = GetComponent<IDamageable>();
        _poolSnowball = new ObjectPool<Snowball>(CreateSnowBall, OnTakeSnowBallFromPoll, OnReturnSnowBallToPoll);
    }

    private void Start()
    {
        HudManager.Instance.SetCoins(_coins.ToString());
        HudManager.Instance.SetLifes(_lifes.ToString());
    }

    private void OnEnable()
    {
        _damagable.OnDeathEvent += OnDeathEvent;
        _damagable.OnDamageEvent += OnDamageEvent;
    }

    private void OnDisable()
    {
        if(_damagable != null)
        {
            _damagable.OnDeathEvent -= OnDeathEvent;
            _damagable.OnDamageEvent += OnDamageEvent;
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
            if (_characterMovement.IsGrounded && !_characterMovement.IsJumping)
            {
                _audioSource.PlayOneShot(jumpSound);
            }
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

    // Adicionar valor de objeto coletado
    public void AddCoins(int coins)
    {
        // Ataulizar valor;
        _coins += coins;

        // Atualizar UI
        HudManager.Instance.SetCoins(_coins.ToString());
    }

    // Primeiramente quando acontece o trigger OnTriggerEnter2D no enimigo ele vai ver se
    // o que colideu é IDamageable, se for vai chamar o função takeDamage e 
    // invocar o evento "DamageEvent" que estamos a subscrever aqui
    private void OnDeathEvent()
    {
        _characterMovement.StopImmediately();
        enabled = false;
    }

    private void OnDamageEvent()
    {
        // Ao sofrer dano
        // Decrease lifes
        _lifes -= 1;
        // Reproduzir som
        _audioSource.PlayOneShot(hurtSound);
        // Mudar velocity
        Vector2 moveDirection = _characterMovement.CurrentVelocity.normalized * -1;
        if (moveDirection == Vector2.zero) moveDirection = Vector2.one;
        _characterMovement.CurrentVelocity = hurtVelocity * moveDirection;
        // Atualizar UI
        HudManager.Instance.SetLifes(_lifes.ToString());
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
