using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(CharacterFacing))]
public class Snowball : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private CharacterFacing _characterFacing;
    private ObjectPool<SnowBallEffect> _poolSnowEffect;
    private IObjectPool<Snowball> _poll;
    private IInteractable _interactable;

    [SerializeField]
    private SnowBallEffect _snowBallEffect;
    [SerializeField]
    [Min(1)]
    private float speed;
    [SerializeField]
    [Min(1)]
    private float lifeTime = 5.0f;

    private Vector2 moveDirection;

    public float LifeTime
    {
        get { return lifeTime; }
        set { lifeTime = Mathf.Clamp(value, 0, 5.0f); }
    }
    public Vector2 MoveDirection
    {
        get { return moveDirection; }
        set { moveDirection = new Vector2(Mathf.Clamp(value.x, -1, 1), Mathf.Clamp(value.y, -1, 1)); }
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _characterFacing = GetComponent<CharacterFacing>();
        _interactable = GetComponent<IInteractable>();

        // Criar uma pool para o snow ball effect onde passamos 3 funções
        // Uma função onde criar o efeito e associa a pool
        // Outra que é chamada quando executamos o get() da pool
        // e por ultimo uma que é chamada quando executamos o return para a poll
        _poolSnowEffect = new ObjectPool<SnowBallEffect>(CreateSnowEffect, OnTakeSnowEffectFromPoll, OnReturnSnowEffectToPoll);
    }

    private void OnEnable()
    {
        _interactable.ChangeDirectionEvent += OnChangeDirection;
    }

    private void OnDisable()
    {
        if (_interactable != null)
        {
            _interactable.ChangeDirectionEvent -= OnChangeDirection;
        }
    }

    private void Update()
    {
        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0)
        {
            if (_poll != null)
                _poll.Release(this);
            else
                Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        _characterFacing.UpdateFacing(new Vector2(MoveDirection.x*-1, 0));

        _rigidbody2D.velocity = new Vector2(MoveDirection.x * speed, MoveDirection.y * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            var snowEffect = _poolSnowEffect.Get();
            snowEffect.transform.position = transform.position;
            snowEffect.transform.rotation = transform.rotation;
            snowEffect.LifeTime = 1.0f;

            if (_poll != null)
                _poll.Release(this);

            damageable.TakeDamage();
        }
        else if (this.isActiveAndEnabled)
        {
            var snowEffect = _poolSnowEffect.Get();
            snowEffect.transform.position = transform.position;
            snowEffect.transform.rotation = transform.rotation;
            snowEffect.LifeTime = 1.0f;

            if (_poll != null)
                _poll.Release(this);
        }
    }
    private void OnChangeDirection(Vector2 direction)
    {
        Debug.Log("#Entrou3"  + direction.ToString());
        moveDirection = direction;
    }

    public void SetPool(IObjectPool<Snowball> poll) => _poll = poll;

    #region SnowBallEffect Pool
    private SnowBallEffect CreateSnowEffect()
    {
        var snowBallEffect = Instantiate(_snowBallEffect);
        snowBallEffect.SetPool(_poolSnowEffect);
        return snowBallEffect;
    }

    private void OnTakeSnowEffectFromPoll(SnowBallEffect snowBallEffect)
    {
        snowBallEffect.gameObject.SetActive(true);
    }

    private void OnReturnSnowEffectToPoll(SnowBallEffect snowBallEffect)
    {
        snowBallEffect.gameObject.SetActive(false);
    }
    #endregion
}
