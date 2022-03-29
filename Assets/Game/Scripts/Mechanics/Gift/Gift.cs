using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class Gift : MonoBehaviour
{
    private AudioSource _audioSource;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;
    private Animator _animator;

    [SerializeField] private AudioClip pickSound;
    [SerializeField] private Sprite[] images;
    [SerializeField] private ConfettiEffect _confettiEffect; //TODO:: Remover daqui o efeito

    private int _random;
    private int _amount;
    private int _randomAnim;
    private ObjectPool<ConfettiEffect> _poolConfettiEffect; //TODO:: Remover daqui a pool

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();

        //TODO:: Remover daqui a pool
        _poolConfettiEffect = new ObjectPool<ConfettiEffect>(CreateConfettiEffect, OnTakeConfettiEffectFromPoll, OnReturnConfettiEffectToPoll);
    }

    private void Start()
    {
        _random = Random.Range(0, images.Length);
        _randomAnim = Random.Range(0, 2);
        _amount = _randomAnim > 0 ? Random.Range(15, 50) : 10;

        _spriteRenderer.sprite = images[_random];
        _animator.SetInteger(GiftAnimationKeys.Animation, _randomAnim);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if(collision.transform.tag == "Player")
        {
            PlayerControl playerControl = collision.GetComponent<PlayerControl>();
            if(playerControl != null)
            {
                // Adicionar score
                playerControl.AddCoins(_amount);
                // Reproduzir som
                _audioSource.PlayOneShot(pickSound);
                // Adicionar efeito especial
                if(_randomAnim > 0)
                {
                    //TODO:: Ir buscar a pool do GameController
                    var snowEffect = _poolConfettiEffect.Get();
                    snowEffect.transform.position = transform.position;
                    snowEffect.transform.rotation = transform.rotation;
                    snowEffect.LifeTime = 1.0f;
                }

                //TODO:: Alterar para pool do GameController??
                _spriteRenderer.enabled = false;
                _collider.enabled = false;
                Destroy(gameObject, 0.5f);
            }
        }
    }

    //TODO:: Remover daqui a pool
    #region ConfettiEffect Pool
    private ConfettiEffect CreateConfettiEffect()
    {
        var confettiEffect = Instantiate(_confettiEffect);
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

public static class GiftAnimationKeys
{
    public const string Animation = "Animation";
}

