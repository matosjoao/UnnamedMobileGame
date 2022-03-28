using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(CharacterFacing))]
[RequireComponent(typeof(IDamageable))]
public class EnemyAIController : MonoBehaviour
{
    private CharacterMovement _enemyMovement;
    private CharacterFacing _enemyFacing;
    private IDamageable _damagable;

    private bool _isAlive;
    private Vector2 _movementInput;

    public Vector2 MovementInput { 
        get { return _movementInput; } 
        set { _movementInput = new Vector2(Mathf.Clamp(value.x, -1, 1), Mathf.Clamp(value.y, -1, 1)); }
    }

    [SerializeField]
    private TriggerDamage damager;

    private void Awake()
    {
        _enemyMovement = GetComponent<CharacterMovement>();
        _enemyFacing = GetComponent<CharacterFacing>();
        _damagable = GetComponent<IDamageable>();

    }

    private void OnEnable()
    {
        _damagable.OnDamageEvent += OnDeath;
    }

    private void OnDisable()
    {
        if (_damagable != null)
        {
            _damagable.OnDamageEvent -= OnDeath;
        }
    }

    private void Start()
    {
        _isAlive = true;
        StartCoroutine(EnemyWalkEnumerator());
    }

    void Update()
    {
        // Processar movimento
        _enemyMovement.ProcessMovementInput(_movementInput);

        // Processar flip de sprite
        _enemyFacing.UpdateFacing(_movementInput);
    }

    IEnumerator EnemyWalkEnumerator()
    {
        while (_isAlive)
        {
            _movementInput.x = 1;
            yield return new WaitForSeconds(1.0f);
            _movementInput.x = 0;
            yield return new WaitForSeconds(2.0f);
            _movementInput.x = -1;
            yield return new WaitForSeconds(1.0f);
            _movementInput.x = 0;
            yield return new WaitForSeconds(2.0f);
        }
    }

    private void OnDeath()
    {
        _enemyMovement.StopImmediately();
        enabled = false;
        damager.gameObject.SetActive(false);
        Destroy(gameObject, 0.8f);
    }

}
