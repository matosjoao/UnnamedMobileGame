using System;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

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
    private GameController _gameController;

    private ObjectPool<Snowball> _poolSnowball;
    private ObjectPool<ConfettiEffect> _poolConfettiEffect;

    [Header("Player Attack")]
    [SerializeField] private Snowball fireBall;
    [SerializeField] private Transform firePosition;

    [Header("Player Collect")]
    [SerializeField] private ConfettiEffect confettiEffectParticle;

    [Header("Sounds")]
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip jumpSound;

    [Header("Velocities")]
    [SerializeField] private Vector2 hurtVelocity;

    private int _levelScore;
    private int _lifes;
    public int Score { get { return _levelScore; } }
    public int Lifes { get { return _lifes; } set { _lifes = value; } }

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _characterFacing = GetComponent<CharacterFacing>();
        _characterMovement = GetComponent<CharacterMovement>();
        _audioSource = GetComponent<AudioSource>();
        _damagable = GetComponent<IDamageable>();
        _gameController = FindObjectOfType<GameController>();

        // Inicializar pools
        _poolSnowball = new ObjectPool<Snowball>(CreateSnowBall, OnTakeSnowBallFromPoll, OnReturnSnowBallToPoll);
        _poolConfettiEffect = new ObjectPool<ConfettiEffect>(CreateConfettiEffect, OnTakeConfettiEffectFromPoll, OnReturnConfettiEffectToPoll);
    }

    private void Start()
    {
        HudManager.Instance.SetScore(_levelScore.ToString());
        if(_gameController != null)
        {
            HudManager.Instance.SetLifes(_gameController.Lifes.ToString());
            _lifes = _gameController.Lifes;
        }
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
    public void AddScore(int score)
    {
        // Ataulizar valor;
        _levelScore += score;

        // Atualizar UI
        HudManager.Instance.SetScore(_levelScore.ToString());
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

    public void OnFinishLevel()
    {
        // Parar o Player
        _characterMovement.StopImmediately();

        // Completar nível no GameController
        // Adicionar points aos points atuais e vidas no GameController
        Scene scene = SceneManager.GetActiveScene();
        _gameController.OnCompleteLevel(_lifes, _levelScore, scene.name);
    }

    public ConfettiEffect GetConfetti()
    {
        return _poolConfettiEffect.Get();
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

    #region ConfettiEffect Pool
    private ConfettiEffect CreateConfettiEffect()
    {
        var confettiEffect = Instantiate(confettiEffectParticle);
        confettiEffect.SetPool(_poolConfettiEffect);
        return confettiEffect;
    }

    private void OnTakeConfettiEffectFromPoll(ConfettiEffect confettiEffect)
    {
        confettiEffect.gameObject.SetActive(true);
    }

    private void OnReturnConfettiEffectToPoll(ConfettiEffect confettiEffect)
    {
        confettiEffect.gameObject.SetActive(false);
    }
    #endregion
}
